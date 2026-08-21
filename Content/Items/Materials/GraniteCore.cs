using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace yourmod.Content.Items.Materials
{
    public class GraniteCore : ModItem
    {
        public override void SetDefaults()
        {
            //24,22*3
            Item.width = Item.height = 24;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Green;
            Item.maxStack = Item.CommonMaxStack;
        }
        public override void SetStaticDefaults()
        {
            ItemID.Sets.AnimatesAsSoul[Type] = true;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 3));
            Item.ResearchUnlockCount = 20;
        }
    }
}
