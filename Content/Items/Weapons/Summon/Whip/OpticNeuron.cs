using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace TimeDomain.Content.Items.Weapons.Summon.Whip
{
    public class OpticNeuron : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<OpticNeuronProj>(), 23, 4f, 6);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.rare = ItemRarityID.LightRed;
        }

        //public override void AddRecipes()
        //{
        //    CreateRecipe()
        //        .AddIngredient(ModContent.ItemType<>(), 12)
        //        .AddIngredient(ModContent.ItemType<>(), 12)
        //        .AddIngredient(ItemID.Silk, 7)
        //        .AddTile(TileID.DemonAltar)
        //        .Register();
        //}

        public override bool MeleePrefix()
        {
            return true;
        }
    }
}
