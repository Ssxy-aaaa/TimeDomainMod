using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TimeDomain.Content.Projectiles
{
    public class ElementalAura : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.alpha = 255;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = ProjAIStyleID.Beam;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            AIType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            Projectile.alpha = 0;
            Projectile.scale = 1f;
            Projectile.rotation = Projectile.velocity.ToRotation();

            Projectile.frameCounter++;
            if (Projectile.frameCounter % 5 == 0)
            {
                Projectile.frame++;
                Projectile.frame %= Main.projFrames[Type];
            }

            Lighting.AddLight(Projectile.Center, GetAuraColor().ToVector3() * 0.9f);

            if (Main.netMode != NetmodeID.Server && Main.rand.NextBool(2))
            {
                Vector2 spawnPos = Projectile.Center + Main.rand.NextVector2Circular(8f, 8f);
                Dust d = Dust.NewDustDirect(spawnPos, 1, 1, DustID.RainbowMk2);
                d.velocity = -Projectile.velocity * 0.1f + Main.rand.NextVector2Circular(1.5f, 1.5f);
                d.noGravity = true;
                d.scale = 1.2f;
                d.color = GetAuraColor();
                d.fadeIn = 0.6f;
            }
        }

        // 随时间流动的彩虹色
        private Color GetAuraColor()
        {
            float hue = (Main.GameUpdateCount * 4f + Projectile.whoAmI * 30f) % 360f;
            return Main.hslToRgb(hue / 360f, 1f, 0.55f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            int frameHeight = tex.Height / Main.projFrames[Type];
            Rectangle sourceRect = new Rectangle(0, frameHeight * Projectile.frame, tex.Width, frameHeight);
            Vector2 origin = sourceRect.Size() / 2f;

            Color auraColor = GetAuraColor();
            float pulse = 0.85f + (float)Math.Sin(Main.GameUpdateCount * 0.25f + Projectile.whoAmI) * 0.15f;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                if (Projectile.oldPos[i] == Vector2.Zero) continue;

                float progress = i / (float)Projectile.oldPos.Length;
                float alpha = (1f - progress) * 0.55f;
                float trailScale = Projectile.scale * (1f - progress * 0.5f);

                float trailHue = ((Main.GameUpdateCount * 4f + i * 12f + Projectile.whoAmI * 30f) % 360f) / 360f;
                Color trailColor = Main.hslToRgb(trailHue, 1f, 0.55f) * alpha;

                Vector2 drawPos = Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition;

                Main.spriteBatch.Draw(tex, drawPos, sourceRect, trailColor,
                    Projectile.rotation, origin, trailScale, SpriteEffects.None, 0f);
            }

            Texture2D glow = TextureAssets.Extra[ExtrasID.SharpTears].Value;
            Vector2 glowOrigin = glow.Size() / 2f;
            float glowScale = Projectile.scale * 0.6f * pulse;

            Main.EntitySpriteDraw(glow,
                Projectile.Center - Main.screenPosition,
                null,
                auraColor * 0.75f,
                Projectile.rotation + MathHelper.PiOver2,
                glowOrigin,
                glowScale,
                SpriteEffects.None, 0);

            Main.EntitySpriteDraw(tex,
                Projectile.Center - Main.screenPosition,
                sourceRect,
                lightColor,
                Projectile.rotation,
                origin,
                Projectile.scale,
                SpriteEffects.None, 0);

            Main.EntitySpriteDraw(tex,
                Projectile.Center - Main.screenPosition,
                sourceRect,
                auraColor * 0.6f,
                Projectile.rotation,
                origin,
                Projectile.scale * 1.05f,
                SpriteEffects.None, 0);

            return false;
        }
    }
}