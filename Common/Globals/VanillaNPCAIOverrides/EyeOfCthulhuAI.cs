using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace TimeDomain.Common.Globals.VanillaNPCAIOverrides
{
    public static class EyeOfCthulhuAI
    {
        public static NPC thisNPC = null;
        public static Player player => Main.player[thisNPC.target];
        public static void ChangeVanillaAI(NPC npc)
        {
            bool flag2 = Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.12;
            bool flag3 = Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.04;
            float num4 = 20f;
            if (flag3)
            {
                num4 = 10f;
            }
            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(true);
            }
            bool dead = player.dead;
            float num5 = npc.position.X + (float)(npc.width / 2) - player.position.X - (float)(player.width / 2);
            float num6 = npc.position.Y + (float)npc.height - 59f - player.position.Y - (float)(player.height / 2);
            float num7 = (float)Math.Atan2((double)num6, (double)num5) + 1.57f;
            if (num7 < 0f)
            {
                num7 += 6.283f;
            }
            else if ((double)num7 > 6.283)
            {
                num7 -= 6.283f;
            }
            float num8 = 0f;
            if (npc.ai[0] == 0f && npc.ai[1] == 0f)
            {
                num8 = 0.02f;
            }
            if (npc.ai[0] == 0f && npc.ai[1] == 2f && npc.ai[2] > 40f)
            {
                num8 = 0.05f;
            }
            if (npc.ai[0] == 3f && npc.ai[1] == 0f)
            {
                num8 = 0.05f;
            }
            if (npc.ai[0] == 3f && npc.ai[1] == 2f && npc.ai[2] > 40f)
            {
                num8 = 0.08f;
            }
            if (npc.ai[0] == 3f && npc.ai[1] == 4f && npc.ai[2] > num4)
            {
                num8 = 0.15f;
            }
            if (npc.ai[0] == 3f && npc.ai[1] == 5f)
            {
                num8 = 0.05f;
            }
            if (Main.expertMode)
            {
                num8 *= 1.5f;
            }
            if (flag3 && Main.expertMode)
            {
                num8 = 0f;
            }
            if (npc.rotation < num7)
            {
                if ((double)(num7 - npc.rotation) > 3.1415)
                {
                    npc.rotation -= num8;
                }
                else
                {
                    npc.rotation += num8;
                }
            }
            else if (npc.rotation > num7)
            {
                if ((double)(npc.rotation - num7) > 3.1415)
                {
                    npc.rotation += num8;
                }
                else
                {
                    npc.rotation -= num8;
                }
            }
            if (npc.rotation > num7 - num8 && npc.rotation < num7 + num8)
            {
                npc.rotation = num7;
            }
            if (npc.rotation < 0f)
            {
                npc.rotation += 6.283f;
            }
            else if ((double)npc.rotation > 6.283)
            {
                npc.rotation -= 6.283f;
            }
            if (npc.rotation > num7 - num8 && npc.rotation < num7 + num8)
            {
                npc.rotation = num7;
            }
            if (Main.rand.Next(5) == 0)
            {
                int num9 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f), npc.width, (int)((float)npc.height * 0.5f), DustID.Blood, npc.velocity.X, 2f, 0, default(Color), 1f);
                Main.dust[num9].velocity.X *= 0.5f;
                Main.dust[num9].velocity.Y *= 0.1f;
            }
            npc.reflectsProjectiles = false;
            if (Main.IsItDay() || dead)
            {
                npc.velocity.Y -= 0.04f;
                npc.EncourageDespawn(10);
                return;
            }
            if (npc.ai[0] == 0f)
            {
                if (npc.ai[1] == 0f)
                {
                    //克眼最大速度
                    float MaxSpeed = Main.expertMode ? 7f : 5f;
                    //克眼的加速度
                    float Acceleration = Main.expertMode ? 0.15f : 0.04f;
                    if (Main.getGoodWorld)
                    {
                        Acceleration += 0.05f;
                        MaxSpeed += 1f;
                    }
                    Vector2 vector = npc.Center;
                    Vector2 ToPlayerBottom = player.Center - vector - Vector2.UnitY * 200;
                    float num15 = ToPlayerBottom.Length();
                    ToPlayerBottom /= ToPlayerBottom.Length();
                    ToPlayerBottom *= MaxSpeed;
                    if (true)
                    {
                        if (npc.velocity.X < ToPlayerBottom.X)
                        {
                            npc.velocity.X += Acceleration;
                            if (npc.velocity.X < 0f && ToPlayerBottom.X > 0f)
                            {
                                npc.velocity.X += Acceleration;
                            }
                        }
                        else if (npc.velocity.X > ToPlayerBottom.X)
                        {
                            npc.velocity.X -= Acceleration;
                            if (npc.velocity.X > 0f && ToPlayerBottom.X < 0f)
                            {
                                npc.velocity.X -= Acceleration;
                            }
                        }
                        if (npc.velocity.Y < ToPlayerBottom.Y)
                        {
                            npc.velocity.Y += Acceleration;
                            if (npc.velocity.Y < 0f && ToPlayerBottom.Y > 0f)
                            {
                                npc.velocity.Y += Acceleration;
                            }
                        }
                        else if (npc.velocity.Y > ToPlayerBottom.Y)
                        {
                            npc.velocity.Y -= Acceleration;
                            if (npc.velocity.Y > 0f && ToPlayerBottom.Y < 0f)
                            {
                                npc.velocity.Y -= Acceleration;
                            }
                        }
                    }
                    else if (false)
                    {
                        npc.velocity.X += (npc.velocity.X < ToPlayerBottom.X).ToInt() * Acceleration;
                        npc.velocity.X += (npc.velocity.X < 0f && ToPlayerBottom.X > 0f).ToInt() * Acceleration;

                        npc.velocity.X -= (npc.velocity.X > ToPlayerBottom.X).ToInt() * Acceleration;
                        npc.velocity.X -= (npc.velocity.X > 0f && ToPlayerBottom.X < 0f).ToInt() * Acceleration;

                        npc.velocity.Y += (npc.velocity.Y < ToPlayerBottom.Y).ToInt() * Acceleration;
                        npc.velocity.Y += (npc.velocity.Y < 0f && ToPlayerBottom.Y > 0f).ToInt() * Acceleration;

                        npc.velocity.Y -= (npc.velocity.Y > ToPlayerBottom.Y).ToInt() * Acceleration;
                        npc.velocity.Y -= (npc.velocity.Y > 0f && ToPlayerBottom.Y < 0f).ToInt() * Acceleration;
                    }
                    else
                    {
                        npc.velocity += ToPlayerBottom * Acceleration;
                    }
                    npc.ai[2] += 1f;
                    float Time = 600f;
                    if (Main.expertMode)
                    {
                        Time *= 0.35f;
                    }
                    if (npc.ai[2] >= Time)
                    {
                        npc.ai[1] = 1f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        npc.target = 255;
                        npc.netUpdate = true;
                    }
                    else if ((npc.position.Y + (float)npc.height < player.position.Y && num15 < 500f) || (Main.expertMode && num15 < 500f))
                    {
                        if (!player.dead)
                        {
                            npc.ai[3] += 1f;
                        }
                        float num17 = 110f;
                        if (Main.expertMode)
                        {
                            num17 *= 0.4f;
                        }
                        if (Main.getGoodWorld)
                        {
                            num17 *= 0.8f;
                        }
                        if (npc.ai[3] >= num17)
                        {
                            npc.ai[3] = 0f;
                            npc.rotation = num7;
                            float num18 = 5f;
                            if (Main.expertMode)
                            {
                                num18 = 6f;
                            }
                            float num19 = player.position.X + (float)(player.width / 2) - vector.X;
                            float num20 = player.position.Y + (float)(player.height / 2) - vector.Y;
                            float num21 = (float)Math.Sqrt((double)(num19 * num19 + num20 * num20));
                            num21 = num18 / num21;
                            Vector2 vector2 = vector;
                            Vector2 vector3 = default(Vector2);
                            vector3.X = num19 * num21;
                            vector3.Y = num20 * num21;
                            vector2.X += vector3.X * 10f;
                            vector2.Y += vector3.Y * 10f;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                int num22 = NPC.NewNPC(npc.GetSource_FromAI(), (int)vector2.X, (int)vector2.Y, NPCID.ServantofCthulhu, 0, 0f, 0f, 0f, 0f, 255);
                                Main.npc[num22].velocity.X = vector3.X;
                                Main.npc[num22].velocity.Y = vector3.Y;
                                if (Main.netMode == NetmodeID.Server && num22 < 200)
                                {
                                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num22, 0f, 0f, 0f, 0, 0, 0);
                                }
                            }
                            SoundEngine.PlaySound(SoundID.NPCHit1, vector2);
                            int num1598;
                            for (int m = 0; m < 10; m = num1598 + 1)
                            {
                                Dust.NewDust(vector2, 20, 20, DustID.Blood, vector3.X * 0.4f, vector3.Y * 0.4f, 0, default(Color), 1f);
                                num1598 = m;
                            }
                        }
                    }
                }
                else if (npc.ai[1] == 1f)
                {
                    npc.rotation = num7;
                    float Speed = Main.expertMode ? 7f : 6f;
                    if (Main.getGoodWorld)
                    {
                        Speed += 1f;
                    }
                    Vector2 ToPlayer = player.Center - npc.Center;
                    ToPlayer /= ToPlayer.Length();
                    ToPlayer *= Speed;
                    npc.velocity = ToPlayer;
                    npc.ai[1] = 2f;
                    npc.netUpdate = true;
                    if (npc.netSpam > 10)
                    {
                        npc.netSpam = 10;
                    }
                }
                else if (npc.ai[1] == 2f)
                {
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 40f)
                    {
                        npc.velocity *= 0.98f;
                        if (Main.expertMode)
                            npc.velocity *= 0.985f;
                        if (Main.getGoodWorld)
                            npc.velocity *= 0.99f;
                        if (npc.velocity.Length() < 0.2) npc.velocity = Vector2.Zero;
                    }
                    else
                    {
                        npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) - 1.57f;
                    }
                    int Time = Main.expertMode ? 100 : 150;
                    if (Main.getGoodWorld)
                    {
                        Time -= 15;
                    }
                    if (npc.ai[2] >= Time)
                    {
                        npc.ai[3] += 1f;
                        npc.ai[2] = 0f;
                        npc.target = 255;
                        npc.rotation = num7;
                        if (npc.ai[3] >= 3f)
                        {
                            npc.ai[1] = 0f;
                            npc.ai[3] = 0f;
                        }
                        else
                        {
                            npc.ai[1] = 1f;
                        }
                    }
                }
                float Proportion = 0.5f;
                if (Main.expertMode)
                {
                    Proportion = 0.65f;
                }
                if ((float)npc.life < (float)npc.lifeMax * Proportion)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    if (npc.netSpam > 10)
                    {
                        npc.netSpam = 10;
                    }
                }
                return;
            }
            //二阶段
            if (npc.ai[0] == 1f || npc.ai[0] == 2f)
            {
                if (npc.ai[0] == 1f || npc.ai[3] == 1f)
                {
                    npc.ai[2] += 0.005f;
                    if ((double)npc.ai[2] > 0.5)
                    {
                        npc.ai[2] = 0.5f;
                    }
                }
                else
                {
                    npc.ai[2] -= 0.005f;
                    if (npc.ai[2] < 0f)
                    {
                        npc.ai[2] = 0f;
                    }
                }
                npc.rotation += npc.ai[2];
                npc.ai[1] += 1f;
                if (Main.getGoodWorld)
                {
                    npc.reflectsProjectiles = true;
                }
                int num29 = 20;
                if (Main.getGoodWorld && npc.life < npc.lifeMax / 3)
                {
                    num29 = 10;
                }
                if (Main.expertMode && npc.ai[1] % (float)num29 == 0f)
                {
                    float Speed = 5f;
                    Vector2 vector5 = npc.Center;
                    Vector2 v = new Vector2(Main.rand.Next(-200, 200), Main.rand.Next(-200, 200));
                    float length = v.Length();
                    if (Main.getGoodWorld) v *= 3f;
                    Vector2 vector6 = npc.Center;
                    Vector2 vector7 = ((Main.rand.Next(628) - 314) / 100f).ToRotationVector2();
                    npc.velocity = v / length * Speed;
                    vector6.X += vector7.X * 10f;
                    vector6.Y += vector7.Y * 10f;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int num34 = NPC.NewNPC(npc.GetSource_FromAI(), (int)vector6.X, (int)vector6.Y, NPCID.ServantofCthulhu, 0, 0f, 0f, 0f, 0f, 255);
                        Main.npc[num34].velocity.X = vector7.X;
                        Main.npc[num34].velocity.Y = vector7.Y;
                        if (Main.netMode == NetmodeID.Server && num34 < 200)
                        {
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num34, 0f, 0f, 0f, 0, 0, 0);
                        }
                    }
                    int num1598;
                    for (int n = 0; n < 10; n = num1598 + 1)
                    {
                        Dust.NewDust(vector6, 20, 20, DustID.Blood, vector7.X * 0.4f, vector7.Y * 0.4f, 0, default(Color), 1f);
                        num1598 = n;
                    }
                }
                if (npc.ai[1] >= 100f)
                {
                    if (npc.ai[3] == 1f)
                    {
                        npc.ai[3] = 0f;
                        npc.ai[1] = 0f;
                    }
                    else
                    {
                        npc.ai[0] += 1f;
                        npc.ai[1] = 0f;
                        if (npc.ai[0] == 3f)
                        {
                            npc.ai[2] = 0f;
                        }
                        else
                        {
                            SoundEngine.PlaySound(SoundID.NPCHit1, npc.position);
                            int num1598;
                            for (int num35 = 0; num35 < 2; num35 = num1598 + 1)
                            {
                                Gore.NewGore(npc.GetSource_FromAI(), npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 8, 1f);
                                Gore.NewGore(npc.GetSource_FromAI(), npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7, 1f);
                                Gore.NewGore(npc.GetSource_FromAI(), npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6, 1f);
                                num1598 = num35;
                            }
                            for (int num36 = 0; num36 < 20; num36 = num1598 + 1)
                            {
                                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
                                num1598 = num36;
                            }
                            SoundEngine.PlaySound(SoundID.Roar, npc.position);
                        }
                    }
                }
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
                npc.velocity.X *= 0.98f;
                npc.velocity.Y *= 0.98f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                {
                    npc.velocity.Y = 0f;
                }
                return;
            }
            npc.defense = 0;
            int num37 = 23;
            int num38 = 18;
            if (Main.expertMode)
            {
                if (flag2)
                {
                    npc.defense = -15;
                }
                if (flag3)
                {
                    num38 = 20;
                    npc.defense = -30;
                }
            }
            npc.damage = npc.GetAttackDamage_LerpBetweenFinalValues((float)num37, (float)num38);
            npc.damage = npc.GetAttackDamage_ScaledByStrength((float)npc.damage);
            if (npc.ai[1] == 0f && flag2)
            {
                npc.ai[1] = 5f;
            }
            if (npc.ai[1] == 0f)
            {
                float num39 = 6f;
                float num40 = 0.07f;
                Vector2 vector8 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num41 = player.position.X + (float)(player.width / 2) - vector8.X;
                float num42 = player.position.Y + (float)(player.height / 2) - 120f - vector8.Y;
                float num43 = (float)Math.Sqrt((double)(num41 * num41 + num42 * num42));
                if (num43 > 400f && Main.expertMode)
                {
                    num39 += 1f;
                    num40 += 0.05f;
                    if (num43 > 600f)
                    {
                        num39 += 1f;
                        num40 += 0.05f;
                        if (num43 > 800f)
                        {
                            num39 += 1f;
                            num40 += 0.05f;
                        }
                    }
                }
                if (Main.getGoodWorld)
                {
                    num39 += 1f;
                    num40 += 0.1f;
                }
                num43 = num39 / num43;
                num41 *= num43;
                num42 *= num43;
                if (npc.velocity.X < num41)
                {
                    npc.velocity.X += num40;
                    if (npc.velocity.X < 0f && num41 > 0f)
                    {
                        npc.velocity.X += num40;
                    }
                }
                else if (npc.velocity.X > num41)
                {
                    npc.velocity.X -= num40;
                    if (npc.velocity.X > 0f && num41 < 0f)
                    {
                        npc.velocity.X -= num40;
                    }
                }
                if (npc.velocity.Y < num42)
                {
                    npc.velocity.Y += num40;
                    if (npc.velocity.Y < 0f && num42 > 0f)
                    {
                        npc.velocity.Y += num40;
                    }
                }
                else if (npc.velocity.Y > num42)
                {
                    npc.velocity.Y -= num40;
                    if (npc.velocity.Y > 0f && num42 < 0f)
                    {
                        npc.velocity.Y -= num40;
                    }
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= 200f)
                {
                    npc.ai[1] = 1f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.35)
                    {
                        npc.ai[1] = 3f;
                    }
                    npc.target = 255;
                    npc.netUpdate = true;
                }
                if (Main.expertMode && flag3)
                {
                    npc.TargetClosest(true);
                    npc.netUpdate = true;
                    npc.ai[1] = 3f;
                    npc.ai[2] = 0f;
                    npc.ai[3] -= 1000f;
                }
            }
            else if (npc.ai[1] == 1f)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, npc.position);
                npc.rotation = num7;
                float num44 = 6.8f;
                if (Main.expertMode && npc.ai[3] == 1f)
                {
                    num44 *= 1.15f;
                }
                if (Main.expertMode && npc.ai[3] == 2f)
                {
                    num44 *= 1.3f;
                }
                if (Main.getGoodWorld)
                {
                    num44 *= 1.2f;
                }
                Vector2 vector9 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num45 = player.position.X + (float)(player.width / 2) - vector9.X;
                float num46 = player.position.Y + (float)(player.height / 2) - vector9.Y;
                float num47 = (float)Math.Sqrt((double)(num45 * num45 + num46 * num46));
                num47 = num44 / num47;
                npc.velocity.X = num45 * num47;
                npc.velocity.Y = num46 * num47;
                npc.ai[1] = 2f;
                npc.netUpdate = true;
                if (npc.netSpam > 10)
                {
                    npc.netSpam = 10;
                }
            }
            else if (npc.ai[1] == 2f)
            {
                float num48 = 40f;
                npc.ai[2] += 1f;
                if (Main.expertMode)
                {
                    num48 = 50f;
                }
                if (npc.ai[2] >= num48)
                {
                    npc.velocity *= 0.97f;
                    if (Main.expertMode)
                    {
                        npc.velocity *= 0.98f;
                    }
                    if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                    {
                        npc.velocity.X = 0f;
                    }
                    if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                    {
                        npc.velocity.Y = 0f;
                    }
                }
                else
                {
                    npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f;
                }
                int num49 = 130;
                if (Main.expertMode)
                {
                    num49 = 90;
                }
                if (npc.ai[2] >= (float)num49)
                {
                    npc.ai[3] += 1f;
                    npc.ai[2] = 0f;
                    npc.target = 255;
                    npc.rotation = num7;
                    if (npc.ai[3] >= 3f)
                    {
                        npc.ai[1] = 0f;
                        npc.ai[3] = 0f;
                        if (Main.expertMode && Main.netMode != NetmodeID.MultiplayerClient && (double)npc.life < (double)npc.lifeMax * 0.5)
                        {
                            npc.ai[1] = 3f;
                            npc.ai[3] += (float)Main.rand.Next(1, 4);
                        }
                        npc.netUpdate = true;
                        if (npc.netSpam > 10)
                        {
                            npc.netSpam = 10;
                        }
                    }
                    else
                    {
                        npc.ai[1] = 1f;
                    }
                }
            }
            else if (npc.ai[1] == 3f)
            {
                if (npc.ai[3] == 4f && flag2 && npc.Center.Y > player.Center.Y)
                {
                    npc.TargetClosest(true);
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    if (npc.netSpam > 10)
                    {
                        npc.netSpam = 10;
                    }
                }
                else if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.TargetClosest(true);
                    float num50 = 20f;
                    Vector2 vector10 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num51 = player.position.X + (float)(player.width / 2) - vector10.X;
                    float num52 = player.position.Y + (float)(player.height / 2) - vector10.Y;
                    float num53 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) / 4f;
                    num53 += 10f - num53;
                    if (num53 < 5f)
                    {
                        num53 = 5f;
                    }
                    if (num53 > 15f)
                    {
                        num53 = 15f;
                    }
                    if (npc.ai[2] == -1f && !flag3)
                    {
                        num53 *= 4f;
                        num50 *= 1.3f;
                    }
                    if (flag3)
                    {
                        num53 *= 2f;
                    }
                    num51 -= player.velocity.X * num53;
                    num52 -= player.velocity.Y * num53 / 4f;
                    num51 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
                    num52 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
                    if (flag3)
                    {
                        num51 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
                        num52 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
                    }
                    float num54 = (float)Math.Sqrt((double)(num51 * num51 + num52 * num52));
                    float num55 = num54;
                    num54 = num50 / num54;
                    npc.velocity.X = num51 * num54;
                    npc.velocity.Y = num52 * num54;
                    npc.velocity.X += (float)Main.rand.Next(-20, 21) * 0.1f;
                    npc.velocity.Y += (float)Main.rand.Next(-20, 21) * 0.1f;
                    if (flag3)
                    {
                        npc.velocity.X += (float)Main.rand.Next(-50, 51) * 0.1f;
                        npc.velocity.Y += (float)Main.rand.Next(-50, 51) * 0.1f;
                        float num56 = Math.Abs(npc.velocity.X);
                        float num57 = Math.Abs(npc.velocity.Y);
                        if (npc.Center.X > player.Center.X)
                        {
                            num57 *= -1f;
                        }
                        if (npc.Center.Y > player.Center.Y)
                        {
                            num56 *= -1f;
                        }
                        npc.velocity.X = num57 + npc.velocity.X;
                        npc.velocity.Y = num56 + npc.velocity.Y;
                        npc.velocity.Normalize();
                        npc.velocity *= num50;
                        npc.velocity.X += (float)Main.rand.Next(-20, 21) * 0.1f;
                        npc.velocity.Y += (float)Main.rand.Next(-20, 21) * 0.1f;
                    }
                    else if (num55 < 100f)
                    {
                        if (Math.Abs(npc.velocity.X) > Math.Abs(npc.velocity.Y))
                        {
                            float num58 = Math.Abs(npc.velocity.X);
                            float num59 = Math.Abs(npc.velocity.Y);
                            if (npc.Center.X > player.Center.X)
                            {
                                num59 *= -1f;
                            }
                            if (npc.Center.Y > player.Center.Y)
                            {
                                num58 *= -1f;
                            }
                            npc.velocity.X = num59;
                            npc.velocity.Y = num58;
                        }
                    }
                    else if (Math.Abs(npc.velocity.X) > Math.Abs(npc.velocity.Y))
                    {
                        float num60 = (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) / 2f;
                        float num61 = num60;
                        if (npc.Center.X > player.Center.X)
                        {
                            num61 *= -1f;
                        }
                        if (npc.Center.Y > player.Center.Y)
                        {
                            num60 *= -1f;
                        }
                        npc.velocity.X = num61;
                        npc.velocity.Y = num60;
                    }
                    npc.ai[1] = 4f;
                    npc.netUpdate = true;
                    if (npc.netSpam > 10)
                    {
                        npc.netSpam = 10;
                    }
                }
            }
            else if (npc.ai[1] == 4f)
            {
                if (npc.ai[2] == 0f)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, npc.position);
                }
                float num62 = num4;
                npc.ai[2] += 1f;
                if (npc.ai[2] == num62 && Vector2.Distance(npc.position, player.position) < 200f)
                {
                    npc.ai[2] -= 1f;
                }
                if (npc.ai[2] >= num62)
                {
                    npc.velocity *= 0.95f;
                    if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                    {
                        npc.velocity.X = 0f;
                    }
                    if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                    {
                        npc.velocity.Y = 0f;
                    }
                }
                else
                {
                    npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f;
                }
                float num63 = num62 + 13f;
                if (npc.ai[2] >= num63)
                {
                    npc.netUpdate = true;
                    if (npc.netSpam > 10)
                    {
                        npc.netSpam = 10;
                    }
                    npc.ai[3] += 1f;
                    npc.ai[2] = 0f;
                    if (npc.ai[3] >= 5f)
                    {
                        npc.ai[1] = 0f;
                        npc.ai[3] = 0f;
                        if (npc.target >= 0 && Main.getGoodWorld && Collision.CanHit(npc.position, npc.width, npc.height, player.position, npc.width, npc.height))
                        {
                            SoundEngine.PlaySound(SoundID.Roar, npc.position);
                            npc.ai[0] = 2f;
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 1f;
                            npc.netUpdate = true;
                        }
                    }
                    else
                    {
                        npc.ai[1] = 3f;
                    }
                }
            }
            else if (npc.ai[1] == 5f)
            {
                float num64 = 600f;
                float num65 = 9f;
                float num66 = 0.3f;
                Vector2 vector11 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num67 = player.position.X + (float)(player.width / 2) - vector11.X;
                float num68 = player.position.Y + (float)(player.height / 2) + num64 - vector11.Y;
                float num69 = (float)Math.Sqrt((double)(num67 * num67 + num68 * num68));
                num69 = num65 / num69;
                num67 *= num69;
                num68 *= num69;
                if (npc.velocity.X < num67)
                {
                    npc.velocity.X += num66;
                    if (npc.velocity.X < 0f && num67 > 0f)
                    {
                        npc.velocity.X += num66;
                    }
                }
                else if (npc.velocity.X > num67)
                {
                    npc.velocity.X -= num66;
                    if (npc.velocity.X > 0f && num67 < 0f)
                    {
                        npc.velocity.X -= num66;
                    }
                }
                if (npc.velocity.Y < num68)
                {
                    npc.velocity.Y += num66;
                    if (npc.velocity.Y < 0f && num68 > 0f)
                    {
                        npc.velocity.Y += num66;
                    }
                }
                else if (npc.velocity.Y > num68)
                {
                    npc.velocity.Y -= num66;
                    if (npc.velocity.Y > 0f && num68 < 0f)
                    {
                        npc.velocity.Y -= num66;
                    }
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= 70f)
                {
                    npc.TargetClosest(true);
                    npc.ai[1] = 3f;
                    npc.ai[2] = -1f;
                    npc.ai[3] = (float)Main.rand.Next(-3, 1);
                    npc.netUpdate = true;
                }
            }
            if (flag3 && npc.ai[1] == 5f)
            {
                npc.ai[1] = 3f;
            }
            return;
        }



        static float ptrHelp = 0f;
        /// <summary>
        /// 妈的反编译代码怎么逻辑这么神
        /// 这是原版的克眼ai
        /// </summary>
        /// <param name="npc"></param>
        public static void VanillaAI(NPC npc)
        {
            ref float ptr = ref ptrHelp;
            bool flag2 = false;
            if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.12)
            {
                flag2 = true;
            }
            bool flag3 = false;
            if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.04)
            {
                flag3 = true;
            }
            float num4 = 20f;
            if (flag3)
            {
                num4 = 10f;
            }
            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(true);
            }
            bool dead = player.dead;
            float num5 = npc.position.X + (float)(npc.width / 2) - player.position.X - (float)(player.width / 2);
            float num6 = npc.position.Y + (float)npc.height - 59f - player.position.Y - (float)(player.height / 2);
            float num7 = (float)Math.Atan2((double)num6, (double)num5) + 1.57f;
            if (num7 < 0f)
            {
                num7 += 6.283f;
            }
            else if ((double)num7 > 6.283)
            {
                num7 -= 6.283f;
            }
            float num8 = 0f;
            if (npc.ai[0] == 0f && npc.ai[1] == 0f)
            {
                num8 = 0.02f;
            }
            if (npc.ai[0] == 0f && npc.ai[1] == 2f && npc.ai[2] > 40f)
            {
                num8 = 0.05f;
            }
            if (npc.ai[0] == 3f && npc.ai[1] == 0f)
            {
                num8 = 0.05f;
            }
            if (npc.ai[0] == 3f && npc.ai[1] == 2f && npc.ai[2] > 40f)
            {
                num8 = 0.08f;
            }
            if (npc.ai[0] == 3f && npc.ai[1] == 4f && npc.ai[2] > num4)
            {
                num8 = 0.15f;
            }
            if (npc.ai[0] == 3f && npc.ai[1] == 5f)
            {
                num8 = 0.05f;
            }
            if (Main.expertMode)
            {
                num8 *= 1.5f;
            }
            if (flag3 && Main.expertMode)
            {
                num8 = 0f;
            }
            if (npc.rotation < num7)
            {
                if ((double)(num7 - npc.rotation) > 3.1415)
                {
                    npc.rotation -= num8;
                }
                else
                {
                    npc.rotation += num8;
                }
            }
            else if (npc.rotation > num7)
            {
                if ((double)(npc.rotation - num7) > 3.1415)
                {
                    npc.rotation += num8;
                }
                else
                {
                    npc.rotation -= num8;
                }
            }
            if (npc.rotation > num7 - num8 && npc.rotation < num7 + num8)
            {
                npc.rotation = num7;
            }
            if (npc.rotation < 0f)
            {
                npc.rotation += 6.283f;
            }
            else if ((double)npc.rotation > 6.283)
            {
                npc.rotation -= 6.283f;
            }
            if (npc.rotation > num7 - num8 && npc.rotation < num7 + num8)
            {
                npc.rotation = num7;
            }
            if (Main.rand.Next(5) == 0)
            {
                int num9 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f), npc.width, (int)((float)npc.height * 0.5f), DustID.Blood, npc.velocity.X, 2f, 0, default(Color), 1f);
                ptr = ref Main.dust[num9].velocity.X;
                ptr *= 0.5f;
                ptr = ref Main.dust[num9].velocity.Y;
                ptr *= 0.1f;
            }
            npc.reflectsProjectiles = false;
            if (Main.IsItDay() || dead)
            {
                ptr = ref npc.velocity.Y;
                ptr -= 0.04f;
                npc.EncourageDespawn(10);
                return;
            }
            if (npc.ai[0] == 0f)
            {
                if (npc.ai[1] == 0f)
                {
                    float num10 = 5f;
                    float num11 = 0.04f;
                    if (Main.expertMode)
                    {
                        num11 = 0.15f;
                        num10 = 7f;
                    }
                    if (Main.getGoodWorld)
                    {
                        num11 += 0.05f;
                        num10 += 1f;
                    }
                    Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num12 = player.position.X + (float)(player.width / 2) - vector.X;
                    float num13 = player.position.Y + (float)(player.height / 2) - 200f - vector.Y;
                    float num14 = (float)Math.Sqrt((double)(num12 * num12 + num13 * num13));
                    float num15 = num14;
                    num14 = num10 / num14;
                    num12 *= num14;
                    num13 *= num14;
                    if (npc.velocity.X < num12)
                    {
                        ptr = ref npc.velocity.X;
                        ptr += num11;
                        if (npc.velocity.X < 0f && num12 > 0f)
                        {
                            ptr = ref npc.velocity.X;
                            ptr += num11;
                        }
                    }
                    else if (npc.velocity.X > num12)
                    {
                        ptr = ref npc.velocity.X;
                        ptr -= num11;
                        if (npc.velocity.X > 0f && num12 < 0f)
                        {
                            ptr = ref npc.velocity.X;
                            ptr -= num11;
                        }
                    }
                    if (npc.velocity.Y < num13)
                    {
                        ptr = ref npc.velocity.Y;
                        ptr += num11;
                        if (npc.velocity.Y < 0f && num13 > 0f)
                        {
                            ptr = ref npc.velocity.Y;
                            ptr += num11;
                        }
                    }
                    else if (npc.velocity.Y > num13)
                    {
                        ptr = ref npc.velocity.Y;
                        ptr -= num11;
                        if (npc.velocity.Y > 0f && num13 < 0f)
                        {
                            ptr = ref npc.velocity.Y;
                            ptr -= num11;
                        }
                    }
                    ptr = ref npc.ai[2];
                    ptr += 1f;
                    float num16 = 600f;
                    if (Main.expertMode)
                    {
                        num16 *= 0.35f;
                    }
                    if (npc.ai[2] >= num16)
                    {
                        npc.ai[1] = 1f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        npc.target = 255;
                        npc.netUpdate = true;
                    }
                    else if ((npc.position.Y + (float)npc.height < player.position.Y && num15 < 500f) || (Main.expertMode && num15 < 500f))
                    {
                        if (!player.dead)
                        {
                            ptr = ref npc.ai[3];
                            ptr += 1f;
                        }
                        float num17 = 110f;
                        if (Main.expertMode)
                        {
                            num17 *= 0.4f;
                        }
                        if (Main.getGoodWorld)
                        {
                            num17 *= 0.8f;
                        }
                        if (npc.ai[3] >= num17)
                        {
                            npc.ai[3] = 0f;
                            npc.rotation = num7;
                            float num18 = 5f;
                            if (Main.expertMode)
                            {
                                num18 = 6f;
                            }
                            float num19 = player.position.X + (float)(player.width / 2) - vector.X;
                            float num20 = player.position.Y + (float)(player.height / 2) - vector.Y;
                            float num21 = (float)Math.Sqrt((double)(num19 * num19 + num20 * num20));
                            num21 = num18 / num21;
                            Vector2 vector2 = vector;
                            Vector2 vector3 = default(Vector2);
                            vector3.X = num19 * num21;
                            vector3.Y = num20 * num21;
                            vector2.X += vector3.X * 10f;
                            vector2.Y += vector3.Y * 10f;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                int num22 = NPC.NewNPC(npc.GetSource_FromAI(), (int)vector2.X, (int)vector2.Y, NPCID.ServantofCthulhu, 0, 0f, 0f, 0f, 0f, 255);
                                Main.npc[num22].velocity.X = vector3.X;
                                Main.npc[num22].velocity.Y = vector3.Y;
                                if (Main.netMode == NetmodeID.Server && num22 < 200)
                                {
                                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num22, 0f, 0f, 0f, 0, 0, 0);
                                }
                            }
                            SoundEngine.PlaySound(SoundID.NPCHit1, vector2);
                            int num1598;
                            for (int m = 0; m < 10; m = num1598 + 1)
                            {
                                Dust.NewDust(vector2, 20, 20, DustID.Blood, vector3.X * 0.4f, vector3.Y * 0.4f, 0, default(Color), 1f);
                                num1598 = m;
                            }
                        }
                    }
                }
                else if (npc.ai[1] == 1f)
                {
                    npc.rotation = num7;
                    float num23 = 6f;
                    if (Main.expertMode)
                    {
                        num23 = 7f;
                    }
                    if (Main.getGoodWorld)
                    {
                        num23 += 1f;
                    }
                    Vector2 vector4 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num24 = player.position.X + (float)(player.width / 2) - vector4.X;
                    float num25 = player.position.Y + (float)(player.height / 2) - vector4.Y;
                    float num26 = (float)Math.Sqrt((double)(num24 * num24 + num25 * num25));
                    num26 = num23 / num26;
                    npc.velocity.X = num24 * num26;
                    npc.velocity.Y = num25 * num26;
                    npc.ai[1] = 2f;
                    npc.netUpdate = true;
                    if (npc.netSpam > 10)
                    {
                        npc.netSpam = 10;
                    }
                }
                else if (npc.ai[1] == 2f)
                {
                    ptr = ref npc.ai[2];
                    ptr += 1f;
                    if (npc.ai[2] >= 40f)
                    {
                        npc.velocity *= 0.98f;
                        if (Main.expertMode)
                        {
                            npc.velocity *= 0.985f;
                        }
                        if (Main.getGoodWorld)
                        {
                            npc.velocity *= 0.99f;
                        }
                        if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                        {
                            npc.velocity.X = 0f;
                        }
                        if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                        {
                            npc.velocity.Y = 0f;
                        }
                    }
                    else
                    {
                        npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f;
                    }
                    int num27 = 150;
                    if (Main.expertMode)
                    {
                        num27 = 100;
                    }
                    if (Main.getGoodWorld)
                    {
                        num27 -= 15;
                    }
                    if (npc.ai[2] >= (float)num27)
                    {
                        ptr = ref npc.ai[3];
                        ptr += 1f;
                        npc.ai[2] = 0f;
                        npc.target = 255;
                        npc.rotation = num7;
                        if (npc.ai[3] >= 3f)
                        {
                            npc.ai[1] = 0f;
                            npc.ai[3] = 0f;
                        }
                        else
                        {
                            npc.ai[1] = 1f;
                        }
                    }
                }
                float num28 = 0.5f;
                if (Main.expertMode)
                {
                    num28 = 0.65f;
                }
                if ((float)npc.life < (float)npc.lifeMax * num28)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    if (npc.netSpam > 10)
                    {
                        npc.netSpam = 10;
                    }
                }
                return;
            }
            if (npc.ai[0] == 1f || npc.ai[0] == 2f)
            {
                if (npc.ai[0] == 1f || npc.ai[3] == 1f)
                {
                    ptr = ref npc.ai[2];
                    ptr += 0.005f;
                    if ((double)npc.ai[2] > 0.5)
                    {
                        npc.ai[2] = 0.5f;
                    }
                }
                else
                {
                    ptr = ref npc.ai[2];
                    ptr -= 0.005f;
                    if (npc.ai[2] < 0f)
                    {
                        npc.ai[2] = 0f;
                    }
                }
                npc.rotation += npc.ai[2];
                ptr = ref npc.ai[1];
                ptr += 1f;
                if (Main.getGoodWorld)
                {
                    npc.reflectsProjectiles = true;
                }
                int num29 = 20;
                if (Main.getGoodWorld && npc.life < npc.lifeMax / 3)
                {
                    num29 = 10;
                }
                if (Main.expertMode && npc.ai[1] % (float)num29 == 0f)
                {
                    float num30 = 5f;
                    Vector2 vector5 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num31 = (float)Main.rand.Next(-200, 200);
                    float num32 = (float)Main.rand.Next(-200, 200);
                    if (Main.getGoodWorld)
                    {
                        num31 *= 3f;
                        num32 *= 3f;
                    }
                    float num33 = (float)Math.Sqrt((double)(num31 * num31 + num32 * num32));
                    num33 = num30 / num33;
                    Vector2 vector6 = vector5;
                    Vector2 vector7 = default(Vector2);
                    vector7.X = num31 * num33;
                    vector7.Y = num32 * num33;
                    vector6.X += vector7.X * 10f;
                    vector6.Y += vector7.Y * 10f;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int num34 = NPC.NewNPC(npc.GetSource_FromAI(), (int)vector6.X, (int)vector6.Y, NPCID.ServantofCthulhu, 0, 0f, 0f, 0f, 0f, 255);
                        Main.npc[num34].velocity.X = vector7.X;
                        Main.npc[num34].velocity.Y = vector7.Y;
                        if (Main.netMode == NetmodeID.Server && num34 < 200)
                        {
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num34, 0f, 0f, 0f, 0, 0, 0);
                        }
                    }
                    int num1598;
                    for (int n = 0; n < 10; n = num1598 + 1)
                    {
                        Dust.NewDust(vector6, 20, 20, DustID.Blood, vector7.X * 0.4f, vector7.Y * 0.4f, 0, default(Color), 1f);
                        num1598 = n;
                    }
                }
                if (npc.ai[1] >= 100f)
                {
                    if (npc.ai[3] == 1f)
                    {
                        npc.ai[3] = 0f;
                        npc.ai[1] = 0f;
                    }
                    else
                    {
                        ptr = ref npc.ai[0];
                        ptr += 1f;
                        npc.ai[1] = 0f;
                        if (npc.ai[0] == 3f)
                        {
                            npc.ai[2] = 0f;
                        }
                        else
                        {
                            SoundEngine.PlaySound(SoundID.NPCHit1, npc.position);
                            int num1598;
                            for (int num35 = 0; num35 < 2; num35 = num1598 + 1)
                            {
                                Gore.NewGore(npc.GetSource_FromAI(), npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 8, 1f);
                                Gore.NewGore(npc.GetSource_FromAI(), npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7, 1f);
                                Gore.NewGore(npc.GetSource_FromAI(), npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6, 1f);
                                num1598 = num35;
                            }
                            for (int num36 = 0; num36 < 20; num36 = num1598 + 1)
                            {
                                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
                                num1598 = num36;
                            }
                            SoundEngine.PlaySound(SoundID.Roar, npc.position);
                        }
                    }
                }
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
                ptr = ref npc.velocity.X;
                ptr *= 0.98f;
                ptr = ref npc.velocity.Y;
                ptr *= 0.98f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                {
                    npc.velocity.Y = 0f;
                }
                return;
            }
            npc.defense = 0;
            int num37 = 23;
            int num38 = 18;
            if (Main.expertMode)
            {
                if (flag2)
                {
                    npc.defense = -15;
                }
                if (flag3)
                {
                    num38 = 20;
                    npc.defense = -30;
                }
            }
            npc.damage = npc.GetAttackDamage_LerpBetweenFinalValues((float)num37, (float)num38);
            npc.damage = npc.GetAttackDamage_ScaledByStrength((float)npc.damage);
            if (npc.ai[1] == 0f && flag2)
            {
                npc.ai[1] = 5f;
            }
            if (npc.ai[1] == 0f)
            {
                float num39 = 6f;
                float num40 = 0.07f;
                Vector2 vector8 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num41 = player.position.X + (float)(player.width / 2) - vector8.X;
                float num42 = player.position.Y + (float)(player.height / 2) - 120f - vector8.Y;
                float num43 = (float)Math.Sqrt((double)(num41 * num41 + num42 * num42));
                if (num43 > 400f && Main.expertMode)
                {
                    num39 += 1f;
                    num40 += 0.05f;
                    if (num43 > 600f)
                    {
                        num39 += 1f;
                        num40 += 0.05f;
                        if (num43 > 800f)
                        {
                            num39 += 1f;
                            num40 += 0.05f;
                        }
                    }
                }
                if (Main.getGoodWorld)
                {
                    num39 += 1f;
                    num40 += 0.1f;
                }
                num43 = num39 / num43;
                num41 *= num43;
                num42 *= num43;
                if (npc.velocity.X < num41)
                {
                    ptr = ref npc.velocity.X;
                    ptr += num40;
                    if (npc.velocity.X < 0f && num41 > 0f)
                    {
                        ptr = ref npc.velocity.X;
                        ptr += num40;
                    }
                }
                else if (npc.velocity.X > num41)
                {
                    ptr = ref npc.velocity.X;
                    ptr -= num40;
                    if (npc.velocity.X > 0f && num41 < 0f)
                    {
                        ptr = ref npc.velocity.X;
                        ptr -= num40;
                    }
                }
                if (npc.velocity.Y < num42)
                {
                    ptr = ref npc.velocity.Y;
                    ptr += num40;
                    if (npc.velocity.Y < 0f && num42 > 0f)
                    {
                        ptr = ref npc.velocity.Y;
                        ptr += num40;
                    }
                }
                else if (npc.velocity.Y > num42)
                {
                    ptr = ref npc.velocity.Y;
                    ptr -= num40;
                    if (npc.velocity.Y > 0f && num42 < 0f)
                    {
                        ptr = ref npc.velocity.Y;
                        ptr -= num40;
                    }
                }
                ptr = ref npc.ai[2];
                ptr += 1f;
                if (npc.ai[2] >= 200f)
                {
                    npc.ai[1] = 1f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.35)
                    {
                        npc.ai[1] = 3f;
                    }
                    npc.target = 255;
                    npc.netUpdate = true;
                }
                if (Main.expertMode && flag3)
                {
                    npc.TargetClosest(true);
                    npc.netUpdate = true;
                    npc.ai[1] = 3f;
                    npc.ai[2] = 0f;
                    ptr = ref npc.ai[3];
                    ptr -= 1000f;
                }
            }
            else if (npc.ai[1] == 1f)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, npc.position);
                npc.rotation = num7;
                float num44 = 6.8f;
                if (Main.expertMode && npc.ai[3] == 1f)
                {
                    num44 *= 1.15f;
                }
                if (Main.expertMode && npc.ai[3] == 2f)
                {
                    num44 *= 1.3f;
                }
                if (Main.getGoodWorld)
                {
                    num44 *= 1.2f;
                }
                Vector2 vector9 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num45 = player.position.X + (float)(player.width / 2) - vector9.X;
                float num46 = player.position.Y + (float)(player.height / 2) - vector9.Y;
                float num47 = (float)Math.Sqrt((double)(num45 * num45 + num46 * num46));
                num47 = num44 / num47;
                npc.velocity.X = num45 * num47;
                npc.velocity.Y = num46 * num47;
                npc.ai[1] = 2f;
                npc.netUpdate = true;
                if (npc.netSpam > 10)
                {
                    npc.netSpam = 10;
                }
            }
            else if (npc.ai[1] == 2f)
            {
                float num48 = 40f;
                ptr = ref npc.ai[2];
                ptr += 1f;
                if (Main.expertMode)
                {
                    num48 = 50f;
                }
                if (npc.ai[2] >= num48)
                {
                    npc.velocity *= 0.97f;
                    if (Main.expertMode)
                    {
                        npc.velocity *= 0.98f;
                    }
                    if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                    {
                        npc.velocity.X = 0f;
                    }
                    if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                    {
                        npc.velocity.Y = 0f;
                    }
                }
                else
                {
                    npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f;
                }
                int num49 = 130;
                if (Main.expertMode)
                {
                    num49 = 90;
                }
                if (npc.ai[2] >= (float)num49)
                {
                    ptr = ref npc.ai[3];
                    ptr += 1f;
                    npc.ai[2] = 0f;
                    npc.target = 255;
                    npc.rotation = num7;
                    if (npc.ai[3] >= 3f)
                    {
                        npc.ai[1] = 0f;
                        npc.ai[3] = 0f;
                        if (Main.expertMode && Main.netMode != NetmodeID.MultiplayerClient && (double)npc.life < (double)npc.lifeMax * 0.5)
                        {
                            npc.ai[1] = 3f;
                            ptr = ref npc.ai[3];
                            ptr += (float)Main.rand.Next(1, 4);
                        }
                        npc.netUpdate = true;
                        if (npc.netSpam > 10)
                        {
                            npc.netSpam = 10;
                        }
                    }
                    else
                    {
                        npc.ai[1] = 1f;
                    }
                }
            }
            else if (npc.ai[1] == 3f)
            {
                if (npc.ai[3] == 4f && flag2 && npc.Center.Y > player.Center.Y)
                {
                    npc.TargetClosest(true);
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    if (npc.netSpam > 10)
                    {
                        npc.netSpam = 10;
                    }
                }
                else if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.TargetClosest(true);
                    float num50 = 20f;
                    Vector2 vector10 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num51 = player.position.X + (float)(player.width / 2) - vector10.X;
                    float num52 = player.position.Y + (float)(player.height / 2) - vector10.Y;
                    float num53 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) / 4f;
                    num53 += 10f - num53;
                    if (num53 < 5f)
                    {
                        num53 = 5f;
                    }
                    if (num53 > 15f)
                    {
                        num53 = 15f;
                    }
                    if (npc.ai[2] == -1f && !flag3)
                    {
                        num53 *= 4f;
                        num50 *= 1.3f;
                    }
                    if (flag3)
                    {
                        num53 *= 2f;
                    }
                    num51 -= player.velocity.X * num53;
                    num52 -= player.velocity.Y * num53 / 4f;
                    num51 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
                    num52 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
                    if (flag3)
                    {
                        num51 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
                        num52 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
                    }
                    float num54 = (float)Math.Sqrt((double)(num51 * num51 + num52 * num52));
                    float num55 = num54;
                    num54 = num50 / num54;
                    npc.velocity.X = num51 * num54;
                    npc.velocity.Y = num52 * num54;
                    ptr = ref npc.velocity.X;
                    ptr += (float)Main.rand.Next(-20, 21) * 0.1f;
                    ptr = ref npc.velocity.Y;
                    ptr += (float)Main.rand.Next(-20, 21) * 0.1f;
                    if (flag3)
                    {
                        ptr = ref npc.velocity.X;
                        ptr += (float)Main.rand.Next(-50, 51) * 0.1f;
                        ptr = ref npc.velocity.Y;
                        ptr += (float)Main.rand.Next(-50, 51) * 0.1f;
                        float num56 = Math.Abs(npc.velocity.X);
                        float num57 = Math.Abs(npc.velocity.Y);
                        if (npc.Center.X > player.Center.X)
                        {
                            num57 *= -1f;
                        }
                        if (npc.Center.Y > player.Center.Y)
                        {
                            num56 *= -1f;
                        }
                        npc.velocity.X = num57 + npc.velocity.X;
                        npc.velocity.Y = num56 + npc.velocity.Y;
                        npc.velocity.Normalize();
                        npc.velocity *= num50;
                        ptr = ref npc.velocity.X;
                        ptr += (float)Main.rand.Next(-20, 21) * 0.1f;
                        ptr = ref npc.velocity.Y;
                        ptr += (float)Main.rand.Next(-20, 21) * 0.1f;
                    }
                    else if (num55 < 100f)
                    {
                        if (Math.Abs(npc.velocity.X) > Math.Abs(npc.velocity.Y))
                        {
                            float num58 = Math.Abs(npc.velocity.X);
                            float num59 = Math.Abs(npc.velocity.Y);
                            if (npc.Center.X > player.Center.X)
                            {
                                num59 *= -1f;
                            }
                            if (npc.Center.Y > player.Center.Y)
                            {
                                num58 *= -1f;
                            }
                            npc.velocity.X = num59;
                            npc.velocity.Y = num58;
                        }
                    }
                    else if (Math.Abs(npc.velocity.X) > Math.Abs(npc.velocity.Y))
                    {
                        float num60 = (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) / 2f;
                        float num61 = num60;
                        if (npc.Center.X > player.Center.X)
                        {
                            num61 *= -1f;
                        }
                        if (npc.Center.Y > player.Center.Y)
                        {
                            num60 *= -1f;
                        }
                        npc.velocity.X = num61;
                        npc.velocity.Y = num60;
                    }
                    npc.ai[1] = 4f;
                    npc.netUpdate = true;
                    if (npc.netSpam > 10)
                    {
                        npc.netSpam = 10;
                    }
                }
            }
            else if (npc.ai[1] == 4f)
            {
                if (npc.ai[2] == 0f)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, npc.position);
                }
                float num62 = num4;
                ptr = ref npc.ai[2];
                ptr += 1f;
                if (npc.ai[2] == num62 && Vector2.Distance(npc.position, player.position) < 200f)
                {
                    ptr = ref npc.ai[2];
                    ptr -= 1f;
                }
                if (npc.ai[2] >= num62)
                {
                    npc.velocity *= 0.95f;
                    if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                    {
                        npc.velocity.X = 0f;
                    }
                    if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                    {
                        npc.velocity.Y = 0f;
                    }
                }
                else
                {
                    npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f;
                }
                float num63 = num62 + 13f;
                if (npc.ai[2] >= num63)
                {
                    npc.netUpdate = true;
                    if (npc.netSpam > 10)
                    {
                        npc.netSpam = 10;
                    }
                    ptr = ref npc.ai[3];
                    ptr += 1f;
                    npc.ai[2] = 0f;
                    if (npc.ai[3] >= 5f)
                    {
                        npc.ai[1] = 0f;
                        npc.ai[3] = 0f;
                        if (npc.target >= 0 && Main.getGoodWorld && Collision.CanHit(npc.position, npc.width, npc.height, player.position, npc.width, npc.height))
                        {
                            SoundEngine.PlaySound(SoundID.Roar, npc.position);
                            npc.ai[0] = 2f;
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 1f;
                            npc.netUpdate = true;
                        }
                    }
                    else
                    {
                        npc.ai[1] = 3f;
                    }
                }
            }
            else if (npc.ai[1] == 5f)
            {
                float num64 = 600f;
                float num65 = 9f;
                float num66 = 0.3f;
                Vector2 vector11 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num67 = player.position.X + (float)(player.width / 2) - vector11.X;
                float num68 = player.position.Y + (float)(player.height / 2) + num64 - vector11.Y;
                float num69 = (float)Math.Sqrt((double)(num67 * num67 + num68 * num68));
                num69 = num65 / num69;
                num67 *= num69;
                num68 *= num69;
                if (npc.velocity.X < num67)
                {
                    ptr = ref npc.velocity.X;
                    ptr += num66;
                    if (npc.velocity.X < 0f && num67 > 0f)
                    {
                        ptr = ref npc.velocity.X;
                        ptr += num66;
                    }
                }
                else if (npc.velocity.X > num67)
                {
                    ptr = ref npc.velocity.X;
                    ptr -= num66;
                    if (npc.velocity.X > 0f && num67 < 0f)
                    {
                        ptr = ref npc.velocity.X;
                        ptr -= num66;
                    }
                }
                if (npc.velocity.Y < num68)
                {
                    ptr = ref npc.velocity.Y;
                    ptr += num66;
                    if (npc.velocity.Y < 0f && num68 > 0f)
                    {
                        ptr = ref npc.velocity.Y;
                        ptr += num66;
                    }
                }
                else if (npc.velocity.Y > num68)
                {
                    ptr = ref npc.velocity.Y;
                    ptr -= num66;
                    if (npc.velocity.Y > 0f && num68 < 0f)
                    {
                        ptr = ref npc.velocity.Y;
                        ptr -= num66;
                    }
                }
                ptr = ref npc.ai[2];
                ptr += 1f;
                if (npc.ai[2] >= 70f)
                {
                    npc.TargetClosest(true);
                    npc.ai[1] = 3f;
                    npc.ai[2] = -1f;
                    npc.ai[3] = (float)Main.rand.Next(-3, 1);
                    npc.netUpdate = true;
                }
            }
            if (flag3 && npc.ai[1] == 5f)
            {
                npc.ai[1] = 3f;
            }
            return;
        }
    }
}
