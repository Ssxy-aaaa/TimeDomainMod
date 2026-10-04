using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Projectiles.Melee;

namespace TimeDomain.Content.Items.Weapons.Melee
{
    public class TinThrowingKnife : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ShadowFlameKnife);

            Item.shoot = ModContent.ProjectileType<TinThrowingKnifeProj>();
            Item.damage = 3;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TinBar, 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}