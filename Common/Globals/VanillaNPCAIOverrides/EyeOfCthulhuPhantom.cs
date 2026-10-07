using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TimeDomain.Common.Globals.VanillaNPCAIOverrides
{
    public class EyeOfCthulhuPhantom : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_4";
        public override void SetDefaults()
        {
            NPC.CloneDefaults(4);
            NPC.boss = false;
            //ModUtil.SetNPCDamageAndLifeMax(NPC, 80, 120, 150, 800, 1200, 1500);
            NPC.damage = 0;
            NPC.value = 0;
        }
        public float Time
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        public NPC EyeOfCthulhu => Main.npc[(int)NPC.ai[0]];
        public Player player => Main.player[NPC.target];
        public float dx = 0;
        public float dy = 0;
        public float Acc = 0.2f;
        public override void AI()
        {
            Time++;
            if (Time == 1)
            {
                dx = Main.rand.Next(3) - 1;
                dy = Main.rand.Next(3) - 1;
                if (dx == dy && dx == 0)
                {
                    dx = 1;
                    dy = Main.rand.Next(3) - 1;
                }
            }
            NPC.TargetClosest(true);
            NPC.rotation = (player.Center - NPC.Center).ToRotation() - MathHelper.PiOver2;
            if (!EyeOfCthulhu.active || EyeOfCthulhu.type != NPCID.EyeofCthulhu)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.type == NPCID.EyeofCthulhu && npc.active)
                    {
                        NPC.ai[0] = i;
                        break;
                    }
                }
                if (!EyeOfCthulhu.active)
                {
                    NPC.velocity.Y -= 0.04f;
                    NPC.EncourageDespawn(10);
                    return;
                }
            }
            if (EyeOfCthulhu.active)
            {
                if (EyeOfCthulhu.dontTakeDamage)
                {
                    if (Time % 24 == 0)
                    {
                        Vector2 v = player.Center - NPC.Center;
                        v.Normalize();
                        v *= 7f;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, v, ProjectileID.DeathLaser, 17, 0, player.whoAmI, NPCID.EyeofCthulhu);
                    }
                }
                Vector2 C = new Vector2((EyeOfCthulhu.Center.X - player.Center.X) * dx, (EyeOfCthulhu.Center.Y - player.Center.Y) * dy);
                Vector2 toP = Vector2.Normalize(C + player.Center - NPC.Center);
                NPC.velocity += toP * Acc;
                if (Math.Abs(toP.ToRotation() - NPC.velocity.ToRotation()) > MathHelper.PiOver2)
                {
                    NPC.velocity += toP * Acc;
                }
                if (NPC.velocity.Length() > EyeOfCthulhuAI.MaxSpeed)
                {
                    NPC.velocity.Normalize();
                    NPC.velocity *= EyeOfCthulhuAI.MaxSpeed;
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return false;
        }
    }
}
