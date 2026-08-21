using yourmod.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace yourmod.Content.Items.Arrows
{
    public class ShimmerBullet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 0, 8);
            Item.damage = 16;
            //Item.DamageType = DamageClass.Ranged;
            Item.ammo = AmmoID.Bullet;
            Item.shoot = ModContent.ProjectileType<ShimmerBulletProj>();
            Item.consumable = true;
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }
    }
}
