using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace yourmod.Content.Items.Weapons.Melee
{
    public class TestSpear : ModItem
    {
        public override string Texture => "Terraria/Images/Item_280";
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Spear);
            Item.width = Item.height = 32;
            Item.useAnimation = Item.useTime = 10;
            Item.damage = 10;
            Item.knockBack = 10;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<TestSpearProj>();
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] > 0)
                return false;
            return base.CanUseItem(player);
        }
        //添加物品配方
        public override void AddRecipes()
        {
            //添加一个配方，括号里可以填数字，代表一次合成几个，默认为1
            CreateRecipe()
                //添加材料
                .AddIngredient(ItemID.IronBar, 5)
                .AddIngredient(ItemID.Wood, 10)
                //添加合成地
                .AddTile(TileID.Anvils)
                //注册
                .Register();
        }
    }
    public class TestSpearProj : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_49";
        public override void SetDefaults()
        {
            //Projectile.CloneDefaults(ProjectileID.Spear);
            Projectile.width = Projectile.height = 54;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.wet = false;
            Projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
        }
        public virtual float MaxPunctureDistance => 45;
        public virtual float MinPunctureDistance => 0;
        public virtual float PunctureTimer => 15;
        public int Dir = 1;
        public float Timer
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        public Vector2 v = Vector2.Zero;
        public Player Owner => Main.player[Projectile.owner]?? Main.player[Main.myPlayer];
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.Center = Owner.Center;
            v = Main.MouseWorld - Owner.Center;
        }
        public override void AI()
        {
            float c = MaxPunctureDistance - MinPunctureDistance;
            Timer++;
            Projectile.Center = Owner.Center;
            Projectile.rotation = v.ToRotation() + MathHelper.PiOver4 + MathHelper.PiOver2;

            Dir = Timer < PunctureTimer ? 1 : -1;

            Projectile.Center = Owner.Center + Vector2.Normalize(v) * ((Dir == 1 ? MinPunctureDistance : MaxPunctureDistance) + (((Dir == 1 ? Timer : Timer - PunctureTimer) / PunctureTimer) * c * Dir));
            if (Timer >= PunctureTimer * 2)
            {
                Projectile.Kill();
            }
        }
    }
}
