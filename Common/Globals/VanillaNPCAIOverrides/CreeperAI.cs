using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace TimeDomain.Common.Globals.VanillaNPCAIOverrides
{
    public static class CreeperAI
    {
        public static NPC thisNPC = null;
        public static float BossTime
        {
            get => thisNPC.localAI[1];
            set => thisNPC.localAI[1] = value;
        }
        public static float BossTime2
        {
            get => thisNPC.localAI[2];
            set => thisNPC.localAI[2] = value;
        }
        public static float BossTime3
        {
            get => thisNPC.localAI[3];
            set => thisNPC.localAI[3] = value;
        }

        public static bool BerserkMode = false;
        public static void Text(object a)
        {
            Main.NewText(a, Color.Red);
        }

        public static bool BuffedAI(NPC npc)
        {
            thisNPC = npc;
            return false;
        }
        public static void VanillaAI(NPC npc)
        {
            
        }
        //public static bool IsDeadOnce = false;
        public static bool CheckDead(NPC npc)
        {
            if (!npc.dontTakeDamage)
            {
                thisNPC = npc;
                npc.active = true;
                npc.life = 1;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
                return false;
            }
            return true;
        }
    }
}
