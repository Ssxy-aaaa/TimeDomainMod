using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;

namespace TimeDomain.Common.Utils
{
    public static class SimpleSprite
    {
        public static void SpriteIt(Projectile proj, Color color)
        {
            Texture2D tex = TextureAssets.Projectile[proj.type].Value;
            int frameHeight = tex.Height / Main.projFrames[proj.type];
            Rectangle source = new Rectangle(0, proj.frame * frameHeight, tex.Width, frameHeight);
            Vector2 origin = source.Size() / 2f;

            Main.EntitySpriteDraw(
                tex,
                proj.Center - Main.screenPosition,
                source,
                color,
                proj.rotation,
                origin,
                proj.scale,
                SpriteEffects.None,
                0
            );
        }
    }
}