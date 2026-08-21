using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using yourmod.Content.NPCs.Bosses.PrimordialSlime;

namespace yourmod.Content.Projectiles
{
    public class ElementalAura : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.alpha = 255;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = ProjAIStyleID.Beam;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.alpha = 0;
            Projectile.scale = 1f;
            Projectile.rotation = Projectile.velocity.ToRotation()/* - MathHelper.PiOver2*/;
            Projectile.frameCounter++;
            if (Projectile.frameCounter % 10 == 0)
            {
                //Projectile.frame = Projectile.frameCounter / 3 % Main.projFrames[Type];
                Projectile.frame++;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
        }
        public Color color = Color.White;
        Color[] colors = { Color.Red, Color.OrangeRed, Color.Orange, Color.Yellow, Color.YellowGreen, Color.Green, Color.Indigo, Color.Blue, Color.BlueViolet, Color.Purple, Color.Pink, Color.HotPink };
        public int CurrentCount = 0;
        public bool CanShoot = true;
        public override bool PreDraw(ref Color lightColor)
        {
            if (Main.GameUpdateCount % 20 == 0)
            {
                CurrentCount++;
                if (CurrentCount >= colors.Length)
                {
                    CurrentCount = 0;
                }
            }
            color = Color.Lerp(color, colors[CurrentCount], 0.05f);
            lightColor = color;
            return true;
        }
    }
}
