using Terraria;
using Terraria.ModLoader;

namespace TimeDomain.Content.Buffs
{
    public class OpticNeuronBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            
        }
        public override string Texture => "Terraria/Images/Buff_313";
    }
}
