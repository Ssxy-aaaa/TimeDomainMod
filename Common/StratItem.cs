using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TimeDomain.Content.Items.Weapons.Melee;

namespace TimeDomain.Common
{
    public class StartItems : ModPlayer
    {
        private bool receivedStartKnives;

        public override void OnEnterWorld()
        {
            if (receivedStartKnives)
                return;

            receivedStartKnives = true;

            Player.QuickSpawnItem(
                new EntitySource_Misc("TimeDomain:StartItems"),
                ModContent.ItemType<CopperThrowingKnife>(),
                1
            );
        }

        public override void SaveData(TagCompound tag)
        {
            tag["receivedStartKnives"] = receivedStartKnives;
        }

        public override void LoadData(TagCompound tag)
        {
            receivedStartKnives = tag.GetBool("receivedStartKnives");
        }
    }
}