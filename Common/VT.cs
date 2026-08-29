#if false
//using System;
//using System.Collections.Generic;
//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.DataStructures;
//using Terraria.GameContent.Achievements;
//using Terraria.GameContent.Drawing;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Terraria.WorldBuilding;

//namespace TimeDomain.Common
//{
//    public static class VT
//    {
//        public static void Damage(Projectile projectile)
//        {
//            bool vanillaCanDamage = true;
//            if (projectile.type == 18 || projectile.type == 72 || projectile.type == 86 || projectile.type == 87 || projectile.aiStyle == 31 || projectile.aiStyle == 32 || projectile.type == 226 || projectile.type == 378 || projectile.type == 613 || projectile.type == 650 || projectile.type == 882 || projectile.type == 888 || projectile.type == 895 || projectile.type == 896 || (projectile.type == 434 && projectile.localAI[0] != 0f) || projectile.type == 439 || projectile.type == 444 || (projectile.type == 451 && ((int)(projectile.ai[0] - 1f) / projectile.penetrate == 0 || projectile.ai[1] < 5f) && projectile.ai[0] != 0f) || (projectile.type == 500 || projectile.type == 653 || projectile.type == 1018 || projectile.type == 460 || projectile.type == 633 || projectile.type == 600 || projectile.type == 601 || projectile.type == 602 || projectile.type == 535 || (projectile.type == 631 && projectile.localAI[1] == 0f) || (projectile.type == 537 && projectile.localAI[0] <= 30f) || projectile.type == 651 || (projectile.type == 188 && projectile.localAI[0] < 5f) || (projectile.aiStyle == 137 && projectile.ai[0] != 0f) || projectile.aiStyle == 138 || (projectile.type == 261 && projectile.velocity.Length() < 1.5f) || (projectile.type == 818 && projectile.ai[0] < 1f) || projectile.type == 831 || projectile.type == 970 || (projectile.type == 833 && projectile.ai[0] == 4f) || (projectile.type == 834 && projectile.ai[0] == 4f) || (projectile.type == 835 && projectile.ai[0] == 4f) || (projectile.type == 281 && projectile.ai[0] == -3f) || ((projectile.type == 598 || projectile.type == 636 || projectile.type == 614 || projectile.type == 971 || projectile.type == 975) && projectile.ai[0] == 1f)) || (projectile.type == 923 && projectile.localAI[0] <= 60f) || (projectile.type == 919 && projectile.localAI[0] <= 60f) || (projectile.aiStyle == 15 && projectile.ai[0] == 0f && projectile.localAI[1] <= 12f) || (projectile.type == 861 || (projectile.type >= 511 && projectile.type <= 513 && projectile.ai[1] >= 1f)) || (projectile.type == 1007 || (projectile.aiStyle == 93 && projectile.ai[0] != 0f && projectile.ai[0] != 2f)) || (projectile.aiStyle == 10 && projectile.localAI[1] == -1f) || (projectile.type == 85 && projectile.localAI[0] >= 54f) || (Main.projPet[projectile.type] && projectile.type != 266 && projectile.type != 407 && projectile.type != 317 && (projectile.type != 388 || projectile.ai[0] != 2f) && (projectile.type < 390 || projectile.type > 392) && (projectile.type < 393 || projectile.type > 395) && (projectile.type != 533 || projectile.ai[0] < 6f || projectile.ai[0] > 8f) && (projectile.type < 625 || projectile.type > 628) && (projectile.type != 755 || projectile.ai[0] == 0f) && (projectile.type != 946 || projectile.ai[0] == 0f) && projectile.type != 758 && projectile.type != 951 && projectile.type != 963 && (projectile.type != 759 || projectile.frame == Main.projFrames[projectile.type] - 1) && projectile.type != 833 && projectile.type != 834 && projectile.type != 835 && projectile.type != 864 && (projectile.type != 623 || projectile.ai[0] != 2f)))
//            {
//                vanillaCanDamage = false;
//            }
//            if (Main.projPet[projectile.type] && ProjectileLoader.MinionContactDamage(projectile))
//            {
//                vanillaCanDamage = true;
//            }
//            if (!ProjectileLoader.CanDamage(projectile).GetValueOrDefault(vanillaCanDamage))
//            {
//                return;
//            }
//            Rectangle rectangle = /*projectile.*/Damage_GetHitbox(projectile);
//            if (projectile.friendly && Main.getGoodWorld && (projectile.aiStyle == 16 || ProjectileID.Sets.Explosive[projectile.type]) && Main.netMode == 1 && projectile.owner != Main.myPlayer && !projectile.npcProj)
//            {
//                /*projectile.*/
//                BombsHurtPlayers(rectangle, Main.myPlayer, projectile);
//                return;
//            }
//            if (projectile.friendly && projectile.owner == Main.myPlayer && !projectile.npcProj)
//            {
//                /*projectile.*/
//                BombsHurtPlayers(rectangle, projectile.owner, projectile);
//                if (!projectile.minion)
//                {
//                    if (!(bool)ProjectileLoader.CanCutTiles(projectile))
//                    {
//                        return;
//                    }
//                }
//            }
//            int num50;
//            if (projectile.owner == Main.myPlayer)
//            {
//                float num = 1f;
//                if (ProjectileID.Sets.IsARocketThatDealsDoubleDamageToPrimaryEnemy[projectile.type] && projectile.timeLeft > 3)
//                {
//                    num *= 2f;
//                }
//                if (ProjectileID.Sets.IsAMineThatDealsTripleDamageWhenStationary[projectile.type] && projectile.velocity.Length() < 0.5f)
//                {
//                    num *= 3f;
//                }
//                if (projectile.type == 34 && projectile.penetrate == 1)
//                {
//                    num *= 1f;
//                }
//                if (projectile.aiStyle == 15 && projectile.ai[0] == 0f)
//                {
//                    num *= 1.2f;
//                }
//                if (projectile.aiStyle == 15 && (projectile.ai[0] == 1f || projectile.ai[0] == 2f))
//                {
//                    num *= 2f;
//                }
//                if (projectile.type == 877 || projectile.type == 879 || projectile.type == 878)
//                {
//                    num = 0.1f + Main.player[projectile.owner].velocity.Length() / 7f * 0.9f;
//                }
//                if (projectile.type == 968)
//                {
//                    num = 1f;
//                    switch ((int)projectile.ai[1])
//                    {
//                        case 0:
//                            num = 1.5f;
//                            break;
//                        case 1:
//                            num = 1f;
//                            break;
//                        case 2:
//                            num = 1.5f;
//                            break;
//                        case 3:
//                            num = 0.1f;
//                            break;
//                        case 4:
//                            num = 1f;
//                            break;
//                        case 5:
//                            num = 1f;
//                            break;
//                        case 6:
//                            num = 1f;
//                            break;
//                        case 7:
//                            num = 0.25f;
//                            break;
//                        case 8:
//                            num = 1f;
//                            break;
//                        case 9:
//                            num = 0.75f;
//                            break;
//                        case 10:
//                            num = 0.5f;
//                            break;
//                        case 11:
//                            num = 0.5f;
//                            break;
//                        case 12:
//                            num = 0.5f;
//                            break;
//                        case 13:
//                            num = 0.5f;
//                            break;
//                        case 14:
//                            num = 0.5f;
//                            break;
//                        case 15:
//                            num = 0.5f;
//                            break;
//                        case 16:
//                            num = 0.5f;
//                            break;
//                        case 17:
//                            num = 1.5f;
//                            break;
//                        case 18:
//                            num = 1.6f;
//                            break;
//                        case 19:
//                            num = 1.8f;
//                            break;
//                        case 20:
//                            num = 1.9f;
//                            break;
//                        case 21:
//                            num = 1.7f;
//                            break;
//                        case 22:
//                            num = 2f;
//                            break;
//                        case 23:
//                            num = 1.9f;
//                            break;
//                    }
//                }
//                if (projectile.type == 533 && projectile.localAI[2] >= 40f)
//                {
//                    num *= 0.5f;
//                }
//                bool flag = !projectile.npcProj && !projectile.trap;
//                bool flag2 = projectile.usesOwnerMeleeHitCD && flag && projectile.owner < 255;
//                bool flag3 = projectile.damage > 0;
//                if (flag3)
//                {
//                    int[] array = projectile.localNPCImmunity;
//                    if (projectile.type == 626 || projectile.type == 627 || projectile.type == 628)
//                    {
//                        Projectile projectile2 = FindStardustDragonHeadOfOwner(projectile);
//                        if (projectile != null)
//                        {
//                            array = projectile.localNPCImmunity;
//                        }
//                    }
//                    bool flag4 = true;
//                    int i = 0;
//                    while (i < 200 && flag4)
//                    {
//                        if (Main.npc[i].active)
//                        {
//                            bool flag5 = (!projectile.usesLocalNPCImmunity && !projectile.usesIDStaticNPCImmunity) || (projectile.usesLocalNPCImmunity && array[i] == 0) || (projectile.usesIDStaticNPCImmunity && Projectile.IsNPCIndexImmuneToProjectileType(projectile.type, i));
//                            if (flag2 && !Main.player[projectile.owner].CanHitNPCWithMeleeHit(i))
//                            {
//                                flag5 = false;
//                            }
//                            if ((!Main.npc[i].dontTakeDamage || NPCID.Sets.ZappingJellyfish[Main.npc[i].type]) && flag5 && (Main.npc[i].aiStyle != 112 || Main.npc[i].ai[2] <= 1f))
//                            {
//                                bool canHitFlag = false;
//                                bool? flag29 = CombinedHooks.CanHitNPCWithProj(projectile, Main.npc[i]);
//                                if (flag29 != null)
//                                {
//                                    bool b = flag29.GetValueOrDefault();
//                                    if (!b)
//                                    {
//                                        goto IL_47B6;
//                                    }
//                                    canHitFlag = true;
//                                }
//                                Main.npc[i].position += Main.npc[i].netOffset;
//                                bool flag6 = !Main.npc[i].friendly;
//                                flag6 |= (projectile.type == 318);
//                                flag6 |= (Main.npc[i].type == 22 && projectile.owner < 255 && Main.player[projectile.owner].killGuide);
//                                flag6 |= (Main.npc[i].type == 54 && projectile.owner < 255 && Main.player[projectile.owner].killClothier);
//                                if (projectile.owner < 255 && !Main.player[projectile.owner].CanNPCBeHitByPlayerOrPlayerProjectile(Main.npc[i], projectile))
//                                {
//                                    flag6 = false;
//                                }
//                                bool flag7 = Main.npc[i].friendly && !Main.npc[i].dontTakeDamageFromHostiles;
//                                if (canHitFlag || (projectile.friendly && (flag6 || NPCID.Sets.ZappingJellyfish[Main.npc[i].type])) || (projectile.hostile && flag7))
//                                {
//                                    bool flag8 = projectile.maxPenetrate == 1 && !projectile.usesLocalNPCImmunity && !projectile.usesIDStaticNPCImmunity;
//                                    if (canHitFlag)
//                                    {
//                                        flag8 = true;
//                                    }
//                                    if (projectile.owner < 0 || Main.npc[i].immune[projectile.owner] == 0 || flag8)
//                                    {
//                                        bool flag9 = false;
//                                        if (projectile.type == 11 && (Main.npc[i].type == 47 || Main.npc[i].type == 57))
//                                        {
//                                            flag9 = true;
//                                        }
//                                        else if (projectile.type == 31 && Main.npc[i].type == 69)
//                                        {
//                                            flag9 = true;
//                                        }
//                                        else if (Main.npc[i].trapImmune && projectile.trap)
//                                        {
//                                            flag9 = true;
//                                        }
//                                        else if (Main.npc[i].immortal && projectile.npcProj)
//                                        {
//                                            flag9 = true;
//                                        }
//                                        if (canHitFlag)
//                                        {
//                                            flag9 = false;
//                                        }
//                                        if (!flag9 && (Main.npc[i].noTileCollide || !projectile.ownerHitCheck || projectile.CanHitWithMeleeWeapon(Main.npc[i])))
//                                        {
//                                            bool flag10;
//                                            if (Main.npc[i].type == 414)
//                                            {
//                                                Rectangle rect = Main.npc[i].getRect();
//                                                int num2 = 8;
//                                                rect.X -= num2;
//                                                rect.Y -= num2;
//                                                rect.Width += num2 * 2;
//                                                rect.Height += num2 * 2;
//                                                flag10 = projectile.Colliding(rectangle, rect);
//                                            }
//                                            else
//                                            {
//                                                flag10 = projectile.Colliding(rectangle, Main.npc[i].getRect());
//                                            }
//                                            if (flag10)
//                                            {
//                                                NPC nPC = Main.npc[i];
//                                                if (NPCID.Sets.ZappingJellyfish[nPC.type])
//                                                {
//                                                    if ((nPC.dontTakeDamage || !Main.player[projectile.owner].CanNPCBeHitByPlayerOrPlayerProjectile(nPC, projectile)) && (projectile.aiStyle == 19 || projectile.aiStyle == 161 || projectile.aiStyle == 75 || projectile.aiStyle == 140 || ProjectileID.Sets.IsAWhip[projectile.type] || ProjectileID.Sets.AllowsContactDamageFromJellyfish[projectile.type]))
//                                                    {
//                                                        Main.player[projectile.owner].TakeDamageFromJellyfish(i);
//                                                    }
//                                                    if (nPC.dontTakeDamage || !flag6)
//                                                    {
//                                                        goto IL_47B6;
//                                                    }
//                                                }
//                                                if (projectile.type == 876)
//                                                {
//                                                    Vector2 vector = projectile.position;
//                                                    if (Main.rand.Next(20) == 0)
//                                                    {
//                                                        projectile.tileCollide = false;
//                                                        projectile.position.X = projectile.position.X + (float)Main.rand.Next(-256, 257);
//                                                    }
//                                                    if (Main.rand.Next(20) == 0)
//                                                    {
//                                                        projectile.tileCollide = false;
//                                                        projectile.position.Y = projectile.position.Y + (float)Main.rand.Next(-256, 257);
//                                                    }
//                                                    if (Main.rand.Next(2) == 0)
//                                                    {
//                                                        projectile.tileCollide = false;
//                                                    }
//                                                    if (Main.rand.Next(3) != 0)
//                                                    {
//                                                        vector = projectile.position;
//                                                        projectile.position -= projectile.velocity * (float)Main.rand.Next(0, 40);
//                                                        if (projectile.tileCollide && Collision.SolidTiles(projectile.position, projectile.width, projectile.height))
//                                                        {
//                                                            projectile.position = vector;
//                                                            projectile.position -= projectile.velocity * (float)Main.rand.Next(0, 40);
//                                                            if (projectile.tileCollide && Collision.SolidTiles(projectile.position, projectile.width, projectile.height))
//                                                            {
//                                                                projectile.position = vector;
//                                                            }
//                                                        }
//                                                    }
//                                                    projectile.velocity *= 0.6f;
//                                                    if (Main.rand.Next(7) == 0)
//                                                    {
//                                                        projectile.velocity.X = projectile.velocity.X + (float)Main.rand.Next(30, 31) * 0.01f;
//                                                    }
//                                                    if (Main.rand.Next(7) == 0)
//                                                    {
//                                                        projectile.velocity.Y = projectile.velocity.Y + (float)Main.rand.Next(30, 31) * 0.01f;
//                                                    }
//                                                    projectile.damage = (int)((double)projectile.damage * 0.9);
//                                                    projectile.knockBack *= 0.9f;
//                                                    if (Main.rand.Next(20) == 0)
//                                                    {
//                                                        projectile.knockBack *= 10f;
//                                                    }
//                                                    if (Main.rand.Next(50) == 0)
//                                                    {
//                                                        projectile.damage *= 10;
//                                                    }
//                                                    if (Main.rand.Next(7) == 0)
//                                                    {
//                                                        vector = projectile.position;
//                                                        projectile.position.X = projectile.position.X + (float)Main.rand.Next(-64, 65);
//                                                        if (projectile.tileCollide && Collision.SolidTiles(projectile.position, projectile.width, projectile.height))
//                                                        {
//                                                            projectile.position = vector;
//                                                        }
//                                                    }
//                                                    if (Main.rand.Next(7) == 0)
//                                                    {
//                                                        vector = projectile.position;
//                                                        projectile.position.Y = projectile.position.Y + (float)Main.rand.Next(-64, 65);
//                                                        if (projectile.tileCollide && Collision.SolidTiles(projectile.position, projectile.width, projectile.height))
//                                                        {
//                                                            projectile.position = vector;
//                                                        }
//                                                    }
//                                                    if (Main.rand.Next(14) == 0)
//                                                    {
//                                                        projectile.velocity.X = projectile.velocity.X * -1f;
//                                                    }
//                                                    if (Main.rand.Next(14) == 0)
//                                                    {
//                                                        projectile.velocity.Y = projectile.velocity.Y * -1f;
//                                                    }
//                                                    if (Main.rand.Next(10) == 0)
//                                                    {
//                                                        projectile.velocity *= (float)Main.rand.Next(1, 201) * 0.0005f;
//                                                    }
//                                                    if (projectile.tileCollide)
//                                                    {
//                                                        projectile.ai[1] = 0f;
//                                                    }
//                                                    else
//                                                    {
//                                                        projectile.ai[1] = 1f;
//                                                    }
//                                                    projectile.netUpdate = true;
//                                                }
//                                                bool flag11 = nPC.reflectsProjectiles;
//                                                if (Main.getGoodWorld && NPCID.Sets.ReflectStarShotsInForTheWorthy[Main.npc[i].type] && (projectile.type == 955 || projectile.type == 728))
//                                                {
//                                                    flag11 = true;
//                                                }
//                                                if (flag11 && projectile.CanBeReflected() && nPC.CanReflectProjectile(projectile))
//                                                {
//                                                    nPC.ReflectProjectile(projectile);
//                                                    Main.npc[i].position -= Main.npc[i].netOffset;
//                                                    return;
//                                                }
//                                                if (projectile.type == 604)
//                                                {
//                                                    Main.player[projectile.owner].Counterweight(nPC.Center, projectile.damage, projectile.knockBack);
//                                                }
//                                                NPC.HitModifiers modifiers = nPC.GetIncomingStrikeModifiers(projectile.DamageType, projectile.direction, false);
//                                                modifiers.ArmorPenetration += (float)projectile.ArmorPenetration;
//                                                CombinedHooks.ModifyHitNPCWithProj(projectile, nPC, ref modifiers);
//                                                float num3 = projectile.knockBack;
//                                                bool flag12 = false;
//                                                int num4 = (int)(Main.player[projectile.owner].GetArmorPenetration(DamageClass.Generic));
//                                                float armorPenetrationPercent = 0f;
//                                                bool flag13 = false;
//                                                num50 = projectile.type;
//                                                if (num50 <= 595)
//                                                {
//                                                    if (num50 <= 410)
//                                                    {
//                                                        if (num50 <= 152)
//                                                        {
//                                                            if (num50 != 85)
//                                                            {
//                                                                if (num50 - 150 <= 2)
//                                                                {
//                                                                    num4 += 10;
//                                                                }
//                                                            }
//                                                            else
//                                                            {
//                                                                num4 += 15;
//                                                            }
//                                                        }
//                                                        else if (num50 != 189)
//                                                        {
//                                                            if (num50 == 410)
//                                                            {
//                                                                if (Main.remixWorld)
//                                                                {
//                                                                    num4 += 20;
//                                                                }
//                                                            }
//                                                        }
//                                                        else
//                                                        {
//                                                            num4 += 10;
//                                                            if (flag && Main.player[projectile.owner].strongBees)
//                                                            {
//                                                                modifiers.ArmorPenetration += 5f;
//                                                            }
//                                                        }
//                                                    }
//                                                    else if (num50 <= 494)
//                                                    {
//                                                        if (num50 != 442)
//                                                        {
//                                                            if (num50 - 493 <= 1)
//                                                            {
//                                                                num4 += 10;
//                                                            }
//                                                        }
//                                                        else
//                                                        {
//                                                            flag13 = true;
//                                                        }
//                                                    }
//                                                    else if (num50 != 532)
//                                                    {
//                                                        if (num50 == 595)
//                                                        {
//                                                            num4 += 20;
//                                                        }
//                                                    }
//                                                    else
//                                                    {
//                                                        num4 += 25;
//                                                    }
//                                                }
//                                                else if (num50 <= 916)
//                                                {
//                                                    if (num50 <= 864)
//                                                    {
//                                                        if (num50 - 723 > 3)
//                                                        {
//                                                            if (num50 == 864)
//                                                            {
//                                                                num4 += 25;
//                                                            }
//                                                        }
//                                                        else
//                                                        {
//                                                            num4 += 25;
//                                                        }
//                                                    }
//                                                    else if (num50 - 877 > 2)
//                                                    {
//                                                        if (num50 == 916)
//                                                        {
//                                                            num4 += 50;
//                                                        }
//                                                    }
//                                                    else
//                                                    {
//                                                        num3 *= Main.player[projectile.owner].velocity.Length() / 7f;
//                                                    }
//                                                }
//                                                else if (num50 <= 963)
//                                                {
//                                                    if (num50 != 917)
//                                                    {
//                                                        if (num50 == 963)
//                                                        {
//                                                            num3 *= 0.25f;
//                                                        }
//                                                    }
//                                                    else
//                                                    {
//                                                        num4 += 30;
//                                                    }
//                                                }
//                                                else if (num50 != 964)
//                                                {
//                                                    if (num50 != 969)
//                                                    {
//                                                        switch (num50)
//                                                        {
//                                                            case 974:
//                                                                num4 += 5;
//                                                                break;
//                                                            case 976:
//                                                                num4 += 20;
//                                                                break;
//                                                            case 977:
//                                                                num4 += 5;
//                                                                break;
//                                                        }
//                                                    }
//                                                    else
//                                                    {
//                                                        num4 += 10;
//                                                    }
//                                                }
//                                                else
//                                                {
//                                                    num4 += 20;
//                                                }
//                                                if (flag13)
//                                                {
//                                                    projectile.Kill();
//                                                    return;
//                                                }
//                                                modifiers.SourceDamage *= num;
//                                                float num5 = 1000f;
//                                                int num6 = 0;
//                                                if (projectile.type > 0 && ProjectileID.Sets.StardustDragon[projectile.type])
//                                                {
//                                                    float value = (projectile.scale - 1f) * 100f;
//                                                    value = Utils.Clamp<float>(value, 0f, 50f);
//                                                    num5 = (float)((int)(num5 * (1f + value * 0.23f)));
//                                                }
//                                                if (projectile.type > 0 && projectile.type < (int)ProjectileID.Count && ProjectileID.Sets.StormTiger[projectile.type])
//                                                {
//                                                    int num7 = Math.Max(0, Main.player[projectile.owner].ownedProjectileCounts[831] - 1);
//                                                    num5 = (float)((int)(num5 * (1f + (float)num7 * 0.4f)));
//                                                }
//                                                if (projectile.type == 818)
//                                                {
//                                                    int num8 = Math.Max(0, Main.player[projectile.owner].ownedProjectileCounts[831] - 1);
//                                                    num5 = (float)((int)(num5 * (1.5f + (float)num8 * 0.4f)));
//                                                }
//                                                if (projectile.type == 963)
//                                                {
//                                                    int num9 = Math.Max(0, Main.player[projectile.owner].ownedProjectileCounts[970] - 1);
//                                                    int num10 = 3 + num9 / 2;
//                                                    if (CountEnemiesWhoAreImmuneToMeRightNow(num10, projectile) >= num10)
//                                                    {
//                                                        return;
//                                                    }
//                                                    float num11 = 0.55f;
//                                                    if (Main.hardMode)
//                                                    {
//                                                        num11 = 1.3f;
//                                                    }
//                                                    num5 = (float)((int)(num5 * (1f + (float)num9 * num11)));
//                                                }
//                                                if (flag && projectile.type == 189 && Main.player[projectile.owner].strongBees)
//                                                {
//                                                    modifiers.SourceDamage.Base = modifiers.SourceDamage.Base + 5f;
//                                                }
//                                                if (flag)
//                                                {
//                                                    if (projectile.DamageType.UseStandardCritCalcs && Main.rand.Next(100) < projectile.CritChance)
//                                                    {
//                                                        flag12 = true;
//                                                    }
//                                                    if (projectile.type - 688 <= 2)
//                                                    {
//                                                        if (Main.player[projectile.owner].setMonkT3)
//                                                        {
//                                                            if (Main.rand.Next(4) == 0)
//                                                            {
//                                                                flag12 = true;
//                                                            }
//                                                        }
//                                                        else if (Main.player[projectile.owner].setMonkT2 && Main.rand.Next(6) == 0)
//                                                        {
//                                                            flag12 = true;
//                                                        }
//                                                    }
//                                                }
//                                                modifiers.SourceDamage *= num5 / 1000f;
//                                                num5 = modifiers.SourceDamage.ApplyTo((float)projectile.damage);
//                                                float num12 = ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type];
//                                                if (flag && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type] || projectile.sentry || ProjectileID.Sets.SentryShot[projectile.type]))
//                                                {
//                                                    bool flag14 = false;
//                                                    bool flag15 = false;
//                                                    bool flag16 = false;
//                                                    bool flag17 = false;
//                                                    bool flag18 = false;
//                                                    bool flag19 = false;
//                                                    bool flag20 = false;
//                                                    bool flag21 = false;
//                                                    bool flag22 = false;
//                                                    for (int j = 0; j < NPC.maxBuffs; j++)
//                                                    {
//                                                        if (nPC.buffTime[j] >= 1)
//                                                        {
//                                                            num50 = nPC.buffType[j];
//                                                            switch (num50)
//                                                            {
//                                                                case 307:
//                                                                    flag14 = true;
//                                                                    break;
//                                                                case 308:
//                                                                case 311:
//                                                                case 312:
//                                                                case 314:
//                                                                case 317:
//                                                                case 318:
//                                                                    break;
//                                                                case 309:
//                                                                    flag15 = true;
//                                                                    break;
//                                                                case 310:
//                                                                    flag17 = true;
//                                                                    break;
//                                                                case 313:
//                                                                    flag16 = true;
//                                                                    break;
//                                                                case 315:
//                                                                    flag18 = true;
//                                                                    break;
//                                                                case 316:
//                                                                    flag22 = true;
//                                                                    break;
//                                                                case 319:
//                                                                    flag21 = true;
//                                                                    break;
//                                                                default:
//                                                                    if (num50 != 326)
//                                                                    {
//                                                                        if (num50 == 340)
//                                                                        {
//                                                                            flag20 = true;
//                                                                        }
//                                                                    }
//                                                                    else
//                                                                    {
//                                                                        flag19 = true;
//                                                                    }
//                                                                    break;
//                                                            }
//                                                        }
//                                                    }
//                                                    if (flag14)
//                                                    {
//                                                        num6 += 4;
//                                                    }
//                                                    if (flag18)
//                                                    {
//                                                        num6 += 6;
//                                                    }
//                                                    if (flag19)
//                                                    {
//                                                        num6 += 7;
//                                                    }
//                                                    if (flag20)
//                                                    {
//                                                        num6 += 6;
//                                                    }
//                                                    if (flag15)
//                                                    {
//                                                        num6 += 9;
//                                                    }
//                                                    if (flag21)
//                                                    {
//                                                        num6 += 8;
//                                                        if (Main.rand.Next(100) < 12)
//                                                        {
//                                                            flag12 = true;
//                                                        }
//                                                    }
//                                                    if (flag17)
//                                                    {
//                                                        int num13 = 10;
//                                                        num6 += num13;
//                                                        int num14 = Projectile.NewProjectile(projectile.GetSource_FromThis(), nPC.Center, Vector2.Zero, 916, (int)((float)num13 * num12), 0f, projectile.owner, 0f, 0f, 0f);
//                                                        Main.projectile[num14].localNPCImmunity[i] = -1;
//                                                        Projectile.EmitBlackLightningParticles(nPC);
//                                                    }
//                                                    if (flag22)
//                                                    {
//                                                        int num15 = 20;
//                                                        num6 += num15;
//                                                        if (Main.rand.Next(10) == 0)
//                                                        {
//                                                            flag12 = true;
//                                                        }
//                                                        ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.RainbowRodHit, new ParticleOrchestraSettings
//                                                        {
//                                                            PositionInWorld = projectile.Center
//                                                        }, null);
//                                                    }
//                                                    if (flag16)
//                                                    {
//                                                        nPC.RequestBuffRemoval(313);
//                                                        int num16 = (int)(num5 * 1.75f);
//                                                        int num17 = Projectile.NewProjectile(projectile.GetSource_FromThis(), nPC.Center, Vector2.Zero, 918, num16, 0f, projectile.owner, 0f, 0f, 0f);
//                                                        Main.projectile[num17].localNPCImmunity[i] = -1;
//                                                        modifiers.ScalingBonusDamage += 1.75f * num12;
//                                                    }
//                                                }
//                                                num6 = (int)((float)num6 * num12);
//                                                modifiers.FlatBonusDamage += (float)num6;
//                                                if (flag)
//                                                {
//                                                    float luck = Main.player[projectile.owner].luck;
//                                                }
//                                                float num18 = 1000f;
//                                                if (projectile.type == 1002)
//                                                {
//                                                    num18 /= 2f;
//                                                }
//                                                if (projectile.trap && NPCID.Sets.BelongsToInvasionOldOnesArmy[nPC.type])
//                                                {
//                                                    num18 /= 2f;
//                                                }
//                                                if (projectile.type == 482 && (nPC.aiStyle == 6 || nPC.aiStyle == 37))
//                                                {
//                                                    num18 /= 2f;
//                                                }
//                                                if (flag)
//                                                {
//                                                    Vector2 positionInWorld = Main.rand.NextVector2FromRectangle(nPC.Hitbox);
//                                                    ParticleOrchestraSettings settings = new ParticleOrchestraSettings
//                                                    {
//                                                        PositionInWorld = positionInWorld
//                                                    };
//                                                    num50 = projectile.type;
//                                                    if (num50 != 972)
//                                                    {
//                                                        if (num50 != 973)
//                                                        {
//                                                            switch (num50)
//                                                            {
//                                                                case 982:
//                                                                    ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.Excalibur, settings, new int?(projectile.owner));
//                                                                    break;
//                                                                case 983:
//                                                                    ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TrueExcalibur, settings, new int?(projectile.owner));
//                                                                    break;
//                                                                case 984:
//                                                                case 985:
//                                                                    settings.MovementVector = projectile.velocity;
//                                                                    ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TerraBlade, settings, new int?(projectile.owner));
//                                                                    break;
//                                                            }
//                                                        }
//                                                        else
//                                                        {
//                                                            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TrueNightsEdge, settings, new int?(projectile.owner));
//                                                        }
//                                                    }
//                                                    else
//                                                    {
//                                                        ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.NightsEdge, settings, new int?(projectile.owner));
//                                                    }
//                                                }
//                                                if (projectile.type == 604)
//                                                {
//                                                    projectile.friendly = false;
//                                                    projectile.ai[1] = 1000f;
//                                                }
//                                                if ((projectile.type == 400 || projectile.type == 401 || projectile.type == 402) && nPC.type >= 13 && nPC.type <= 15)
//                                                {
//                                                    num18 = (float)((int)((double)num18 * 0.65));
//                                                    if (projectile.penetrate > 1)
//                                                    {
//                                                        projectile.penetrate--;
//                                                    }
//                                                }
//                                                Point point;
//                                                if (projectile.type == 710 && !WorldUtils.Find(projectile.Center.ToTileCoordinates(), Searches.Chain(new Searches.Down(12), new GenCondition[]
//                                                {
//                                                    new Conditions.NotNull(),
//                                                    new Conditions.IsSolid()
//                                                }), out point))
//                                                {
//                                                    num18 = (float)((int)(num18 * 1.5f));
//                                                }
//                                                if (projectile.type == 504 || projectile.type == 954 || projectile.type == 979)
//                                                {
//                                                    float num19 = (60f - projectile.ai[0]) / 2f;
//                                                    projectile.ai[0] += num19;
//                                                }
//                                                if (projectile.aiStyle == 3 && projectile.type != 301 && projectile.type != 866 && projectile.type != 902)
//                                                {
//                                                    if (projectile.ai[0] == 0f)
//                                                    {
//                                                        if (projectile.type == 106)
//                                                        {
//                                                            LightDisc_Bounce(projectile.Center + projectile.velocity.SafeNormalize(Vector2.UnitX) * 8f, (-projectile.velocity).SafeNormalize(Vector2.UnitX), projectile);
//                                                        }
//                                                        projectile.velocity.X = 0f - projectile.velocity.X;
//                                                        projectile.velocity.Y = 0f - projectile.velocity.Y;
//                                                        projectile.netUpdate = true;
//                                                    }
//                                                    projectile.ai[0] = 1f;
//                                                }
//                                                else if (projectile.type == 951)
//                                                {
//                                                    Vector2 vector2 = (nPC.Center - projectile.Center).SafeNormalize(Vector2.Zero);
//                                                    vector2.X += (-0.5f + Main.rand.NextFloat()) * 13f;
//                                                    vector2.Y = -5f;
//                                                    projectile.velocity.X = vector2.X;
//                                                    projectile.velocity.Y = vector2.Y;
//                                                    projectile.netUpdate = true;
//                                                }
//                                                else if (projectile.type == 582 || projectile.type == 902)
//                                                {
//                                                    if (projectile.ai[0] != 0f)
//                                                    {
//                                                        projectile.direction *= -1;
//                                                    }
//                                                }
//                                                else if (projectile.type == 612 || projectile.type == 953 || projectile.type == 978)
//                                                {
//                                                    projectile.direction = Main.player[projectile.owner].direction;
//                                                }
//                                                else if (projectile.type == 624)
//                                                {
//                                                    float num20 = 1f;
//                                                    if (nPC.knockBackResist > 0f)
//                                                    {
//                                                        num20 = 1f / nPC.knockBackResist;
//                                                    }
//                                                    projectile.knockBack = 4f * num20;
//                                                    num3 = projectile.knockBack;
//                                                    if (nPC.Center.X < projectile.Center.X)
//                                                    {
//                                                        projectile.direction = 1;
//                                                    }
//                                                    else
//                                                    {
//                                                        projectile.direction = -1;
//                                                    }
//                                                }
//                                                else if (projectile.aiStyle == 16 || ProjectileID.Sets.Explosive[projectile.type])
//                                                {
//                                                    if (projectile.timeLeft > 3)
//                                                    {
//                                                        projectile.timeLeft = 3;
//                                                    }
//                                                    if (nPC.position.X + (float)(nPC.width / 2) < projectile.position.X + (float)(projectile.width / 2))
//                                                    {
//                                                        projectile.direction = -1;
//                                                    }
//                                                    else
//                                                    {
//                                                        projectile.direction = 1;
//                                                    }
//                                                }
//                                                else if (projectile.aiStyle == 68)
//                                                {
//                                                    if (projectile.timeLeft > 3)
//                                                    {
//                                                        projectile.timeLeft = 3;
//                                                    }
//                                                    if (nPC.position.X + (float)(nPC.width / 2) < projectile.position.X + (float)(projectile.width / 2))
//                                                    {
//                                                        projectile.direction = -1;
//                                                    }
//                                                    else
//                                                    {
//                                                        projectile.direction = 1;
//                                                    }
//                                                }
//                                                else if (projectile.aiStyle == 50)
//                                                {
//                                                    if (nPC.position.X + (float)(nPC.width / 2) < projectile.position.X + (float)(projectile.width / 2))
//                                                    {
//                                                        projectile.direction = -1;
//                                                    }
//                                                    else
//                                                    {
//                                                        projectile.direction = 1;
//                                                    }
//                                                }
//                                                else if (projectile.type == 908)
//                                                {
//                                                    if (nPC.position.X + (float)(nPC.width / 2) < projectile.position.X + (float)(projectile.width / 2))
//                                                    {
//                                                        projectile.direction = -1;
//                                                    }
//                                                    else
//                                                    {
//                                                        projectile.direction = 1;
//                                                    }
//                                                }
//                                                if (projectile.type == 509)
//                                                {
//                                                    int num21 = Main.rand.Next(2, 6);
//                                                    for (int k = 0; k < num21; k++)
//                                                    {
//                                                        Vector2 vector3 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
//                                                        vector3 += projectile.velocity * 3f;
//                                                        vector3.Normalize();
//                                                        vector3 *= (float)Main.rand.Next(35, 81) * 0.1f;
//                                                        int num22 = (int)((double)projectile.damage * 0.5);
//                                                        Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, vector3.X, vector3.Y, 504, num22, projectile.knockBack * 0.2f, projectile.owner, 0f, 0f, 0f);
//                                                    }
//                                                }
//                                                if ((projectile.type == 476 || projectile.type == 950) && !projectile.npcProj)
//                                                {
//                                                    float x = Main.player[projectile.owner].Center.X;
//                                                    if (nPC.Center.X < x)
//                                                    {
//                                                        projectile.direction = -1;
//                                                    }
//                                                    else
//                                                    {
//                                                        projectile.direction = 1;
//                                                    }
//                                                }
//                                                if (projectile.type == 598 || projectile.type == 636 || projectile.type == 614 || projectile.type == 971 || projectile.type == 975)
//                                                {
//                                                    projectile.ai[0] = 1f;
//                                                    projectile.ai[1] = (float)i;
//                                                    projectile.velocity = (nPC.Center - projectile.Center) * 0.75f;
//                                                    projectile.netUpdate = true;
//                                                }
//                                                if (projectile.type >= 511 && projectile.type <= 513)
//                                                {
//                                                    projectile.ai[1] += 1f;
//                                                    projectile.netUpdate = true;
//                                                }
//                                                if (projectile.type == 659)
//                                                {
//                                                    projectile.timeLeft = 0;
//                                                }
//                                                if (projectile.type == 524)
//                                                {
//                                                    projectile.netUpdate = true;
//                                                    projectile.ai[0] += 50f;
//                                                }
//                                                if ((projectile.type == 688 || projectile.type == 689 || projectile.type == 690) && nPC.type != 68 && nPC.defense < 999)
//                                                {
//                                                    armorPenetrationPercent = 1f;
//                                                }
//                                                if (projectile.aiStyle == 39)
//                                                {
//                                                    if (projectile.ai[1] == 0f)
//                                                    {
//                                                        projectile.ai[1] = (float)(i + 1);
//                                                        projectile.netUpdate = true;
//                                                    }
//                                                    if (Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) < projectile.position.X + (float)(projectile.width / 2))
//                                                    {
//                                                        projectile.direction = 1;
//                                                    }
//                                                    else
//                                                    {
//                                                        projectile.direction = -1;
//                                                    }
//                                                }
//                                                if (projectile.type == 41 && projectile.timeLeft > 1)
//                                                {
//                                                    projectile.timeLeft = 1;
//                                                }
//                                                if (projectile.aiStyle == 99)
//                                                {
//                                                    Main.player[projectile.owner].Counterweight(nPC.Center, projectile.damage, projectile.knockBack);
//                                                    if (nPC.Center.X < Main.player[projectile.owner].Center.X)
//                                                    {
//                                                        projectile.direction = -1;
//                                                    }
//                                                    else
//                                                    {
//                                                        projectile.direction = 1;
//                                                    }
//                                                    if (projectile.ai[0] >= 0f)
//                                                    {
//                                                        Vector2 vector4 = projectile.Center - nPC.Center;
//                                                        vector4.Normalize();
//                                                        float num23 = 16f;
//                                                        projectile.velocity *= -0.5f;
//                                                        projectile.velocity += vector4 * num23;
//                                                        projectile.netUpdate = true;
//                                                        projectile.localAI[0] += 20f;
//                                                        if (!Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.player[projectile.owner].position, Main.player[projectile.owner].width, Main.player[projectile.owner].height))
//                                                        {
//                                                            projectile.localAI[0] += 40f;
//                                                            num18 = (float)((int)((double)num18 * 0.75));
//                                                        }
//                                                    }
//                                                }
//                                                if (projectile.type == 856 && !Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.player[projectile.owner].position, Main.player[projectile.owner].width, Main.player[projectile.owner].height))
//                                                {
//                                                    num18 = (float)((int)((double)num18 * 0.75));
//                                                }
//                                                if (projectile.aiStyle == 93)
//                                                {
//                                                    if (projectile.ai[0] == 0f)
//                                                    {
//                                                        projectile.ai[1] = 0f;
//                                                        int num24 = -i - 1;
//                                                        projectile.ai[0] = (float)num24;
//                                                        projectile.velocity = nPC.Center - projectile.Center;
//                                                    }
//                                                    num18 = (float)((projectile.ai[0] != 2f) ? ((int)((double)num18 * 0.15)) : ((int)((double)num18 * 1.35)));
//                                                }
//                                                if (flag)
//                                                {
//                                                    int num25 = Item.NPCtoBanner(nPC.BannerID());
//                                                    if (num25 >= 0)
//                                                    {
//                                                        Main.player[Main.myPlayer].lastCreatureHit = num25;
//                                                    }
//                                                }
//                                                if (Main.netMode != 2 && flag)
//                                                {
//                                                    Main.player[projectile.owner].ApplyBannerOffenseBuff(nPC, ref modifiers);
//                                                }
//                                                if (Main.expertMode)
//                                                {
//                                                    if ((projectile.type == 30 || projectile.type == 397 || projectile.type == 517 || projectile.type == 28 || projectile.type == 37 || projectile.type == 516 || projectile.type == 29 || projectile.type == 470 || projectile.type == 637 || projectile.type == 108 || projectile.type == 281 || projectile.type == 588 || projectile.type == 519 || projectile.type == 773 || projectile.type == 183 || projectile.type == 181 || projectile.type == 566 || projectile.type == 1002) && nPC.type >= 13 && nPC.type <= 15)
//                                                    {
//                                                        num18 /= 5f;
//                                                    }
//                                                    if (projectile.type == 280 && ((nPC.type >= 134 && nPC.type <= 136) || nPC.type == 139))
//                                                    {
//                                                        num18 = (float)((int)((double)num18 * 0.75));
//                                                    }
//                                                }
//                                                if (Main.netMode != 2 && nPC.type == 439 && projectile.type >= 0 && ProjectileID.Sets.CultistIsResistantTo[projectile.type])
//                                                {
//                                                    num18 = (float)((int)(num18 * 0.75f));
//                                                }
//                                                if (projectile.type == 497 && projectile.penetrate != 1)
//                                                {
//                                                    projectile.ai[0] = 25f;
//                                                    float num26 = projectile.velocity.Length();
//                                                    Vector2 vector5 = nPC.Center - projectile.Center;
//                                                    vector5.Normalize();
//                                                    vector5 *= num26;
//                                                    projectile.velocity = -vector5 * 0.9f;
//                                                    projectile.netUpdate = true;
//                                                }
//                                                if (projectile.type == 323 && (nPC.type == 159 || nPC.type == 158))
//                                                {
//                                                    num18 *= 10f;
//                                                }
//                                                if (projectile.type == 981 && nPC.type == 104)
//                                                {
//                                                    num18 *= 3f;
//                                                }
//                                                if (projectile.type == 261 && projectile.velocity.Length() < 3.5f)
//                                                {
//                                                    modifiers.SourceDamage /= 2f;
//                                                    num3 /= 2f;
//                                                }
//                                                if (flag && projectile.DamageType == DamageClass.Melee && Main.player[projectile.owner].parryDamageBuff && !ProjectileID.Sets.DontApplyParryDamageBuff[projectile.type])
//                                                {
//                                                    modifiers.ScalingBonusDamage += 4f;
//                                                    Main.player[projectile.owner].parryDamageBuff = false;
//                                                    Main.player[projectile.owner].ClearBuff(198);
//                                                }
//                                                int? num27 = null;
//                                                num50 = projectile.type;
//                                                if (num50 <= 699)
//                                                {
//                                                    if (num50 == 697 || num50 == 699)
//                                                    {
//                                                        goto IL_3052;
//                                                    }
//                                                }
//                                                else if (num50 - 707 <= 1 || num50 == 759)
//                                                {
//                                                    goto IL_3052;
//                                                }
//                                            IL_3081:
//                                                if (projectile.aiStyle == 188 || projectile.aiStyle == 189 || projectile.aiStyle == 190 || projectile.aiStyle == 191)
//                                                {
//                                                    num27 = new int?((Main.player[projectile.owner].Center.X < nPC.Center.X) ? 1 : -1);
//                                                }
//                                                if (projectile.aiStyle == 15)
//                                                {
//                                                    num27 = new int?((Main.player[projectile.owner].Center.X < nPC.Center.X) ? 1 : -1);
//                                                    if (projectile.ai[0] == 0f)
//                                                    {
//                                                        num3 *= 0.35f;
//                                                    }
//                                                    if (projectile.ai[0] == 6f)
//                                                    {
//                                                        num3 *= 0.5f;
//                                                    }
//                                                }
//                                                modifiers.ScalingArmorPenetration += armorPenetrationPercent;
//                                                modifiers.Knockback *= num3 / projectile.knockBack;
//                                                modifiers.TargetDamageMultiplier *= num18 / 1000f;
//                                                if (num27 != null)
//                                                {
//                                                    modifiers.HitDirectionOverride = num27;
//                                                }
//                                                NPC.HitInfo strike = modifiers.ToHitInfo((float)projectile.damage, flag12, num3, true, flag ? Main.player[projectile.owner].luck : 0f);
//                                                num27 = new int?(strike.HitDirection);
//                                                if (projectile.type == 294)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.9);
//                                                }
//                                                if (projectile.type == 265)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.75);
//                                                }
//                                                if (projectile.type == 355)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.75);
//                                                }
//                                                if (projectile.type == 114)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.9);
//                                                }
//                                                if (projectile.type == 76 || projectile.type == 78 || projectile.type == 77)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.95);
//                                                }
//                                                if (projectile.type == 85)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.85);
//                                                }
//                                                if (projectile.type == 866)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.8);
//                                                }
//                                                if (projectile.type == 841)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.5);
//                                                }
//                                                if (projectile.type == 914)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.6);
//                                                }
//                                                if (projectile.type == 952)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.9);
//                                                }
//                                                if (projectile.type == 913)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.66);
//                                                }
//                                                if (projectile.type == 912)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.7);
//                                                }
//                                                if (projectile.type == 847)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.8);
//                                                }
//                                                if (projectile.type == 848)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.95);
//                                                }
//                                                if (projectile.type == 849)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.9);
//                                                }
//                                                if (projectile.type == 915)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.9);
//                                                }
//                                                if (projectile.type == 931)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.8);
//                                                }
//                                                if (projectile.type == 242)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.85);
//                                                }
//                                                if (projectile.type == 323)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.9);
//                                                }
//                                                if (projectile.type == 5)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.9);
//                                                }
//                                                if (projectile.type == 4)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.95);
//                                                }
//                                                if (projectile.type == 309)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.85);
//                                                }
//                                                if (projectile.type == 132)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.85);
//                                                }
//                                                if (projectile.type == 985)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.75);
//                                                }
//                                                if (projectile.type == 950)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.98);
//                                                }
//                                                if (projectile.type == 964)
//                                                {
//                                                    projectile.damage = (int)((double)projectile.damage * 0.85);
//                                                }
//                                                if (projectile.type == 477 && projectile.penetrate > 1)
//                                                {
//                                                    int[] array2 = new int[10];
//                                                    int num28 = 0;
//                                                    int num29 = 700;
//                                                    int num30 = 20;
//                                                    for (int l = 0; l < 200; l++)
//                                                    {
//                                                        if (l != i && Main.npc[l].CanBeChasedBy(projectile, false))
//                                                        {
//                                                            float num31 = (projectile.Center - Main.npc[l].Center).Length();
//                                                            if (num31 > (float)num30 && num31 < (float)num29 && Collision.CanHitLine(projectile.Center, 1, 1, Main.npc[l].Center, 1, 1))
//                                                            {
//                                                                array2[num28] = l;
//                                                                num28++;
//                                                                if (num28 >= 9)
//                                                                {
//                                                                    break;
//                                                                }
//                                                            }
//                                                        }
//                                                    }
//                                                    if (num28 > 0)
//                                                    {
//                                                        num28 = Main.rand.Next(num28);
//                                                        Vector2 vector6 = Main.npc[array2[num28]].Center - projectile.Center;
//                                                        float num32 = projectile.velocity.Length();
//                                                        vector6.Normalize();
//                                                        projectile.velocity = vector6 * num32;
//                                                        projectile.netUpdate = true;
//                                                    }
//                                                }
//                                                projectile.StatusNPC(i);
//                                                if (flag && nPC.life > 5)
//                                                {
//                                                    TryDoingOnHitEffects(nPC, projectile);
//                                                }
//                                                if (ProjectileID.Sets.ImmediatelyUpdatesNPCBuffFlags[projectile.type])
//                                                {
//                                                    nPC.UpdateNPC_BuffSetFlags(false);
//                                                }
//                                                if (projectile.type == 317)
//                                                {
//                                                    projectile.ai[1] = -1f;
//                                                    projectile.netUpdate = true;
//                                                }
//                                                NPCKillAttempt attempt = new NPCKillAttempt(nPC);
//                                                int num33 = nPC.StrikeNPC(strike, false, !flag);
//                                                if (flag && attempt.DidNPCDie())
//                                                {
//                                                    Main.player[projectile.owner].OnKillNPC(ref attempt, projectile);
//                                                }
//                                                if (flag && Main.player[projectile.owner].accDreamCatcher && !nPC.HideStrikeDamage)
//                                                {
//                                                    Main.player[projectile.owner].addDPS(num33);
//                                                }
//                                                bool flag23 = !nPC.immortal;
//                                                bool flag24 = num33 > 0 && nPC.lifeMax > 5 && projectile.friendly && !projectile.hostile && projectile.aiStyle != 59;
//                                                bool flag25 = false;
//                                                if (flag23 && projectile.active && projectile.timeLeft > 10 && nPC.active && nPC.type == 676 && projectile.CanBeReflected())
//                                                {
//                                                    nPC.ReflectProjectile(projectile);
//                                                    projectile.penetrate++;
//                                                }
//                                                if (flag && flag23)
//                                                {
//                                                    if (projectile.type == 997 && (!nPC.immortal || flag25) && !nPC.SpawnedFromStatue && !NPCID.Sets.CountsAsCritter[nPC.type])
//                                                    {
//                                                        Main.player[projectile.owner].HorsemansBlade_SpawnPumpkin(i, (int)((float)projectile.damage * 1f), projectile.knockBack);
//                                                    }
//                                                    if (projectile.type == 756 && projectile.penetrate == 1)
//                                                    {
//                                                        projectile.damage = 0;
//                                                        projectile.penetrate = -1;
//                                                        flag4 = false;
//                                                    }
//                                                    if ((flag25 || nPC.value > 0f) && Main.player[projectile.owner].hasLuckyCoin && Main.rand.Next(5) == 0)
//                                                    {
//                                                        int num34 = 71;
//                                                        if (Main.rand.Next(10) == 0)
//                                                        {
//                                                            num34 = 72;
//                                                        }
//                                                        if (Main.rand.Next(100) == 0)
//                                                        {
//                                                            num34 = 73;
//                                                        }
//                                                        int num35 = Item.NewItem(projectile.GetSource_OnHit(nPC), (int)nPC.position.X, (int)nPC.position.Y, nPC.width, nPC.height, num34, 1, false, 0, false, false);
//                                                        Main.item[num35].stack = Main.rand.Next(1, 11);
//                                                        Main.item[num35].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
//                                                        Main.item[num35].velocity.X = (float)Main.rand.Next(10, 31) * 0.2f * (float)num27.Value;
//                                                        Main.item[num35].timeLeftInWhichTheItemCannotBeTakenByEnemies = 60;
//                                                        if (Main.netMode == 1)
//                                                        {
//                                                            NetMessage.SendData(148, -1, -1, null, num35, 0f, 0f, 0f, 0, 0, 0);
//                                                        }
//                                                    }
//                                                    if (projectile.type == 999 && projectile.owner == Main.myPlayer && Main.rand.Next(3) == 0)
//                                                    {
//                                                        Player player = Main.player[projectile.owner];
//                                                        Vector2 vector7 = (projectile.Center - nPC.Center).SafeNormalize(Vector2.Zero) * 0.25f;
//                                                        int dmg = projectile.damage / 2;
//                                                        float kB = projectile.knockBack;
//                                                        int num36 = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, vector7.X, vector7.Y, player.beeType(), player.beeDamage(dmg), player.beeKB(kB), projectile.owner, 0f, 0f, 0f);
//                                                        Main.projectile[num36].DamageType = DamageClass.Melee;
//                                                    }
//                                                    if (flag24)
//                                                    {
//                                                        if (projectile.type == 304 && !Main.player[projectile.owner].moonLeech)
//                                                        {
//                                                            projectile.vampireHeal(num33, new Vector2(nPC.Center.X, nPC.Center.Y), nPC);
//                                                        }
//                                                        if (nPC.canGhostHeal || flag25)
//                                                        {
//                                                            if (Main.player[projectile.owner].ghostHeal && !Main.player[projectile.owner].moonLeech)
//                                                            {
//                                                                projectile.ghostHeal(num33, new Vector2(nPC.Center.X, nPC.Center.Y), nPC);
//                                                            }
//                                                            if (Main.player[projectile.owner].ghostHurt)
//                                                            {
//                                                                projectile.ghostHurt(num33, new Vector2(nPC.Center.X, nPC.Center.Y), nPC);
//                                                            }
//                                                            if (projectile.DamageType == DamageClass.Magic && Main.player[projectile.owner].setNebula && Main.player[projectile.owner].nebulaCD == 0 && Main.rand.Next(3) == 0)
//                                                            {
//                                                                Main.player[projectile.owner].nebulaCD = 30;
//                                                                int num37 = Utils.SelectRandom<int>(Main.rand, new int[]
//                                                                {
//                                                                    3453,
//                                                                    3454,
//                                                                    3455
//                                                                });
//                                                                int num38 = Item.NewItem(projectile.GetSource_OnHit(nPC), (int)nPC.position.X, (int)nPC.position.Y, nPC.width, nPC.height, num37, 1, false, 0, false, false);
//                                                                Main.item[num38].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
//                                                                Main.item[num38].velocity.X = (float)Main.rand.Next(10, 31) * 0.2f * (float)num27.Value;
//                                                                if (Main.netMode == 1)
//                                                                {
//                                                                    NetMessage.SendData(21, -1, -1, null, num38, 0f, 0f, 0f, 0, 0, 0);
//                                                                }
//                                                            }
//                                                        }
//                                                        if (projectile.DamageType == DamageClass.Melee && Main.player[projectile.owner].beetleOffense && (!nPC.immortal || flag25))
//                                                        {
//                                                            if (Main.player[projectile.owner].beetleOrbs == 0)
//                                                            {
//                                                                Main.player[projectile.owner].beetleCounter += (float)(num33 * 3);
//                                                            }
//                                                            else if (Main.player[projectile.owner].beetleOrbs == 1)
//                                                            {
//                                                                Main.player[projectile.owner].beetleCounter += (float)(num33 * 2);
//                                                            }
//                                                            else
//                                                            {
//                                                                Main.player[projectile.owner].beetleCounter += (float)num33;
//                                                            }
//                                                            Main.player[projectile.owner].beetleCountdown = 0;
//                                                        }
//                                                        if (projectile.arrow && projectile.type != 631 && Main.player[projectile.owner].phantasmTime > 0)
//                                                        {
//                                                            Vector2 source = Main.player[projectile.owner].position + Main.player[projectile.owner].Size * Utils.RandomVector2(Main.rand, 0f, 1f);
//                                                            Vector2 vector8 = nPC.DirectionFrom(source) * 6f;
//                                                            int num39 = (int)((float)projectile.damage * 0.3f);
//                                                            Projectile.NewProjectile(projectile.GetSource_FromThis(), source.X, source.Y, vector8.X, vector8.Y, 631, num39, 0f, projectile.owner, (float)i, 0f, 0f);
//                                                            Projectile.NewProjectile(projectile.GetSource_FromThis(), source.X, source.Y, vector8.X, vector8.Y, 631, num39, 0f, projectile.owner, (float)i, 15f, 0f);
//                                                            Projectile.NewProjectile(projectile.GetSource_FromThis(), source.X, source.Y, vector8.X, vector8.Y, 631, num39, 0f, projectile.owner, (float)i, 30f, 0f);
//                                                        }
//                                                        Player player2 = Main.player[projectile.owner];
//                                                        num50 = projectile.type;
//                                                        if (num50 <= 849)
//                                                        {
//                                                            if (num50 != 847)
//                                                            {
//                                                                if (num50 == 849)
//                                                                {
//                                                                    player2.AddBuff(311, 180, true, false);
//                                                                }
//                                                            }
//                                                            else
//                                                            {
//                                                                player2.AddBuff(308, 180, true, false);
//                                                            }
//                                                        }
//                                                        else if (num50 != 912)
//                                                        {
//                                                            if (num50 == 914)
//                                                            {
//                                                                player2.AddBuff(314, 180, true, false);
//                                                            }
//                                                        }
//                                                        else
//                                                        {
//                                                            int num40 = 15;
//                                                            if (!player2.coolWhipBuff)
//                                                            {
//                                                                Projectile.NewProjectile(projectile.GetSource_FromThis(), nPC.Center, Vector2.Zero, 917, num40, 0f, projectile.owner, 0f, 0f, 0f);
//                                                                player2.coolWhipBuff = true;
//                                                            }
//                                                            player2.AddBuff(312, 180, true, false);
//                                                        }
//                                                    }
//                                                }
//                                                if (flag && (projectile.DamageType == DamageClass.Melee || ProjectileID.Sets.IsAWhip[projectile.type]) && Main.player[projectile.owner].meleeEnchant == 7)
//                                                {
//                                                    Projectile.NewProjectile(projectile.GetSource_FromThis(), nPC.Center.X, nPC.Center.Y, nPC.velocity.X, nPC.velocity.Y, 289, 0, 0f, projectile.owner, 0f, 0f, 0f);
//                                                }
//                                                if (flag && projectile.type == 913)
//                                                {
//                                                    projectile.localAI[0] = 1f;
//                                                }
//                                                if (Main.netMode != 0)
//                                                {
//                                                    NetMessage.SendStrikeNPC(nPC, strike, -1);
//                                                }
//                                                if (projectile.type == 916)
//                                                {
//                                                    Projectile.EmitBlackLightningParticles(nPC);
//                                                }
//                                                if (projectile.type >= 390 && projectile.type <= 392)
//                                                {
//                                                    projectile.localAI[1] = 20f;
//                                                }
//                                                if (projectile.usesIDStaticNPCImmunity)
//                                                {
//                                                    if (projectile.penetrate != 1 || projectile.appliesImmunityTimeOnSingleHits)
//                                                    {
//                                                        nPC.immune[projectile.owner] = 0;
//                                                        Projectile.perIDStaticNPCImmunity[projectile.type][i] = Main.GameUpdateCount + (uint)projectile.idStaticNPCHitCooldown;
//                                                    }
//                                                }
//                                                else if (projectile.type == 434)
//                                                {
//                                                    projectile.numUpdates = 0;
//                                                }
//                                                else if (projectile.type == 598 || projectile.type == 636 || projectile.type == 614)
//                                                {
//                                                    Point[] bufferForScan = new Point[6];
//                                                    if (projectile.type == 636)
//                                                    {
//                                                        bufferForScan = new Point[8];
//                                                    }
//                                                    if (projectile.type == 614)
//                                                    {
//                                                        bufferForScan = new Point[10];
//                                                    }
//                                                    Projectile.KillOldestJavelin(projectile.whoAmI, projectile.type, i, bufferForScan);
//                                                }
//                                                else if (projectile.type == 632)
//                                                {
//                                                    nPC.immune[projectile.owner] = 5;
//                                                }
//                                                else if (projectile.type == 514)
//                                                {
//                                                    nPC.immune[projectile.owner] = 1;
//                                                }
//                                                else if (projectile.type == 611)
//                                                {
//                                                    if (projectile.localAI[1] <= 0f)
//                                                    {
//                                                        Main.projectile[Projectile.NewProjectile(projectile.GetSource_FromThis(), nPC.Center.X, nPC.Center.Y, 0f, 0f, 612, projectile.damage, 10f, projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f, 0f)].CritChance = 0;
//                                                    }
//                                                    projectile.localAI[1] = 4f;
//                                                }
//                                                else if (projectile.type == 595 || projectile.type == 735)
//                                                {
//                                                    nPC.immune[projectile.owner] = 5;
//                                                }
//                                                else if (projectile.type == 927)
//                                                {
//                                                    nPC.immune[projectile.owner] = 4;
//                                                }
//                                                else if (projectile.type == 286)
//                                                {
//                                                    nPC.immune[projectile.owner] = 5;
//                                                }
//                                                else if (projectile.type == 443)
//                                                {
//                                                    nPC.immune[projectile.owner] = 8;
//                                                }
//                                                else if (projectile.type >= 424 && projectile.type <= 426)
//                                                {
//                                                    nPC.immune[projectile.owner] = 5;
//                                                }
//                                                else if (projectile.type == 634 || projectile.type == 635)
//                                                {
//                                                    nPC.immune[projectile.owner] = 5;
//                                                }
//                                                else if (projectile.type == 659)
//                                                {
//                                                    nPC.immune[projectile.owner] = 5;
//                                                }
//                                                else if (projectile.type == 246)
//                                                {
//                                                    nPC.immune[projectile.owner] = 7;
//                                                }
//                                                else if (projectile.type == 249)
//                                                {
//                                                    nPC.immune[projectile.owner] = 7;
//                                                }
//                                                else if (projectile.type == 16)
//                                                {
//                                                    nPC.immune[projectile.owner] = 8;
//                                                }
//                                                else if (projectile.type == 409)
//                                                {
//                                                    nPC.immune[projectile.owner] = 6;
//                                                }
//                                                else if (projectile.type == 311)
//                                                {
//                                                    nPC.immune[projectile.owner] = 7;
//                                                }
//                                                else if (projectile.type == 582 || projectile.type == 902)
//                                                {
//                                                    nPC.immune[projectile.owner] = 7;
//                                                    if (projectile.ai[0] != 1f)
//                                                    {
//                                                        projectile.ai[0] = 1f;
//                                                        projectile.netUpdate = true;
//                                                    }
//                                                }
//                                                else
//                                                {
//                                                    if (projectile.type == 451)
//                                                    {
//                                                        if (projectile.ai[0] == 0f)
//                                                        {
//                                                            projectile.ai[0] += (float)projectile.penetrate;
//                                                        }
//                                                        else
//                                                        {
//                                                            projectile.ai[0] -= (float)(projectile.penetrate + 1);
//                                                        }
//                                                        projectile.ai[1] = 0f;
//                                                        projectile.netUpdate = true;
//                                                        Main.npc[i].position -= Main.npc[i].netOffset;
//                                                        break;
//                                                    }
//                                                    if (projectile.type == 864)
//                                                    {
//                                                        array[i] = 10;
//                                                        nPC.immune[projectile.owner] = 0;
//                                                        if (projectile.ai[0] > 0f)
//                                                        {
//                                                            projectile.ai[0] = -1f;
//                                                            projectile.ai[1] = 0f;
//                                                            projectile.netUpdate = true;
//                                                        }
//                                                    }
//                                                    else if (projectile.type == 661 || projectile.type == 856)
//                                                    {
//                                                        array[i] = 8;
//                                                        nPC.immune[projectile.owner] = 0;
//                                                    }
//                                                    else if (projectile.type == 866)
//                                                    {
//                                                        array[i] = -1;
//                                                        nPC.immune[projectile.owner] = 0;
//                                                        projectile.penetrate--;
//                                                        if (projectile.penetrate == 0)
//                                                        {
//                                                            projectile.penetrate = 1;
//                                                            projectile.damage = 0;
//                                                            projectile.ai[1] = -1f;
//                                                            projectile.netUpdate = true;
//                                                            Main.npc[i].position -= Main.npc[i].netOffset;
//                                                            break;
//                                                        }
//                                                        if (projectile.owner == Main.myPlayer)
//                                                        {
//                                                            int num41 = projectile.FindTargetWithLineOfSight(800f);
//                                                            float num42 = projectile.ai[1];
//                                                            projectile.ai[1] = (float)num41;
//                                                            if (projectile.ai[1] != num42)
//                                                            {
//                                                                projectile.netUpdate = true;
//                                                            }
//                                                            if (num41 != -1)
//                                                            {
//                                                                projectile.velocity = projectile.velocity.Length() * projectile.DirectionTo(Main.npc[num41].Center);
//                                                            }
//                                                        }
//                                                    }
//                                                    else if (projectile.usesLocalNPCImmunity && projectile.localNPCHitCooldown != -2)
//                                                    {
//                                                        nPC.immune[projectile.owner] = 0;
//                                                        array[i] = projectile.localNPCHitCooldown;
//                                                    }
//                                                    else if (projectile.penetrate != 1 || projectile.appliesImmunityTimeOnSingleHits)
//                                                    {
//                                                        nPC.immune[projectile.owner] = 10;
//                                                    }
//                                                }
//                                                if (projectile.type == 710)
//                                                {
//                                                    BetsySharpnel(i, projectile);
//                                                }
//                                                CombinedHooks.OnHitNPCWithProj(projectile, nPC, strike, num33);
//                                                if (projectile.penetrate > 0 && projectile.type != 317 && projectile.type != 866)
//                                                {
//                                                    if (projectile.type == 357)
//                                                    {
//                                                        projectile.damage = (int)((double)projectile.damage * 0.8);
//                                                    }
//                                                    projectile.penetrate--;
//                                                    if (projectile.penetrate == 0)
//                                                    {
//                                                        Main.npc[i].position -= Main.npc[i].netOffset;
//                                                        if (projectile.stopsDealingDamageAfterPenetrateHits)
//                                                        {
//                                                            projectile.penetrate = -1;
//                                                            projectile.damage = 0;
//                                                        }
//                                                        flag4 = false;
//                                                    }
//                                                }
//                                                if (projectile.aiStyle == 7)
//                                                {
//                                                    projectile.ai[0] = 1f;
//                                                    projectile.damage = 0;
//                                                    projectile.netUpdate = true;
//                                                }
//                                                else if (projectile.aiStyle == 13)
//                                                {
//                                                    projectile.ai[0] = 1f;
//                                                    projectile.netUpdate = true;
//                                                }
//                                                else if (projectile.aiStyle == 69)
//                                                {
//                                                    projectile.ai[0] = 1f;
//                                                    projectile.netUpdate = true;
//                                                }
//                                                else if (projectile.type == 607)
//                                                {
//                                                    projectile.ai[0] = 1f;
//                                                    projectile.netUpdate = true;
//                                                    projectile.friendly = false;
//                                                }
//                                                else if (projectile.type == 638 || projectile.type == 639 || projectile.type == 640)
//                                                {
//                                                    array[i] = -1;
//                                                    nPC.immune[projectile.owner] = 0;
//                                                    projectile.damage = (int)((double)projectile.damage * 0.96);
//                                                }
//                                                else if (projectile.type == 617)
//                                                {
//                                                    array[i] = 8;
//                                                    nPC.immune[projectile.owner] = 0;
//                                                }
//                                                else if (projectile.type == 656)
//                                                {
//                                                    array[i] = 8;
//                                                    nPC.immune[projectile.owner] = 0;
//                                                    projectile.localAI[0] += 1f;
//                                                }
//                                                else if (projectile.type == 618)
//                                                {
//                                                    array[i] = 20;
//                                                    nPC.immune[projectile.owner] = 0;
//                                                }
//                                                else if (projectile.type == 642)
//                                                {
//                                                    array[i] = 10;
//                                                    nPC.immune[projectile.owner] = 0;
//                                                }
//                                                else if (projectile.type == 857)
//                                                {
//                                                    array[i] = 10;
//                                                    nPC.immune[projectile.owner] = 0;
//                                                }
//                                                else if (projectile.type == 611 || projectile.type == 612)
//                                                {
//                                                    array[i] = 6;
//                                                    nPC.immune[projectile.owner] = 4;
//                                                }
//                                                else if (projectile.type == 645)
//                                                {
//                                                    array[i] = -1;
//                                                    nPC.immune[projectile.owner] = 0;
//                                                    if (projectile.ai[1] != -1f)
//                                                    {
//                                                        projectile.ai[0] = 0f;
//                                                        projectile.ai[1] = -1f;
//                                                        projectile.netUpdate = true;
//                                                    }
//                                                }
//                                                projectile.numHits++;
//                                                if (projectile.type == 697)
//                                                {
//                                                    if (projectile.ai[0] >= 42f)
//                                                    {
//                                                        projectile.localAI[1] = 1f;
//                                                    }
//                                                }
//                                                else if (projectile.type == 699)
//                                                {
//                                                    SummonMonkGhast(projectile);
//                                                }
//                                                else if (projectile.type == 706)
//                                                {
//                                                    projectile.damage = (int)((float)projectile.damage * 0.95f);
//                                                }
//                                                else if (projectile.type == 728)
//                                                {
//                                                    SummonSuperStarSlash(nPC.Center, projectile);
//                                                }
//                                                else if (projectile.type == 34)
//                                                {
//                                                    if (projectile.ai[0] == -1f)
//                                                    {
//                                                        projectile.ai[1] = -1f;
//                                                        projectile.netUpdate = true;
//                                                    }
//                                                }
//                                                else if (projectile.type == 79)
//                                                {
//                                                    if (projectile.ai[0] == -1f)
//                                                    {
//                                                        projectile.ai[1] = -1f;
//                                                        projectile.netUpdate = true;
//                                                    }
//                                                    ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.RainbowRodHit, new ParticleOrchestraSettings
//                                                    {
//                                                        PositionInWorld = nPC.Center,
//                                                        MovementVector = projectile.velocity
//                                                    }, null);
//                                                }
//                                                else if (projectile.type == 931)
//                                                {
//                                                    int num43 = projectile.FindTargetWithLineOfSight(800f);
//                                                    if (num43 != -1)
//                                                    {
//                                                        projectile.ai[0] = (float)num43;
//                                                        projectile.netUpdate = true;
//                                                    }
//                                                }
//                                                else if (projectile.aiStyle == 165)
//                                                {
//                                                    if (nPC.active)
//                                                    {
//                                                        Main.player[projectile.owner].MinionAttackTargetNPC = i;
//                                                    }
//                                                }
//                                                else if (projectile.type == 623)
//                                                {
//                                                    ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.StardustPunch, new ParticleOrchestraSettings
//                                                    {
//                                                        PositionInWorld = Vector2.Lerp(projectile.Center, nPC.Hitbox.ClosestPointInRect(projectile.Center), 0.5f) + new Vector2(0f, Main.rand.NextFloatDirection() * 10f),
//                                                        MovementVector = new Vector2((float)projectile.direction, Main.rand.NextFloatDirection() * 0.5f) * (3f + 3f * Main.rand.NextFloat())
//                                                    }, null);
//                                                }
//                                                if (flag2)
//                                                {
//                                                    Main.player[projectile.owner].SetMeleeHitCooldown(i, Main.player[projectile.owner].itemAnimation);
//                                                    goto IL_4791;
//                                                }
//                                                goto IL_4791;
//                                            IL_3052:
//                                                num27 = new int?((Main.player[projectile.owner].Center.X < nPC.Center.X) ? 1 : -1);
//                                                goto IL_3081;
//                                            }
//                                        }
//                                    }
//                                }
//                            IL_4791:
//                                Main.npc[i].position -= Main.npc[i].netOffset;
//                            }
//                        }
//                    IL_47B6:
//                        i++;
//                    }
//                }
//                if (flag3 && Main.player[Main.myPlayer].hostile)
//                {
//                    for (int m = 0; m < 255; m++)
//                    {
//                        if (m != projectile.owner)
//                        {
//                            Player player3 = Main.player[m];
//                            if (player3.active && !player3.dead && !player3.immune && player3.hostile && projectile.playerImmune[m] <= 0 && (Main.player[Main.myPlayer].team == 0 || Main.player[Main.myPlayer].team != player3.team))
//                            {
//                                bool flag26 = !projectile.ownerHitCheck;
//                                if (projectile.ownerHitCheck)
//                                {
//                                    flag26 |= projectile.CanHitWithMeleeWeapon(player3);
//                                }
//                                if (flag26 && projectile.Colliding(rectangle, player3.getRect()) && CombinedHooks.CanHitPvpWithProj(projectile, player3))
//                                {
//                                    if (projectile.aiStyle == 3)
//                                    {
//                                        if (projectile.ai[0] == 0f)
//                                        {
//                                            projectile.velocity.X = 0f - projectile.velocity.X;
//                                            projectile.velocity.Y = 0f - projectile.velocity.Y;
//                                            projectile.netUpdate = true;
//                                        }
//                                        projectile.ai[0] = 1f;
//                                    }
//                                    else if (projectile.aiStyle == 16 || ProjectileID.Sets.Explosive[projectile.type])
//                                    {
//                                        if (projectile.timeLeft > 3)
//                                        {
//                                            projectile.timeLeft = 3;
//                                        }
//                                        if (player3.position.X + (float)(player3.width / 2) < projectile.position.X + (float)(projectile.width / 2))
//                                        {
//                                            projectile.direction = -1;
//                                        }
//                                        else
//                                        {
//                                            projectile.direction = 1;
//                                        }
//                                    }
//                                    else if (projectile.aiStyle == 68)
//                                    {
//                                        if (projectile.timeLeft > 3)
//                                        {
//                                            projectile.timeLeft = 3;
//                                        }
//                                        if (player3.position.X + (float)(player3.width / 2) < projectile.position.X + (float)(projectile.width / 2))
//                                        {
//                                            projectile.direction = -1;
//                                        }
//                                        else
//                                        {
//                                            projectile.direction = 1;
//                                        }
//                                    }
//                                    int playerIndex = projectile.owner;
//                                    if (ProjectileID.Sets.IsAGravestone[projectile.type])
//                                    {
//                                        playerIndex = (int)projectile.ai[0];
//                                    }
//                                    PlayerDeathReason playerDeathReason = PlayerDeathReason.ByProjectile(playerIndex, projectile.whoAmI);
//                                    if (projectile.type == 41 && projectile.timeLeft > 1)
//                                    {
//                                        projectile.timeLeft = 1;
//                                    }
//                                    bool flag27 = false;
//                                    int num44 = Main.DamageVar((float)((int)((float)projectile.damage * num)), Main.player[projectile.owner].luck);
//                                    bool dodgeable = projectile.IsDamageDodgable();
//                                    if (!player3.immune)
//                                    {
//                                        projectile.StatusPvP(m);
//                                    }
//                                    TryDoingOnHitEffects(player3, projectile);
//                                    int num45 = (int)player3.Hurt(playerDeathReason, num44, projectile.direction, true, false, -1, dodgeable, 0f);
//                                    if (num45 > 0 && Main.player[projectile.owner].ghostHeal && projectile.friendly && !projectile.hostile)
//                                    {
//                                        projectile.ghostHeal(num45, new Vector2(player3.Center.X, player3.Center.Y), player3);
//                                    }
//                                    if (projectile.type == 304 && num45 > 0)
//                                    {
//                                        projectile.vampireHeal(num45, new Vector2(player3.Center.X, player3.Center.Y), player3);
//                                    }
//                                    if ((projectile.DamageType == DamageClass.Melee || ProjectileID.Sets.IsAWhip[projectile.type]) && Main.player[projectile.owner].meleeEnchant == 7)
//                                    {
//                                        Projectile.NewProjectile(projectile.GetSource_FromThis(), player3.Center.X, player3.Center.Y, player3.velocity.X, player3.velocity.Y, 289, 0, 0f, projectile.owner, 0f, 0f, 0f);
//                                    }
//                                    if (Main.netMode != 0)
//                                    {
//                                        /*NetMessage.*/
//                                        SendPlayerHurt(m, playerDeathReason, num44, projectile.direction, flag27, true, -1, -1, -1);
//                                    }
//                                    projectile.playerImmune[m] = 40;
//                                    if (projectile.penetrate > 0)
//                                    {
//                                        projectile.penetrate--;
//                                        if (projectile.penetrate == 0)
//                                        {
//                                            break;
//                                        }
//                                    }
//                                    if (projectile.aiStyle == 7)
//                                    {
//                                        projectile.ai[0] = 1f;
//                                        projectile.damage = 0;
//                                        projectile.netUpdate = true;
//                                    }
//                                    else if (projectile.aiStyle == 13)
//                                    {
//                                        projectile.ai[0] = 1f;
//                                        projectile.netUpdate = true;
//                                    }
//                                    else if (projectile.aiStyle == 69)
//                                    {
//                                        projectile.ai[0] = 1f;
//                                        projectile.netUpdate = true;
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            if (projectile.type == 10 && Main.netMode != 1)
//            {
//                for (int n = 0; n < 200; n++)
//                {
//                    NPC nPC2 = Main.npc[n];
//                    if (nPC2.active)
//                    {
//                        if (nPC2.type == 534)
//                        {
//                            if (rectangle.Intersects(nPC2.Hitbox))
//                            {
//                                nPC2.Transform(441);
//                            }
//                        }
//                        else if (nPC2.type == 687 && rectangle.Intersects(nPC2.Hitbox))
//                        {
//                            nPC2.Transform(683);
//                            Vector2 vector9 = nPC2.Center - new Vector2(20f);
//                            Utils.PoofOfSmoke(vector9);
//                            if (Main.netMode == 2)
//                            {
//                                NetMessage.SendData(106, -1, -1, null, (int)vector9.X, vector9.Y, 0f, 0f, 0, 0, 0);
//                            }
//                            if (!NPC.unlockedSlimeYellowSpawn)
//                            {
//                                NPC.unlockedSlimeYellowSpawn = true;
//                                if (Main.netMode == 2)
//                                {
//                                    NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            if ((projectile.type == 11 || projectile.type == 463) && Main.netMode != 1)
//            {
//                bool crimson = projectile.type == 463;
//                for (int num46 = 0; num46 < 200; num46++)
//                {
//                    if (Main.npc[num46].active)
//                    {
//                        Rectangle value2 = new Rectangle((int)Main.npc[num46].position.X, (int)Main.npc[num46].position.Y, Main.npc[num46].width, Main.npc[num46].height);
//                        if (rectangle.Intersects(value2))
//                        {
//                            Main.npc[num46].AttemptToConvertNPCToEvil(crimson);
//                        }
//                    }
//                }
//            }
//            if (Main.netMode == 2 || !projectile.hostile || Main.myPlayer >= 255 || projectile.damage <= 0)
//            {
//                return;
//            }
//            int num47 = -1;
//            num50 = projectile.type;
//            if (num50 <= 462)
//            {
//                if (num50 == 452 || num50 - 454 <= 1 || num50 == 462)
//                {
//                    num47 = 1;
//                }
//            }
//            else if (num50 - 871 <= 3 || num50 == 919 || num50 - 923 <= 1)
//            {
//                num47 = 1;
//            }
//            if (projectile.ModProjectile != null)
//            {
//                num47 = projectile.ModProjectile.CooldownSlot;
//            }
//            int myPlayer = Main.myPlayer;
//            bool flag28 = Main.player[myPlayer].active && !Main.player[myPlayer].dead && (!Main.player[myPlayer].immune || num47 != -1);
//            if (flag28 && projectile.type == 281)
//            {
//                flag28 = (projectile.ai[1] - 1f == (float)myPlayer);
//            }
//            if (Main.getGoodWorld && projectile.type == 281)
//            {
//                flag28 = true;
//            }
//            if (!flag28 || !projectile.Colliding(rectangle, Main.player[myPlayer].getRect()))
//            {
//                return;
//            }
//            if (!CombinedHooks.CanBeHitByProjectile(Main.player[myPlayer], projectile))
//            {
//                return;
//            }
//            int num48 = projectile.direction;
//            num48 = ((Main.player[myPlayer].position.X + (float)(Main.player[myPlayer].width / 2) >= projectile.position.X + (float)(projectile.width / 2)) ? 1 : -1);
//            if (!Main.player[myPlayer].CanParryAgainst(Main.player[myPlayer].Hitbox, projectile.Hitbox, projectile.velocity))
//            {
//                int num49 = Main.DamageVar((float)projectile.damage, 0f - Main.player[projectile.owner].luck) * 2;
//                if (projectile.type == 961)
//                {
//                    if (projectile.penetrate == 1)
//                    {
//                        projectile.damage = 0;
//                        projectile.penetrate = -1;
//                    }
//                    else
//                    {
//                        projectile.damage = (int)((double)projectile.damage * 0.7);
//                    }
//                }
//                bool dodgeable2 = projectile.IsDamageDodgable();
//                int playerIndex2 = -1;
//                if (ProjectileID.Sets.IsAGravestone[projectile.type])
//                {
//                    playerIndex2 = (int)projectile.ai[0];
//                }
//                if (Main.player[myPlayer].Hurt(PlayerDeathReason.ByProjectile(playerIndex2, projectile.whoAmI), num49, num48, false, false, num47, dodgeable2, (float)projectile.ArmorPenetration) > 0.0 && !Main.player[myPlayer].dead)
//                {
//                    projectile.StatusPlayer(myPlayer);
//                }
//                if (projectile.trap)
//                {
//                    Main.player[myPlayer].trapDebuffSource = true;
//                    if (Main.player[myPlayer].dead)
//                    {
//                        AchievementsHelper.HandleSpecialEvent(Main.player[myPlayer], 4);
//                    }
//                }
//            }
//            if (projectile.type == 435 || projectile.type == 682)
//            {
//                projectile.penetrate--;
//            }
//            if (projectile.type == 436)
//            {
//                projectile.penetrate--;
//            }
//            if (projectile.type == 681)
//            {
//                projectile.timeLeft = 0;
//            }
//            if (projectile.type == 437)
//            {
//                projectile.penetrate--;
//            }
//        }
//        public static Rectangle Damage_GetHitbox(Projectile projectile)
//        {
//            Rectangle result = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
//            if (projectile.type == 101)
//            {
//                result.Inflate(30, 30);
//            }
//            if (projectile.type == 85)
//            {
//                int num = (int)Utils.Remap(projectile.localAI[0], 0f, 72f, 10f, 40f, true);
//                result.Inflate(num, num);
//            }
//            if (projectile.type == 188)
//            {
//                result.Inflate(20, 20);
//            }
//            if (projectile.aiStyle == 29)
//            {
//                result.Inflate(4, 4);
//            }
//            if (projectile.type == 967)
//            {
//                result.Inflate(10, 10);
//            }
//            ProjectileLoader.ModifyDamageHitbox(projectile, ref result);
//            return result;
//        }
//        public static void BombsHurtPlayers(Rectangle projRectangle, int j, Projectile projectile)
//        {
//            if ((projectile.aiStyle != 16 && !ProjectileID.Sets.Explosive[projectile.type]) || (ProjectileID.Sets.RocketsSkipDamageForPlayers[projectile.type] || (projectile.timeLeft > 1 && projectile.type != 108 && projectile.type != 164 && projectile.type != 1002)) || !Main.player[j].active || Main.player[j].dead || Main.player[j].immune || (projectile.ownerHitCheck && !projectile.CanHitWithMeleeWeapon(Main.player[j])))
//            {
//                return;
//            }
//            Rectangle value = new Rectangle((int)Main.player[j].position.X, (int)Main.player[j].position.Y, Main.player[j].width, Main.player[j].height);
//            if (!projRectangle.Intersects(value))
//            {
//                return;
//            }
//            if (Main.player[j].position.X + (float)(Main.player[j].width / 2) < projectile.position.X + (float)(projectile.width / 2))
//            {
//                projectile.direction = -1;
//            }
//            else
//            {
//                projectile.direction = 1;
//            }
//            int num = Main.DamageVar((float)projectile.damage, 0f - Main.player[j].luck);
//            int playerIndex = projectile.owner;
//            bool pvp = true;
//            if (projectile.type == 108 || projectile.type == 1002)
//            {
//                playerIndex = -1;
//                pvp = false;
//            }
//            if (ProjectileID.Sets.IsAGravestone[projectile.type])
//            {
//                playerIndex = (int)projectile.ai[0];
//            }
//            bool dodgeable = projectile.IsDamageDodgable();
//            PlayerDeathReason damageSource = PlayerDeathReason.ByProjectile(playerIndex, projectile.whoAmI);
//            if (Main.player[j].Hurt(damageSource, num, projectile.direction, pvp, false, -1, dodgeable, (float)projectile.ArmorPenetration) > 0.0 && !Main.player[j].dead)
//            {
//                projectile.StatusPlayer(j);
//            }
//            if (projectile.trap)
//            {
//                Main.player[j].trapDebuffSource = true;
//                if (Main.player[j].dead)
//                {
//                    AchievementsHelper.HandleSpecialEvent(Main.player[j], 4);
//                }
//            }
//        }
//        public static Projectile FindStardustDragonHeadOfOwner(Projectile projectile)
//        {
//            for (int i = 0; i < 1000; i++)
//            {
//                Projectile projectile2 = Main.projectile[i];
//                if (projectile2.active && projectile2.owner == projectile.owner && projectile2.type == 625)
//                {
//                    return projectile2;
//                }
//            }
//            return null;
//        }
//        // Token: 0x06000D06 RID: 3334 RVA: 0x002FE244 File Offset: 0x002FC444
//        public static int CountEnemiesWhoAreImmuneToMeRightNow(int cap, Projectile projectile)
//        {
//            int num = 0;
//            for (int i = 0; i < projectile.localNPCImmunity.Length; i++)
//            {
//                if (projectile.localNPCImmunity[i] > 0)
//                {
//                    num++;
//                    if (num >= cap)
//                    {
//                        break;
//                    }
//                }
//            }
//            return num;
//        }
//        // Token: 0x06000D08 RID: 3336 RVA: 0x002FE2B0 File Offset: 0x002FC4B0
//        public static void TryDoingOnHitEffects(Entity entity, Projectile projectile)
//        {
//            int num = projectile.type;
//            if (num <= 614)
//            {
//                if (num != 221 && num != 227 && num != 614)
//                {
//                    goto IL_42;
//                }
//            }
//            else if (num != 729 && num != 908 && num != 977)
//            {
//                goto IL_42;
//            }
//            return;
//        IL_42:
//            Main.player[projectile.owner].OnHit(entity.Center.X, entity.Center.Y, entity);
//        }
//        public static void SendPlayerHurt(int playerTargetIndex, PlayerDeathReason reason, int damage, int direction, bool critical, bool pvp, int hitContext, int remoteClient = -1, int ignoreClient = -1)
//        {
//            if (!pvp)
//            {
//                throw new ArgumentException("SendPlayerHurt is legacy, for pvp usage only. Call Player.Hurt with quiet: false, or use the Player.HurtInfo overload");
//            }
//            NetMessage._currentPlayerDeathReason = reason;
//            BitsByte bitsByte = 0;
//            bitsByte[0] = critical;
//            bitsByte[1] = pvp;
//            NetMessage.SendData(117, remoteClient, ignoreClient, null, playerTargetIndex, (float)damage, (float)direction, (float)bitsByte, hitContext, 0, 0);
//        }
//        public static void SummonMonkGhast(Projectile projectile)
//        {
//            if (projectile.localAI[0] > 0f)
//            {
//                return;
//            }
//            projectile.localAI[0] = 1000f;
//            List<NPC> list = new List<NPC>();
//            for (int i = 0; i < 200; i++)
//            {
//                NPC nPC = Main.npc[i];
//                if (nPC.CanBeChasedBy(projectile, false) && projectile.Distance(nPC.Center) < 800f)
//                {
//                    list.Add(nPC);
//                }
//            }
//            Vector2 center = projectile.Center;
//            Vector2 zero = Vector2.Zero;
//            if (list.Count > 0)
//            {
//                NPC npc = list[Main.rand.Next(list.Count)];
//                center = npc.Center;
//                zero = npc.velocity;
//            }
//            int num = Main.rand.Next(2) * 2 - 1;
//            Vector2 vector = new Vector2((float)num * (4f + (float)Main.rand.Next(3)), 0f);
//            Vector2 vector2 = center + new Vector2((float)(-(float)num * 120), 0f);
//            vector += (center + zero * 15f - vector2).SafeNormalize(Vector2.Zero) * 2f;
//            Projectile.NewProjectile(projectile.GetSource_FromThis(), vector2, vector, 700, projectile.damage, 0f, projectile.owner, 0f, 0f, 0f);
//        }
//        public static void SummonSuperStarSlash(Vector2 target, Projectile projectile)
//        {
//            Vector2 v = Main.rand.NextVector2CircularEdge(200f, 200f);
//            if (v.Y < 0f)
//            {
//                v.Y *= -1f;
//            }
//            v.Y += 100f;
//            Vector2 vector = v.SafeNormalize(Vector2.UnitY) * 6f;
//            Projectile.NewProjectile(projectile.GetSource_FromThis(), target - vector * 20f, vector, 729, (int)((double)projectile.damage * 0.75), 0f, projectile.owner, 0f, target.Y, 0f);
//        }
//        public static void BetsySharpnel(int npcIndex, Projectile projectile)
//        {
//            if (projectile.ai[1] != -1f && projectile.owner == Main.myPlayer)
//            {
//                Vector2 spinningpoint = new Vector2(0f, 6f);
//                Vector2 center = projectile.Center;
//                float num = 0.7853982f;
//                int num2 = 5;
//                float num3 = (0f - num * 2f) / (float)(num2 - 1);
//                for (int i = 0; i < num2; i++)
//                {
//                    int num4 = Projectile.NewProjectile(projectile.GetSource_FromThis(), center, spinningpoint.RotatedBy((double)(num + num3 * (float)i), default(Vector2)), 710, projectile.damage, projectile.knockBack, projectile.owner, 0f, -1f, 0f);
//                    Projectile p = Main.projectile[num4];
//                    CopyLocalNPCImmunityTimes(p, projectile);
//                }
//            }
//        }
//        public static void CopyLocalNPCImmunityTimes(Projectile p, Projectile projectile)
//        {
//            for (int i = 0; i < projectile.localNPCImmunity.Length; i++)
//            {
//                p.localNPCImmunity[i] = projectile.localNPCImmunity[i];
//            }
//        }
//        public static void LightDisc_Bounce(Vector2 hitPoint, Vector2 normal, Projectile projectile)
//        {
//            Vector2 spinningpoint = Vector2.Reflect(projectile.velocity, normal);
//            for (int i = 0; i < 4; i++)
//            {
//                Dust dust = Dust.NewDustPerfect(hitPoint, 306, new Vector2?(spinningpoint.RotatedBy((double)(0.7853982f * Main.rand.NextFloatDirection()), default(Vector2)) * 0.6f * Main.rand.NextFloat()), 200, default(Color), 1.6f);
//                dust.color = Color.Lerp(new Color(219, 253, 0), Color.Cyan, Main.rand.NextFloat());
//                Dust dust2 = Dust.CloneDust(dust);
//                dust2.color = Color.White;
//                dust2.scale = 1f;
//                dust2.alpha = 50;
//            }
//        }
//    }
//}
#endif