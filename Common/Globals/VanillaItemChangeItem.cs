using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
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
        public override bool InstancePerEntity => true;
        public int uTime = 0;
        public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            uTime++;
            if (item.type == ItemID.Zenith/* || item.rare >= ItemRarityID.Red*/)
            {
                Texture2D itemTexture = TextureAssets.Item[item.type].Value;

                spriteBatch.Draw(itemTexture, position, new Rectangle?(frame), drawColor, 0f, origin, scale, 0, 0f);
                if (item.color != Color.Transparent)
                {
                    spriteBatch.Draw(itemTexture, position, new Rectangle?(frame), item.GetColor(Color.White), 0f, origin, scale, 0, 0f);
                }
                for (int i = 0; i < 6; i++)
                {
                    float ro = -MathHelper.Pi + (i / 6f) * MathHelper.TwoPi + (uTime / 100f);
                    Color newColor = drawColor;
                    newColor.A /= 3;
                    newColor *= (float)((Math.Sin(uTime / 100f)));
                    spriteBatch.Draw(itemTexture, position + ro.ToRotationVector2() * 5, new Rectangle?(frame), newColor, 0f, origin, scale, 0, 0f);
                }
                return false;
            }
            return true;
        }
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ItemID.EyeOfCthulhuBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<OpticNeuron>(), 2)); // copy of non-expert drops from ModNPC.ModifyNPCLoot
                //itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCID.EyeofCthulhu)); // drop money
            }
        }
    }
    //public class ItemConfig : ModConfig
    //{
    //    public override ConfigScope Mode => throw new NotImplementedException();
    //}
}
