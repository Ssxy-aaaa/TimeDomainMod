using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using yourmod;

namespace yourmod.Content.Items.Weapons.Common
{
    public class VampireStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 20;
            if (Main.masterMode)
            {
                Item.damage = 25;
            }
            if (Main.getGoodWorld && Main.masterMode)
            {
                Item.damage = 30;
            }
            Item.crit = 4;
            Item.knockBack = 4;
            Item.rare = ItemRarityID.Orange;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0,8,0,0);
            Item.staff[Type] = true;
            //Item.expert = true;
            Item.useTurn = true;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.mana = 8;
            Item.shoot = ProjectileID.SoulDrain;
            Item.shootSpeed = 10f;
            //Item.DefaultToStaff();
        }
        //public void s()
        //{
        //    int num701 = 31;
        //    if (this.life > 0)
        //    {
        //        int num702 = 0;
        //        while ((double)num702 < dmg / (double)this.lifeMax * 50.0)
        //        {
        //            Dust.NewDust(this.position, this.width, this.height, num701, 0f, 0f, 0, default(Color), 1f);
        //            num856 = num702;
        //            num702 = num856 + 1;
        //        }
        //        return;
        //    }
        //    for (int num703 = 0; num703 < 20; num703 = num856 + 1)
        //    {
        //        Dust.NewDust(this.position, this.width, this.height, num701, 0f, 0f, 0, default(Color), 1f);
        //        num856 = num703;
        //    }
        //    int num704 = Gore.NewGore(base.Center, new Vector2((float)hitDirection, 0f), 61, this.scale);
        //    Gore gore16 = Main.gore[num704];
        //    Gore gore32 = gore16;
        //    gore32.velocity *= 0.3f;
        //    num704 = Gore.NewGore(base.Center, new Vector2((float)hitDirection, 0f), 62, this.scale);
        //    gore16 = Main.gore[num704];
        //    gore32 = gore16;
        //    gore32.velocity *= 0.3f;
        //    num704 = Gore.NewGore(base.Center, new Vector2((float)hitDirection, 0f), 63, this.scale);
        //    gore16 = Main.gore[num704];
        //    gore32 = gore16;
        //    gore32.velocity *= 0.3f;
        //}
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CrimtaneBar, 12)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                ChatMessageContainer chatMessageContainer = new ChatMessageContainer();
                //Main.chatMonitor.;

                Mod templateMod2 = ModLoader.GetMod("Terraria");
                var targetText = templateMod2.GetType().GetField("_messages", BindingFlags.Instance | BindingFlags.NonPublic);
                //?
                if (targetText.ToString() == "textcs")
                {
                    Main.NewText("yesyes");
                }
                if (chatMessageContainer.OriginalText == "textcs")
                {
                    Main.NewText("yesyes");
                }
            }
            return true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.MouseWorld;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback);
            player.AddBuff(BuffID.HeartyMeal, 90);
            //if (Main.rand.Next(2) == 0)
            //{
            //    player.Heal(damage / 6 + 1);
            //}
            //player.soulDrain += 10;
            return false;
        }
    }
}
