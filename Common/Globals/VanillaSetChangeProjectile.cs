using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Buffs;
using TimeDomain.Content.Items.Weapons.Summon.Whip;

namespace TimeDomain.Common.Globals
{
    public class VanillaSetChangeProjectile : GlobalProjectile
    {
        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == ProjectileID.PulseBolt)
            {
                projectile.penetrate = -1;
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.type == ProjectileID.PulseBolt)
            {
                target.AddBuff(BuffID.Electrified, 60 * 2);
            }


            if (projectile.type == ModContent.ProjectileType<OpticNeuronProj>())
            {
                if (projectile.owner == Main.myPlayer)
                {
                    if (projectile.damage > 0)
                    {
                        for (int i = 0; i < Main.maxProjectiles; i++)
                        {
                            if (Main.projectile[i].type == ModContent.ProjectileType<OpticNeuronProj>())
                            {
                                projectile.damage = (int)((double)projectile.damage * 0.9);
                            }
                        }
                    }
                }
            }

            if (projectile.type == ModContent.ProjectileType<OpticNeuronProj>())
            {
                if (projectile.owner == Main.myPlayer)
                {
                    if (projectile.damage > 0)
                    {
                        target.AddBuff(ModContent.BuffType<OpticNeuronBuff>(), 240);
                    }
                }
            }

            if (!projectile.npcProj && !projectile.trap && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type] || projectile.sentry || ProjectileID.Sets.SentryShot[projectile.type]))
            {
                if (projectile.damage > 0)
                {
                    if (target.HasBuff(ModContent.BuffType<OpticNeuronBuff>()))
                    {
                        //target.RequestBuffRemoval(ModContent.BuffType<OpticNeuronBuff>());
                        //target.buffTime[ModContent.BuffType<OpticNeuronBuff>()] = 0;
                        //target.buffType[ModContent.BuffType<OpticNeuronBuff>()] = 0;
                        //target.DelBuff(ModContent.BuffType<OpticNeuronBuff>());
                        for (int i = 0; i < 20; i++)
                        {
                            if (target.buffType[i] == ModContent.BuffType<OpticNeuronBuff>())
                            {
                                target.buffTime[i] = 0;
                            }
                        }
                        int proj = Projectile.NewProjectile(projectile.GetSource_FromThis(), target.Center + new Vector2(Main.rand.Next(-40, 40), Main.rand.Next(-40, 40)), Vector2.Normalize(target.Center - projectile.Center) * -5f, ModContent.ProjectileType<OpticNeuronProj2>(), damageDone, 5);
                        for (int i = 0; i < 7; i++)
                        {
                            int L = Main.rand.Next(9, 15);
                            int dust = Dust.NewDust(projectile.Center, L, L, DustID.Blood, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4));
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 9 / L;
                            Main.dust[dust].scale = L / 6;
                            //Main.dust[dust].alpha = 255;
                        }
                    }
                }
            }
        }
        public override bool InstancePerEntity => true;
        bool flag10 = false;
        //public override void AI(Projectile projectile)
        //{
        //    int[] array = projectile.localNPCImmunity;
        //    if (projectile.type == ModContent.ProjectileType<OpticNeuronProj>())
        //    {
        //        if (projectile.owner == Main.myPlayer)
        //        {
        //            if (!flag10)
        //            {
        //                for (int i = 0; i < Main.maxNPCs; i++)
        //                {
        //                    bool flag5 = (!projectile.usesLocalNPCImmunity && !projectile.usesIDStaticNPCImmunity) || (projectile.usesLocalNPCImmunity && array[i] == 0) || (projectile.usesIDStaticNPCImmunity && Projectile.IsNPCIndexImmuneToProjectileType(projectile.type, i));
        //                    bool flag3 = projectile.damage > 0;
        //                    if ((!Main.npc[i].dontTakeDamage || NPCID.Sets.ZappingJellyfish[Main.npc[i].type]) && flag5 && (Main.npc[i].aiStyle != 112 || Main.npc[i].ai[2] <= 1f))
        //                    {
        //                        bool flag8 = projectile.maxPenetrate == 1 && !projectile.usesLocalNPCImmunity && !projectile.usesIDStaticNPCImmunity;
        //                        bool flag9 = false;
        //                        if (Main.npc[i].trapImmune && projectile.trap)
        //                        {
        //                            flag9 = true;
        //                        }
        //                        else if (Main.npc[i].immortal && projectile.npcProj)
        //                        {
        //                            flag9 = true;
        //                        }
        //                        if (flag8 && !flag9 && (Main.npc[i].noTileCollide || !projectile.ownerHitCheck || projectile.CanHitWithMeleeWeapon(Main.npc[i])))
        //                        {
        //                            //bool flag10;
        //                            if (!flag10)
        //                            {
        //                                flag10 = projectile.Colliding(ModUtil.Damage_GetHitbox(projectile), Main.npc[i].getRect());
        //                            }
        //                            if (flag10)
        //                            {
        //                                projectile.damage = (int)((double)projectile.damage * 0.9);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
