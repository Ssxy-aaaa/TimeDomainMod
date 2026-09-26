using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Common.Utils;

namespace TimeDomain.Content.Projectiles.Ranged
{
    public class EmeraldArrowShardProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = 1000;
            Projectile.frame = Main.rand.Next(0, 2);
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.3f;
            Projectile.rotation += 0.1f;

            if (System.Math.Abs(Projectile.velocity.Y) < 0.1f && Projectile.ai[1] == 0)
            {
                Projectile.ai[1] = 1;
            }

            if (Projectile.ai[1] == 1)
            {
                Projectile.ai[0]++;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Tex = ModContent.Request<Texture2D>("TimeDomain/Assets/Textures/Misc/Effect_7").Value;
            Color c = Color.White;
            c.A = 0;
            Color c2 = new Color(33, 184, 115);

            switch ((int)Projectile.ai[2])
            {
                case 1:
                    c2 = new Color(219, 96, 255);
                    break;
                case 2:
                    c2 = new Color(255, 221, 62);
                    break;
                case 3:
                    c2 = new Color(23, 147, 234);
                    break;
                case 4:
                    c2 = new Color(33, 184, 115);
                    break;
                case 5:
                    c2 = new Color(195, 41, 44);
                    break;
                case 6:
                    c2 = new Color(246, 250, 252);
                    break;
                case 7:
                    c2 = new Color(252, 193, 45);
                    break;
            }

            c2.A = 0;
            Texture2D Tex2 = ModContent.Request<Texture2D>("TimeDomain/Assets/Textures/Misc/Glow").Value;

            float alphaFactor = Projectile.ai[0] > 10
                ? (1f - Projectile.ai[0] / 20f)
                : (Projectile.ai[0] / 10f);

            Main.spriteBatch.Draw(Tex2, Projectile.Center - Main.screenPosition, null, c2 * alphaFactor, Projectile.rotation * 2, Tex2.Size() / 2, 1, 0, 0);
            Main.spriteBatch.Draw(Tex, Projectile.Center - Main.screenPosition, null, c * alphaFactor, Projectile.rotation * 2, Tex.Size() / 2, 0.8f, 0, 0);

            SimpleSprite.SpriteIt(Projectile, c2 with { A = 2 });
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            int shardType = (int)Projectile.ai[2];

            int dustType = shardType switch
            {
                1 => DustID.GemAmethyst,
                2 => DustID.GemTopaz,
                3 => DustID.GemSapphire,
                4 => DustID.GemEmerald,
                5 => DustID.GemRuby,
                6 => DustID.GemDiamond,
                7 => DustID.GemAmber,
                _ => DustID.GemEmerald
            };

            Dust d = Dust.NewDustDirect(Projectile.Center, 2, 2, dustType);
            d.noGravity = true;
        }
    }
}