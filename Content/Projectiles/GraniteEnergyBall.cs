using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TimeDomain.Content.Projectiles
{
    public class GraniteEnergyBall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = Projectile.width = 16;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 1;
        }
        public int Timer = 0;
        public override void AI()
        {
            if (Projectile.ai[0] == 1)
            {
                if (Projectile.ai[1] == Main.myPlayer)
                {
                    
                }
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Timer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Timer = reader.ReadInt32();
        }
    }
}
