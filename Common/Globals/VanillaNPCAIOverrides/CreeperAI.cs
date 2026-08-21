using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace yourmod.Common.Globals.VanillaNPCAIOverrides
{
    public static class CreeperAI
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




        public static int[] Creeper = new int[1000];
        public static int[] Creeper_NoDeath = new int[1000];
        static Vector2 Creeper_origins;
        //static Vector2 CreeperRealOrigins;
        static Vector2 CreeperO = Vector2.Zero;
        static float[] ExtraCreeperRange = new float[1000];
        public static void OverrideOnSpawn(NPC npc)
        {
            thisNPC = npc;
            BossTime = BossTime2 = BossTime3 = 0;
            CreeperO = npc.Center;
            Creeper_origins = npc.Center;
            //CreeperRealOrigins = npc.Center;
            IsDeath = new bool[1000];
            ExtraCreeperRange = new float[1000];
        }







        static int c2 = -1;
        static int c3 = -1;
        static int Ro
        {
            get
            {
                if (Main.masterMode)
                {
                    if (Main.getGoodWorld)
                    {
                        return 400;
                    }
                    return 300;
                }
                return 200;
            }
        }
        public static int WhoAmI_InCreeper = -1;
        public static int WhoAmI_InCreeperNOD = -1;
        public static void BuffedAI(NPC npc)
        {
            thisNPC = npc;

            Creeper = new int[200];

            bool Active = false;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].type == NPCID.BrainofCthulhu && Main.npc[i].active)
                    Active = true;
            }
            npc.active = Active;






            if (CreeperO == Vector2.Zero)
            {
                CreeperO = Main.npc[(int)npc.ai[3]].Center;
            }

            npc.ai[1]++;
            npc.ai[2]++;
            npc.TargetClosest(true);
            Player p = Main.player[npc.target];
            BerserkMode = !p.ZoneCrimson;

            int TotalCreeper = 0;
            int a = 0;
            int b = 0;
            //检查所有npc，看有多少飞眼怪
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].type == NPCID.Creeper && Main.npc[i].active)
                    TotalCreeper++;
            }
            //检查所有npc，旋转
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].type == NPCID.Creeper && Main.npc[i].active)
                {
                    Creeper[a] = i;
                    if (!IsDeath[i])
                    {
                        Creeper_NoDeath[b] = i;
                        if (npc.whoAmI == i)
                        {
                            WhoAmI_InCreeperNOD = a;
                        }
                        b++;
                    }

                    if (npc.whoAmI == i)
                    {
                        WhoAmI_InCreeper = a;
                    }




                    a++;
                }
            }
            float Creeper_rotation;
            float Creeper_range = (float)(Ro + Math.Sin(npc.ai[1] / 20) * Ro / 4);

            if (IsDeath[npc.whoAmI])
            {
                if (ExtraCreeperRange[npc.whoAmI] < 200)
                {
                    ExtraCreeperRange[npc.whoAmI]++;
                }
            }
            if (npc.whoAmI == Creeper[WhoAmI_InCreeper] && IsDeath[npc.whoAmI])
                Creeper_range += ExtraCreeperRange[npc.whoAmI];



            Vector2 Creeper_circles;
            float Creeper_r2;
            Creeper_r2 = ((float)WhoAmI_InCreeper / TotalCreeper) * MathHelper.TwoPi;
            Creeper_rotation = npc.ai[2] * 0.02f + Creeper_r2;

            //if (Vector2.Normalize(p.Center - CreeperO).Length() > 5)
            //{
            //    CreeperO += Vector2.Normalize(p.Center - CreeperO) * 2;
            //}
            CreeperO = p.Center;
            Creeper_origins = CreeperO;
            //Creeper_origins = p.Center;
            Creeper_circles = Creeper_rotation.ToRotationVector2() * Creeper_range;
            Creeper_origins += Creeper_circles;
            npc.velocity = Creeper_origins - npc.Center;


            int CreeperAttackTime = 300;
            if (Main.expertMode)
                CreeperAttackTime = 180;
            if (Main.masterMode)
                CreeperAttackTime = 150;
            if (Main.getGoodWorld && Main.masterMode)
                CreeperAttackTime = 120;
            if (BerserkMode)
                CreeperAttackTime /= 2;



            {
                //NPC NowCreeper = null;
                //for (int d = 0; d < Creeper.Length; d++)
                //{
                //    if (Creeper[d] == npc.whoAmI)
                //    {
                //        if (d == 1)
                //        {
                //            CreeperC++;
                //        }
                //    }
                //    CreeperC %= TotalCreeper;
                //    if (d == CreeperC)
                //    {
                //        NowCreeper = Main.npc[Creeper[d]];
                //        break;
                //    }
                //}
                //for (int d = 0; d < Creeper.Length; d++)
                //{
                //    if (Creeper[d] == npc.whoAmI)
                //    {
                //        if (d == 1)
                //        {
                //            for (int i = 0; i < 3; i++)
                //            {
                //                if (BerserkMode)
                //                {
                //                    int proj = Projectile.NewProjectile(npc.GetSource_FromThis(), NowCreeper.Center, Vector2.Normalize(p.Center - NowCreeper.Center) * 6, ProjectileID.GoldenShowerHostile, 15, 3);
                //                }
                //                else
                //                {
                //                    int proj = Projectile.NewProjectile(npc.GetSource_FromThis(), NowCreeper.Center, Vector2.Normalize(p.Center - NowCreeper.Center) * 4, ProjectileID.GoldenShowerHostile, 13, 3);
                //                }
                //            }
                //        }
                //    }
                //}
            }
            if (c2 == -1)
            {
                c2 = Main.rand.Next(1, Creeper_NoDeath.Length);

            }
            if (c3 == -1)
            {
                c3 = Main.rand.Next(1, Creeper_NoDeath.Length);
            }

            int c = Creeper_NoDeath.Length / 3;
            if (!IsDeath[npc.whoAmI])
            {
                if (WhoAmI_InCreeperNOD + 1 == c || WhoAmI_InCreeperNOD + 1 == c * 2 || WhoAmI_InCreeperNOD + 1 == c * 3 || (Main.getGoodWorld && Main.masterMode && WhoAmI_InCreeperNOD + 1 == c2) || (Main.zenithWorld && WhoAmI_InCreeperNOD + 1 == c3))
                {
                    if (npc.ai[1] % CreeperAttackTime == 0)
                    {
                        int v = Main.getGoodWorld && Main.masterMode ? 3 : 2;
                        Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center, Vector2.Normalize(p.Center - npc.Center) * v, ProjectileID.GoldenShowerHostile, 15, 3);
                        if (WhoAmI_InCreeperNOD + 1 == c)
                        {
                            c2 = Main.rand.Next(1, Creeper_NoDeath.Length);
                            c3 = Main.rand.Next(1, Creeper_NoDeath.Length);
                        }
                    }
                    npc.color = Color.Yellow;
                }
                else
                {
                    npc.color = Color.Transparent;
                }
            }
            else
            {
                npc.color = Color.Transparent;
            }
            if (Main.zenithWorld)
            {
                Color color = Main.hslToRgb((npc.ai[1] / 100) % 1, 1, 0.5f, 0);
                npc.color = new Color(npc.color.R + color.R, npc.color.G + color.G, npc.color.B + color.B);
                //Main.NewText(1, color);
            }


        }
        public static void VanillaAI(NPC npc)
        {
            
        }
        public static bool[] IsDeath = new bool[1000];
        public static bool PreKill(NPC npc)
        {
            if (!yourmod.AncientMode)
            {
                return true;
            }
            thisNPC = npc;
            npc.active = true;
            npc.life = 1;
            npc.dontTakeDamage = true;
            IsDeath[npc.whoAmI] = true;
            return false;
        }
    }
}
