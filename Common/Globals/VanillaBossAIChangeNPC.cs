using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Common.Globals.VanillaNPCAIOverrides;

namespace TimeDomain.Common.Globals
{
    public class VanillaBossAIChangeNPC : GlobalNPC
    {
        public override void SetDefaults(NPC npc)
        {
            if (!TimeDomain.AncientMode)
            {
                return;
            }
            if (npc.type == NPCID.Creeper)
            {
                npc.lifeMax = 150;
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                npc.knockBackResist = 0;
                npc.damage = 45;
                npc.lifeMax = 1800;
            }
            if (npc.type == NPCID.KingSlime)
            {
                npc.noGravity = false;
            }
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (!TimeDomain.AncientMode)
            {
                return;
            }
            Timer[0] = 0f;
            Timer[1] = 0f;
            Timer[2] = 0f;
            Timer[3] = 0f;
            Timer[4] = 0f;
            Timer[5] = 0f;
            Timer[6] = 0f;
            Timer[7] = 0f;
            Timer[8] = 0f;
            Timer[9] = 0f;
            if (npc.type == NPCID.Creeper)
            {
                CreeperAI.OverrideOnSpawn(npc);
                return;
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                BrainofCthulhuAI.OverrideOnSpawn(npc);
                return;
            }
            if (npc.type == NPCID.KingSlime)
            {
                KingSlimeAI.OverrideOnSpawn(npc);
                return;
            }
        }
        public override bool InstancePerEntity => true;
        public float[] Timer = new float[10];


        public int KSTime = 0;
        public Vector2 KSTargetVector2;
        public override bool PreAI(NPC npc)
        {
            if (!TimeDomain.AncientMode)
            {
                return base.PreAI(npc);
            }
            if (npc.type == NPCID.Creeper)
            {
                CreeperAI.BuffedAI(npc);
                return false;
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                BrainofCthulhuAI.BuffedAI(npc);
                return false;
            }
            if (npc.type == NPCID.KingSlime)
            {
                KingSlimeAI.BuffedAI(npc);
                return false;
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                if (!NPC.downedBoss2)
                {
                    npc.active = false;
                    NPC.NewNPC(npc.GetSource_FromAI(), (int)Main.player[npc.target].Center.X, (int)Main.player[npc.target].Center.Y, NPCID.Guide);
                }
            }
            return base.PreAI(npc);
        }

