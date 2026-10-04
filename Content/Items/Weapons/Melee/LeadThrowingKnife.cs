using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Projectiles.Melee;

namespace TimeDomain.Content.Items.Weapons.Melee
{
    public class LeadThrowingKnife : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ShadowFlameKnife);

            Item.shoot = ModContent.ProjectileType<LeadThrowingKnifeProj>();
            Item.damage = 5;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LeadBar, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}