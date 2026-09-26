using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Buffs;
using Microsoft.Xna.Framework;
using TimeDomain.Content.Projectiles.Summon;


namespace TimeDomain.Content.Items.Weapons.Summon
{
    public class SpikeSlimeSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Summon;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item44;
            Item.mana = 10;

            Item.shoot = ModContent.ProjectileType<SpikeSlimeMinion>();
            Item.buffType = ModContent.BuffType<SpikeSlimeBuff>();

        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = player.Center;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 3600);
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Stinger, 15);
            recipe.AddIngredient(ItemID.Vine, 3);
            recipe.AddIngredient(ItemID.JungleSpores, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
