using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TimeDomain.Common;

namespace TimeDomain.Content.NPCs.Bosses.ThePonder
{
    public class Plugin : ModNPC
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            NPC.width = NPC.height=45;
            NPC.knockBackResist = 0.1f;
            NPC.defense = 10;
            ModUtil.SetNPCDamageAndLifeMax(NPC, 70, 90, 110, 250, 300, 350);
            NPC.value = Item.buyPrice(silver: 50);
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.buffImmune[31] = true;
            //Array.Fill(NPC.buffImmune, true);
        }
        public NPC Owner => Main.npc[(int)NPC.ai[0]];
        public float Timer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        public float Timer2
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        enum SkillState
        {
            None,
            Sprint,
            Shoot,
        }
        SkillState state = SkillState.None;
        public Player player => Main.player[(NPC.target >= 0 && NPC.target <= 255) ? NPC.target : Main.myPlayer];
        float ro;
        public override void AI()
        {
            NPC.TargetClosest(true);
            Timer++;
            switch (state)
            {
                case SkillState.None:
                    Timer2++;
                    if(Timer2 %60==0)
                    ro = (Main.rand.Next(5) - 2) / 2 * MathHelper.PiOver2;

                    Vector2 v = Main.npc[(int)NPC.ai[3]].Center - NPC.Center;
                    NPC.velocity += Vector2.Normalize(v) * 1.5f;
                    NPC.velocity = NPC.velocity / NPC.velocity.Length() * 5;
                    if (Timer2 >= 540)
                    {
                        Timer2 = 0;
                        state = SkillState.Sprint;
                    }
                    break;
                case SkillState.Sprint:
                    Timer2++;
                    if (Timer2 % 60 == 0)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Normalize(player.Center - NPC.Center) * 4.5f, ProjectileID.DeathLaser, ModUtil.SetProjectileDamage(70, 90, 110), 3f);
                        Vector2 v1 = player.Center - NPC.Center;
                        NPC.velocity += Vector2.Normalize(v1) * 4.5f;
                        NPC.velocity = NPC.velocity / NPC.velocity.Length() * 5;
                    }
                    if (Timer2 >= 200)
                    {
                        Timer2 = 0;
                        state = SkillState.None;
                    }
                    break;
            }
            NPC.rotation += NPC.velocity.Length() / 50f;
            if (!(NPC.target >= 0 && NPC.target <= 255))
            {
                NPC.TargetClosest(true);
            }
            base.AI();
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());//第一行是创建一个掉落规则，在非专家大师模式时掉落
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.Heart, 2));//第二行是向这个掉落规则里放一个掉落，1/3概率掉落，一次掉落5~15个石头
            npcLoot.Add(notExpertRule);//第三行是把这个掉落规则放入总掉落里
                                       //npcLoot.Add(ItemDropRule.BossBag(ItemID.Heart));//第四行是向总掉落里放一个石头，这个石头以宝藏袋的标准掉落出来（只在专家大师掉落）
                                       //npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemID.Heart));//第五行，在大师模式下掉落一个石头
                                       //npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ItemID.Heart, 1));//第六行，在大师模式下，给所有玩家，都掉落一个/石头/，人人有份，每个人在自己的客户端都有/一/个
                                       //具体内容可以试试自动补全
            LeadingConditionRule ExpertRule = new LeadingConditionRule(new Conditions.IsExpert());
            ExpertRule.OnSuccess(ItemDropRule.Common(ItemID.Heart));
            npcLoot.Add(ExpertRule);
        }
    }
}
