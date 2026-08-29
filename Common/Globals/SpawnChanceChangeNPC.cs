using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Items.Materials;
using TimeDomain.Content.NPCs;

namespace TimeDomain.Common.Globals
{
    public class SpawnChanceChangeNPC : GlobalNPC
    {
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (Main.hardMode)
            {
                if (npc.type == NPCID.SpikedJungleSlime)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        npc.type = ModContent.NPCType<AcidSpikedJungleSlime>();
                    }
                }
            }
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.GraniteGolem || npc.type == NPCID.GraniteFlyer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GraniteCore>(), 2, 2, 5));
            }
        }
    }
}
