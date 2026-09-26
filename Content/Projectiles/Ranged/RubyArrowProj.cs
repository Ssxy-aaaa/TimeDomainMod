using Terraria;
using Terraria.Audio;
using Terraria.ID;
using TimeDomain.Common.Utils;
using TimeDomain.Content.Projectiles.BaseProjectile;

namespace TimeDomain.Content.Projectiles.Ranged
{
    public class RubyArrowProj : GemArrowActionProj
    {
        public override string Texture => "TimeDomain/Content/Projectiles/Ranged/RubyArrowProj";

        public override void SetDefaults()
        {
            slot[0] = Ruby;
            base.SetDefaults();
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);

            ProjectileBehaviorUtils.Explode(Projectile, Projectile.Center, 200, Projectile.damage / 2, 0, Projectile.owner);
            SoundEngine.PlaySound(SoundID.Item102, Projectile.Center);

            if (Main.netMode != NetmodeID.Server)
            {
                SpawnRingDust(Projectile.Center, 10, 50, DustID.GemRuby, speed: 10);
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby);
                }
            }
        }
    }
}