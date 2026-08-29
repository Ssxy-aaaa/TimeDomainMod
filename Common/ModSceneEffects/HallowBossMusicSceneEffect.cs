using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TimeDomain.Common.ModSceneEffects
{
    public class OceanMusicChange : ModSceneEffect
    {
        Player player = Main.player[Main.myPlayer];
        public override int Music
        {
            get
            {
                return MusicLoader.GetMusicSlot(Mod, "Assets/Music/Surrender");
            }
        }
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override bool IsSceneEffectActive(Player player)
        {
            bool flag = false;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].type == NPCID.HallowBoss && Main.npc[i].active)
                {
                    flag = true; break;
                }
            }
            return flag;
        }
    }
}
