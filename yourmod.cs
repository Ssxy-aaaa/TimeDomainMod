using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using yourmod.Common.Globals.VanillaNPCAIOverrides;
using yourmod.Common.Systems;
using yourmod.Content.Items.Arrows;
using yourmod.Content.Items.Consumables;
using yourmod.Content.NPCs.Bosses.PrimordialSlime;

namespace yourmod
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class yourmod : Mod
    {
        public static yourmod Instance = ModContent.GetInstance<yourmod>();
        public static bool AncientMode = false;
        public List<ChatMessageContainer> ChatMessages = new List<ChatMessageContainer>();
        public object CM = new object();
        public override void PostSetupContent()
        {
            //Mod templateMod2 = ModLoader.GetMod("Terraria");

            //if (templateMod2 != null)
            //{
            //    // 获取TemplateMod2._text这个私有成员字段的句柄
            //    // 后面的两个过滤器一定要打对，才能准确索引到这个字段
            //    var targetText = templateMod2.GetType().GetField("_messages", BindingFlags.Instance | BindingFlags.NonPublic);
            //    // 接下来我们对TemplateMod2的实例进行修改
            //    //targetText.SetValue(templateMod2, “你才不是模板Mod”);
            //    CM = targetText;
            //}
            OverrideVa();
            BossChecklist_Check();
        }
        public static void OverrideVa()
        {

            TextureAssets.Item[4924] = ModContent.Request<Texture2D>("yourmod/Common/Textures/Item/Vanilla/Item_4924");
            TextureAssets.Item[1360] = ModContent.Request<Texture2D>("yourmod/Common/Textures/Item/Vanilla/Item_1360");
            TextureAssets.Item[2112] = ModContent.Request<Texture2D>("yourmod/Common/Textures/Item/Vanilla/Item_2112");
            TextureAssets.Item[3097] = ModContent.Request<Texture2D>("yourmod/Common/Textures/Item/Vanilla/Item_3097");
            TextureAssets.Item[3319] = ModContent.Request<Texture2D>("yourmod/Common/Textures/Item/Vanilla/Item_3319");

            TextureAssets.ArmorHead[154] = ModContent.Request<Texture2D>("yourmod/Common/Textures/Item/Vanilla/Armor_Head_154");


            TextureAssets.Extra[ExtrasID.MasterTrophyHeads] = ModContent.Request<Texture2D>("yourmod/Common/Textures/Item/Vanilla/Extra_198");

            //int L = ItemID.Sets.ShimmerTransformToItem.Length;
            //ItemID.Sets.ShimmerTransformToItem[L] = 97;
            //ItemID.Sets.ShimmerTransformToItem[L + 1] = ModContent.ItemType<ShimmerBullet>();
            ItemID.Sets.ShimmerTransformToItem[ItemID.MusketBall] = ModContent.ItemType<ShimmerBullet>();
        }
        public static void BossChecklist_Check()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist == null)
                return;

            LocalizedText spawnInfo = LocalizedText.Empty;
            spawnInfo = Instance.GetLocalization("NPCs.PrimordialSlime.spawnInfo") ?? LocalizedText.Empty;

            Dictionary<string, object> PrimordialSlimeNPCInfo = new Dictionary<string, object>() {
                ["displayName"] = Instance.GetLocalization("NPCs.PrimordialSlime.DisplayName"),
                ["spawnInfo"] = (Func<LocalizedText>)(()=> spawnInfo),
                ["spawnItems"] = ModContent.GetInstance<PrimordialSlime>().SpawnItem,
                ["collectibles"] = new List<int>{ ModContent.ItemType<BrokenPocketWatch>()},
                //["customPortrait"] = customPortrait

            };
            

            bossChecklist.Call(
                "LogBoss",
                Instance,
                "PrimordialSlime",
                0.9f,
                (Func<bool>)(() => DownedBossSystem.downedPrimordialSlime),
                ModContent.NPCType<PrimordialSlime>(),
                PrimordialSlimeNPCInfo);

            //bossChecklist.Call("AddBossWithInfo", "PrimordialSlime", 0.9f, (Func<bool>)(() => DownedBossSystem.downedPrimordialSlime), "Use a [i:" + ModContent.ItemType<BrokenPocketWatch>() + "]");




            void AddNPC(string NPCName,float value,Func<bool> NPCFlag,int NpcType,Dictionary<string,object> Info)
            {
                bossChecklist.Call("LogBoss", Instance, NPCName, value, NPCFlag, NpcType, Info);
            }

            {
                //SlimeKing = 1f;
                //EyeOfCthulhu = 2f;
                //EaterOfWorlds = 3f;
                //QueenBee = 4f;
                //Skeletron = 5f;
                //WallOfFlesh = 6f;
                //TheTwins = 7f;
                //TheDestroyer = 8f;
                //SkeletronPrime = 9f;
                //Plantera = 10f;
                //Golem = 11f;
                //DukeFishron = 12f;
                //LunaticCultist = 13f;
                //Moonlord = 14f;
            }
        }
    }
}
