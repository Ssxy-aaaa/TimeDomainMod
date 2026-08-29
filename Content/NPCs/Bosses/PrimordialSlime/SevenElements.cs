using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Common;
using TimeDomain.Content.Projectiles;

namespace TimeDomain.Content.NPCs.Bosses.PrimordialSlime
{
    public class SevenElements : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 54;//54
            NPC.height = 46;//368
            NPC.noGravity = true;
            NPC.damage = 50;
            NPC.lifeMax = 1000;
            NPC.noTileCollide = true;
            NPC.boss = true;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.defense = 3;
            ModUtil.SetNPCDamageAndLifeMax(NPC, 55, 70, 96, 1200, 1700, 2100);
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 8;
        }
        public override void FindFrame(int frameHeight)
        {
            frameHeight = 46;
            NPC.frameCounter++;
            if (NPC.frameCounter % 6 == 0)
            {
                NPC.frame.Y += frameHeight;
            }
            if (NPC.frame.Y > frameHeight * 7)
            {
                NPC.frame.Y = 0;
            }
        }
        public Player player
        {
            get => Main.player[Main.npc[(int)NPC.ai[0]].target] ?? Main.player[Main.myPlayer];
        }
        public float BossTime
        {
            get => NPC.localAI[0];
            set => NPC.localAI[0] = value;
        }
        public float BossTime2
        {
            get => NPC.localAI[1];
            set => NPC.localAI[1] = value;
        }
        public override void AI()
        {
            BossTime++;
            BossTime2++;
            if (BossTime2 == 1)
            {
                NPC.velocity = new Vector2(0, -10);
            }
            float Vx = Math.Clamp(NPC.velocity.X, -20, 20);
            NPC.rotation = Vx / (20 / (MathHelper.PiOver2 / 2));

            if (Main.npc[(int)NPC.ai[0]] != null && Main.npc[(int)NPC.ai[0]].active && Main.npc[(int)NPC.ai[0]].type == ModContent.NPCType<PrimordialSlime>())
            {
                NPC.active = true;
            }
            else
            {
                NPC.active = false;
            }
            Vector2 Goal = (Main.npc[(int)NPC.ai[0]].Center + player.Center) / 2;
            Vector2 v = Goal - NPC.Center;
            float s = 0.01f;
            float L = NPC.velocity.Length();
            if (L < 10)
                L = 10;
            if (BossTime2 % 3 == 0)
                NPC.velocity = Vector2.Normalize(NPC.velocity + (v * s)) * L;
            if (BossTime2 % 90 == 0)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(player.Center - NPC.Center) * 5f, ModContent.ProjectileType<ElementalAura>(), ModUtil.SetProjectileDamage(110, 140, 162), 0);
            }
        }
    }
}
