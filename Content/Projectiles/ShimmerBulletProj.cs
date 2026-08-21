using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace yourmod.Content.Projectiles
{
    public class ShimmerBulletProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(14);
            Projectile.width = 14;
            Projectile.height = 32;
            Projectile.scale = 1f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;    
            Projectile.alpha = 0;
            Projectile.friendly = true;
            //Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = ProjAIStyleID.Beam;
            Projectile.wet = false;
            Projectile.light = 0f;
            Projectile.timeLeft = 600;
            AIType = ProjectileID.Bullet;
            //ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            //ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
        }
        public void DustLight()
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            //SoundEngine.PlaySound(SoundID.,Projectile.position);
            float num684 = Main.rand.NextFloat() * 6.2831855f;
            for (float num685 = 0f; num685 < 1f; num685 += 1f)
            {
                float num686 = num684 + 6.2831855f * num685;
                Vector2 vector59 = Vector2.UnitX.RotatedBy((double)num686, default(Vector2));
                Vector2 center = Projectile.Center;
                float num687 = 0.4f;
                ParticleOrchestrator.RequestParticleSpawn(true, ParticleOrchestraType.ShimmerArrow, new ParticleOrchestraSettings
                {
                    PositionInWorld = center,
                    MovementVector = vector59 * num687
                }, new int?(Projectile.owner));
            }
        }
        public override void OnKill(int timeLeft)
        {
            DustLight();
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            float c = Projectile.velocity.Length();
            Projectile.velocity = new Vector2(0, -c/* / 2*/);
            DustLight();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            float c = Projectile.velocity.Length();
            Projectile.velocity = new Vector2(0, -c/* / 2*/);
            DustLight();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = new Color(255, 255, 255) * 1f;
            return base.PreDraw(ref lightColor);
        }
        public override void AI()
        {
            Projectile.alpha = 0;
            Projectile.scale = 1f;
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Main.rand.Next(8) == 0)
            {
                Dust dust23 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(4f, 4f), DustID.SparkForLightDisc, new Vector2?(Projectile.velocity * 1.25f), 0, Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f, byte.MaxValue), 1f + Main.rand.NextFloat() * 0.4f);
                dust23.noGravity = true;
                dust23.scale += 0.05f;
                Dust dust24 = Dust.CloneDust(dust23);
                dust24.color = Color.White;
                dust24.scale -= 0.3f;
            }
        }
    }
}
