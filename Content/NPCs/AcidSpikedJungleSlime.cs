using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Common;
using TimeDomain.Content.Projectiles;

namespace TimeDomain.Content.NPCs
{
    public class AcidSpikedJungleSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 2;
        }
        int FindFrameTimer = 0;
        int FindFrameCounter = 0;
        public override void FindFrame(int frameHeight)
        {
            FindFrameTimer++;
            if (FindFrameTimer % 15 == 0)
            {
                FindFrameCounter++;
                if (FindFrameCounter > 1)
                {
                    FindFrameCounter = 0;
                }
            }
            frameHeight = 96 / 2;
            NPC.frame.Y = FindFrameCounter * frameHeight;
        }
        public override void SetDefaults()
        {
            //NPC.CloneDefaults(204);
            NPC.width = 42;
            NPC.height = 32;
            //大师模式：生命670，碰撞289，尖刺306
            NPC.lifeMax = 672 / 3;
            NPC.damage = 288 / 3;
            NPC.defense = 12;
            NPC.knockBackResist = 0.4f;
            NPC.value = Item.buyPrice(0, 0, 16, 0);
            NPC.aiStyle = NPCAIStyleID.Slime;
            //AIType = NPCID.SpikedJungleSlime;
            //大师模式：生命值670，碰撞289，尖刺306
            //专家模式：生命值335，碰撞195，尖刺153，防御和抗击退不变
            //普通模式：生命值164，碰撞98，尖刺78，防御和抗击退不变
            //ModUtil.SetNPCDamageAndLifeMax(NPC, 98, 195, 289, 164, 335, 670);
            ModUtil.SetNPCDamageAndLifeMax(NPC, 98, 196, 291, 164, 336, 672);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle,
                new FlavorTextBestiaryInfoElement("这种黏糊糊的绿色巨型史莱姆，食用了远古的南方巨型植物遗留的根茎后，长出一枚类似核心的粉色玫瑰，这似乎就是它身上长出茂密尖刺的原因")
            });
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                if (spawnInfo.Player.ZoneJungle)
                {
                    if (spawnInfo.Player.Center.Y > Main.rockLayer)
                    {
                        return 0.4f;
                    }
                    return 0.1f;
                }
            }
            return 0f;
        }
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }
        public override void OnKill()
        {
            base.OnKill();
        }
        public override bool PreAI()
        {
            return base.PreAI();
        }
        public static int JungleAcidSpikeProj => ModContent.ProjectileType<JungleAcidSpike>();
        public override void AI()
        {
            #region 原版参考AI
            //bool flag3 = false;
            //if (NPC.type == 204)
            //{
            //flag3 = true;
            if (NPC.localAI[0] > 0f)
            {
                NPC.localAI[0] -= 1f;
            }
            if (!NPC.wet && Main.player[NPC.target].active && !Main.player[NPC.target].dead && !Main.player[NPC.target].npcTypeNoAggro[NPC.type])
            {
                Vector2 vector6 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float num49 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector6.X;
                float num50 = Main.player[NPC.target].position.Y - vector6.Y;
                float num51 = (float)Math.Sqrt((double)(num49 * num49 + num50 * num50));
                if (Main.expertMode && num51 < 400 && Collision.CanHit(new Vector2(NPC.position.X, NPC.position.Y - 20f), NPC.width, NPC.height + 20, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && NPC.velocity.Y == 0f)
                {
                    NPC.ai[0] = -40f;
                    //if (NPC.velocity.Y == 0f)
                    //{
                    //    NPC.velocity.X = NPC.velocity.X * 0.9f;
                    //}
                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.localAI[0] == 0f)
                    {
                        for (int l = 0; l < 9; l++)
                        {
                            Vector2 vector7 = new Vector2((float)(l - 2), -2f);
                            vector7.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.02f;
                            vector7.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.02f;
                            vector7.Normalize();
                            vector7 *= 3f + (float)Main.rand.Next(-50, 51) * 0.01f;
                            int attackDamage_ForProjectiles3 = NPC.GetAttackDamage_ForProjectiles(34, 34);
                            vector7 *= Main.rand.Next(1, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), vector6, vector7, JungleAcidSpikeProj, attackDamage_ForProjectiles3, 0f, Main.myPlayer, 0f, 0f, 0f);
                            NPC.localAI[0] = 60;//80f;
                        }
                    }
                }
                if (num51 < 800 && Collision.CanHit(new Vector2(NPC.position.X, NPC.position.Y - 20f), NPC.width, NPC.height + 20, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && NPC.velocity.Y == 0f)
                {
                    NPC.ai[0] = -80f;
                    //if (NPC.velocity.Y == 0f)
                    //{
                    //    NPC.velocity.X = NPC.velocity.X * 0.9f;
                    //}
                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.localAI[0] == 0f)
                    {
                        num50 = Main.player[NPC.target].position.Y - vector6.Y - (float)Main.rand.Next(-30, 20);
                        num50 -= num51 * 0.05f;
                        num49 = Main.player[NPC.target].position.X - vector6.X - (float)Main.rand.Next(-20, 20);
                        num51 = (float)Math.Sqrt((double)(num49 * num49 + num50 * num50));
                        num51 = 7f / num51;
                        num49 *= num51;
                        num50 *= num51;
                        NPC.localAI[0] = 45;//65f;
                        Vector2 v = new(num49, num50);
                        v *= Main.rand.Next(1, 2);
                        for (int l = 0; l < 3; l++)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), vector6, v + new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(-3, 3)), JungleAcidSpikeProj, 34, 0f, Main.myPlayer, 0f, 0f, 0f);
                    }
                }
            }
            //}
            #endregion
        }
        public override void PostAI()
        {
            
        }
        public override void DrawEffects(ref Color drawColor)
        {
            
        }
    }
}
