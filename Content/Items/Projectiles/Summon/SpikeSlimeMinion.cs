using TimeDomain.Content.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TimeDomain.Content.Items.Projectiles.Summon
{

    internal class SpikeSlimeMinion : ModProjectile
    {
        private enum AttackMode
        {
            SlimeJump,
            SpikeShot
        }

        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 28;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.damage = 25;

            Projectile.localAI[1] = 0f;
            Projectile.localAI[2] = 0f;
        }

        public override void AI()
        {
            if (Projectile.localAI[2] > 0)
                Projectile.localAI[2]--;

            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<SpikeSlimeBuff>());
                return;
            }

            if (player.HasBuff(ModContent.BuffType<SpikeSlimeBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            float teleportDistance = 2000f;

            if (Vector2.Distance(player.Center, Projectile.Center) > teleportDistance)
            {
                Vector2 teleportPos = player.Center + new Vector2(-40 * player.direction, -20f);

                Projectile.Center = teleportPos;
                Projectile.velocity = Vector2.Zero;
                Projectile.tileCollide = false;
                Projectile.netUpdate = true;

                return;
            }

            bool playerOnGround = Collision.SolidCollision(player.position + new Vector2(0, player.height), player.width, 4);
            bool playerInAir = !playerOnGround;

            float screenMargin = 200f;

            bool nearScreenEdge =
                Projectile.Center.X < Main.screenPosition.X + screenMargin ||
                Projectile.Center.X > Main.screenPosition.X + Main.screenWidth - screenMargin ||
                Projectile.Center.Y < Main.screenPosition.Y + screenMargin ||
                Projectile.Center.Y > Main.screenPosition.Y + Main.screenHeight - screenMargin;

            bool shouldReturnToPlayer = playerInAir || nearScreenEdge;

            if (shouldReturnToPlayer)
            {
                Projectile.tileCollide = false;

                Vector2 flyTarget = player.Center + new Vector2(-40 * player.direction, -20f);
                Vector2 toFly = flyTarget - Projectile.Center;
                float flyDist = toFly.Length();

                Vector2 desiredVelocity;
                if (flyDist > 80f)
                    desiredVelocity = toFly.SafeNormalize(Vector2.Zero) * 15f;

                else if (flyDist > 30f)
                    desiredVelocity = toFly.SafeNormalize(Vector2.Zero) * 10f;

                else if (flyDist > 10f)
                    desiredVelocity = toFly.SafeNormalize(Vector2.Zero) * 5f;

                else
                    desiredVelocity = Vector2.Zero;

                Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 0.05f);

                Projectile.spriteDirection = player.direction;
                float tilt = MathHelper.Clamp(Projectile.velocity.X * 0.05f, -0.7f, 0.7f);
                Projectile.rotation = tilt;

                UpdateAnimation(true, false);

                return;
            }
            else
            {
                Projectile.tileCollide = true;
            }

            NPC target = FindTarget(player);
            if (target == null)
            {
                UpdateAnimation(true, false);
                IdleBehavior(player);
                return;
            }

            AttackMode currentMode = (AttackMode)(int)Projectile.ai[0];
            int attackTimer = (int)Projectile.ai[1];
            int spikeRounds = (int)Projectile.localAI[0];

            Projectile.ai[1]++;

            switch (currentMode)
            {
                case AttackMode.SlimeJump:
                    UpdateAnimation(true, false);
                    SlimeJumpAttack(target);
                    if (attackTimer > 900)
                    {
                        Projectile.ai[0] = (int)AttackMode.SpikeShot;
                        Projectile.ai[1] = 0;
                        Projectile.localAI[0] = 0;
                    }
                    break;

                case AttackMode.SpikeShot:
                    int phase = (int)(Projectile.ai[1] % 120);
                    if (phase < 60)
                    {
                        UpdateAnimation(false, true);
                    }
                    else if (phase == 60)
                    {
                        UpdateAnimation(false, true);
                        FireSpikes(target);
                        Projectile.localAI[0]++;
                    }
                    else
                    {
                        UpdateAnimation(false, true);
                    }

                    SpikeShotAttack(target);

                    if (spikeRounds >= 3)
                    {
                        Projectile.ai[0] = (int)AttackMode.SlimeJump;
                        Projectile.ai[1] = 0;
                        Projectile.localAI[0] = 0;
                    }
                    break;
            }

            if (Projectile.localAI[1] > 0)
                Projectile.localAI[1]--;

            if (Projectile.localAI[1] <= 0)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly && !npc.dontTakeDamage
                        && npc.Distance(Projectile.Center) < 40f)
                    {
                        player.StrikeNPCDirect(npc, npc.CalculateHitInfo(Projectile.damage, Projectile.velocity.X > 0 ? 1 : -1, false, 0f, DamageClass.Summon));
                        Projectile.localAI[1] = 10;
                        break;
                    }
                }
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float radius = 50f;
            return Vector2.Distance(Projectile.Center, targetHitbox.Center.ToVector2()) < radius;
        }

        private void UpdateAnimation(bool isJump, bool isSpike)
        {
            Projectile.frameCounter++;

            if (isJump)
            {
                if (Projectile.frameCounter > 5)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                    if (Projectile.frame > 1) Projectile.frame = 0;
                }
            }
            else if (isSpike)
            {
                if (Projectile.frameCounter > 5)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                    if (Projectile.frame < 2 || Projectile.frame > 3) Projectile.frame = 2;
                }
            }
        }

        private void SlimeJumpAttack(NPC target)
        {

            // 史莱姆式接触攻击：向目标跳跃，造成接触伤害
            Projectile.velocity.Y += 0.4f;

            // 落地检测
            if (Projectile.velocity.Y > 0 && IsOnGround())
            {
                if (Projectile.localAI[2] <= 0)
                {
                    // 计算向目标的跳跃
                    Vector2 toTarget = target.Center - Projectile.Center;
                    float distX = Math.Abs(toTarget.X);
                    float distY = target.Center.Y - Projectile.Center.Y;

                    // 水平速度：向目标
                    float jumpSpeedX = Math.Min(distX * 0.05f, 4.5f);
                    if (toTarget.X > 0)
                        Projectile.velocity.X = jumpSpeedX;
                    else
                        Projectile.velocity.X = -jumpSpeedX;

                    // 垂直速度：根据高度调整
                    if (distY < -50f)
                        Projectile.velocity.Y = -7f;  // 高跳
                    else if (distY > 50f)
                        Projectile.velocity.Y = -3f;   // 低跳或下落
                    else
                        Projectile.velocity.Y = -5f;  // 普通跳

                    // 如果目标很近，直接冲刺
                    if (distX < 40f && Math.Abs(distY) < 30f)
                    {
                        Projectile.velocity.X *= 1.01f;
                    }

                    Projectile.localAI[2] = 45;
                }
                else
                {
                    Projectile.velocity.X *= 0.8f; //减速
                }
            }

            if (target.Distance(Projectile.Center) < 80f)
            {
                Vector2 toTarget = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
                Projectile.velocity += toTarget * 0.2f;
            }

            float xDiff = target.Center.X - Projectile.Center.X;
            if (Math.Abs(xDiff) > 15f)
            {
                int desiredDir = xDiff > 0 ? -1 : 1;
                if (Projectile.spriteDirection != desiredDir)
                    Projectile.spriteDirection = desiredDir;
            }
            Projectile.rotation = 0;
        }

        private void SpikeShotAttack(NPC target)
        {
            int phase = (int)(Projectile.ai[1] % 120);

            if (phase < 60)
            {
                // 移动到目标脚下
                Vector2 hoverPos = target.Bottom + new Vector2(0, 0);
                Vector2 toHover = hoverPos - Projectile.Center;
                float dist = toHover.Length();

                Projectile.velocity.Y += 0.4f;

                float targetXVel = 0f;
                if (Math.Abs(toHover.X) > 25f)
                {
                    targetXVel = toHover.X > 0 ? 4f : -4f;
                }
                Projectile.velocity.X = MathHelper.Lerp(Projectile.velocity.X, targetXVel, 0.2f);

                if (IsOnGround() && Projectile.velocity.Y >= 0f && Collision.SolidCollision(Projectile.position + new Vector2(Projectile.velocity.X, 0), Projectile.width, Projectile.height))
                {
                    Projectile.velocity.Y = -8f;
                }
            }
            else if (phase > 70)
            {
                Projectile.velocity.X *= 0.9f;
            }

            Projectile.spriteDirection = target.Center.X > Projectile.Center.X ? -1 : 1;
            Projectile.rotation = 0;
        }

        private void FireSpikes(NPC target)
        {
            SoundEngine
        .PlaySound(SoundID.Item17, Projectile.Center);

            float baseAngle = -MathHelper.PiOver2;
            float spread = MathHelper.Pi / 4;

            for (int i = -1; i <= 1; i++)
            {
                float angle = baseAngle + spread * i / 2f;
                Vector2 velocity = angle.ToRotationVector2() * 10f;

                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.Bottom,
                    velocity,
                    ModContent.ProjectileType<SlimeSpike>(),
                    Projectile.damage,
                    1f,
                    Projectile.owner
                );
            }
        }

        private NPC FindTarget(Player player)
        {
            NPC target = null;
            float targetDist = 700f;

            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                if (npc.CanBeChasedBy(Projectile) && npc.Distance(player.Center) < targetDist)
                {
                    return npc;
                }
            }

            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy(Projectile))
                {
                    float dist = npc.Distance(player.Center);
                    if (dist < targetDist)
                    {
                        targetDist= dist;
                        target= npc;
                    }
                }
            }

            return target;
        }

        private void IdleBehavior(Player player)
        {
            Vector2 idlePos = player.Center + new Vector2(-40 * player.direction, 0);
            Vector2 toIdle = idlePos - Projectile.Center;
            float dist = toIdle.Length();

            Projectile.velocity.Y += 0.4f;  // 重力

            if (IsOnGround() && Projectile.localAI[2] <= 0)
            {
                if (dist > 60f)
                {
                    float direction = toIdle.X > 0 ? 1f : -1f;
                    Projectile.velocity.X = direction * 4f;
                    Projectile.velocity.Y = -6f;
                    Projectile.localAI[2] = 20;
                }

                else if (dist > 12f)
                {
                    Projectile.velocity.X *= 0.4f;
                }

                else
                {
                    Projectile.velocity.X = 0f;
                }
            }
            if (IsOnGround() && Projectile.localAI[2] > 0)
            {
                Projectile.velocity.X *= 0.85f;  // 冷却中停在地上
            }

            Projectile.spriteDirection = player.direction;
            Projectile.rotation = 0;
        }

        // 检测是否在地面上
        private bool IsOnGround()
        {
            return Collision.SolidCollision(Projectile.position + new Vector2(0, Projectile.height), Projectile.width, 4);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
                Projectile.velocity.X = -oldVelocity.X * 0.5f;
            if (Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 0)
                Projectile.velocity.Y = 0;

            return false;
        }
    }
}