using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TimeDomain.Content.Projectiles
{
    public class JungleAcidSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(176);
            Projectile.alpha = 255;
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            AIType = ProjectileID.JungleSpike;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Main.rand.Next(4) == 0)
            {
                target.AddBuff(BuffID.Venom, 1200, true, false);
                return;
            }
            if (Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Venom, 300, true, false);
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }
        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }
        public override void AI()
        {
            base.AI();
        }
    }
}
