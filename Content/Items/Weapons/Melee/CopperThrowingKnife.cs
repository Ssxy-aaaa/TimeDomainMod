using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Projectiles.Melee;

namespace TimeDomain.Content.Items.Weapons.Melee
{
    public class CopperThrowingKnife : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ShadowFlameKnife);

            Item.shoot = ModContent.ProjectileType<CopperThrowingKnifeProj>();
            Item.damage = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CopperBar, 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}