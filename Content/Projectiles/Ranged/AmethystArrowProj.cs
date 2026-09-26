using Terraria;
using Terraria.ID;
using TimeDomain.Common.Utils;
using TimeDomain.Content.Projectiles.BaseProjectile;

namespace TimeDomain.Content.Projectiles.Ranged
{
    public class AmethystArrowProj : GemArrowActionProj
    {
        public override string Texture => "TimeDomain/Content/Projectiles/Ranged/AmethystArrowProj";

        public override void SetDefaults()
        {
            slot[0] = Amethyst;
            base.SetDefaults();
        }

        public override void AI()
        {
            base.AI();
            ProjectileBehaviorUtils.HomingToNearestNPC(Projectile, 200);
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);

            if (Main.netMode != NetmodeID.Server)
            {
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemAmethyst);
                }
            }
        }
    }
}