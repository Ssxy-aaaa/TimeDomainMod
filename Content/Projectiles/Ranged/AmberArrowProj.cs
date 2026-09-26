using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TimeDomain.Content.Projectiles.BaseProjectile;

namespace TimeDomain.Content.Projectiles.Ranged
{
    public class AmberArrowProj : GemArrowActionProj
    {
        public override string Texture => "TimeDomain/Content/Projectiles/Ranged/AmberArrowProj";

        public override void SetDefaults()
        {
            slot[0] = Amber;
            base.SetDefaults();
        }

        public override void AI()
        {
            if (Projectile.timeLeft == 260)
            {
                Vector2 baseDir = Projectile.velocity.SafeNormalize(Vector2.Zero);
                float speed = Projectile.velocity.Length();

                Vector2 dir1 = baseDir.RotatedBy(MathHelper.ToRadians(-30));
                Vector2 dir2 = baseDir.RotatedBy(MathHelper.ToRadians(30));

                int proj1 = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, dir1 * speed, Projectile.type, Projectile.damage / 2, 2f, Projectile.owner);
                int proj2 = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, dir2 * speed, Projectile.type, Projectile.damage / 2, 2f, Projectile.owner);

                Main.projectile[proj1].timeLeft = 100;
                Main.projectile[proj2].timeLeft = 100;
                Projectile.Kill();
            }

            base.AI();
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);

            if (Main.netMode != NetmodeID.Server)
            {
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemAmber);
                }
            }
        }
    }
}