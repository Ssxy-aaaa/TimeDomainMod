using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using yourmod.Common;
using static System.Net.Mime.MediaTypeNames;

namespace yourmod.Content.NPCs.Bosses.ThePonder
{
    public class ThePonder : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 12;
            
        }
        public override void SetDefaults()
        {
            NPC.width = 160;//194;
            NPC.height = 140;//176;
            NPC.noGravity = true;
            NPC.boss = true;
            ModUtil.SetNPCDamageAndLifeMax(NPC, 300, 450, 600, 30000, 45000, 60000);
            NPC.HitSound = new SoundStyle?(SoundID.NPCHit4);
            NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath14);
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.defense = 30;
        }
        public override void FindFrame(int frameHeight)
        {
            frameHeight = 2112 / 12;//176
            NPC.frame.Y = ++NPC.frameCounter % 10 == 0 ? (NPC.frame.Y == frameHeight * (Main.npcFrameCount[Type] - 1) ? 0 : NPC.frame.Y + frameHeight) : NPC.frame.Y;
        }
        /// <summary>
        /// 当前生成了几个机械boss
        /// </summary>
        public int SpawnPrimeBossCount = 0;
        /// <summary>
        /// 当前boss阶段
        /// </summary>
        public int Phase
        {
            get 
            {
                return
                    Main.expertMode ?
                    (1 + (NPC.life < NPC.lifeMax / 3 * 2).ToInt() + (NPC.life < NPC.lifeMax / 5).ToInt()) :
                    (1 + (NPC.life < NPC.lifeMax / 2).ToInt());
            }
        }
        public float Timer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        public int PerShootTick => Main.masterMode ? 10 : (Main.expertMode ? 15 : 20) - Main.getGoodWorld.ToInt() * 3;
        public int ShootCount => Main.getGoodWorld.ToInt() + Main.masterMode.ToInt() + Main.expertMode.ToInt() + 1;
        public Player player => Main.player[NPC.target];
        public void SetNPCPos_Lerp(object obj, float a = 0.05f)
        {
            if (obj is Vector2 Target)
            {
                NPC.velocity = Vector2.Lerp(NPC.Center, Target, a) - NPC.Center;
            }
            if (obj is Entity entity)
            {
                NPC.velocity = Vector2.Lerp(NPC.Center, entity.Center, a) - NPC.Center;
            }
        }
        public int IsShootT = 0;
        Vector2 Target;
        public override void AI()
        {
            Timer++;
            if (Timer == 1)
                Target = player.Center;
            //Main.NewText(Phase, Color.Red);
            if (Phase == 1 && !(Main.getGoodWorld && Main.masterMode) && !Main.dayTime)
                NPC.damage = 0;
            else
                ModUtil.SetNPCDamageAndLifeMax_InBossFight(NPC, 300, 450, 600, 30000, 45000, 60000);
            if (player == null || !player.active || player.dead)
            {
                NPC.TargetClosest(true);
            }
            for (int c = 0; c < ShootCount; c++)
            {
                if (Timer % 180 == c * PerShootTick)
                {
                    float Rand = Main.rand.Next(79) / 100f;
                    for (int i = 0; i < 8; i++)
                    {
                        float ro = -MathHelper.Pi + Rand * (Phase - 1) + ((float)i / 8f * MathHelper.TwoPi);
                        int damage = ModUtil.SetProjectileDamage(150, 250, 350);
                        Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), player.Center + ro.ToRotationVector2() * 960, Vector2.Zero, ProjectileID.DeathLaser, damage, 0);
                        proj.velocity = Vector2.Normalize(player.Center - proj.Center) * 1;
                        if (!Main.masterMode)
                            proj.velocity /= 2;
                        else if (Main.getGoodWorld)
                            proj.velocity *= 2;
                    }
                    //if (Timer % 180 == 0)
                    {
                        IsShootT = 60;
                        Target = player.Center + (Main.rand.Next(4) * MathHelper.PiOver2 + MathHelper.PiOver4).ToRotationVector2() * 720;
                    }
                }
            }
            if (IsShootT > 0)
                IsShootT--;
            Target = IsShootT > 0 ? Target : player.Center;
            SetNPCPos_Lerp(Target, 0.05f - (Phase > 1).ToInt() * 0.04f + (Phase > 2).ToInt() * 0.01f);
            if (NPC.life < NPC.lifeMax * 0.9f)
            {
                if (SpawnPrimeBossCount == 0)
                {
                    SpawnPrimeBossCount++;
                    int npc = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.Retinazer);
                    int npc2 = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.Spazmatism);
                    Main.npc[npc].damage /= 3;
                    Main.npc[npc].lifeMax /= 3;
                    Main.npc[npc].life /= 3;
                    Main.npc[npc2].damage /= 3;
                    Main.npc[npc2].lifeMax /= 3;
                    Main.npc[npc2].life /= 3;
                }
            }
            if (NPC.life < NPC.lifeMax * 0.75f)
            {
                if (SpawnPrimeBossCount == 1)
                {
                    SpawnPrimeBossCount++;
                    int npc = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.TheDestroyer);
                    Main.npc[npc].damage /= 3;
                    Main.npc[npc].lifeMax /= 3;
                    Main.npc[npc].life /= 3;
                }
            }
            if (NPC.life < NPC.lifeMax * 0.6f)
            {
                if (SpawnPrimeBossCount == 2)
                {
                    SpawnPrimeBossCount++;
                    int npc = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.SkeletronPrime);
                    Main.npc[npc].damage /= 3;
                    Main.npc[npc].lifeMax /= 3;
                    Main.npc[npc].life /= 3;
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {

            return base.PreDraw(spriteBatch, screenPos, drawColor);
        }
        public override void DrawEffects(ref Color drawColor)
        {
            drawColor *= 1.25f;
            SpriteBatch sb = Main.spriteBatch;
            Vector2 screenPos = Main.screenPosition;
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            for (int i = 1; i < 10; i++)
            {
                Color c = drawColor;
                c.A = (byte)(150 - (i / 10f) * 100);
                sb.Draw(TextureAssets.Npc[Type].Value, NPC.oldPos[i] - screenPos, NPC.frame, drawColor, NPC.rotation, Vector2.Zero, NPC.scale, 0, 0);
            }
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
        }
    }
    public class DeathLaserChange : GlobalProjectile
    {
        public override bool PreAI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.DeathLaser)
            {
                //if (projectile.ai[0] == 114514)
                {
                    projectile.velocity *= 1.01f;
                }
            }
            return true;
        }
    }
}
