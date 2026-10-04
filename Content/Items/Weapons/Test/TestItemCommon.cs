using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TimeDomain.Content.Items.Weapons.Test
{
    public abstract class TestItemCommon : ModItem
    {
        public override string Texture => "Terraria/Images/Item_1";
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.IronPickaxe);
            Item.shoot = ProjectileID.WoodenArrowFriendly;
        }
        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .Register();
        }
    }
}
