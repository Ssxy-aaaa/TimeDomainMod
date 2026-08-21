using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace yourmod.Content.Items.Weapons.Magic
{
    public class IceFogProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;         
            Projectile.timeLeft = 180;         
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.scale = 0.5f;
            
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(19, 30, 50, 5);//浅蓝色半透明冰雾
        }
        //private bool hasSlowed = false;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 180);
        }
        public override void AI()
        {
            Projectile.velocity *= 0.97f;
            if (Projectile.scale < 3)
                Projectile.scale += 0.05f;
        }
    }
}