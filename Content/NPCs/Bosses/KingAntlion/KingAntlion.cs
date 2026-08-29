using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace TimeDomain.Content.NPCs.Bosses.KingAntlion
{
    //蚁狮马王
    public class KingAntlion : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 150;   
            NPC.lifeMax = 2312;//血量
            NPC.damage = 34;//伤害
            NPC.defense = 9;//防御
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 10, 0, 0);
            NPC.boss = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
        }
        public override void FindFrame(int frameHeight)
        {
            frameHeight = 195;
            NPC.frameCounter++;
            if (NPC.frameCounter % 15 == 0)
            {
                NPC.frame.Y += frameHeight;
            }
            if (NPC.frame.Y > frameHeight * 3)
            {
                NPC.frame.Y = 0;
            }
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                new FlavorTextBestiaryInfoElement("隐匿于地下的巨型节肢动物，用生命的代价守护着他们的孩子……"/*"隐秘于地下的巨型节肢动物，用生命的代价守护着他们的孩子"*/)//图鉴
            });
        }
    }
}
