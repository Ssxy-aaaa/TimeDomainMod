using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace yourmod.Content.Items.Weapons.Summon.Whip
{
    public class OpticNeuronProj2 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Default;
            Projectile.timeLeft = 360;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 6;
        }
        public void UpdateFrame()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 2)
            {
                Projectile.frame = 0;
            }
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
        }
        public int Timer = 0;

        #region OldAI
        //public NPC TargetClosest()
        //{
        //    float c = 3600;
        //    NPC TarNPC = null;
        //    foreach (NPC npc in Main.npc)
        //    {
        //        if (Vector2.Distance(Projectile.Center, npc.Center) < c)
        //        {
        //            c = Vector2.Distance(Projectile.Center, npc.Center);
        //            TarNPC = npc;
        //        }
        //    }
        //    if (TarNPC == null)
        //    {
        //        Main.NewText("1");
        //    }
        //    return TarNPC;
        //}
        //public NPC ProjTargetNPC = null;
        //public float disF = 0;
        //public override void AI()
        //{
        //    Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.Pi / 2;
        //    Timer++;
        //    if (Timer < 40)
        //    {
        //        Projectile.velocity *= 0.96f;
        //    }
        //    if (Timer == 41)
        //    {
        //        ProjTargetNPC = TargetClosest();
        //    }
        //    if (Timer > 41)
        //    {
        //        NPC npc = TargetClosest();
        //        float nr = (npc.Center - Projectile.Center).ToRotation();
        //        float a = Projectile.velocity.ToRotation();
        //        float r = nr - Projectile.velocity.ToRotation();
        //        if (r > 0)
        //        {
        //            a += MathHelper.Pi / 30;
        //        }
        //        else
        //        {
        //            a -= MathHelper.Pi / 30;
        //        }
        //        Projectile.velocity = a.ToRotationVector2() * 6f;
        //        //Main.NewText(Projectile.rotation, new Color(100, 200, 100));
        //    }

        //    Projectile.frameCounter++;
        //    if (Projectile.frameCounter > 6)
        //    {
        //        Projectile.frame++;
        //        Projectile.frameCounter = 0;
        //    }
        //    if (Projectile.frame > 2)
        //    {
        //        Projectile.frame = 0;
        //    }
        //}
        #endregion
        public NPC npc => Main.npc[TargetWhoAmI];
        public int TargetWhoAmI = -1;
        public override bool? CanHitNPC(NPC target)
        {
            if (Timer <= 40)
            {
                return false;
            }
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 7; i++)
            {
                int L = Main.rand.Next(6, 12);
                int dust = Dust.NewDust(Projectile.Center, L, L, DustID.Blood, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4));
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 9 / L;
                Main.dust[dust].scale = L / 6;
                //Main.dust[dust].alpha = 255;
            }
        }
        public override void AI()
        {
            bool NPCNULL = false;
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.Pi / 2;
            Timer++;
            if (TargetWhoAmI == -1 || npc == null || !npc.active)
            {
                TargetWhoAmI = Projectile.FindTargetWithLineOfSight(1200);
                NPCNULL = TargetWhoAmI == -1 || npc == null || !npc.active;
            }
            if (Timer <= 40)
            {
                //Projectile.velocity *= 0.98f;
                if (Timer == 20)
                {
                    Projectile.velocity += new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1));
                }
            }
            else
            {
                float L = Projectile.velocity.Length();
                Vector2 ToTargetV = NPCNULL || npc.friendly ? Main.player[Projectile.owner].Center - Projectile.Center : npc.Center - Projectile.Center;
                float s = 0.01f + (float)Timer / 7500;
                Projectile.velocity = Vector2.Normalize(Projectile.velocity + (ToTargetV * s)) * L;
                if (Timer % 6 == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int dL = Main.rand.Next(6, 12);
                        int dust = Dust.NewDust(Projectile.Center, dL, dL, DustID.Blood, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4));
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].velocity *= 9 / dL;
                        Main.dust[dust].scale = dL / 6;
                        //Main.dust[dust].alpha = 255;
                    }
                }
                if (Timer <= 180)
                    Projectile.velocity *= 1.01f;
                if(Timer > 300)
                    Projectile.Kill();
            }


            UpdateFrame();
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
