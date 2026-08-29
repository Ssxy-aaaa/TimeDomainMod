using System;
using TimeDomain.Content.Items.Projectiles.Summon;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace TimeDomain.Content.Buffs
{
    internal class SpikeSlimeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

            if (player.ownedProjectileCounts[ModContent.ProjectileType<SpikeSlimeMinion>()] > 0)
            {

                player.buffTime[buffIndex] = 18000;
            }
            else
            {

                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}