using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace TimeDomain.Content.Projectiles.Melee
{
    public class ThornLeafProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.arrow = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 240;
            Projectile.penetrate = 3;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;

            Projectile.ai[1] = 0f;

            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = 1000;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 1)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Type])
                    Projectile.frame = 0;
            }

            Projectile.velocity *= 1.01f;
            Projectile.velocity.Y += 0.01f;
            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Main.rand.NextBool(2))
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust d = Dust.NewDustDirect(Projectile.Center + Main.rand.NextVector2Circular(6f, 6f), 4, 4, DustID.GreenMoss);
                    d.velocity = -Projectile.velocity * 0.15f + Main.rand.NextVector2Circular(1.5f, 1.5f);
                    d.scale = Main.rand.NextFloat(0.5f, 1.0f);
                    d.noGravity = true;
                }
            }

            if (Projectile.ai[0]++ > 240)
                Projectile.velocity *= 0.99f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 300);

            if (Main.netMode != NetmodeID.Server)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust d = Dust.NewDustDirect(target.Center, 10, 10, DustID.Poisoned);
                    d.velocity = Main.rand.NextVector2Circular(8f, 8f);
                    d.scale = Main.rand.NextFloat(0.2f, 0.6f);
                    d.noGravity = true;
                }
                SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
            }

            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                if (Main.rand.NextFloat() < 0.75f)
                {
                    int splitCount = 2;
                    for (int i = 0; i < splitCount; i++)
                    {
                        float angleOffset = Main.rand.NextFloat(-0.5f, 0.5f);
                        Vector2 splitVelocity = Projectile.velocity.RotatedBy(angleOffset) * Main.rand.NextFloat(0.9f, 1.1f);
                        int splitDamage = (int)(Projectile.damage * 0.5f);
                        if (splitDamage < 1) splitDamage = 1;
                        int projIndex = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, splitVelocity, Projectile.type, splitDamage, Projectile.knockBack * 0.5f, Projectile.owner, ai0: 0, ai1: 1);

                        Main.projectile[projIndex].penetrate = 1;
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                for (int i = 0; i < 12; i++)
                {
                    Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Grass);
                    d.velocity = Main.rand.NextVector2Circular(4f, 4f);
                    d.scale = Main.rand.NextFloat(0.5f, 1.5f);
                    d.noGravity = true;
                }
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            }
        }
    }
}