using Microsoft.CodeAnalysis.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Projectiles;

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
            Projectile.rotation = Projectile.velocity.ToRotation()/* - MathHelper.PiOver2*/;

            Projectile.frameCounter++;
            Projectile.frame += (Projectile.frameCounter % 10 == 0).ToInt();
            Projectile.frame %= 4;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D slimeTex = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle sourceRect = new Rectangle(0,0,50,Projectile.frame);
            Vector2 origin = new Vector2(sourceRect.Width / 2f, sourceRect.Height / 2f);

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                float progress = i / (float)Projectile.oldPos.Length;
                float alpha = (1f - progress) * 0.6f;
                Vector2 drawPos = Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition;
                Color trailColor = /*Main.hslToRgb(Main.GameUpdateCount+i,170,255,255)*/Color.Aqua * alpha;
                Main.spriteBatch.Draw(TextureAssets.Projectile[Type].Value, drawPos, sourceRect, trailColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
