using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace TimeDomain.Common.Globals.VanillaNPCAIOverrides
{
    public static class KingSlimeAI
    {
        public static NPC thisNPC = null;
        public static float BossTime
        {
            get => thisNPC.localAI[1];
            set => thisNPC.localAI[1] = value;
        }
        public static float BossTime2
        {
            get => thisNPC.localAI[2];
            set => thisNPC.localAI[2] = value;
        }
        public static float BossTime3
        {
            get => thisNPC.localAI[3];
            set => thisNPC.localAI[3] = value;
        }

        public static bool BerserkMode = false;
        public static void Text(object a)
        {
            Main.NewText(a, Color.Red);
        }


        public static int JumpMaxX
        {
            get
            {
                if (Main.masterMode && Main.getGoodWorld)
                    return 224;//14
                if (Main.masterMode)
                    return 160;//10
                if (Main.expertMode)
                    return 144;//9
                return 112;//7
            }
        }
        public static int JumpMaxY
        {
            get
            {
                if (Main.masterMode && Main.getGoodWorld)
                    return 7 * 16;//
                if (Main.masterMode)
                    return 5 * 16;//
                if (Main.expertMode)
                    return 4 * 16;//
                return 3 * 16;//
            }
        }
        public static int BigJumpMaxX
        {
            get
            {
                if (Main.masterMode && Main.getGoodWorld)
                    return 23 * 16;
                if (Main.masterMode)
                    return 18 * 16;
                if (Main.expertMode)
                    return 15 * 16;
                return 13 * 16;
            }
        }

        enum State
        {
            Jump,
            BigJump,
            Teleport
        }
        static State state = new State();
        public static int SkillTimer = 0;
        public static int NumberOfJumps = 0;
        public static int direction;
        public static int TeleportReduceTime = 0;
        public static int CurrentSkill = 0;


        public static int TeleportDelay = 0;


        public static bool SetTextWrite = true;
        public static bool BuffedAI(NPC npc)
        {
            thisNPC = npc;

            float npcScale = (float)npc.life / (float)npc.lifeMax;
            npcScale = npcScale * 0.5f + 0.75f;
            npc.scale = npcScale;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            #region OldCode
            //switch (state)
            //{
            //    case State.Start:
            //        Text("Start");
            //        SkillTimer = 0;
            //        state = State.Wait;
            //        break;
            //    case State.Wait:
            //        SkillTimer++;
            //        if (SkillTimer == 1)
            //        {
            //            Text("Wait");
            //        }
            //        npc.velocity = Vector2.Zero;
            //        int JumpIntervalTime = 90;
            //        if (Main.expertMode)
            //            JumpIntervalTime = 60;
            //        if (Main.masterMode)
            //            JumpIntervalTime = 40;
            //        if (SkillTimer > JumpIntervalTime)
            //        {
            //            SkillTimer = 0;
            //            state = State.Jump;
            //        }
            //        break;
            //    case State.Jump:
            //        SkillTimer++;
            //        if (SkillTimer == 1)
            //        {
            //            Text("Jump");
            //        }
            //        if (SkillTimer == 1)
            //        {
            //            if (player.Center.X > npc.Center.X)
            //                direction = 1;
            //            else
            //                direction = -1;
            //            int JumpDistance = 10 / 4;
            //            if (Main.expertMode)
            //                JumpDistance = 15 / 4;
            //            if (Main.masterMode)
            //                JumpDistance = 20 / 4;
            //            npc.velocity = new Vector2(JumpDistance * direction, -JumpDistance);
            //        }
            //        if (npc.velocity.Y == 0)
            //        {
            //            NumberOfJumps++;
            //            if (NumberOfJumps >= 3)
            //            {
            //                NumberOfJumps = 0;
            //                SkillTimer = 0;
            //                state = State.BigJump;
            //            }
            //            else
            //            {
            //                SkillTimer = 0;
            //                state = State.Wait;
            //            }
            //        }
            //        break;
            //    case State.BigJump:
            //        SkillTimer++;
            //        if (SkillTimer == 1)
            //        {
            //            Text("BigJump");
            //        }
            //        int BigJumpIntervalTime = 80;
            //        if (Main.expertMode)
            //            BigJumpIntervalTime = 50;
            //        if (Main.masterMode)
            //            BigJumpIntervalTime = 30;
            //        if (SkillTimer > BigJumpIntervalTime)
            //        {
            //            if (player.Center.X > npc.Center.X)
            //                direction = 1;
            //            else
            //                direction = -1;
            //            int BigJumpDistance = 15 / 4;
            //            if (Main.expertMode)
            //                BigJumpDistance = 20 / 4;
            //            if (Main.masterMode)
            //                BigJumpDistance = 25 / 4;
            //            npc.velocity = new Vector2(BigJumpDistance * direction, -BigJumpDistance * 2);
            //        }
            //        if (npc.velocity.Y == 0)
            //        {
            //            if (SkillTimer > 0)
            //            {
            //                npc.velocity = Vector2.Zero;
            //                SkillTimer = 0;
            //            }
            //            SkillTimer--;
            //            if (SkillTimer <= -30)
            //            {
            //                SkillTimer = 0;
            //                state = State.Teleport;
            //            }
            //        }
            //        break;
            //    case State.Teleport:
            //        SkillTimer++;
            //        if (SkillTimer == 1)
            //        {
            //            Text("Teleport");
            //        }
            //        TeleportReduceTime = 45;
            //        if (Main.expertMode)
            //            TeleportReduceTime = 30;
            //        if (Main.masterMode)
            //            TeleportReduceTime = 20;

            //        if (SkillTimer < TeleportReduceTime)
            //        {
            //            float CurrentReduceScale = SkillTimer / TeleportReduceTime;
            //            npc.scale = 1 - CurrentReduceScale * 0.5f;
            //            npc.width = (int)(98 * npc.scale);
            //            npc.width = (int)(92 * npc.scale);
            //        }
            //        else
            //        {
            //            if (SkillTimer == TeleportReduceTime)
            //            {
            //                npc.Center = TeleportPosition(npc, Main.player[npc.target]);
            //                if (player.Center.X > npc.Center.X)
            //                    direction = 1;
            //                else
            //                    direction = -1;
            //                npc.velocity.X = direction * 10;
            //            }
            //            if (npc.velocity.Y == 0)
            //            {
            //                SkillTimer = 0;
            //                state = State.Wait;
            //            }
            //        }
            //        break;
            //}
            #endregion

#if SetTextWrite
            Text(state);
#endif





            BossTime++;
            if (BossTime == 1)
            {
                CurrentSkill = 0;
                state = State.Jump;
                npc.TargetClosest(true);
            }
            //Main.NewText(state.ToString() + CurrentSkill);
            int ElseJump = (npc.lifeMax - npc.life) / npc.lifeMax * 5;
            switch (state)
            {
                case State.Jump:
                    BossTime2++;
                    if (BossTime2 == 1)
                        CurrentSkill++;
                    if (BossTime2 == 30)
                    {
                        int dx = (int)(player.Center.X - npc.Center.X);
                        int dy = (int)(player.Center.Y - npc.Center.Y);
                        if (Math.Abs(dx) > JumpMaxX)
                            dx = dx < 0 ? -JumpMaxX : JumpMaxX;
                        if (Math.Abs(dy) > JumpMaxY)
                            dy = JumpMaxY;
                        dy = (dy + JumpMaxY) / 2;
                        npc.velocity = new Vector2(dx, -JumpMaxY * 2 - ElseJump) / 30;
                    }

                    if (npc.velocity.Y == 0)
                    {
                        npc.velocity.X *= 0.9f;
                    }

                    if (BossTime2 > 40 && npc.velocity.Y == 0)
                    {
                        BossTime2 = 0;
                        if (CurrentSkill == 1 || CurrentSkill == 4)
                            state = State.Jump;
                        if (CurrentSkill == 2)
                            state = State.BigJump;
                        if (CurrentSkill == 5)
                            state = State.Teleport;
                    }
                    break;
                case State.BigJump:
                    BossTime2++;
                    if (BossTime2 == 1)
                        CurrentSkill++;
                    if (BossTime2 == 30)
                    {
                        int dx = (int)(player.Center.X - npc.Center.X);
                        int dy = (int)(player.Center.Y - npc.Center.Y);
                        if (Math.Abs(dx) > BigJumpMaxX)
                            dx = dx < 0 ? -BigJumpMaxX : BigJumpMaxX;
                        if (Math.Abs(dy) > JumpMaxY)
                            dy = JumpMaxY;
                        dy = (dy + JumpMaxY) / 2;
                        npc.velocity = new Vector2(dx, -JumpMaxY * 3 - ElseJump) / 30;
                    }

                    if (npc.velocity.Y == 0)
                    {
                        npc.velocity.X *= 0.9f;
                    }

                    if (BossTime2 > 40 && npc.velocity.Y == 0)
                    {
                        BossTime2 = 0;
                        if (CurrentSkill == 3)
                            state = State.Jump;
                        npc.TargetClosest(true);
                    }
                    break;
                case State.Teleport:
                    BossTime2++;
                    if (BossTime2 == 1)
                    {
                        CurrentSkill++;
                        BossTime3 = 0;
                    }
                    if (npc.velocity.Y == 0)
                    {
                        npc.velocity.X *= 0.9f;
                    }
                    if (BossTime2 >= 40&&BossTime2 <= 70)
                    {
                        BossTime3++;
                        npc.scale = npc.scale - (npc.scale - 0.75f) / 30 * BossTime3;
                    }
                    if (BossTime2 == 71)
                    {
                        npc.position = TeleportPosition(npc, player);
                    }

                    if (BossTime2 > 90)
                    {
                        BossTime2 = 0;
                        if (CurrentSkill == 6)
                            state = State.Jump;
                        CurrentSkill = 0;
                        npc.TargetClosest(true);
                    }
                    break;
            }
            npc.width = (int)(98f * npc.scale);
            npc.height = (int)(92f * npc.scale);
            npc.noTileCollide = npc.Center.Y < player.Center.Y && (player.Center.Y - npc.Center.Y) > 80;

            //if (Vector2.Distance(player.Center, npc.Center) > 3000)
            //{
            //    npc.TargetClosest(true);
            //    if (Vector2.Distance(player.Center, npc.Center) > 3000)
            //    {
            //        npc.active = false;
            //        //Main.NewText("它遁逃了......", Color.Purple);
            //    }
            //}
            if (!player.active && player.dead)
            {
                npc.TargetClosest(true);
                if (!player.active && player.dead)
                {
                    npc.active = false;
                    //Main.NewText("它失去了兴趣......", Color.Purple);
                }
            }
            return false;
        }

        public static void OverrideOnSpawn(NPC npc)
        {
            thisNPC = npc;
            BossTime = BossTime2 = BossTime3 = 0;
        }

        public static Vector2 TeleportPosition(NPC npc,Player player)
        {
            Vector2 v = player.Center;
            int TileCount = 30;
            if (Main.expertMode)
            {
                TileCount = 20;
            }
            if (Main.masterMode)
            {
                TileCount = 10;
            }
            int H = npc.height;
            if (player.Center.X > npc.Center.X)
            {
                v += new Vector2(-TileCount * 16, -H * 2); 
            }
            else
            {
                v += new Vector2(TileCount * 16, -H * 2);
            }
            return v;
        }









        public static void VanillaAI(NPC npc)
        {
            //
            //
            //
            //
            //npc.localAI[0]-有点用，看不懂
            //npc.localAI[1]-史莱姆王x坐标
            //npc.localAI[2]-史莱姆王y坐标
            //npc.localAI[3]-AI里没效果
            float num236 = 1f;
            float num237 = 1f;
            bool flag6 = false;
            bool flag7 = false;
            bool flag8 = false;
            float num238 = 2f;
            if (Main.getGoodWorld)
            {
                num238 -= 1f - (float)npc.life / (float)npc.lifeMax;
                num237 *= num238;
            }
            npc.aiAction = 0;
            if (npc.ai[3] == 0f && npc.life > 0)
            {
                npc.ai[3] = (float)npc.lifeMax;
            }
            if (npc.localAI[3] == 0f)
            {
                npc.localAI[3] = 1f;
                flag6 = true;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.ai[0] = -100f;
                    npc.TargetClosest(true);
                    npc.netUpdate = true;
                }
            }
            //传送
            int KingSlimeDistanceMax/*num239*/ = 3000;//史莱姆王距离上限
            if (Main.player[npc.target].dead || Vector2.Distance(npc.Center, Main.player[npc.target].Center) > (float)KingSlimeDistanceMax)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead || Vector2.Distance(npc.Center, Main.player[npc.target].Center) > (float)KingSlimeDistanceMax)
                {
                    npc.EncourageDespawn(10);
                    if (Main.player[npc.target].Center.X < npc.Center.X)
                    {
                        npc.direction = 1;
                    }
                    else
                    {
                        npc.direction = -1;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && npc.ai[1] != 5f)
                    {
                        npc.netUpdate = true;
                        npc.ai[2] = 0f;
                        npc.ai[0] = 0f;
                        npc.ai[1] = 5f;
                        npc.localAI[1] = (float)(Main.maxTilesX * 16);
                        npc.localAI[2] = (float)(Main.maxTilesY * 16);
                    }
                }
            }
            if (!Main.player[npc.target].dead && npc.timeLeft > 10 && npc.ai[2] >= 300f && npc.ai[1] < 5f && npc.velocity.Y == 0f)
            {
                npc.ai[2] = 0f;
                npc.ai[0] = 0f;
                npc.ai[1] = 5f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.TargetClosest(false);
                    Point point3 = npc.Center.ToTileCoordinates();
                    Point point4 = Main.player[npc.target].Center.ToTileCoordinates();
                    Vector2 vector30 = Main.player[npc.target].Center - npc.Center;
                    int num240 = 10;//检索范围数值 Retrieval range value
                    int num241 = 0;
                    int num242 = 7;
                    int num243 = 0;
                    bool flag9 = false;
                    if (npc.localAI[0] >= 360f || vector30.Length() > 2000f)
                    {
                        if (npc.localAI[0] >= 360f)
                        {
                            npc.localAI[0] = 360f;
                        }
                        flag9 = true;
                        num243 = 100;
                    }
                    while (!flag9 && num243 < 100)
                    {
                        num243++;
                        int num244 = Main.rand.Next(point4.X - num240, point4.X + num240 + 1);//玩家
                        int num245 = Main.rand.Next(point4.Y - num240, point4.Y + 1);//
                        if ((num245 < point4.Y - num242 || num245 > point4.Y + num242 || num244 < point4.X - num242 || num244 > point4.X + num242) && (num245 < point3.Y - num241 || num245 > point3.Y + num241 || num244 < point3.X - num241 || num244 > point3.X + num241) && !Main.tile[num244, num245].HasUnactuatedTile)
                        {
                            int num246 = num245;
                            int num247 = 0;
                            if (Main.tile[num244, num246].HasUnactuatedTile && Main.tileSolid[(int)(Main.tile[num244, num246].TileType)] && !Main.tileSolidTop[(int)(Main.tile[num244, num246].TileType)])
                            {
                                num247 = 1;
                            }
                            else
                            {
                                while (num247 < 150 && num246 + num247 < Main.maxTilesY)
                                {
                                    int num248 = num246 + num247;
                                    if (Main.tile[num244, num248].HasUnactuatedTile && Main.tileSolid[(int)(Main.tile[num244, num248].TileType)] && !Main.tileSolidTop[(int)(Main.tile[num244, num248].TileType)])
                                    {
                                        num247--;
                                        break;
                                    }
                                    num247--;
                                }
                            }
                            num245 += num247;
                            bool flag10 = true;
                            if (flag10 && (Main.tile[num244, num245].LiquidType == LiquidID.Lava))
                            {
                                flag10 = false;
                            }
                            if (flag10 && !Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
                            {
                                flag10 = false;
                            }
                            if (flag10)
                            {
                                npc.localAI[1] = (float)(num244 * 16 + 8);
                                npc.localAI[2] = (float)(num245 * 16 + 16);
                                break;
                            }
                        }
                    }
                    if (num243 >= 100)
                    {
                        Vector2 bottom = Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].Bottom;
                        npc.localAI[1] = bottom.X;
                        npc.localAI[2] = bottom.Y;
                    }
                }
            }
            if (!Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0) || Math.Abs(npc.Top.Y - Main.player[npc.target].Bottom.Y) > 160f)
            {
                ref float ptr = ref npc.ai[2];
                float num1599 = ptr;
                ptr = num1599 + 1f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    ptr = ref npc.localAI[0];
                    ref float ptr3 = ref ptr;
                    num1599 = ptr;
                    ptr3 = num1599 + 1f;
                }
            }
            else if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                ref float ptr = ref npc.localAI[0];
                ref float ptr4 = ref ptr;
                float num1599 = ptr;
                ptr4 = num1599 - 1f;
                if (npc.localAI[0] < 0f)
                {
                    npc.localAI[0] = 0f;
                }
            }
            if (npc.timeLeft < 10 && (npc.ai[0] != 0f || npc.ai[1] != 0f))
            {
                npc.ai[0] = 0f;
                npc.ai[1] = 0f;
                npc.netUpdate = true;
                flag7 = false;
            }
            Dust dust87;
            Dust dust26;
            if (npc.ai[1] == 5f)
            {
                flag7 = true;
                npc.aiAction = 1;
                ref float ptr = ref npc.ai[0];
                ref float ptr5 = ref ptr;
                float num1599 = ptr;
                ptr5 = num1599 + 1f;
                num236 = MathHelper.Clamp((60f - npc.ai[0]) / 60f, 0f, 1f);
                num236 = 0.5f + num236 * 0.5f;
                if (npc.ai[0] >= 60f)
                {
                    flag8 = true;
                }
                if (npc.ai[0] == 60f)
                {
                    Gore.NewGore(npc.GetSource_FromThis(), npc.Center + new Vector2(-40f, (float)(-(float)npc.height / 2)), npc.velocity, 734, 1f);
                }
                if (npc.ai[0] >= 60f && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.Bottom = new Vector2(npc.localAI[1], npc.localAI[2]);
                    npc.ai[1] = 6f;
                    npc.ai[0] = 0f;
                    npc.netUpdate = true;
                }
                if (Main.netMode == NetmodeID.MultiplayerClient && npc.ai[0] >= 120f)
                {
                    npc.ai[1] = 6f;
                    npc.ai[0] = 0f;
                }
                if (!flag8)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        int num250 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, DustID.TintableDust, npc.velocity.X, npc.velocity.Y, 150, new Color(78, 136, 255, 80), 2f);
                        Main.dust[num250].noGravity = true;
                        dust26 = Main.dust[num250];
                        dust87 = dust26;
                        dust87.velocity *= 0.5f;
                    }
                }
            }
            else if (npc.ai[1] == 6f)
            {
                flag7 = true;
                npc.aiAction = 0;
                ref float ptr = ref npc.ai[0];
                ref float ptr6 = ref ptr;
                float num1599 = ptr;
                ptr6 = num1599 + 1f;
                num236 = MathHelper.Clamp(npc.ai[0] / 30f, 0f, 1f);
                num236 = 0.5f + num236 * 0.5f;
                if (npc.ai[0] >= 30f && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.ai[1] = 0f;
                    npc.ai[0] = 0f;
                    npc.netUpdate = true;
                    npc.TargetClosest(true);
                }
                if (Main.netMode == NetmodeID.MultiplayerClient && npc.ai[0] >= 60f)
                {
                    npc.ai[1] = 0f;
                    npc.ai[0] = 0f;
                    npc.TargetClosest(true);
                }
                for (int i = 0; i < 10; i++)
                {
                    int num252 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, DustID.TintableDust, npc.velocity.X, npc.velocity.Y, 150, new Color(78, 136, 255, 80), 2f);
                    Main.dust[num252].noGravity = true;
                    dust26 = Main.dust[num252];
                    dust87 = dust26;
                    dust87.velocity *= 2f;
                }
            }
            npc.dontTakeDamage = (npc.hide = flag8);
            if (npc.velocity.Y == 0f)
            {
                ref float ptr = ref npc.velocity.X;
                ptr *= 0.8f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if (!flag7)
                {
                    ptr = ref npc.ai[0];
                    ptr += 2f;
                    if ((double)npc.life < (double)npc.lifeMax * 0.8)
                    {
                        ptr = ref npc.ai[0];
                        ptr += 1f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.6)
                    {
                        ptr = ref npc.ai[0];
                        ptr += 1f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.4)
                    {
                        ptr = ref npc.ai[0];
                        ptr += 2f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.2)
                    {
                        ptr = ref npc.ai[0];
                        ptr += 3f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.1)
                    {
                        ptr = ref npc.ai[0];
                        ptr += 4f;
                    }
                    if (npc.ai[0] >= 0f)
                    {
                        npc.netUpdate = true;
                        npc.TargetClosest(true);
                        if (npc.ai[1] == 3f)
                        {
                            npc.velocity.Y = -13f;
                            ptr = ref npc.velocity.X;
                            ptr += 3.5f * (float)npc.direction;
                            npc.ai[0] = -200f;
                            npc.ai[1] = 0f;
                        }
                        else if (npc.ai[1] == 2f)
                        {
                            npc.velocity.Y = -6f;
                            ptr = ref npc.velocity.X;
                            ptr += 4.5f * (float)npc.direction;
                            npc.ai[0] = -120f;
                            ptr = ref npc.ai[1];
                            ptr += 1f;
                        }
                        else
                        {
                            npc.velocity.Y = -8f;
                            ptr = ref npc.velocity.X;
                            ptr += 4f * (float)npc.direction;
                            npc.ai[0] = -120f;
                            ptr = ref npc.ai[1];
                            ptr += 1f;
                        }
                    }
                    else if (npc.ai[0] >= -30f)
                    {
                        npc.aiAction = 1;
                    }
                }
            }
            else if (npc.target < 255)
            {
                float num253 = 3f;
                if (Main.getGoodWorld)
                {
                    num253 = 6f;
                }
                if ((npc.direction == 1 && npc.velocity.X < num253) || (npc.direction == -1 && npc.velocity.X > 0f - num253))
                {
                    if ((npc.direction == -1 && (double)npc.velocity.X < 0.1) || (npc.direction == 1 && (double)npc.velocity.X > -0.1))
                    {
                        npc.velocity.X += 0.2f * (float)npc.direction;
                    }
                    else
                    {
                        npc.velocity.X *= 0.93f;
                    }
                }
            }
            int num254 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.TintableDust, npc.velocity.X, npc.velocity.Y, 255, new Color(0, 80, 255, 80), npc.scale * 1.2f);
            Main.dust[num254].noGravity = true;
            dust26 = Main.dust[num254];
            dust87 = dust26;
            dust87.velocity *= 0.5f;
            if (npc.life <= 0)
            {
                return;
            }
            float num255 = (float)npc.life / (float)npc.lifeMax;
            num255 = num255 * 0.5f + 0.75f;
            num255 *= num236;
            num255 *= num237;
            if (num255 != npc.scale || flag6)
            {
                ref float ptr = ref npc.position.X;
                ptr += (float)(npc.width / 2);
                ptr = ref npc.position.Y;
                ptr += (float)npc.height;
                npc.scale = num255;
                npc.width = (int)(98f * npc.scale);
                npc.height = (int)(92f * npc.scale);
                ptr = ref npc.position.X;
                ptr -= (float)(npc.width / 2);
                ptr = ref npc.position.Y;
                ptr -= (float)npc.height;
            }
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }
            int num256 = (int)((double)npc.lifeMax * 0.05);
            if ((float)(npc.life + num256) >= npc.ai[3])
            {
                return;
            }
            npc.ai[3] = (float)npc.life;
            int num257 = Main.rand.Next(1, 4);
            for (int i = 0; i < num257; i++)
            {
                int x = (int)(npc.position.X + (float)Main.rand.Next(npc.width - 32));
                int y = (int)(npc.position.Y + (float)Main.rand.Next(npc.height - 32));
                int num259 = 1;
                if (Main.expertMode && Main.rand.Next(4) == 0)
                {
                    num259 = 535;
                }
                int num260 = NPC.NewNPC(npc.GetSource_FromThis(), x, y, num259, 0, 0f, 0f, 0f, 0f, 255);
                Main.npc[num260].SetDefaults(num259, default(NPCSpawnParams));
                Main.npc[num260].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
                Main.npc[num260].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
                Main.npc[num260].ai[0] = (float)(-1000 * Main.rand.Next(3));
                Main.npc[num260].ai[1] = 0f;
                if (Main.netMode != NetmodeID.Server || num260 >= 200)
                {
                }
                else
                {
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num260, 0f, 0f, 0f, 0, 0, 0);
                }
            }
            return;
        }
    }
}
