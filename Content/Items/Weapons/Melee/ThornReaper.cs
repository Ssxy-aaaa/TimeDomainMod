using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Projectiles.Melee;

namespace TimeDomain.Content.Items.Weapons.Melee
{
    public class ThornReaper : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.crit = 16;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 88;
            Item.height = 78;
            Item.useTime = 50;
            Item.useAnimation = 35;
            Item.knockBack = 2;

            Item.shoot = ModContent.ProjectileType<ThornLeafProjectile>();
            Item.shootSpeed = 9f;

            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int shootType = ModContent.ProjectileType<ThornLeafProjectile>();
            float spread = Main.rand.NextFloat(0.1f, 0.15f);

            for (int i = -1; i <= 1; i++)
            {
                Vector2 v = velocity.RotatedBy(i * spread);
                Projectile.NewProjectile(source, position, v, shootType, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                for (int i = 0; i < 15; i++)
                {
                    Dust d = Dust.NewDustDirect(player.Center, 16, 16, DustID.Grass);
                    d.velocity = Main.rand.NextVector2Circular(4f, 4f);
                    d.scale = Main.rand.NextFloat(0.6f, 1.2f);
                    d.noGravity = true;
                }
            }
            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.JungleSpores, 18)
                .AddIngredient(ItemID.Vine, 3)
                .AddIngredient(ItemID.Stinger, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}