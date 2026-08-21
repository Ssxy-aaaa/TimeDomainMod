using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using yourmod.Content.Items.Materials;

namespace yourmod.Content.Items.Armors
{
    [AutoloadEquip(EquipType.Body)]
    public class GraniteBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.defense = 7;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed -= 0.07f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Granite, 70)
                .AddIngredient(ModContent.ItemType<GraniteCore>(), 7)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
