using Microsoft.Xna.Framework;
using TimeDomain.Content.Projectiles.Ranged;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Common.Utils;

namespace TimeDomain.Content.Projectiles.BaseProjectile
{
    public abstract class GemArrowActionProj : ModProjectile
    {
        public const int Amethyst = 1;
        public const int Topaz = 2;
        public const int Sapphire = 3;
        public const int Emerald = 4;
        public const int Ruby = 5;
        public const int Diamond = 6;
        public const int Amber = 7;

        protected int[] slot = new int[3];

        public override string Texture => "Terraria/Images/Projectile_0";

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            if (slot.Contains(Diamond))
            {
                Projectile.penetrate = 2;
            }
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = -1;
            Projectile.arrow = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.velocity.Y += 0.2f;

            if (Main.netMode != NetmodeID.Server && Main.rand.NextBool(2))
            {
                SpawnDust();
            }

            if (slot.Contains(Amethyst) && slot[0] != Amethyst)
            {
                ProjectileBehaviorUtils.HomingToNearestNPC(Projectile, 200);
            }

            if (slot.Contains(Amber) && slot[0] != Amber)
            {
                if (Projectile.timeLeft == 260)
                {
                    Vector2 baseDir = Projectile.velocity.SafeNormalize(Vector2.Zero);
                    float speed = Projectile.velocity.Length();

                    Vector2 dir1 = baseDir.RotatedBy(MathHelper.ToRadians(-30));
                    Vector2 dir2 = baseDir.RotatedBy(MathHelper.ToRadians(30));

                    int proj1 = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, dir1 * speed, Projectile.type, Projectile.damage / 2, 2f, Projectile.owner);
                    int proj2 = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, dir2 * speed, Projectile.type, Projectile.damage / 2, 2f, Projectile.owner);

                    Main.projectile[proj1].timeLeft = 100;
                    Main.projectile[proj2].timeLeft = 100;
                    Projectile.Kill();
                }
            }

            if (slot.Contains(Topaz) && slot[0] != Topaz)
            {
                if (Projectile.velocity.Length() < 30)
                {
                    Projectile.velocity.Y += 0.2f;
                }
            }

            if (slot.Contains(Sapphire) && slot[0] != Sapphire)
            {
                if (Projectile.velocity.Length() < 30)
                {
                    Projectile.velocity *= 1.02f;
                }
            }
        }

        public void SpawnDust()
        {
            if (slot.Contains(Amethyst) && slot[0] != Amethyst)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.GemAmethyst);
                d.noGravity = true;
            }
            if (slot.Contains(Topaz) && slot[0] != Topaz)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.GemTopaz);
                d.noGravity = true;
            }
            if (slot.Contains(Sapphire) && slot[0] != Sapphire)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.GemSapphire);
                d.noGravity = true;
            }
            if (slot.Contains(Emerald) && slot[0] != Emerald)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.GemEmerald);
                d.noGravity = true;
            }
            if (slot.Contains(Ruby) && slot[0] != Ruby)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.GemRuby);
                d.noGravity = true;
            }
            if (slot.Contains(Diamond) && slot[0] != Diamond)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.GemDiamond);
                d.noGravity = true;
            }
            if (slot.Contains(Amber) && slot[0] != Amber)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.GemAmber);
                d.noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (slot.Contains(Emerald) && slot[0] != Emerald)
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Projectile.NewProjectile(
                            Projectile.GetSource_OnHit(target),
                            Projectile.Center,
                            new Vector2(10, 0).RotatedByRandom(2),
                            ModContent.ProjectileType<EmeraldArrowShardProj>(),
                            Projectile.damage / 2,
                            2f,
                            Projectile.owner,
                            ai2: slot[0]
                        );
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);

            if (Main.netMode != NetmodeID.Server)
            {
                Collision.HitTiles(Projectile.position, Projectile.oldVelocity, Projectile.width, Projectile.height);
            }

            if (slot.Contains(Ruby) && slot[0] != Ruby)
            {
                SoundEngine.PlaySound(SoundID.Item102, Projectile.Center);
                ProjectileBehaviorUtils.Explode(Projectile, Projectile.Center, 200, Projectile.damage / 2, 0, Projectile.owner);
                SpawnRingDust(Projectile.Center, 10, 50, DustID.GemRuby, speed: 10);
            }
        }

        public static void SpawnRingDust(Vector2 center, float radius, int count,
            int dustType = DustID.TintableDustLighted, Color? color = null,
            float scale = 1f, float speed = 0f, int alpha = 0)
        {
            Color dustColor = color ?? Color.White;
            for (int i = 0; i < count; i++)
            {
                float angle = MathHelper.TwoPi * i / count;
                Vector2 pos = center + new Vector2(radius, 0).RotatedBy(angle);
                Vector2 vel = (pos - center).SafeNormalize(Vector2.Zero) * speed;

                Dust dust = Dust.NewDustPerfect(pos, dustType, vel, alpha, dustColor, scale);
                dust.noGravity = true;
                dust.noLight = false;
                dust.fadeIn = 0f;
            }
        }
    }
}