using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TimeDomain.Content.Items.Consumables
{
    public class BrokenPocketWatch : ModItem
    {
        public SoundStyle Sound = new SoundStyle("TimeDomain/Assets/Sounds/TS");
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = Sound;
            Item.useTurn = false;
            Item.autoReuse = false;
            Item.rare = ItemRarityID.Purple;
            Item.consumable = true;
        }
        public override bool ConsumeItem(Player player)
        {
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.DemonAltar)
                .Register();
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
                if (Main.npc[i].boss && Main.npc[i].active)
                    return false;

            if (TimeDomain.AncientMode)
            {
                TimeDomain.AncientMode = false;
                Main.NewText("世界正在回归现世", new Color(180, 40, 255));
            }
            else
            {
                TimeDomain.AncientMode = true;
                Main.NewText("世界正在变得远古", new Color(180, 40, 255));
            }
            return true;
        }
        //public override bool? UseItem(Player player)
        //{
        //return true;
        //}
    }
}
