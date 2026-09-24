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

            NPCID.Sets.TrailingMode[Type] = 3;
            NPCID.Sets.TrailCacheLength[Type] = 10;
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
                ModUtil.SetNPCDamageAndLifeMax_InBossFight(NPC, 200, 270, 350, 30000, 45000, 60000);
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
        public List<int> ControledPlugin = new List<int>();
        public override void AI()
        {
            Timer++;
            if (Timer == 1)
                Target = player.Center;
            if (player == null || !player.active || player.dead)
                NPC.TargetClosest(true);
            SetDamage();
            GlobalShoot();
            //Main.NewText($"State:{State}_Timer2:{Timer2}", Color.Red);
            switch (State)
            {
                case SkillState.None:
                    Timer2++;
                    Target = IsShootTime > 0 ? Target : player.Center;
                    SetNPCPos_Lerp(Target, 0.03f/* - (Phase > 1).ToInt() * 0.04f + (Phase > 2).ToInt() * 0.01f*/);
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
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 64, (int)NPC.Center.Y, ModContent.NPCType<Plugin>(), 0, 0, 0, 0, NPC.whoAmI, player.whoAmI);
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 64, (int)NPC.Center.Y, ModContent.NPCType<Plugin>(), 0, 0, 0, 0, NPC.whoAmI, player.whoAmI);
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
                    NPC.velocity *= 0.95f;
                    if (Timer2 == 120)
                    {
                        IsTeleportDraw = false;
                        NPC.position = NextTeleportPos;
                        NPC.velocity *= 0f;
                    }
                    if (Timer2 == 130)
                    {
                        Timer2 = 0;
                        SkillCount++;
                        SetSkill();
                    }
                    break;
            }
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.ModNPC is Plugin plugin)
                {
                    if (plugin.state == Plugin.SkillState.BeenControlled)
                    {
                        plugin.IsBeenControlled = true;
                    }
                }
            }
            //if (ControledPlugin.Count >= 1)
            //{
            //    Main.npc[ControledPlugin[Main.rand.Next(ControledPlugin.Count)]].
            //}
        }
        public bool IsTeleportDraw;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {

            Texture2D Tex = TextureAssets.Npc[NPC.type].Value;
            if (IsTeleportDraw)
            {
                for (int i = 0; i < 6; i++)
                {
                    float ro = -MathHelper.Pi + (i / 6f) * MathHelper.TwoPi;


                    //Main.spriteBatch.End();
                    //Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                   spriteBatch.Draw(
                        Tex, 
                        NextTeleportPos + ro.ToRotationVector2() * (120f - Timer2) - new Vector2(0, 14) - Main.screenPosition, 
                        NPC.frame,
                        Color.White * (Timer2 / 120f),
                        0f, 
                        Vector2.Zero, 
                        NPC.scale,
                        SpriteEffects.None,
                        0f);
                }
            }
            Rectangle sourceRect = NPC.frame;
            Vector2 Origin = new Vector2(sourceRect.Width / 2f, sourceRect.Height / 2f);
            for (int i = 0; i < NPC.oldPos.Length; i++)
            {
                float progress = i / (float)NPC.oldPos.Length;
                float alpha = (1f - progress) * 0.6f;
                Vector2 drawPos = NPC.oldPos[i]+NPC.Size/2- Origin - new Vector2(0,14)- Main.screenPosition;
                Color trailColor = Color.White * alpha;
                spriteBatch.Draw(Tex, drawPos, sourceRect, trailColor, NPC.rotation, Vector2.Zero, NPC.scale, SpriteEffects.None, 0f);
            }
            for (int i = 0; i < ControledPlugin.Count; i++)
            {
                int npcWhoAmI = ControledPlugin[i];
                NPC npc = Main.npc[npcWhoAmI];
                if (npc.active)
                {
                    Texture2D texture = TextureAssets.FishingLine.Value;
                    Rectangle frame = texture.Frame();
                    Vector2 origin = new Vector2(frame.Width / 2, 0);

                    Vector2 diff = npc.Center - NPC.Center;

                    float rotation = diff.ToRotation() - MathHelper.PiOver2;
                    Color color = Color.Red;
                    Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

                    Main.EntitySpriteDraw(texture, NPC.Center - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

                }
                else
                {
                    ControledPlugin.Remove(npcWhoAmI);
                }
            }
            return true;
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
                    //projectile.velocity *= 1.01f;
                }
            }
            return true;
        }
    }
}
