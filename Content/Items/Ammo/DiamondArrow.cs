using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Projectiles.Ranged;

namespace TimeDomain.Content.Items.Ammo
{
    internal class DiamondArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 32;
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2.5f;
            Item.shoot = ModContent.ProjectileType<DiamondArrowProj>();
            Item.shootSpeed = 7.5f;

            Item.ammo = AmmoID.Arrow;
            Item.value = Item.sellPrice(silver: 4);
            Item.rare = ItemRarityID.Blue;
            Item.consumable = true;
            Item.maxStack = 9999;
        }
    }
}
