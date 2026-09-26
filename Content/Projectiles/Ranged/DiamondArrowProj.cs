using Terraria;
using Terraria.ID;
using TimeDomain.Content.Projectiles.BaseProjectile;

namespace TimeDomain.Content.Projectiles.Ranged
{
    public class DiamondArrowProj : GemArrowActionProj
    {
        public override string Texture => "TimeDomain/Content/Projectiles/Ranged/DiamondArrowProj";

        public override void SetDefaults()
        {
            slot[0] = Diamond;
            slot[1] = Amber;
            slot[2] = Emerald;
            base.SetDefaults();
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);

            if (Main.netMode != NetmodeID.Server)
            {
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemDiamond);
                }
            }
        }
    }
}