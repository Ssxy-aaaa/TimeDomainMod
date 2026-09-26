using Terraria;
using Terraria.ID;
using TimeDomain.Content.Projectiles.BaseProjectile;

namespace TimeDomain.Content.Projectiles.Ranged
{
    public class TopazArrowProj : GemArrowActionProj
    {
        public override string Texture => "TimeDomain/Content/Projectiles/Ranged/TopazArrowProj";

        public override void SetDefaults()
        {
            slot[0] = Topaz;
            base.SetDefaults();
        }

        public override void AI()
        {
            base.AI();
            if (Projectile.velocity.Length() < 30)
            {
                Projectile.velocity.Y += 0.2f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemTopaz);
                }
            }
            base.OnKill(timeLeft);
        }
    }
}