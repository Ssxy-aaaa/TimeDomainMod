using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace yourmod.Common.Globals.VanillaNPCAIOverrides
{
    public static class BrainofCthulhuAI
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




        static float BrainofCthulhu_range = 0;
        static Vector2 BrainofCthulhu_RO;
        static Vector2 vcr = Vector2.One;
        static float BrainofCthulhu_rotation;
        static Vector2 BrainofCthulhu_origins;
        static Vector2 BrainofCthulhu_circles;
        public static void BuffedAI(NPC npc)
        {
            thisNPC = npc;
            BossTime3++;
            Player p = Main.player[npc.target];
            BerserkMode = !p.ZoneCrimson;
            if (BossTime3 == 1)
            {
                npc.TargetClosest(true);
                npc.Center = Main.player[npc.target].Center - new Vector2(0, 600);
                int brainOfCthuluCreepersCount = NPC.GetBrainOfCthuluCreepersCount();
                for (int i = 0; i < brainOfCthuluCreepersCount; i++)
                {
                    int num820 = NPC.NewNPC(npc.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, NPCID.Creeper, 0, 0f, 0f, 0f, npc.whoAmI, 255);
                    Main.npc[num820].netUpdate = true;
                }
            }
            if (BerserkMode)
            {
                if (Main.expertMode)
                {
                    npc.damage = 80;
                    npc.defense = 0;
                }
                if (Main.masterMode)
                {
                    npc.damage = 140;
                    npc.defense = 0;
                }
                if (Main.getGoodWorld && Main.masterMode)
                {
                    npc.damage = 220;
                    npc.defense = 0;
                }
            }
            else
            {
                if (Main.expertMode)
                {
                    npc.damage = 65;
                    npc.defense = 0;
                }
                if (Main.masterMode)
                {
                    npc.damage = 115;
                    npc.defense = 0;
                }
                if (Main.getGoodWorld && Main.masterMode)
                {
                    npc.damage = 175;
                    npc.defense = 0;
                }
                //npc.damage = 45;
                //npc.defense = 5;
            }
            npc.ai[1]++;
            NPC.crimsonBoss = npc.whoAmI;
            ///test
            //if (Main.netMode != NetmodeID.MultiplayerClient && npc.localAI[0] == 0f)
            //{
            //    npc.localAI[0] = 1f;
            //}
            ///
            //脱战
            ScreenPositionModifyPlayer modifyPlayer = p.GetModPlayer<ScreenPositionModifyPlayer>();
            if (BossTime3 <= 180)
            {
                Vector2 HalfScreen = new Vector2(Main.screenWidth, Main.screenHeight) / 2;
                if (BossTime3 < 120)
                {
                    if (BossTime3 == 20)
                        SoundEngine.PlaySound(SoundID.Roar, npc.Center);
                    if (BossTime3 == 115)
                        SoundEngine.PlaySound(SoundID.ForceRoar, npc.Center);

                    npc.velocity = BossTime3 < 90 ? new Vector2(0, 7) : Vector2.Zero;
                    modifyPlayer.IsModifyScreenPosition = true;
                    modifyPlayer.Target = npc.Center - HalfScreen;
                }
                else
                {
                    Vector2 ToPlayerFromNPC = p.Center - npc.Center;
                    float b = (BossTime3 - 120) / 60;
                    Vector2 CurScreenPos = (npc.Center + ToPlayerFromNPC * b) - HalfScreen;
                    
                    modifyPlayer.IsModifyScreenPosition = true;
                    modifyPlayer.Target = CurScreenPos;
                }
            }
            else
            {
                modifyPlayer.IsModifyScreenPosition = false;
                modifyPlayer.Target = Vector2.Zero;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.TargetClosest(true);
                //int num821 = 6000;
                //if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num821)
                //{
                //    npc.active = false;
                //    npc.life = 0;
                //    if (Main.netMode == NetmodeID.Server)
                //    {
                //        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                //    }
                //}
            }

            if (npc.ai[0] < 0)
            {
                if (Main.getGoodWorld)
                    NPC.brainOfGravity = npc.whoAmI;
                npc.dontTakeDamage = false;
                if (npc.ai[0] == -1)
                {
                    npc.dontTakeDamage = false;
                }
                if (npc.ai[0] == -1)
                {
                    BossTime2++;
                    if (BossTime2 == 1)
                    {
                        BrainofCthulhu_range = 360;
                    }
                    if (BossTime2 == 10)
                    {
                        //SoundEngine.PlaySound(SoundID.NPCHit3, npc.Center);
                        SoundEngine.PlaySound(SoundID.Roar, npc.Center);
                    }
                    if (BossTime2 < 180)
                    {
                        //还好吧，我只是时间比较少
                        BrainofCthulhu_range = 360 - 2 * BossTime2;
                        //Text(BrainofCthulhu_range);
                        //Text(BossTime2);
                        float RSpeed = 0.07f;
                        if (BerserkMode)
                            RSpeed = 0.12f;
                        BrainofCthulhu_rotation = BossTime3 * RSpeed;
                        if (BossTime2 < 101)
                            BrainofCthulhu_RO = p.Center;

                        BrainofCthulhu_origins = BrainofCthulhu_RO;
                        BrainofCthulhu_circles = BrainofCthulhu_rotation.ToRotationVector2() * BrainofCthulhu_range;
                        BrainofCthulhu_origins += BrainofCthulhu_circles;
                        npc.velocity = BrainofCthulhu_origins - npc.Center;
                    }
                    #region 旧冲刺
                    /*
                    if (BossTime2 > 180 && BossTime2 <= 183)
                    {
                        npc.velocity = -Vector2.Normalize(p.Center - npc.Center) * 10;
                    }
                    //
                    if (BossTime2 > 210 && BossTime2 <= 300)
                    {
                        if (BrainofCthulhu_JGCenter == new Vector2(-10, -10))
                        {
                            BrainofCthulhu_JGCenter = p.Center;
                            Vector2 v = BrainofCthulhu_JGCenter - npc.Center;
                            v *= 1.2f;
                            //v.Normalize();
                            float val = (v.Length() / 90) / (float)Math.Sqrt((v.X * v.X) + (v.Y * v.Y));
                            v.X *= val;
                            v.Y *= val;
                            //v = v.Length() / 90;
                            v *= 3;
                            npc.velocity = v;
                        }
                        if (BossTime2 > 270)
                        {
                            npc.velocity *= 0.97f;
                        }
                    }
                    if (BossTime2 > 300&& BossTime2<390)
                    {
                        if (BossTime2 == 301)
                        {
                            float VR = MathHelper.Pi / 4;
                            Vector2 v2 = VR.ToRotationVector2();
                            npc.velocity += v2;
                            npc.velocity = -npc.velocity;
                        }
                    }
                    */
                    #endregion
                    if (BossTime2 > 180 && BossTime2 < 390)
                    {
                        npc.ai[3] = 1;
                        if (BossTime2 == 181)
                        {
                            int rand = Main.rand.Next(1, 4);
                            if (rand == 1) { vcr = new Vector2(0, -1); }
                            if (rand == 2) { vcr = new Vector2(1, 0); }
                            if (rand == 3) { vcr = new Vector2(0, 1); }
                            if (rand == 4) { vcr = new Vector2(-1, 0); }

                            vcr *= 800;
                            npc.Center = p.Center + vcr;
                            npc.velocity = -vcr / 4000;
                        }
                        if (BossTime2 == 185)
                        {
                            npc.velocity *= 100f;
                            SoundEngine.PlaySound(SoundID.Roar, npc.Center);
                        }
                        if (BossTime2 > 181 && BossTime2 < 200)
                        {
                            npc.velocity *= 1.01f;
                        }
                        if (BossTime2 > 200 && BossTime2 < 230)
                        {
                            npc.velocity *= 0.97f;
                        }
                        if (BossTime2 == 230)
                        {
                            int rand = Main.rand.Next(1, 4);
                            if (rand == 1) { vcr = new Vector2(-1, -1); }
                            if (rand == 2) { vcr = new Vector2(1, -1); }
                            if (rand == 3) { vcr = new Vector2(1, 1); }
                            if (rand == 4) { vcr = new Vector2(-1, 1); }

                            vcr *= 100;
                            npc.velocity = vcr / 1000;
                            SoundEngine.PlaySound(SoundID.Roar, npc.Center);
                        }
                        if (BossTime2 == 230)
                        {
                            if (BerserkMode)
                            {
                                npc.velocity *= 1.2f;
                            }
                            npc.velocity *= 100f;
                        }
                        if (BossTime2 > 250 && BossTime2 < 260)
                        {
                            if (BerserkMode)
                            {
                                npc.velocity *= 1.01f;
                            }
                            npc.velocity *= 1.01f;
                        }
                        if (BossTime2 > 260 && BossTime2 < 270)
                        {
                            if (BerserkMode)
                            {
                                npc.velocity *= 0.99f;
                            }
                            npc.velocity *= 0.97f;
                        }
                    }
                    else
                    {
                        npc.ai[3] = 0;
                    }
                    if (BossTime2 == 270)
                    {
                        npc.ai[0] = -2;
                        BossTime2 = 0;
                        Vector2 velocity = p.Center - npc.Center;
                        if (BerserkMode)
                        {
                            if (Main.masterMode)
                            {
                                if (Main.getGoodWorld)
                                {
                                    float Angle = MathHelper.Pi / 20;
                                    for (float r = -Angle * 4; r < Angle * 5; r += Angle)
                                    {
                                        float r2 = r + velocity.ToRotation();
                                        Vector2 v = new Vector2((float)Math.Cos(r2), (float)Math.Sin(r2)) * 12f;
                                        Projectile proj = Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, v, ProjectileID.BloodNautilusShot, npc.damage / 5, 0);
                                        proj.tileCollide = false;
                                    }
                                }
                                else
                                {
                                    float Angle = MathHelper.Pi / 18;
                                    for (float r = -Angle * 3; r < Angle * 4; r += Angle)
                                    {
                                        float r2 = r + velocity.ToRotation();
                                        Vector2 v = new Vector2((float)Math.Cos(r2), (float)Math.Sin(r2)) * 12f;
                                        Projectile proj = Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, v, ProjectileID.BloodNautilusShot, npc.damage / 5, 0);
                                        proj.tileCollide = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            float Angle = MathHelper.Pi / 18;
                            for (float r = -Angle * 2; r < Angle * 4; r += Angle)
                            {
                                float r2 = r + velocity.ToRotation();
                                Vector2 v = new Vector2((float)Math.Cos(r2), (float)Math.Sin(r2)) * 10f;
                                Projectile proj = Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, v, ProjectileID.BloodNautilusShot, npc.damage / 6, 0);
                                proj.tileCollide = false;
                            }
                        }
                        SoundEngine.PlaySound(SoundID.Roar, npc.Center);
                    }
                }
                if (npc.ai[0] == -2)
                {
                    npc.alpha += 5;
                    if (npc.alpha >= 255)
                    {
                        npc.alpha = 255;
                        int a = 0;
                        if (Main.rand.Next(0, 1) == 0)
                        { a = 1; }
                        else
                        { a = -1; }
                        npc.position = Main.player[npc.target].Center + new Vector2(Main.rand.Next(-20, 20), 20 * a) + new Vector2(npc.width / 2, npc.height / 2);
                        npc.ai[0] = -3;
                    }
                    SoundEngine.PlaySound(SoundID.Item8, npc.Center);
                }
                if (npc.ai[0] == -3)
                {
                    npc.alpha -= 5;
                    if (npc.alpha <= 0)
                    {
                        npc.ai[0] = -1;
                    }
                    SoundEngine.PlaySound(SoundID.Item8, npc.Center);
                }
            }
            else
            {
                //一阶段
                if (npc.ai[0] == 0 && BossTime3 >= 180)
                {
                    //转二阶段
                    //int CreeperCount = 0;
                    //for (int i = 0; i < Main.maxNPCs; i++)
                    //{
                    //    if (Main.npc[i].type == NPCID.Creeper)
                    //    {
                    //        if (npc.active)
                    //        {
                    //            CreeperCount++;
                    //        }
                    //    }
                    //}
                    //if (CreeperCount == 0)
                    //{
                    //    npc.ai[0] = 1;
                    //}
                    bool Phase2 = false;
                    int CreeperCount = 0;
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        if (Main.npc[i].active && Main.npc[i].type == NPCID.Creeper && !Main.npc[i].dontTakeDamage)
                        {
                            CreeperCount++;
                        }
                    }
                    Phase2 = CreeperCount == 0;
                    if (Phase2)
                    {
                        npc.ai[0] = -1f;
                        ///test
                        //npc.localAI[1] = 0f;
                        ///
                        npc.alpha = 0;
                        npc.netUpdate = true;
                    }


                    BossTime++;
                    float num835 = Main.player[npc.target].Center.X - npc.Center.X;
                    float num836 = Main.player[npc.target].Center.Y - npc.Center.Y;
                    float num837 = (float)Math.Sqrt((double)(num835 * num835 + num836 * num836));
                    float num838 = 1f;
                    if (Main.getGoodWorld)
                    {
                        num838 *= 1.33f;
                    }
                    if (BerserkMode)
                    {
                        num838 *= 1.2f;
                    }
                    num838 *= 3;
                    if (num837 < num838)
                    {
                        npc.velocity.X = num835;
                        npc.velocity.Y = num836;
                    }
                    else
                    {
                        num837 = num838 / num837;
                        npc.velocity.X = num835 * num837;
                        npc.velocity.Y = num836 * num837;
                    }
                    if (BossTime < 40)
                    {
                        npc.velocity *= 3;
                    }
                    if (BossTime % 240 == 0)
                    {
                        BossTime = 0;
                        npc.ai[0] = 1;
                    }
                }
                if (npc.ai[0] == 1)
                {
                    BossTime--;
                    npc.alpha = -5 * (int)BossTime; ;
                    if (BossTime <= -51)
                    {
                        int a = Main.rand.Next(2) == 0 ? 1 : -1;
                        npc.position = Main.player[npc.target].Center + new Vector2(Main.rand.Next(-20, 20), 20 * a) + new Vector2(npc.width / 2, npc.height / 2);
                        npc.ai[0] = 2;
                        BossTime = 0;
                    }
                }
                if (npc.ai[0] == 2)
                {
                    npc.alpha -= 5;
                    if (npc.alpha <= 0)
                    {
                        npc.alpha = 0;
                        npc.ai[0] = 0;
                    }
                }
            }
            if (BerserkMode)
            {
                npc.ai[2] = 1;
            }
            else
            {
                npc.ai[2] = 0;
            }
        }
        public static void OverrideOnSpawn(NPC npc)
        {
            thisNPC = npc;
            BossTime = BossTime2 = BossTime3 = 0;
        }
        #region VanillaAI
        
        //public override void VanillaAI(NPC npc)
        //{
        //    NPC.crimsonBoss = npc.whoAmI;
        //    float a = 0;
        //    ref float ptr = ref a;
        //    if (Main.netMode != 1 && npc.localAI[0] == 0f)
        //    {
        //        npc.localAI[0] = 1f;
        //        int brainOfCthuluCreepersCount = NPC.GetBrainOfCthuluCreepersCount();
        //        int num1598;
        //        for (int num819 = 0; num819 < brainOfCthuluCreepersCount; num819 = num1598 + 1)
        //        {
        //            float x2 = npc.Center.X;
        //            float y4 = npc.Center.Y;
        //            x2 += (float)Main.rand.Next(-npc.width, npc.width);
        //            y4 += (float)Main.rand.Next(-npc.height, npc.height);
        //            int num820 = NPC.NewNPC(npc.GetSource_FromThis(), (int)x2, (int)y4, 267, 0, 0f, 0f, 0f, 0f, 255);
        //            Main.npc[num820].velocity = new Vector2((float)Main.rand.Next(-30, 31) * 0.1f, (float)Main.rand.Next(-30, 31) * 0.1f);
        //            Main.npc[num820].netUpdate = true;
        //            num1598 = num819;
        //        }
        //    }
        //    if (Main.netMode != 1)
        //    {
        //        npc.TargetClosest(true);
        //        int num821 = 6000;
        //        if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num821)
        //        {
        //            npc.active = false;
        //            npc.life = 0;
        //            if (Main.netMode == 2)
        //            {
        //                NetMessage.SendData(23, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
        //            }
        //        }
        //    }
        //    if (npc.ai[0] < 0f)
        //    {
        //        if (Main.getGoodWorld)
        //        {
        //            NPC.brainOfGravity = npc.whoAmI;
        //        }
        //        if (npc.localAI[2] == 0f)
        //        {
        //            SoundEngine.PlaySound(SoundID.NPCHit1, npc.position);
        //            npc.localAI[2] = 1f;
        //            Gore.NewGore(npc.GetSource_FromThis(), npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 392, 1f);
        //            Gore.NewGore(npc.GetSource_FromThis(), npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 393, 1f);
        //            Gore.NewGore(npc.GetSource_FromThis(), npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 394, 1f);
        //            Gore.NewGore(npc.GetSource_FromThis(), npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 395, 1f);
        //            int num1598;
        //            for (int num822 = 0; num822 < 20; num822 = num1598 + 1)
        //            {
        //                Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
        //                num1598 = num822;
        //            }
        //            SoundEngine.PlaySound(SoundID.Roar, npc.position);
        //        }
        //        npc.dontTakeDamage = false;
        //        npc.TargetClosest(true);
        //        float xt = Main.player[npc.target].Center.X - npc.Center.X;
        //        float yt = Main.player[npc.target].Center.Y - npc.Center.Y;
        //        float c = (float)Math.Sqrt((double)(xt * xt + yt * yt));
        //        float num826 = 8f;
        //        c = num826 / c;
        //        xt *= c;
        //        yt *= c;
        //        npc.velocity.X = (npc.velocity.X * 50f + xt) / 51f;
        //        npc.velocity.Y = (npc.velocity.Y * 50f + yt) / 51f;
        //        if (npc.ai[0] == -1f)
        //        {
        //            if (Main.netMode != 1)
        //            {
        //                ptr = ref npc.localAI[1];
        //                ptr += 1f;
        //                if (npc.justHit)
        //                {
        //                    ptr = ref npc.localAI[1];
        //                    ptr -= (float)Main.rand.Next(5);
        //                }
        //                int num827 = 60 + Main.rand.Next(120);
        //                if (Main.netMode != 0)
        //                {
        //                    num827 += Main.rand.Next(30, 90);
        //                }
        //                if (npc.localAI[1] >= (float)num827)
        //                {
        //                    npc.localAI[1] = 0f;
        //                    npc.TargetClosest(true);
        //                    int num828 = 0;
        //                    Player player2 = Main.player[npc.target];
        //                    int num829;
        //                    int num830;
        //                    for (; ; )
        //                    {
        //                        int num1598 = num828;
        //                        num828 = num1598 + 1;
        //                        num829 = (int)player2.Center.X / 16;
        //                        num830 = (int)player2.Center.Y / 16;
        //                        int minValue = 10;
        //                        int num831 = 12;
        //                        float num832 = 16f;
        //                        int num833 = Main.rand.Next(minValue, num831 + 1);
        //                        int num834 = Main.rand.Next(minValue, num831 + 1);
        //                        if (Main.rand.Next(2) == 0)
        //                        {
        //                            num833 *= -1;
        //                        }
        //                        if (Main.rand.Next(2) == 0)
        //                        {
        //                            num834 *= -1;
        //                        }
        //                        Vector2 v;
        //                        v = new Vector2((float)(num833 * 16), (float)(num834 * 16));
        //                        if (Vector2.Dot(player2.velocity.SafeNormalize(Vector2.UnitY), v.SafeNormalize(Vector2.UnitY)) > 0f)
        //                        {
        //                            v += v.SafeNormalize(Vector2.Zero) * num832 * player2.velocity.Length();
        //                        }
        //                        num829 += (int)(v.X / 16f);
        //                        num830 += (int)(v.Y / 16f);
        //                        if (num828 > 100 || !WorldGen.SolidTile(num829, num830, false))
        //                        {
        //                            break;
        //                        }
        //                        if (num828 > 100)
        //                        {
        //                            goto Block_2939;
        //                        }
        //                    }
        //                    npc.ai[3] = 0f;
        //                    npc.ai[0] = -2f;
        //                    npc.ai[1] = (float)num829;
        //                    npc.ai[2] = (float)num830;
        //                    npc.netUpdate = true;
        //                    npc.netSpam = 0;
        //                Block_2939:;
        //                }
        //            }
        //        }
        //        else if (npc.ai[0] == -2f)
        //        {
        //            npc.velocity *= 0.9f;
        //            if (Main.netMode != 0)
        //            {
        //                ptr = ref npc.ai[3];
        //                ptr += 15f;
        //            }
        //            else
        //            {
        //                ptr = ref npc.ai[3];
        //                ptr += 25f;
        //            }
        //            if (npc.ai[3] >= 255f)
        //            {
        //                npc.ai[3] = 255f;
        //                npc.position.X = npc.ai[1] * 16f - (float)(npc.width / 2);
        //                npc.position.Y = npc.ai[2] * 16f - (float)(npc.height / 2);
        //                SoundEngine.PlaySound(SoundID.Item8, new Vector2?(npc.Center), null);
        //                npc.ai[0] = -3f;
        //                npc.netUpdate = true;
        //                npc.netSpam = 0;
        //            }
        //            npc.alpha = (int)npc.ai[3];
        //        }
        //        else if (npc.ai[0] == -3f)
        //        {
        //            if (Main.netMode != 0)
        //            {
        //                ptr = ref npc.ai[3];
        //                ptr -= 15f;
        //            }
        //            else
        //            {
        //                ptr = ref npc.ai[3];
        //                ptr -= 25f;
        //            }
        //            if (npc.ai[3] <= 0f)
        //            {
        //                npc.ai[3] = 0f;
        //                npc.ai[0] = -1f;
        //                npc.netUpdate = true;
        //                npc.netSpam = 0;
        //            }
        //            npc.alpha = (int)npc.ai[3];
        //        }
        //    }
        //    else
        //    {
        //        npc.TargetClosest(true);
        //        float xt = Main.player[npc.target].Center.X - npc.Center.X;
        //        float yt = Main.player[npc.target].Center.Y - npc.Center.Y;
        //        float c = (float)Math.Sqrt((double)(xt * xt + yt * yt));
        //        float num838 = 1f;
        //        if (Main.getGoodWorld)
        //        {
        //            num838 *= 3f;
        //        }
        //        if (c < num838)
        //        {
        //            npc.velocity.X = xt;
        //            npc.velocity.Y = yt;
        //        }
        //        else
        //        {
        //            c = num838 / c;
        //            npc.velocity.X = xt * c;
        //            npc.velocity.Y = yt * c;
        //        }
        //        if (npc.ai[0] == 0f)
        //        {
        //            if (Main.netMode != 1)
        //            {
        //                int num839 = 0;
        //                int num1598;
        //                for (int i = 0; i < 200; i = num1598 + 1)
        //                {
        //                    if (Main.npc[i].active && Main.npc[i].type == 267)
        //                    {
        //                        num1598 = num839;
        //                        num839 = num1598 + 1;
        //                    }
        //                    num1598 = i;
        //                }
        //                if (num839 == 0)
        //                {
        //                    npc.ai[0] = -1f;
        //                    npc.localAI[1] = 0f;
        //                    npc.alpha = 0;
        //                    npc.netUpdate = true;
        //                }
        //                ptr = ref npc.localAI[1];
        //                ptr += 1f;
        //                if (npc.localAI[1] >= (float)(120 + Main.rand.Next(300)))
        //                {
        //                    npc.localAI[1] = 0f;
        //                    npc.TargetClosest(true);
        //                    int num841 = 0;
        //                    Player player3 = Main.player[npc.target];
        //                    int num842;
        //                    int num843;
        //                    for (; ; )
        //                    {
        //                        num1598 = num841;
        //                        num841 = num1598 + 1;
        //                        num842 = (int)player3.Center.X / 16;
        //                        num843 = (int)player3.Center.Y / 16;
        //                        int minValue2 = 12;
        //                        int num844 = 40;
        //                        float num845 = 16f;
        //                        int num846 = Main.rand.Next(minValue2, num844 + 1);
        //                        int num847 = Main.rand.Next(minValue2, num844 + 1);
        //                        if (Main.rand.Next(2) == 0)
        //                        {
        //                            num846 *= -1;
        //                        }
        //                        if (Main.rand.Next(2) == 0)
        //                        {
        //                            num847 *= -1;
        //                        }
        //                        Vector2 v2;
        //                        v2 = new((float)(num846 * 16), (float)(num847 * 16));
        //                        if (Vector2.Dot(player3.velocity.SafeNormalize(Vector2.UnitY), v2.SafeNormalize(Vector2.UnitY)) > 0f)
        //                        {
        //                            v2 += v2.SafeNormalize(Vector2.Zero) * num845 * player3.velocity.Length();
        //                        }
        //                        num842 += (int)(v2.X / 16f);
        //                        num843 += (int)(v2.Y / 16f);
        //                        if (num841 > 100 || (!WorldGen.SolidTile(num842, num843, false) && (num841 > 75 || Collision.CanHit(new Vector2((float)(num842 * 16), (float)(num843 * 16)), 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))))
        //                        {
        //                            break;
        //                        }
        //                        if (num841 > 100)
        //                        {
        //                            goto Block_2961;
        //                        }
        //                    }
        //                    npc.ai[0] = 1f;
        //                    npc.ai[1] = (float)num842;
        //                    npc.ai[2] = (float)num843;
        //                    npc.netUpdate = true;
        //                Block_2961:;
        //                }
        //            }
        //        }
        //        else if (npc.ai[0] == 1f)
        //        {
        //            npc.alpha += 5;
        //            if (npc.alpha >= 255)
        //            {
        //                SoundEngine.PlaySound(SoundID.Item8, new Vector2?(npc.Center), null);
        //                npc.alpha = 255;
        //                npc.position.X = npc.ai[1] * 16f - (float)(npc.width / 2);
        //                npc.position.Y = npc.ai[2] * 16f - (float)(npc.height / 2);
        //                npc.ai[0] = 2f;
        //            }
        //        }
        //        else if (npc.ai[0] == 2f)
        //        {
        //            npc.alpha -= 5;
        //            if (npc.alpha <= 0)
        //            {
        //                npc.alpha = 0;
        //                npc.ai[0] = 0f;
        //            }
        //        }
        //    }
        //    if (Main.player[npc.target].dead || !Main.player[npc.target].ZoneCrimson)
        //    {
        //        if (npc.localAI[3] < 120f)
        //        {
        //            ptr = ref npc.localAI[3];
        //            ref float ptr10 = ref ptr;
        //            float num1599 = ptr;
        //            ptr10 = num1599 + 1f;
        //        }
        //        if (npc.localAI[3] > 60f)
        //        {
        //            ptr = ref npc.velocity.Y;
        //            ptr += (npc.localAI[3] - 60f) * 0.25f;
        //        }
        //        npc.ai[0] = 2f;
        //        npc.alpha = 10;
        //        return;
        //    }
        //    if (npc.localAI[3] > 0f)
        //    {
        //        ptr = ref npc.localAI[3];
        //        ref float ptr11 = ref ptr;
        //        float num1599 = ptr;
        //        ptr11 = num1599 - 1f;
        //        return;
        //    }
        //}
        #endregion
    }
    public class ScreenPositionModifyPlayer : ModPlayer
    {
        public  Vector2 Target = Vector2.Zero;
        public  bool IsModifyScreenPosition = false;
        public override void ModifyScreenPosition()
        {
            if (IsModifyScreenPosition && Target != Vector2.Zero)
                Main.screenPosition = Target;
            else
                Target = Vector2.Zero;
        }
    }
}