using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace yourmod.Content.Items.Weapons.Magic
{
    public class IceCrystalFish : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 5;                     // 普通状态消耗3魔力
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;                 // 普通攻速20帧
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.staff[Type] = true;
            Item.shoot = ModContent.ProjectileType<IceCrystalProjectile>();
            Item.shootSpeed = 10f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 4);
        }
        public static Player Player = Main.player[Main.myPlayer];
        public static IceFishPlayer modPlayer = Player.GetModPlayer<IceFishPlayer>();
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            // 喷雾计时器递减
            if (modPlayer.iceFogSprayTimer > 0)
            {
                modPlayer.iceFogSprayTimer--;
                // 注意：不再自动生成射弹，只负责计时
            }
        }
        // 每次使用前调用，动态调整攻速和魔力消耗
        public override bool CanUseItem(Player player)
        {
            if (modPlayer.iceFogSprayTimer > 0)
            {
                // 喷雾状态：攻速10帧，不消耗魔力
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.mana = 3;
            }
            else
            {
                // 普通状态：攻速20帧，消耗3魔力
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.mana = 5;
            }

            return true;
        }
        public int attackCount = 0;// 攻击计数
        public int storedDamage = 47;// 缓存伤害（备用）
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // 如果处于喷雾状态，发射冰雾
            if (modPlayer.iceFogSprayTimer > 0)
            {
                int fogType = ModContent.ProjectileType<IceFogProjectile>();
                Projectile.NewProjectile(source, position, velocity, fogType, damage/2, knockback, player.whoAmI);
                return false;
            }

            // 普通状态：累计攻击次数
            attackCount++;

            // 当攻击次数达到5次时，触发喷雾状态（本次攻击发射冰雾）
            if (attackCount >= 5)
            {
                // 进入喷雾状态，持续300帧（5秒）
                modPlayer.iceFogSprayTimer = 300;
                attackCount = 0;      // 重置计数
                storedDamage = damage; // 保存伤害（供后续使用）

                // 本次攻击发射冰雾（不消耗魔力，已在CanUseItem中设置 mana=0）
                int fogType = ModContent.ProjectileType<IceFogProjectile>();
                Projectile.NewProjectile(source, position, velocity, fogType, damage/2, knockback, player.whoAmI);
                return false;
            }

            // 未触发喷雾，正常发射3个冰晶
            int count = 3;
            float spread = 0.3f;
            for (int i = 0; i < count; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(spread);
                Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public class IceFishPlayer : ModPlayer
        {

            public int iceFogSprayTimer = 0;// 喷雾剩余时间（帧），0表示未激活
            public override void PostUpdate()
            {
                iceFogSprayTimer--;
            }
        }
    }
}