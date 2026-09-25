using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
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
            NPCID.Sets.TrailingMode[Type] = 3;
            NPCID.Sets.TrailCacheLength[Type] = 5;
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
        public NPC Owner => Main.npc[(int)NPC.ai[3]];
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
        public enum SkillState
        {
            None,
            Sprint,
            Shoot,
            Obstructed,//PredictPlayerPosition
            BeenControlled,
            SelfDestruct
        }
        public SkillState state = SkillState.None;
        public Player player => Main.player[(NPC.target >= 0 && NPC.target <= 255) ? NPC.target : Main.myPlayer];
        public bool IsBeenControlled = false;
        public void ChangeState(bool a)
        {
            if (!a) return;
            Timer2 = 0;
            if (Owner.ModNPC is ThePonder thePonder)
            {
                if (thePonder.Phase == 1)
                {
                    switch (state)
                    {
                        case SkillState.None:
                            state = SkillState.Sprint;
                            break;
                        case SkillState.Sprint:
                            state = SkillState.Shoot;
                            break;
                        case SkillState.Shoot:
                            state = SkillState.Sprint;
                            break;
                    }
                }
                if (thePonder.Phase == 2)
                {
                    switch (state)
                    {
                        case SkillState.None:
                            state = SkillState.Sprint;
                            break;
                        case SkillState.Sprint:
                            state = SkillState.Shoot;
                            break;
                        case SkillState.Shoot:
                            state = Main.rand.NextBool(2) ? SkillState.Obstructed : SkillState.Sprint;
                            break;
                        case SkillState.Obstructed:
                            state = Main.rand.NextBool(2) ? SkillState.BeenControlled : SkillState.Sprint;
                            break;
                    }
                }
                if (thePonder.Phase == 3)
                {
                    switch (state)
                    {
                        case SkillState.None:
                            state = SkillState.Shoot;
                            break;
                        case SkillState.Sprint:
                            state = SkillState.Shoot;
                            break;
                        case SkillState.Shoot:
                            state = SkillState.Obstructed;
                            break;
                        case SkillState.Obstructed:
                            state = /*Main.rand.NextBool(2) ? SkillState.SelfDestruct : */SkillState.Sprint;
                            break;
                        case SkillState.SelfDestruct:
                            state = SkillState.None;
                            break;
                    }
                }
            }
        }
        public void Boom()
        {
            SoundEngine.PlaySound(SoundID.Item14, NPC.Center);
            NPC.width *= 3;
            NPC.height *= 3;
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Timer++;
            switch (state)
            {
                case SkillState.None:
                    Timer2++;

                    ChangeState(Timer2 >= 30);
                    break;
                case SkillState.Sprint:
                    Timer2++;

                    if (Timer2 == 15)
                        NPC.velocity = Vector2.Normalize(player.Center - NPC.Center).RotatedByRandom(MathHelper.Pi / 18f) * 11;

                    if (Timer2 >= 40)
                        NPC.velocity *= 0.96f;

                    ChangeState(Timer2 >= 100);
                    break;
                case SkillState.Shoot:
                    Timer2++;
                    if (Timer2 % 100 == 0)
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Normalize(player.Center - NPC.Center) * (Main.rand.Next(30, 45) / 10f), ProjectileID.DeathLaser, ModUtil.SetProjectileDamage(70, 85, 110), 3f);

                    ChangeState(Timer2 >= 200);
                    break;
                case SkillState.Obstructed:
                    Timer2++;

                    if (Timer2 <= 70 && Timer2 >= 20)
                    {
                        NPC.velocity = Vector2.Lerp(NPC.Center, player.Center + (player.velocity.Length() < 1 ? (Vector2.One * 150f) : (player.velocity.ToRotation().ToRotationVector2() * 150f)), 0.01f) - NPC.Center;
                        NPC.damage = 0;
                    }
                    else
                        ModUtil.SetNPCDamageAndLifeMax_InBossFight(NPC, 70, 90, 110, 250, 300, 350);
                    ChangeState(Timer2 >= 100);
                    break;
                case SkillState.BeenControlled:
                    Timer2++;
                    if (IsBeenControlled)
                    {
                        if (Owner.ModNPC is ThePonder thePonder)
                        {
                            if (!thePonder.ControledPlugin.Contains(NPC.whoAmI))
                            {
                                thePonder.ControledPlugin.Add(NPC.whoAmI);
                            }
                        }
                    }
                    if (Timer2 >= 40)
                    {
                        //Vector2 v = Vector2.Lerp(NPC.Center, player.Center + ((NPC.whoAmI / 50f) * MathHelper.Pi * 2).ToRotationVector2() * 90, 0.03f) - NPC.Center;
                        //NPC.velocity = v;
                        if (Timer2 % 60 == 0)
                            NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * Math.Min((player.Center - NPC.Center).Length() / 5f, 11);
                        NPC.velocity *= 0.98f;
                        // NPC.velocity += new Vector2(Main.rand.Next(20) / 10f);
                        Main.NewText("ICo", Color.Red);
                    }
                    if (Vector2.Distance(player.Center, NPC.Center) < 100 && C++ == 0) 
                    {
                        Boom();
                    }
                    if(C == 20)
                    {
                        NPC.damage = 0;
                        NPC.StrikeInstantKill();
                    }
                    ChangeState(!IsBeenControlled);
                    break;
                case SkillState.SelfDestruct:
                    Timer2++;

                    if (Timer2 % 20 == 0)
                        Main.NewText("a");
                    if (Timer2 % 20 == 0)
                        NPC.StrikeInstantKill();
                    ChangeState(Timer2 >= 20);
                    break;

            }

            NPC.rotation += NPC.velocity.Length() / 50f + 0.05f;
            if (!(NPC.target >= 0 && NPC.target <= 255))
            {
                NPC.TargetClosest(true);
            }
            if (Owner.ModNPC is ThePonder thePonder2)
            {
                if (thePonder2.Phase >= 2)
                {
                    NPC.defense = 15 + (thePonder2.Phase == 3).ToInt() * 10;
                }
            }
            base.AI();
        }
        int C = 0;
        int DrawStyle = 0;
        Texture2D texture = ModContent.Request<Texture2D>("TimeDomain/Assets/Textures/Misc/Effect_7").Value;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {

            Texture2D Tex = TextureAssets.Npc[NPC.type].Value;
            Rectangle sourceRect = NPC.frame;
            Vector2 origin = new Vector2(sourceRect.Width / 2f, sourceRect.Height / 2f);
            Vector2 drawPos = NPC.position + NPC.Size / 2 - Main.screenPosition;
            for (int i = 0; i < NPC.oldPos.Length; i++)
            {
                float progress = i / (float)NPC.oldPos.Length;
                float alpha = (1f - progress) * 0.6f;
                drawPos = NPC.oldPos[i] + NPC.Size / 2 - Main.screenPosition;
                Color trailColor = Color.White * alpha;
                spriteBatch.Draw(Tex, drawPos, sourceRect, trailColor, NPC.rotation, origin, NPC.scale, SpriteEffects.None, 0f);
            }
            DrawStyle = (state == SkillState.BeenControlled).ToInt();
            if (DrawStyle == 1)
            {
                spriteBatch.Draw(texture, drawPos, sourceRect, drawColor, 0, origin, 1, 0, 0);
            }
            return false;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            //LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());//第一行是创建一个掉落规则，在非专家大师模式时掉落
            //notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.Heart, 2));//第二行是向这个掉落规则里放一个掉落，1/3概率掉落，一次掉落5~15个石头
            //npcLoot.Add(notExpertRule);//第三行是把这个掉落规则放入总掉落里
            //                           //npcLoot.Add(ItemDropRule.BossBag(ItemID.Heart));//第四行是向总掉落里放一个石头，这个石头以宝藏袋的标准掉落出来（只在专家大师掉落）
            //                           //npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemID.Heart));//第五行，在大师模式下掉落一个石头
            //                           //npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ItemID.Heart, 1));//第六行，在大师模式下，给所有玩家，都掉落一个/石头/，人人有份，每个人在自己的客户端都有/一/个
            //                           //具体内容可以试试自动补全
            //LeadingConditionRule ExpertRule = new LeadingConditionRule(new Conditions.IsExpert());
            //ExpertRule.OnSuccess(ItemDropRule.Common(ItemID.Heart));
            //npcLoot.Add(ExpertRule);
            npcLoot.Add(ItemDropRule.Common(ItemID.Heart, 3));
        }
    }
}
