using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.NPCs.Bosses.PrimordialSlime;

namespace TimeDomain.Content.Items.Consumables
{
    public class CursedGel : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = Item.height = 32;
            Item.rare = ItemRarityID.Green;
            Item.value = 0;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = Item.useTime = 10;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
        }
        public override bool ConsumeItem(Player player)
        {
            return false;
        }
        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position);
                int type = ModContent.NPCType<PrimordialSlime>();
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);//发包，用来联机同步
                }
            }
            return true;
        }
    }
}
