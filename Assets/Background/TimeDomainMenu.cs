using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace TimeDomain.Common
{
    public class TimeDomainMenu : ModMenu
    {
        public override string DisplayName => "Time Domain";

        private Asset<Texture2D> _logoTexture;

        public override void Load()
        {
            _logoTexture = ModContent.Request<Texture2D>("TimeDomain/Assets/Background/TimeDomainLogo");
        }

        public override void Unload()
        {
            _logoTexture = null;
        }

        public override void OnSelected()
        {
            Main.time = 27000;
            Main.dayTime = true;
        }

        // public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/GunmuMusic");

        public override bool PreDrawLogo(
            SpriteBatch spriteBatch,
            ref Vector2 logoDrawCenter,
            ref float logoRotation,
            ref float logoScale,
            ref Color drawColor)
        {
            drawColor = Color.White;

            Texture2D logoTexture = _logoTexture.Value;

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);

            spriteBatch.Draw(logoTexture, logoDrawCenter, null, drawColor, logoRotation, logoTexture.Size() / 2f, logoScale, SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);

            return false;
        }
    }
}