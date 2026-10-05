using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TimeDomain.Content.Buffs
{
    public class TetanusBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;

            Main.pvpBuff[Type] = true;

            Main.buffNoSave[Type] = false;

            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen -= 16;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.lifeRegen -= 16;
        }
    }
}