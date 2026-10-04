using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.Graphics.Renderers;
using Terraria.ModLoader;

namespace TimeDomain.Content.Items.Weapons.Test
{
    public class PrettySparkleParticleTestItem : TestItemCommon
    {
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, velocity, ModContent.ProjectileType<PrettySparkleParticleTestProj>(), 10, 1, player.whoAmI);
            return false;
        }
    }
    public class PrettySparkleParticleTestProj : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_1";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(79);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ParticleOrchestraSettings settings = new ParticleOrchestraSettings()
            {
                PositionInWorld = target.Center,
                MovementVector = Projectile.velocity
            };
            PrettySparkleParticle GetNewPrettySparkleParticle()
            {
                return new PrettySparkleParticle();
            }
            float num = Main.rand.NextFloat() * 6.2831855f;
            float num2 = 6f;
            float num3 = Main.rand.NextFloat();

            Vector2 vector = settings.MovementVector * Main.rand.NextFloatDirection() * 0.15f;
            Vector2 vector2 = new Vector2(Main.rand.NextFloat() * 0.4f + 0.4f);
            float f = num + Main.rand.NextFloat() * 6.2831855f;
            float rotation = 1.5707964f;
            Vector2 vector3 = 1.5f * vector2;
            float num5 = 60f;
            Vector2 vector4 = Main.rand.NextVector2Circular(8f, 8f) * vector2;
            PrettySparkleParticle prettySparkleParticle = new ParticlePool<PrettySparkleParticle>(200, new ParticlePool<PrettySparkleParticle>.ParticleInstantiator(GetNewPrettySparkleParticle)).RequestParticle();
            prettySparkleParticle.Velocity = f.ToRotationVector2() * vector3 + vector;
            prettySparkleParticle.AccelerationPerFrame = f.ToRotationVector2() * -(vector3 / num5) - vector * 1f / 60f;
            prettySparkleParticle.ColorTint = Main.hslToRgb((num3 + Main.rand.NextFloat() * 0.33f) % 1f, 1f, 0.4f + Main.rand.NextFloat() * 0.25f, byte.MaxValue);
            prettySparkleParticle.ColorTint.A = 0;
            prettySparkleParticle.LocalPosition = settings.PositionInWorld + vector4;
            prettySparkleParticle.Rotation = rotation;
            prettySparkleParticle.Scale = vector2;
            Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
        }
        public override void AI()
        {
            Projectile.velocity = Main.LocalPlayer.Center - Projectile.Center;
            Projectile.velocity /= Projectile.velocity.Length();
            Projectile.velocity *= 5;
        }
    }
}
