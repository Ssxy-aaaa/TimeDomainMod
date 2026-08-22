using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace yourmod.Content.NPCs.Bosses.ThePonder
{
    public class ThePonder : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 12;
        }
        public override void SetDefaults()
        {
            NPC.width = 194;
            NPC.height = 2112 / 12;
            //NPC.HitSound = SoundID.
        }
    }
}
