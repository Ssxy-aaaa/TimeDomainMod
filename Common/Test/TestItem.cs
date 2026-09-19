using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TimeDomain.Common.Test
{
    public class TestItem : ModItem
    {
        public override string Texture => "Terraria/Images/Item_1";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 5;
            Item.useTime = 5;
            SetBossDownedName_Now();
        }
        public override void SetStaticDefaults()
        {
        }
        public override bool AltFunctionUse(Player player) => true;
        public int BossDownedCount_Now = 0;
        //public bool BossDowned_Now;
        //这是石山啊（）
        public override bool CanUseItem(Player player)
        {
            ref bool BossDowned_Now = ref NPC.downedBoss1;
            if (player.altFunctionUse != 2)
            {
                if (BossDowned_Now)
                {
                    BossDowned_Now = false;
                    Main.NewText("已将" + BossDownedName_Now[BossDownedCount_Now + 1] + "变量" + "由true转为false", new Color(100, 200, 100));
                }
                else
                {
                    BossDowned_Now = true;
                    Main.NewText("已将" + BossDownedName_Now[BossDownedCount_Now + 1] + "变量" + "由false转为true", new Color(100, 100, 200));
                }
                if (NPC.downedBoss2)
                {
                    NPC.downedBoss2 = false;
                    Main.NewText("已由true转为false", new Color(100, 200, 100));
                }
                else
                {
                    NPC.downedBoss2 = true;
                    Main.NewText("已由false转为true", new Color(100, 100, 200));
                }
            }
            else
            {
                BossDownedCount_Now++;
                BossDownedCount_Now %= 26;
                if (BossDownedCount_Now + 1 == 1)
                    BossDowned_Now = ref NPC.downedBoss1;
                if (BossDownedCount_Now + 1 == 2)
                    BossDowned_Now = ref NPC.downedBoss2;
                if (BossDownedCount_Now + 1 == 3)
                    BossDowned_Now = ref NPC.downedBoss3;
                if (BossDownedCount_Now + 1 == 4)
                    BossDowned_Now = ref NPC.downedQueenBee;
                if (BossDownedCount_Now + 1 == 5)
                    BossDowned_Now = ref NPC.downedSlimeKing;
                if (BossDownedCount_Now + 1 == 6)
                    BossDowned_Now = ref NPC.downedGoblins;
                if (BossDownedCount_Now + 1 == 7)
                    BossDowned_Now = ref NPC.downedFrost;
                if (BossDownedCount_Now + 1 == 8)
                    BossDowned_Now = ref NPC.downedPirates;
                if (BossDownedCount_Now + 1 == 9)
                    BossDowned_Now = ref NPC.downedClown;
                if (BossDownedCount_Now + 1 == 10)
                    BossDowned_Now = ref NPC.downedPlantBoss;
                if (BossDownedCount_Now + 1 == 11)
                    BossDowned_Now = ref NPC.downedGolemBoss;
                if (BossDownedCount_Now + 1 == 12)
                    BossDowned_Now = ref NPC.downedMartians;
                if (BossDownedCount_Now + 1 == 13)
                    BossDowned_Now = ref NPC.downedFishron;
                if (BossDownedCount_Now + 1 == 14)
                    BossDowned_Now = ref NPC.downedHalloweenTree;
                if (BossDownedCount_Now + 1 == 15)
                    BossDowned_Now = ref NPC.downedHalloweenKing;
                if (BossDownedCount_Now + 1 == 16)
                    BossDowned_Now = ref NPC.downedChristmasIceQueen;
                if (BossDownedCount_Now + 1 == 17)
                    BossDowned_Now = ref NPC.downedChristmasTree;
                if (BossDownedCount_Now + 1 == 18)
                    BossDowned_Now = ref NPC.downedChristmasSantank;
                if (BossDownedCount_Now + 1 == 19)
                    BossDowned_Now = ref NPC.downedAncientCultist;
                if (BossDownedCount_Now + 1 == 20)
                    BossDowned_Now = ref NPC.downedMoonlord;
                if (BossDownedCount_Now + 1 == 21)
                    BossDowned_Now = ref NPC.downedTowerSolar;
                if (BossDownedCount_Now + 1 == 22)
                    BossDowned_Now = ref NPC.downedTowerVortex;
                if (BossDownedCount_Now + 1 == 23)
                    BossDowned_Now = ref NPC.downedTowerNebula;
                if (BossDownedCount_Now + 1 == 24)
                    BossDowned_Now = ref NPC.downedTowerStardust;
                if (BossDownedCount_Now + 1 == 25)
                    BossDowned_Now = ref NPC.downedEmpressOfLight;
                if (BossDownedCount_Now + 1 == 26)
                    BossDowned_Now = ref NPC.downedQueenSlime;
                Main.NewText("已切换至"+BossDownedName_Now[BossDownedCount_Now + 1]+"变量",new Color(200,100,100));
            }
            return true;
        }
        public static string[] BossDownedName_Now = new string[30];

        public static void SetBossDownedName_Now()
        {
            BossDownedName_Now[1] = "downedBoss1";
            BossDownedName_Now[2] = "downedBoss2";
            BossDownedName_Now[3] = "downedBoss3";
            BossDownedName_Now[4] = "downedQueenBee";
            BossDownedName_Now[5] = "downedSlimeKing";
            BossDownedName_Now[6] = "downedGoblins";
            BossDownedName_Now[7] = "downedFrost";
            BossDownedName_Now[8] = "downedPirates";
            BossDownedName_Now[9] = "downedClown";
            BossDownedName_Now[10] = "downedPlantBoss";
            BossDownedName_Now[11] = "downedGolemBoss";
            BossDownedName_Now[12] = "downedMartians";
            BossDownedName_Now[13] = "downedFishron";
            BossDownedName_Now[14] = "downedHalloweenTree";
            BossDownedName_Now[15] = "downedHalloweenKing";
            BossDownedName_Now[16] = "downedChristmasIceQueen";
            BossDownedName_Now[17] = "downedChristmasTree";
            BossDownedName_Now[18] = "downedChristmasSantank";
            BossDownedName_Now[19] = "downedAncientCultist";
            BossDownedName_Now[20] = "downedMoonlord";
            BossDownedName_Now[21] = "downedTowerSolar";
            BossDownedName_Now[22] = "downedTowerVortex";
            BossDownedName_Now[23] = "downedTowerNebula";
            BossDownedName_Now[24] = "downedTowerStardust";
            BossDownedName_Now[25] = "downedEmpressOfLight";
            BossDownedName_Now[26] = "downedQueenSlime";
        }
        public const int downedBoss1 = 1;
        public const int downedBoss2 = 2;
        public const int downedBoss3 = 3;
        public const int downedQueenBee = 4;
        public const int downedSlimeKing = 5;
        public const int downedGoblins = 6;
        public const int downedFrost = 7;
        public const int downedPirates = 8;
        public const int downedClown = 9;
        public const int downedPlantBoss = 10;
        public const int downedGolemBoss = 11;
        public const int downedMartians = 12;
        public const int downedFishron = 13;
        public const int downedHalloweenTree = 14;
        public const int downedHalloweenKing = 15;
        public const int downedChristmasIceQueen = 16;
        public const int downedChristmasTree = 17;
        public const int downedChristmasSantank = 18;
        public const int downedAncientCultist = 19;
        public const int downedMoonlord = 20;
        public const int downedTowerSolar = 21;
        public const int downedTowerVortex = 22;
        public const int downedTowerNebula = 23;
        public const int downedTowerStardust = 24;
        public const int downedEmpressOfLight = 25;
        public const int downedQueenSlime = 26;

        //public static readonly Dictionary<int, bool> BossDownedMappingTable = new Dictionary<int, bool>();
        //public static void SetBossDownedMappingTable()
        //{
        //    BossDownedMappingTable[1] = ;
        //    BossDownedMappingTable[2] = ;
        //    BossDownedMappingTable[3] = ;
        //    BossDownedMappingTable[4] = ;
        //    BossDownedMappingTable[5] = ;
        //    BossDownedMappingTable[6] = ;
        //    BossDownedMappingTable[7] = ;
        //    BossDownedMappingTable[8] = ;
        //    BossDownedMappingTable[9] = ;
        //    BossDownedMappingTable[10] = ;
        //    BossDownedMappingTable[11] = ;
        //    BossDownedMappingTable[12] = ;
        //    BossDownedMappingTable[13] = ;
        //    BossDownedMappingTable[14] = ;
        //    BossDownedMappingTable[15] = ;
        //    BossDownedMappingTable[16] = ;
        //    BossDownedMappingTable[17] = ;
        //    BossDownedMappingTable[18] = ;
        //    BossDownedMappingTable[19] = ;
        //    BossDownedMappingTable[20] = ;
        //    BossDownedMappingTable[21] = ;
        //    BossDownedMappingTable[22] = ;
        //    BossDownedMappingTable[23] = ;
        //    BossDownedMappingTable[24] = ;
        //    BossDownedMappingTable[25] = ;
        //    BossDownedMappingTable[26] = ;
        //}
    }
}
