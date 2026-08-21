using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace yourmod.Common.Systems
{
    public class DownedBossSystem : ModSystem
    {
        public static bool downedPrimordialSlime = false;
        public override void ClearWorld()
        {
            downedPrimordialSlime = false;
        }
        public override void SaveWorldData(TagCompound tag)
        {
            tag["downedPrimordialSlime"] = downedPrimordialSlime;
        }
        public override void LoadWorldData(TagCompound tag)
        {
            downedPrimordialSlime = tag.ContainsKey("downedPrimordialSlime");
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.WriteFlags(downedPrimordialSlime);
        }
        public override void NetReceive(BinaryReader reader)
        {
            reader.ReadFlags(out downedPrimordialSlime);
        }
    }
}
