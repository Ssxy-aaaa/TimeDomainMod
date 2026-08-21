using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.UI;
using yourmod.Content.Items.Weapons.Summon.Whip;

namespace yourmod.Common.Globals
{
    public class VanillaItemChangeItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.PulseBow)
            {
            }
        }
        //public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        //{

        //    if (item.type == ItemID.EyeofCthulhuMasterTrophy)
        //    {
        //        //Texture2D texture = ModContent.Request<Texture2D>("yourmod/Common/Textures/Item/Vanilla/Item_4924").Value;

        //        //Rectangle rectangle2;
        //        //if (Main.itemAnimations[item.type] != null)
        //        //{
        //        //    rectangle2 = Main.itemAnimations[item.type].GetFrame(texture, -1);
        //        //}
        //        //else
        //        //{
        //        //    rectangle2 = texture.Frame(1, 1, 0, 0, 0, 0);
        //        //}
        //        //float pulseScale = 1f;
        //        //int height = rectangle2.Height;
        //        //int width = rectangle2.Width;
        //        //float drawScale = 1f;
        //        //float availableWidth = 52 * 0.75f;
        //        //if ((float)width > availableWidth || (float)height > availableWidth)
        //        //{
        //        //    if (width > height)
        //        //    {
        //        //        drawScale = availableWidth / (float)width;
        //        //    }
        //        //    else
        //        //    {
        //        //        drawScale = availableWidth / (float)height;
        //        //    }
        //        //}
        //        //UIElement uIElement = new UIElement();
        //        //CalculatedStyle dimensions = uIElement.GetInnerDimensions();
        //        //Vector2 vector = new Vector2(52, 52) * 0.75f;
        //        //Vector2 position2 = dimensions.Position() + vector / 2f;
        //        //Vector2 screenPositionForItemCenter = uIElement.GetDimensions().Center();
        //        //spriteBatch.Draw(texture, screenPositionForItemCenter, null, drawColor, 0f, origin, drawScale * pulseScale, 0, 0f);

        //        Asset<Texture2D> NewTex = ModContent.Request<Texture2D>("yourmod/Common/Textures/Item/Vanilla/Item_4924");
        //        TextureAssets.Item[item.type] = NewTex;




        //    }
        //    return true;
        //}
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ItemID.EyeOfCthulhuBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<OpticNeuron>(), 2)); // copy of non-expert drops from ModNPC.ModifyNPCLoot
                //itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCID.EyeofCthulhu)); // drop money
            }
        }
    }
}
