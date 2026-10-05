using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Projectiles.Melee;

namespace TimeDomain.Content.Items.Weapons.Melee
{
    public class IronThrowingKnife : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ShadowFlameKnife);

            Item.shoot = ModContent.ProjectileType<IronThrowingKnifeProj>();
            Item.damage = 4;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}