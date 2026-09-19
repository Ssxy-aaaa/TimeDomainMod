using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;
using TimeDomain.Content.Items.Weapons.Melee;

namespace TimeDomain.Common.Systems.Renders
{
    public class TestRender : ModSystem
    {
        public static RenderTarget2D renderTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
        public override void Load()
        {
            On_FilterManager.EndCapture += FilterManager_EndCapture;//原版绘制场景的最后部分——滤镜。在这里运用render保证不会与原版冲突
            Main.OnResolutionChanged += Main_OnResolutionChanged;
            //CreateRender();
            base.Load();
        }
        public override void Unload()
        {
            On_FilterManager.EndCapture -= FilterManager_EndCapture;
            Main.OnResolutionChanged -= Main_OnResolutionChanged;
            base.Unload();
        }
        public Effect TestTwist;
        public Effect NegativeFilm;
        public override void SetStaticDefaults()
        {
            TestTwist = ModContent.Request<Effect>("TimeDomain/Content/Effects/TestTwist", AssetRequestMode.ImmediateLoad).Value;
            NegativeFilm = ModContent.Request<Effect>("TimeDomain/Content/Effects/NegativeFilm", AssetRequestMode.ImmediateLoad).Value;
        }
        public void CreateRender()
        {
            renderTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
        }
        public void Main_OnResolutionChanged(Vector2 obj)
        {
            CreateRender();
        }
        public void FilterManager_EndCapture(On_FilterManager.orig_EndCapture orig, FilterManager self, RenderTarget2D finalTexture, RenderTarget2D screenTarget1, RenderTarget2D screenTarget2, Color clearColor)
        {
            SpriteBatch sb = Main.spriteBatch;
            GraphicsDevice gd = Main.instance.GraphicsDevice;
            if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<TestRenderItem>())
            {
                gd.SetRenderTarget(Main.screenTargetSwap);
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
                sb.End();


                gd.SetRenderTarget(Main.screenTarget);
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                sb.End();
                sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                //Editor.EditorEffect.CurrentTechnique.Passes[2].Apply();
                //Editor.EditorEffect.Parameters["uTime"].SetValue(/*Main.GameUpdateCount*/Main.GlobalTimeWrappedHourly);
                TestTwist.CurrentTechnique.Passes[0].Apply();
                TestTwist.Parameters["iTime"].SetValue(Main.GlobalTimeWrappedHourly*1f);
                float uo = (float)Math.Abs(Math.Sin(Main.GlobalTimeWrappedHourly / 36f) / 1f);
                TestTwist.Parameters["O"].SetValue(uo);
                sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                sb.End();


                gd.SetRenderTarget(Main.screenTargetSwap);
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
                sb.End();


                gd.SetRenderTarget(Main.screenTarget);
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                //sb.Draw(TestRender.renderTarget, Vector2.Zero, Color.White);
                sb.End();
                sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                NegativeFilm.CurrentTechnique.Passes[0].Apply();
                sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                sb.End();
            }
            orig(self, finalTexture, screenTarget1, screenTarget2, clearColor);
        }
    }
    public class TestRenderItem : ModItem
    {
        public override string Texture => "Terraria/Images/Item_1";
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.IronPickaxe);
        }
        public override bool CanUseItem(Player player)
        {

            return true;
        }
    }
}
