using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace yourmod.Content.Items.Projectiles.Summon
{
    internal class SlimeSpike : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile
.width = 9;
            Projectile
.height = 15;
            Projectile
.friendly = true;
            Projectile
.DamageType = DamageClass.Summon;
            Projectile
.penetrate = -1;
            Projectile
.timeLeft = 3000;
            Projectile
.tileCollide = true;
            Projectile
.aiStyle = -1;
        }

        public override void AI()
        {
            Projectile
.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile
.velocity.Y += 0.1f;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust
.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood);
            }
        }
    }
}