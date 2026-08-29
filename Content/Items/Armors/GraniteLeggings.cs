using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Items.Materials;

namespace TimeDomain.Content.Items.Armors
{
    [AutoloadEquip(EquipType.Legs)]
    public class GraniteLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.defense = 6;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed -= 0.06f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Granite, 60)
                .AddIngredient(ModContent.ItemType<GraniteCore>(), 6)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
