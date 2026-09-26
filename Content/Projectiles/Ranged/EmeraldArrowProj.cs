using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Projectiles.BaseProjectile;

namespace TimeDomain.Content.Projectiles.Ranged
{
    public class EmeraldArrowProj : GemArrowActionProj
    {
        public override string Texture => "TimeDomain/Content/Projectiles/Ranged/EmeraldArrowProj";

        public override void SetDefaults()
        {
            slot[0] = Emerald;
            base.SetDefaults();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                for (int i = 0; i < 2; i++)
                {
                    Projectile.NewProjectile(
                        Projectile.GetSource_OnHit(target),
                        Projectile.Center,
                        new Vector2(0, -10).RotatedByRandom(2),
                        ModContent.ProjectileType<EmeraldArrowShardProj>(),
                        Projectile.damage / 2,
                        2f,
                        Projectile.owner
                    );
                }
            }
            base.OnHitNPC(target, hit, damageDone);
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemEmerald);
                }
            }
            base.OnKill(timeLeft);
        }
    }
}