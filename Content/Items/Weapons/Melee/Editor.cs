using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace yourmod.Content.Items.Weapons.Melee
{
    public class Editor : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 80;
            Item.damage = 276*10;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTime = Item.useAnimation = 10;
            Item.knockBack = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = null;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<EditorProj>();
            Item.shootSpeed = 10f;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.GameUpdateCount % 3 == 0)
            {
                int d1 = Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.Shadowflame);
                int d2 = Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.ShadowbeamStaff);
                Main.dust[d1].noGravity = true;
                Main.dust[d2].noGravity = true;
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback);
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            //
            if (player.ownedProjectileCounts[ModContent.ProjectileType<EditorProj>()] > 0)
                return false;
            return base.CanUseItem(player);
        }
    }
}
