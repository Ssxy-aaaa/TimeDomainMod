using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Items.Weapons.Common;

namespace TimeDomain.Common.Globals
{
    public class VanillaBossAIChangeProjectile : GlobalProjectile
    {
        public override bool PreAI(Projectile projectile)
        {
            //if (projectile.type == ProjectileID.BloodNautilusShot)
            //{
            //    //projectile.rotation = projectile.velocity.ToRotation() - MathHelper.Pi / 2;
            //    return false;
            //}
            if (projectile.type == ProjectileID.GoldenShowerHostile)
            {
                for (int i = 0; i < 3; i++)
                {
                    float num121 = projectile.velocity.X / 3f * (float)i;
                    float num122 = projectile.velocity.Y / 3f * (float)i;
                    int num123 = 14;
                    int num124 = Dust.NewDust(new Vector2(projectile.position.X + (float)num123, projectile.position.Y + (float)num123), projectile.width - num123 * 2, projectile.height - num123 * 2, DustID.Ichor, 0f, 0f, 100, default(Color), 1f);
                    Main.dust[num124].noGravity = true;
                    Dust dust35 = Main.dust[num124];
                    Dust dust212 = dust35;
                    dust212.velocity *= 0.1f;
                    dust35 = Main.dust[num124];
                    dust212 = dust35;
                    dust212.velocity += projectile.velocity * 0.5f;
                    Main.dust[num124].position.X -= num121;
                    Main.dust[num124].position.Y -= num122;
                }
                if (Main.rand.Next(8) == 0)
                {
                    int num125 = 16;
                    int num126 = Dust.NewDust(new Vector2(projectile.position.X + (float)num125, projectile.position.Y + (float)num125), projectile.width - num125 * 2, projectile.height - num125 * 2, DustID.Ichor, 0f, 0f, 100, default(Color), 0.5f);
                    Dust dust36 = Main.dust[num126];
                    Dust dust212 = dust36;
                    dust212.velocity *= 0.25f;
                    dust36 = Main.dust[num126];
                    dust212 = dust36;
                    dust212.velocity += projectile.velocity * 0.5f;
                }
                return false;
            }
            if (projectile.type == ProjectileID.BloodNautilusShot)
            {
                if (projectile.localAI[0] == 0f)
                {
                    SoundEngine.PlaySound(SoundID.Item171, new Vector2?(projectile.Center), null);
                    projectile.localAI[0] = 1f;
                    for (int num162 = 0; num162 < 8; num162++)
                    {
                        Dust dust40 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Blood, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f)];
                        dust40.velocity = (Main.rand.NextFloatDirection() * 3.1415927f).ToRotationVector2() * 2f + projectile.velocity.SafeNormalize(Vector2.Zero) * 2f;
                        dust40.scale = 0.9f;
                        dust40.fadeIn = 1.1f;
                        dust40.position = projectile.Center;
                    }
                }
                projectile.alpha -= 20;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
                for (int num163 = 0; num163 < 2; num163++)
                {
                    Dust dust41 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Blood, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f)];
                    dust41.velocity = dust41.velocity / 4f + projectile.velocity / 2f;
                    dust41.scale = 1.2f;
                    dust41.position = projectile.Center + Main.rand.NextFloat() * projectile.velocity * 2f;
                }
                for (int num164 = 1; num164 < projectile.oldPos.Length; num164++)
                {
                    if (projectile.oldPos[num164] == Vector2.Zero)
                    {
                        break;
                    }
                    if (Main.rand.Next(3) == 0)
                    {
                        Dust dust42 = Main.dust[Dust.NewDust(projectile.oldPos[num164], projectile.width, projectile.height, DustID.Blood, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f)];
                        dust42.velocity = dust42.velocity / 4f + projectile.velocity / 2f;
                        dust42.scale = 1.2f;
                        dust42.position = projectile.oldPos[num164] + projectile.Size / 2f + Main.rand.NextFloat() * projectile.velocity * 2f;
                    }
                }
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
                return false;
            }
            return base.PreAI(projectile);
        }
        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if (projectile.type == ProjectileID.DeathLaser)
            {
                if (ChangeAI == 1)
                {
                    target.AddBuff(BuffID.Darkness, 180);
                }
            }
        }
        public override bool InstancePerEntity => true;
        int ChangeAI = 0;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            ChangeAI = 0;
            if (projectile.type == ProjectileID.DeathLaser)
            {
                if (projectile.ai[0] == NPCID.EyeofCthulhu)
                {
                    ChangeAI = 1;
                }
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC npc, NPC.HitInfo hit, int damageDone)
        {
            int num = 1100;
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active && !Main.player[i].dead && (npc.Center - Main.player[i].position).Length() < (float)num && Main.player[i].inventory[Main.player[i].selectedItem].type == ModContent.ItemType<VampireStaff>() && Main.player[i].itemAnimation > 0)
                {
                    if (i == Main.myPlayer)
                    {
                        Main.player[i].soulDrain += 10;
                    }
                    if (Main.rand.Next(3) != 0)
                    {
                        Vector2 center = npc.Center;
                        center.X += (float)Main.rand.Next(-100, 100) * 0.05f;
                        center.Y += (float)Main.rand.Next(-100, 100) * 0.05f;
                        center += npc.velocity;
                        int num2 = Dust.NewDust(center, 1, 1, DustID.LifeDrain, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num2].velocity *= 0f;
                        Main.dust[num2].scale = (float)Main.rand.Next(70, 85) * 0.01f;
                        Main.dust[num2].fadeIn = (float)(i + 1);
                    }
                }
            }
        }  
    }
}
