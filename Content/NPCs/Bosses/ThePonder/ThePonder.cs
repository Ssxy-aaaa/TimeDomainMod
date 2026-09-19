using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Common;

namespace TimeDomain.Content.NPCs.Bosses.ThePonder
{
    [AutoloadBossHead]
    public class ThePonder : ModNPC
    {
        public Texture2D NPCTexture;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 12;
            NPCTexture = ModContent.Request<Texture2D>("TimeDomain/Content/NPCs/Bosses/ThePonder/ThePonder").Value;
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
            get => 
                Main.expertMode ? 
                (1 + (NPC.life < NPC.lifeMax / 3 * 2).ToInt() + (NPC.life < NPC.lifeMax / 5).ToInt()) : 
                (1 + (NPC.life < NPC.lifeMax / 2).ToInt());
        }
        public float Timer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        public float Timer2
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        public int PerShootTick => Main.masterMode ? 10 : (Main.expertMode ? 15 : 20) - Main.getGoodWorld.ToInt() * 3;
        public int ShootCount => Main.getGoodWorld.ToInt() + Main.masterMode.ToInt() + 1;
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
        public int IsShootTime = 0;
        Vector2 Target;
        enum SkillState
        {
            None,
            ReleasePlugin,
            Teleport,

            ShootLaser,

            Sprint,
        }
        SkillState State = SkillState.None;
        Dictionary<float, SkillState> SkillDicP1 => new Dictionary<float, SkillState>() 
        { 
            { 0, SkillState.None },
            { 1, SkillState.ReleasePlugin } 
        };
        Dictionary<float, SkillState> SkillDicP2 => new Dictionary<float, SkillState>()
        {
            { 0, SkillState.None },
            { 1, SkillState.ReleasePlugin },
            { 2, SkillState.Teleport }
        };
        Dictionary<float, SkillState> SkillDicP3 => new Dictionary<float, SkillState>()
        {
            { 0, SkillState.None },
            { 1, SkillState.ReleasePlugin },
            { 2, SkillState.Teleport }
        };
        public float SkillCount = 0;
        public void SetSkill()
        {
            float skillCount = SkillCount;
            if (Phase == 1)
                skillCount = SkillCount % SkillDicP1.Count;
            if (Phase == 2)
                skillCount = SkillCount % SkillDicP2.Count;
            if (Phase == 3)
                skillCount = SkillCount % SkillDicP3.Count;
            if (Phase == 1)
                State = SkillDicP1[skillCount];
            if (Phase == 2)
                State = SkillDicP2[skillCount];
            if (Phase == 3)
                State = SkillDicP3[skillCount];
        }
        public void SetDamage()
        {
            if (Phase == 1 && !(Main.getGoodWorld && Main.masterMode) && !Main.dayTime)
                NPC.damage = 0;
            else
                ModUtil.SetNPCDamageAndLifeMax_InBossFight(NPC, 300, 450, 600, 30000, 45000, 60000);
        }
        public void GlobalShoot()
        {
            for (int c = 0; c < ShootCount; c++)
            {
                if (Timer % 360 == c * PerShootTick)
                {
                    float Rand = Main.rand.Next(79) / 100f;
                    float ProjC = 2f;
                    for (int i = 0; i < ProjC; i++)
                    {
                        float ro = -MathHelper.Pi + Rand * (Phase - 1) + ((float)i / ProjC * MathHelper.TwoPi);
                        int damage = ModUtil.SetProjectileDamage(150, 250, 350);
                        Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), player.Center + ro.ToRotationVector2() * 960, Vector2.Zero, ProjectileID.DeathLaser, damage, 0);
                        proj.velocity = Vector2.Normalize(player.Center - proj.Center) * 1;
                        if (!Main.masterMode)
                            proj.velocity /= 2;
                        else if (Main.getGoodWorld)
                            proj.velocity *= 2;
                    }
                    IsShootTime = 60;
                    Target = player.Center + (Main.rand.Next(4) * MathHelper.PiOver2 + MathHelper.PiOver4).ToRotationVector2() * 720;
                }
            }
            if (IsShootTime > 0)
                IsShootTime--;
        }
        public override void AI()
        {
            Timer++;
            if (Timer == 1)
                Target = player.Center;
            if (player == null || !player.active || player.dead)
                NPC.TargetClosest(true);
            SetDamage();
            GlobalShoot();
            Main.NewText($"State:{State}_Timer2:{Timer2}", Color.Red);
            switch (State)
            {
                case SkillState.None:
                    Timer2++;
                    Target = IsShootTime > 0 ? Target : player.Center;
                    SetNPCPos_Lerp(Target, 0.05f - (Phase > 1).ToInt() * 0.04f + (Phase > 2).ToInt() * 0.01f);
                    if (Timer2 == 200)
                    {
                        Timer2 = 0;
                        SkillCount++;
                        SetSkill();
                    }
                    break;
                case SkillState.ReleasePlugin:
                    Timer2++;
                    NPC.velocity *= 0.96f;
                    if (Timer2 <= 240 && Timer2 % 60 == 0)
                    {
                        int PluginCount = 0;
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            NPC npc = Main.npc[i];
                            if (npc.type == ModContent.NPCType<Plugin>()&&npc.active)
                            {
                                PluginCount++;
                            }
                        }
                        if (PluginCount < 10)
                        {
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 64, (int)NPC.Center.Y, ModContent.NPCType<Plugin>(), 0, 0, 0, 0, NPC.whoAmI);
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 64, (int)NPC.Center.Y, ModContent.NPCType<Plugin>(), 0, 0, 0, 0, NPC.whoAmI);
                        }
                        NPC.velocity += Vector2.Normalize(player.Center - NPC.Center) * -9f;
                    }
                    if (Timer2 == 250)
                    {
                        Timer2 = 0;
                        SkillCount++;
                        SetSkill();
                    }
                    break;
                case SkillState.Teleport:
                    Timer2++;
                    if (Timer2 == 1)
                    {
                        NextTeleportPos = FindTeleportPos();
                        IsTeleportDraw = true;
                    }
                    if (Timer2 == 120)
                    {
                        IsTeleportDraw = false;
                        NPC.position = NextTeleportPos;
                        Timer2 = 0;
                        SkillCount++;
                        SetSkill();
                    }
                    break;
            }
        }
        public bool IsTeleportDraw;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (IsTeleportDraw)
            {
                for (int i = 0; i < 6; i++)
                {
                    float ro = -MathHelper.Pi + (i / 6f) * MathHelper.TwoPi;
                    Color newColor = NPC.color;
                    newColor.A = (byte)(255f - (Timer2 / 120f) * 255f);


                    //Main.spriteBatch.End();
                    //Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                    Main.spriteBatch.Draw(
                        NPCTexture, 
                        NextTeleportPos + ro.ToRotationVector2() * (120 - Timer2) - Main.screenPosition, 
                        NPC.frame,
                        newColor, 
                        0f, 
                        Vector2.Zero, 
                        NPC.scale, 
                        0, 
                        0f);
                    //Main.spriteBatch.End();
                    //Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                }
            }
            return base.PreDraw(spriteBatch, screenPos, drawColor);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            base.PostDraw(spriteBatch, screenPos, drawColor);
        }
        public Vector2 NextTeleportPos;
        public Vector2 FindTeleportPos()
        {
            Vector2 pos = player.Center + (Main.rand.Next(628) / 100f).ToRotationVector2() * Main.rand.Next(350, 450);
            return pos;
        }
        public void SpawnPrimeBoss()
        {
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