        public override void PostAI(NPC npc)
        {
        //    if (!TimeDomain.AncientMode)
        //    {
        //        return;
        //    }
        //    if (npc.type == NPCID.KingSlime)
        //    {
        //        if (npc.velocity.Y != 0)
        //        {
        //            if (npc.velocity.Y < 0)
        //            {
        //                KSTime++;
        //                npc.TargetClosest(true);
        //                if (KSTime == 1)
        //                {
        //                    KSTargetVector2 = Main.player[npc.target].Center;
        //                }
        //                float c = 0;
        //                if ((KSTargetVector2 - npc.Center).X > 0)
        //                {
        //                    c = -64;
        //                }
        //                else
        //                {
        //                    c = 64;
        //                }
        //                float tx = (KSTargetVector2 + new Vector2(c, 0) - npc.Center).X / 10;
        //                if (tx >= 50)
        //                {
        //                    tx = 50;
        //                }
        //                npc.velocity.X = tx;
        //            }
        //            else
        //            {
        //                KSTime = 0;
        //            }
        //        }
        //    }
        }
        public override void AI(NPC npc)
        {
            if (!TimeDomain.AncientMode)
            {
                return;
            }
            //int num = 1100;
            //for (int i = 0; i < 255; i++)
            //{
            //    if (Main.player[i].active && !Main.player[i].dead && (npc.Center - Main.player[i].position).Length() < (float)num && Main.player[i].inventory[Main.player[i].selectedItem].type == ModContent.ItemType<VampireStaff>() && Main.player[i].itemAnimation > 0)
            //    {
            //        if (i == Main.myPlayer)
            //        {
            //            Main.player[i].soulDrain++;
            //        }
            //        if (Main.rand.Next(3) != 0)
            //        {
            //            Vector2 center = npc.Center;
            //            center.X += (float)Main.rand.Next(-100, 100) * 0.05f;
            //            center.Y += (float)Main.rand.Next(-100, 100) * 0.05f;
            //            center += npc.velocity;
            //            int num2 = Dust.NewDust(center, 1, 1, DustID.LifeDrain, 0f, 0f, 0, default(Color), 1f);
            //            Main.dust[num2].velocity *= 0f;
            //            Main.dust[num2].scale = (float)Main.rand.Next(70, 85) * 0.01f;
            //            Main.dust[num2].fadeIn = (float)(i + 1);
            //        }
            //    }
            //}
        }
        public override bool CheckDead(NPC npc)
        {
            if (npc.type == NPCID.Creeper)
            {
                return CreeperAI.PreKill(npc);
            }
            return base.CheckDead(npc);
        }
        public bool oldai()
        {
            #region Creeper
            //if (npc.type == NPCID.Creeper)
            //{
            //    npc.ai[1]++;
            //    npc.ai[2]++;
            //    npc.TargetClosest(true);
            //    Player p = Main.player[npc.target];
            //    if (p.ZoneCrimson)
            //    {
            //        BerserkMode = false;
            //    }
            //    else
            //    {
            //        BerserkMode = true;
            //    }
            //    int TotalCreeper = 0;
            //    int a = 0;
            //    //int b = 0;
            //    //检查所有npc，看有多少飞眼怪
            //    for (int i = 0; i < Main.maxNPCs; i++)
            //    {
            //        if (Main.npc[i].type == NPCID.Creeper)
            //        {
            //            if (Main.npc[i].active)
            //            {
            //                TotalCreeper++;
            //            }
            //        }
            //    }
            //    //检查所有npc，旋转
            //    for (int i = 0; i < Main.maxNPCs; i++)
            //    {
            //        if (Main.npc[i].type == NPCID.Creeper)
            //        {
            //            if (Main.npc[i].active)
            //            {
            //                a++;
            //                Creeper[i] = a;
            //                //Mod mod = ModLoader.GetMod("HolyEntropyDawnAnnihilation");
            //                //if (mod != null)
            //                //{
            //                //    Creeper_range = 360;
            //                //}
            //                float Creeper_rotation;
            //                float Creeper_range = 200;//200;
            //                Vector2 Creeper_circles;
            //                float Creeper_r2;
            //                if (Vector2.Normalize(p.Center - CreeperRealOrigins) != Vector2.Zero)
            //                {
            //                    CreeperO = Vector2.Normalize(p.Center - CreeperRealOrigins);
            //                }
            //                Creeper_r2 = ((float)a / TotalCreeper) * MathHelper.TwoPi;
            //                Creeper_rotation = npc.ai[2] * 0.02f + Creeper_r2;
            //                if (a == 1)
            //                {
            //                    CreeperRealOrigins += CreeperO;
            //                }
            //                Creeper_origins = CreeperRealOrigins;
            //                Creeper_origins = p.Center;
            //                Creeper_circles = Creeper_rotation.ToRotationVector2() * Creeper_range;
            //                Creeper_origins += Creeper_circles;
            //                Main.npc[i].velocity = Creeper_origins - Main.npc[i].Center;
            //            }
            //        }
            //    }
            //    if (npc.active)
            //    {
            //        int CreeperAttackTime = 300;
            //        if (Main.getGoodWorld)
            //        {
            //            CreeperAttackTime = 60;
            //        }
            //        if (Main.masterMode)
            //        {
            //            CreeperAttackTime = 90;
            //        }
            //        if (Main.expertMode)
            //        {
            //            CreeperAttackTime = 150;
            //        }
            //        if (BerserkMode)
            //        {
            //            CreeperAttackTime /= 2;
            //        }
            //        if (npc.type == NPCID.Creeper)
            //        {
            //            if (npc.ai[1] % CreeperAttackTime == 0)
            //            {
            //                NPC NowCreeper = null;
            //                for (int d = 0; d < Creeper.Length; d++)
            //                {
            //                    if (Creeper[d] == npc.whoAmI)
            //                    {
            //                        if (d == 1)
            //                        {
            //                            CreeperC++;
            //                        }
            //                    }
            //                    CreeperC %= TotalCreeper;
            //                    if (d == CreeperC)
            //                    {
            //                        CanShoot = true;
            //                        NowCreeper = Main.npc[Creeper[d]];
            //                        //Main.NewText(CreeperC, new Color(200,200,200));
            //                        break;
            //                    }
            //                }
            //                for (int d = 0; d < Creeper.Length; d++)
            //                {
            //                    if (Creeper[d] == npc.whoAmI)
            //                    {
            //                        if (d == 1)
            //                        {
            //                            for (int i = 0; i < 3; i++)
            //                            {
            //                                if (BerserkMode)
            //                                {
            //                                    int proj = Projectile.NewProjectile(npc.GetSource_FromThis(), NowCreeper.Center, Vector2.Normalize(p.Center - NowCreeper.Center) * 6, ProjectileID.GoldenShowerHostile, 15, 3);
            //                                }
            //                                else
            //                                {
            //                                    int proj = Projectile.NewProjectile(npc.GetSource_FromThis(), NowCreeper.Center, Vector2.Normalize(p.Center - NowCreeper.Center) * 4, ProjectileID.GoldenShowerHostile, 13, 3);
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    return false;
            //}
            #endregion
            #region BrainofCthulhu
            //if (npc.type == NPCID.BrainofCthulhu)
            //{
            //    BossTime3++;
            //    Player p = Main.player[npc.target];
            //    if (p.ZoneCrimson)
            //    {
            //        BerserkMode = false;
            //    }
            //    else
            //    {
            //        BerserkMode = true;
            //    }
            //    if (BerserkMode)
            //    {
            //        if (Main.expertMode)
            //        {
            //            npc.damage = 80;
            //            npc.defense = 0;
            //        }
            //        if (Main.masterMode)
            //        {
            //            npc.damage = 140;
            //            npc.defense = 0;
            //        }
            //        if (Main.getGoodWorld && Main.masterMode)
            //        {
            //            npc.damage = 220;
            //            npc.defense = 0;
            //        }
            //    }
            //    else
            //    {
            //        if (Main.expertMode)
            //        {
            //            npc.damage = 65;
            //            npc.defense = 0;
            //        }
            //        if (Main.masterMode)
            //        {
            //            npc.damage = 115;
            //            npc.defense = 0;
            //        }
            //        if (Main.getGoodWorld && Main.masterMode)
            //        {
            //            npc.damage = 175;
            //            npc.defense = 0;
            //        }
            //        //npc.damage = 45;
            //        //npc.defense = 5;
            //    }
            //    npc.ai[1]++;
            //    NPC.crimsonBoss = npc.whoAmI;
            //    if (Main.netMode != NetmodeID.MultiplayerClient && npc.localAI[0] == 0f)
            //    {
            //        npc.localAI[0] = 1f;
            //    }
            //    //脱战
            //    if (Main.netMode != NetmodeID.MultiplayerClient)
            //    {
            //        npc.TargetClosest(true);
            //        //int num821 = 6000;
            //        //if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num821)
            //        //{
            //        //    npc.active = false;
            //        //    npc.life = 0;
            //        //    if (Main.netMode == NetmodeID.Server)
            //        //    {
            //        //        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            //        //    }
            //        //}
            //    }

            //    if (npc.ai[0] < 0)
            //    {
            //        if (Main.getGoodWorld)
            //        {
            //            NPC.brainOfGravity = npc.whoAmI;
            //        }
            //        npc.dontTakeDamage = false;
            //        if (npc.ai[0] == -1)
            //        {
            //            npc.dontTakeDamage = false;
            //        }
            //        if (npc.ai[0] == -1)
            //        {

            //            float BrainofCthulhu_rotation;
            //            Vector2 BrainofCthulhu_origins;
            //            Vector2 BrainofCthulhu_circles;

            //            BossTime2++;
            //            if (BossTime2 == 1)
            //            {
            //                BrainofCthulhu_range = 360;
            //            }
            //            if (BossTime2 == 10)
            //            {
            //                //SoundEngine.PlaySound(SoundID.NPCHit3, npc.Center);
            //                SoundEngine.PlaySound(SoundID.Roar, npc.Center);
            //            }
            //            if (BossTime2 < 180)
            //            {
            //                if (BossTime2 == 1)
            //                {
            //                    BrainofCthulhu_RO = p.Center;
            //                }
            //                //还好吧，我只是时间比较少
            //                BrainofCthulhu_range -= 2;
            //                float RSpeed = 0.07f;
            //                if (BerserkMode)
            //                {
            //                    RSpeed = 0.12f;
            //                }
            //                else
            //                {
            //                    RSpeed = 0.07f;
            //                }
            //                BrainofCthulhu_rotation = BossTime3 * RSpeed/* + BrainofCthulhu_r2*/;
            //                if (BossTime2 < 100)
            //                {
            //                    BrainofCthulhu_RO = p.Center;
            //                }
            //                BrainofCthulhu_origins = BrainofCthulhu_RO;
            //                BrainofCthulhu_circles = BrainofCthulhu_rotation.ToRotationVector2() * BrainofCthulhu_range;
            //                BrainofCthulhu_origins += BrainofCthulhu_circles;
            //                npc.velocity = BrainofCthulhu_origins - npc.Center;
            //            }
            //            if (BossTime2 > 150 && BossTime2 < 180)
            //            {
            //                npc.velocity *= 0.97f;
            //            }
            //            #region 旧冲刺
            //            /*
            //            if (BossTime2 > 180 && BossTime2 <= 183)
            //            {
            //                npc.velocity = -Vector2.Normalize(p.Center - npc.Center) * 10;
            //            }
            //            //
            //            if (BossTime2 > 210 && BossTime2 <= 300)
            //            {
            //                if (BrainofCthulhu_JGCenter == new Vector2(-10, -10))
            //                {
            //                    BrainofCthulhu_JGCenter = p.Center;
            //                    Vector2 v = BrainofCthulhu_JGCenter - npc.Center;
            //                    v *= 1.2f;
            //                    //v.Normalize();
            //                    float val = (v.Length() / 90) / (float)Math.Sqrt((v.X * v.X) + (v.Y * v.Y));
            //                    v.X *= val;
            //                    v.Y *= val;
            //                    //v = v.Length() / 90;
            //                    v *= 3;
            //                    npc.velocity = v;
            //                }
            //                if (BossTime2 > 270)
            //                {
            //                    npc.velocity *= 0.97f;
            //                }
            //            }
            //            if (BossTime2 > 300&& BossTime2<390)
            //            {
            //                if (BossTime2 == 301)
            //                {
            //                    float VR = MathHelper.Pi / 4;
            //                    Vector2 v2 = VR.ToRotationVector2();
            //                    npc.velocity += v2;
            //                    npc.velocity = -npc.velocity;
            //                }
            //            }
            //            */
            //            #endregion
            //            if (BossTime2 > 180 && BossTime2 < 390)
            //            {
            //                npc.ai[3] = 1;
            //                if (BossTime2 == 181)
            //                {
            //                    int rand = Main.rand.Next(1, 4);
            //                    if (rand == 1) { vcr = new Vector2(0, -1); }
            //                    if (rand == 2) { vcr = new Vector2(1, 0); }
            //                    if (rand == 3) { vcr = new Vector2(0, 1); }
            //                    if (rand == 4) { vcr = new Vector2(-1, 0); }

            //                    vcr *= 800;
            //                    npc.Center = p.Center + vcr;
            //                    npc.velocity = -vcr / 4000;
            //                }
            //                if (BossTime2 == 185)
            //                {
            //                    npc.velocity *= 100f;
            //                    SoundEngine.PlaySound(SoundID.Roar, npc.Center);
            //                }
            //                if (BossTime2 > 181 && BossTime2 < 200)
            //                {
            //                    npc.velocity *= 1.01f;
            //                }
            //                if (BossTime2 > 200 && BossTime2 < 230)
            //                {
            //                    npc.velocity *= 0.97f;
            //                }
            //                if (BossTime2 == 230)
            //                {
            //                    int rand = Main.rand.Next(1, 4);
            //                    if (rand == 1) { vcr = new Vector2(-1, -1); }
            //                    if (rand == 2) { vcr = new Vector2(1, -1); }
            //                    if (rand == 3) { vcr = new Vector2(1, 1); }
            //                    if (rand == 4) { vcr = new Vector2(-1, 1); }

            //                    vcr *= 100;
            //                    npc.velocity = vcr / 1000;
            //                    SoundEngine.PlaySound(SoundID.Roar, npc.Center);
            //                }
            //                if (BossTime2 == 230)
            //                {
            //                    if (BerserkMode)
            //                    {
            //                        npc.velocity *= 1.2f;
            //                    }
            //                    npc.velocity *= 100f;
            //                }
            //                if (BossTime2 > 250 && BossTime2 < 260)
            //                {
            //                    if (BerserkMode)
            //                    {
            //                        npc.velocity *= 1.01f;
            //                    }
            //                    npc.velocity *= 1.01f;
            //                }
            //                if (BossTime2 > 260 && BossTime2 < 270)
            //                {
            //                    if (BerserkMode)
            //                    {
            //                        npc.velocity *= 0.99f;
            //                    }
            //                    npc.velocity *= 0.97f;
            //                }
            //            }
            //            else
            //            {
            //                npc.ai[3] = 0;
            //            }
            //            if (BossTime2 == 270)
            //            {
            //                npc.ai[0] = -2;
            //                BossTime2 = 0;
            //                BrainofCthulhu_JGCenter = new Vector2(-10, -10);
            //                Vector2 velocity = p.Center - npc.Center;
            //                if (BerserkMode)
            //                {
            //                    if (Main.masterMode)
            //                    {
            //                        if (Main.getGoodWorld)
            //                        {
            //                            float Angle = MathHelper.Pi / 20;
            //                            for (float r = -Angle * 4; r < Angle * 5; r += Angle)
            //                            {
            //                                float r2 = r + velocity.ToRotation();
            //                                Vector2 v = new Vector2((float)Math.Cos(r2), (float)Math.Sin(r2)) * 12f;
            //                                Projectile proj = Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, v, ProjectileID.BloodNautilusShot, npc.damage / 5, 0);
            //                                proj.tileCollide = false;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            float Angle = MathHelper.Pi / 18;
            //                            for (float r = -Angle * 3; r < Angle * 4; r += Angle)
            //                            {
            //                                float r2 = r + velocity.ToRotation();
            //                                Vector2 v = new Vector2((float)Math.Cos(r2), (float)Math.Sin(r2)) * 12f;
            //                                Projectile proj = Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, v, ProjectileID.BloodNautilusShot, npc.damage / 5, 0);
            //                                proj.tileCollide = false;
            //                            }
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    float Angle = MathHelper.Pi / 18;
            //                    for (float r = -Angle * 2; r < Angle * 4; r += Angle)
            //                    {
            //                        float r2 = r + velocity.ToRotation();
            //                        Vector2 v = new Vector2((float)Math.Cos(r2), (float)Math.Sin(r2)) * 10f;
            //                        Projectile proj = Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, v, ProjectileID.BloodNautilusShot, npc.damage / 6, 0);
            //                        proj.tileCollide = false;
            //                    }
            //                }
            //                SoundEngine.PlaySound(SoundID.Roar, npc.Center);
            //            }
            //        }
            //        if (npc.ai[0] == -2)
            //        {
            //            npc.alpha += 5;
            //            if (npc.alpha >= 255)
            //            {
            //                npc.alpha = 255;
            //                int a = 0;
            //                if (Main.rand.Next(0, 1) == 0)
            //                { a = 1; }
            //                else
            //                { a = -1; }
            //                npc.position = Main.player[npc.target].Center + new Vector2(Main.rand.Next(-20, 20), 20 * a) + new Vector2(npc.width / 2, npc.height / 2);
            //                npc.ai[0] = -3;
            //            }
            //            SoundEngine.PlaySound(SoundID.Item8, npc.Center);
            //        }
            //        if (npc.ai[0] == -3)
            //        {
            //            npc.alpha -= 5;
            //            if (npc.alpha <= 0)
            //            {
            //                npc.ai[0] = -1;
            //            }
            //            SoundEngine.PlaySound(SoundID.Item8, npc.Center);
            //        }
            //    }
            //    else
            //    {
            //        //一阶段
            //        if (npc.ai[0] == 0)
            //        {
            //            //转二阶段
            //            //int CreeperCount = 0;
            //            //for (int i = 0; i < Main.maxNPCs; i++)
            //            //{
            //            //    if (Main.npc[i].type == NPCID.Creeper)
            //            //    {
            //            //        if (npc.active)
            //            //        {
            //            //            CreeperCount++;
            //            //        }
            //            //    }
            //            //}
            //            //if (CreeperCount == 0)
            //            //{
            //            //    npc.ai[0] = 1;
            //            //}
            //            int CreeperCount = 0;
            //            for (int i = 0; i < 200; i++)
            //            {
            //                if (Main.npc[i].active && Main.npc[i].type == NPCID.Creeper)
            //                {
            //                    CreeperCount++;
            //                }
            //            }
            //            if (CreeperCount == 0)
            //            {
            //                npc.ai[0] = -1f;
            //                npc.localAI[1] = 0f;
            //                npc.alpha = 0;
            //                npc.netUpdate = true;
            //            }


            //            BossTime++;
            //            float num835 = Main.player[npc.target].Center.X - npc.Center.X;
            //            float num836 = Main.player[npc.target].Center.Y - npc.Center.Y;
            //            float num837 = (float)Math.Sqrt((double)(num835 * num835 + num836 * num836));
            //            float num838 = 1f;
            //            if (Main.getGoodWorld)
            //            {
            //                num838 *= 1.33f;
            //            }
            //            if (BerserkMode)
            //            {
            //                num838 *= 1.2f;
            //            }
            //            num838 *= 3;
            //            if (num837 < num838)
            //            {
            //                npc.velocity.X = num835;
            //                npc.velocity.Y = num836;
            //            }
            //            else
            //            {
            //                num837 = num838 / num837;
            //                npc.velocity.X = num835 * num837;
            //                npc.velocity.Y = num836 * num837;
            //            }
            //            if (BossTime < 40)
            //            {
            //                npc.velocity *= 3;
            //            }
            //            if (BossTime % 240 == 0)
            //            {
            //                BossTime = 0;
            //                npc.ai[0] = 1;
            //            }
            //        }
            //        if (npc.ai[0] == 1)
            //        {
            //            BossTime--;
            //            npc.alpha = -5 * BossTime; ;
            //            if (BossTime <= -51)
            //            {
            //                int a = 0;
            //                if (Main.rand.Next(0, 1) == 0)
            //                { a = 1; }
            //                else
            //                { a = -1; }
            //                npc.position = Main.player[npc.target].Center + new Vector2(Main.rand.Next(-20, 20), 20 * a) + new Vector2(npc.width / 2, npc.height / 2);
            //                npc.ai[0] = 2;
            //                BossTime = 0;
            //            }
            //        }
            //        if (npc.ai[0] == 2)
            //        {
            //            npc.alpha -= 5;
            //            if (npc.alpha <= 0)
            //            {
            //                npc.alpha = 0;
            //                npc.ai[0] = 0;
            //            }
            //        }
            //    }
            //    if (BerserkMode)
            //    {
            //        npc.ai[2] = 1;
            //    }
            //    else
            //    {
            //        npc.ai[2] = 0;
            //    }
            //    return false;
            //}
            #endregion]
            return true;
        }
    }
}
