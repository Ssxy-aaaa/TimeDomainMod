using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Graphics.CameraModifiers;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TimeDomain.Common;
using TimeDomain.Common.Systems;
using TimeDomain.Content.Items.Consumables;
using TimeDomain.Content.Projectiles;

namespace TimeDomain.Content.NPCs.Bosses.PrimordialSlime
{
    [AutoloadBossHead]
    public class PrimordialSlime : ModNPC
    {
        public int SpawnItem => ModContent.ItemType<CursedGel>();
        public override void SetDefaults()
        {
            NPC.width = 100;//130
            NPC.height = 100;//424
            NPC.noGravity = false;
            NPC.damage = 50;
            NPC.lifeMax = 1000;
            NPC.noTileCollide = false;
            NPC.boss = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.defense = 3;
            ModUtil.SetNPCDamageAndLifeMax(NPC, 75, 98, 111, 2500, 3000, 3900);
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
            LocalizedText spawnInfo = LocalizedText.Empty;
            spawnInfo = TimeDomain.Instance.GetLocalization("NPCs.PrimordialSlime.spawnInfo") ?? LocalizedText.Empty;

            NPCID.Sets.TrailingMode[Type] = 3;
            NPCID.Sets.TrailCacheLength[Type] = 40;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("贪婪的古神史莱姆，吸收了7元素的力量，变得无比强大，而今只剩下一副失去灵魂空壳")
            });
        }
        public override void FindFrame(int frameHeight)
        {
            frameHeight = 106;

            if (NPC.velocity.Y != 0 || (style == Style.TeleportSlam && BossTime2 > 0 && BossTime2 < 148))
            {
                NPC.frame.Y = frameHeight * 1;
                return;
            }

            NPC.frameCounter++;
            if (NPC.frameCounter % 10 == 0)
            {
                NPC.frame.Y += frameHeight;
            }
            if (NPC.frame.Y > frameHeight * 3)
            {
                NPC.frame.Y = 0;
            }
        }
        public enum Style
        {
            Jump,
            BigJump,
            Shoot,
            TeleportSlam,   //下砸
        }
        public bool Phase2
        {
            get
            {
                if (Main.expertMode)
                    return NPC.life < NPC.lifeMax / 3 * 2;
                return NPC.life < NPC.lifeMax / 2;
            }
        }
        public Style style = new Style();
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
        public float TooFarTimer
        {
            get => NPC.localAI[2];
            set => NPC.localAI[2] = value;
        }
        public int CurrentSkill = 0;

        public int JumpMaxX
        {
            get
            {
                if (Main.masterMode && Main.getGoodWorld)
                    return 20 * 16;//14
                if (Main.masterMode)
                    return 17 * 16;//10
                if (Main.expertMode)
                    return 12 * 16;//9
                return 7 * 16;//7
            }
        }
        public int JumpMaxY
        {
            get
            {
                if (Main.masterMode && Main.getGoodWorld)
                    return 15 * 16;//
                if (Main.masterMode)
                    return 10 * 16;//
                if (Main.expertMode)
                    return 8 * 16;//
                return 6 * 16;//
            }
        }
        public int BigJumpMaxX
        {
            get
            {
                if (Main.masterMode && Main.getGoodWorld)
                    return 23 * 16;
                if (Main.masterMode)
                    return 18 * 16;
                if (Main.expertMode)
                    return 15 * 16;
                return 13 * 16;
            }
        }
        public Player player => Main.player[NPC.target];
        public Color color = Color.White;
        Color[] colors = { Color.Red, Color.OrangeRed, Color.Orange, Color.Yellow, Color.YellowGreen, Color.Green, Color.Indigo, Color.Blue, Color.BlueViolet, Color.Purple, Color.Pink, Color.HotPink };
        public int CurrentCount = 0;
        public bool CanShoot = true;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (style == Style.TeleportSlam)
            {
                Texture2D slimeTex = TextureAssets.Npc[NPC.type].Value;
                Rectangle sourceRect = NPC.frame;
                Vector2 origin = new Vector2(sourceRect.Width / 2f, sourceRect.Height / 2f);

                for (int i = 0; i < NPC.oldPos.Length; i += 4)
                {
                    float progress = i / (float)NPC.oldPos.Length;
                    float alpha = (1f - progress) * 0.6f;
                    Vector2 drawPos = NPC.oldPos[i] + NPC.Size / 2f - Main.screenPosition;
                    Color trailColor = Color.White * alpha;
                    spriteBatch.Draw(slimeTex, drawPos, sourceRect, trailColor, NPC.rotation, origin, NPC.scale, SpriteEffects.None, 0f);
                }
            }

            return true;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((byte)style);
            writer.Write(TooFarTimer);
            writer.Write(CurrentSkill);
            writer.Write(CanShoot);
            writer.Write(BossTime);
            writer.Write(BossTime2);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            style = (Style)reader.ReadByte();
            TooFarTimer = reader.ReadSingle();
            CurrentSkill = reader.ReadInt32();
            CanShoot = reader.ReadBoolean();

            BossTime = reader.ReadSingle();
            BossTime2 = reader.ReadSingle();
        }
        public void Jump(int jumpMaxX,float jumpStength,int extraJump)
        {
            int dx = (int)(player.Center.X - NPC.Center.X);
            int dy = (int)(player.Center.Y - NPC.Center.Y);
            if (Math.Abs(dx) > jumpMaxX)
                dx = dx < 0 ? -jumpMaxX : jumpMaxX;
            if (Math.Abs(dy) > JumpMaxY)
                dy = JumpMaxY;
            dy = (dy + JumpMaxY) / 2;
            NPC.velocity = new Vector2(dx, -JumpMaxY * jumpStength - extraJump) / 30;
        }

        private Style ChooseNextSkill()
        {
            Style last = (Style)CurrentSkill;

            // 基础权重
            float wJump, wBigJump, wShoot, wSlam;
            if (Phase2)
            {
                wJump = 25f;
                wBigJump = 20f;
                wShoot = 25f;
                wSlam = 30f;
            }
            else
            {
                wJump = 35f;
                wBigJump = 25f;
                wShoot = 25f;
                wSlam = 15f;
            }

            switch (last)
            {
                case Style.Jump: wJump *= 0.3f; break;
                case Style.BigJump: wBigJump *= 0.3f; break;
                case Style.Shoot: wShoot *= 0.3f; break;
                case Style.TeleportSlam: wSlam *= 0.3f; break;
            }

            // 根据与玩家的距离动态调整
            float distToPlayer = Vector2.Distance(NPC.Center, player.Center);
            if (distToPlayer > 500f)
            {
                wJump *= 1.6f;
                wBigJump *= 1.6f;
            }
            else if (distToPlayer < 200f)
            {
                wShoot *= 1.5f;
                wSlam *= 1.5f;
            }

            if (NPC.life < NPC.lifeMax * 0.25f)
            {
                wSlam *= 1.4f;
                wBigJump *= 1.3f;
            }

            float total = wJump + wBigJump + wShoot + wSlam;
            float roll = Main.rand.NextFloat() * total;

            Style pick;
            if (roll < wJump) pick = Style.Jump;
            else if (roll < wJump + wBigJump) pick = Style.BigJump;
            else if (roll < wJump + wBigJump + wShoot) pick = Style.Shoot;
            else pick = Style.TeleportSlam;

            CurrentSkill = (int)pick;
            return pick;
        }

        public override void AI()
        {
            for (int i = NPC.oldPos.Length - 1; i > 0; i--)
            {
                NPC.oldPos[i] = NPC.oldPos[i - 1];
            }
            NPC.oldPos[0] = NPC.position;

            BossTime++;
            if (BossTime == 1)
            {
                CurrentSkill = -1;
                style = Style.Jump;
                NPC.TargetClosest(true);
            }
            if (CurrentSkill == -1)
            {
                Tile tile = Main.tile[(int)(NPC.Bottom.X / 16 + 1), (int)(NPC.Bottom.Y / 16 + 1)];
                if (NPC.velocity.Y == 0 && (tile.HasTile || Main.tileSolidTop[tile.TileType] || Main.tileSolid[tile.TileType]))
                {
                    CurrentSkill = 0;
                }
                else
                {
                    return;
                }
            }
            //Main.NewText(style.ToString() + CurrentSkill);
            int ExtraJump = (int)((NPC.lifeMax - NPC.life) / (float)NPC.lifeMax * 5);
            switch (style)
            {
                case Style.Jump:
                    BossTime2++;

                    if (BossTime2 == 30)
                    {
                        Jump(JumpMaxX, 2, ExtraJump);
                    }

                    if (BossTime2 > 40 && NPC.velocity.Y == 0)
                    {
                        BossTime2 = 0;
                        NPC.velocity.X = 0f;
                        style = ChooseNextSkill();
                        NPC.TargetClosest(true);
                    }
                    break;

                case Style.BigJump:
                    BossTime2++;
                    if (BossTime2 == 30)
                    {
                        Jump(JumpMaxX, 3, ExtraJump * 2);
                    }

                    if (BossTime2 > 40 && NPC.velocity.Y == 0)
                    {
                        BossTime2 = 0;
                        NPC.velocity.X = 0f;
                        ExplodeEffect(true);
                        style = ChooseNextSkill();
                        NPC.TargetClosest(true);
                    }
                    break;

                case Style.Shoot:
                    BossTime2++;
                    if (BossTime2 == 1)
                        CurrentSkill++;

                    if (NPC.velocity.Y == 0)
                    {
                        NPC.velocity.X *= 0.9f;
                    }

                    // 蓄力阶段：灰尘向 Boss 聚拢
                    if (BossTime2 < 40 && BossTime2 % 3 == 0)
                    {
                        Vector2 offset = Main.rand.NextVector2CircularEdge(140f, 140f);
                        Dust d = Dust.NewDustDirect(NPC.Center + offset, 1, 1, DustID.Torch, 0f, 0f, 100, default, 1.6f);
                        d.velocity = -offset.SafeNormalize(Vector2.Zero) * 5f;
                        d.noGravity = true;
                    }

                    // 蓄力发光
                    if (BossTime2 < 40)
                    {
                        Lighting.AddLight(NPC.Center, new Vector3(1.5f, 0.6f, 0.2f) * (BossTime2 / 40f));
                    }

                    // 发射
                    if (BossTime2 > 40 && NPC.velocity.Y == 0 && CanShoot)
                    {
                        SoundEngine.PlaySound(SoundID.Item62, NPC.Center);

                        // 中心爆裂：向外的环形灰尘
                        for (int i = 0; i < 40; i++)
                        {
                            float angle = MathHelper.TwoPi * i / 40f;
                            Vector2 dir = angle.ToRotationVector2();
                            Vector2 spawnPos = NPC.Center + dir * 30f;
                            Dust d = Dust.NewDustDirect(spawnPos, 1, 1, DustID.Torch, 0f, 0f, 100, default, 2f);
                            d.velocity = dir * 8f;
                            d.noGravity = true;
                        }

                        // 中心烟雾
                        for (int i = 0; i < 25; i++)
                        {
                            Dust d = Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                            d.velocity *= 5f;
                            d.noGravity = true;
                        }

                        // 环形火焰粒子
                        for (int i = 0; i < 20; i++)
                        {
                            float angle = MathHelper.TwoPi * i / 20f;
                            Vector2 dir = angle.ToRotationVector2();
                            Vector2 spawnPos = NPC.Center + dir * 50f;
                            Dust d = Dust.NewDustDirect(spawnPos, 1, 1, DustID.Torch, 0f, 0f, 100, Color.Orange, 2.4f);
                            d.velocity = dir * 12f;
                            d.noGravity = true;
                            d.fadeIn = 0.5f;
                        }

                        // 屏幕轻微震动
                        Main.instance.CameraModifiers.Add(new PunchCameraModifier(NPC.Center, Vector2.UnitY, 8f, 6f, 15, 800f));

                        // 生成弹幕
                        Vector2 velocity = new Vector2(0, -1);
                        for (float r = -MathHelper.Pi; r <= MathHelper.Pi; r += MathHelper.Pi / 4)
                        {
                            float r2 = r + velocity.ToRotation();
                            Vector2 v = new Vector2((float)Math.Cos(r2), (float)Math.Sin(r2)) * 10f;
                            int proj = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, v, ModContent.ProjectileType<ElementalAura>(), ModUtil.SetProjectileDamage(110, 140, 162), 0, player.whoAmI);
                        }
                        CanShoot = false;
                    }

                    if (BossTime2 > 70 && !CanShoot)
                    {
                        BossTime2 = 0;
                        CanShoot = true;
                        style = ChooseNextSkill();
                        NPC.TargetClosest(true);
                    }
                    break;

                case Style.TeleportSlam:
                    BossTime2++;

                    Vector2 targetPos = player.Center + new Vector2(0, -500f);
                    Vector2 toTarget = targetPos - NPC.Center;

                    if (BossTime2 <= 18)
                    {
                        NPC.velocity *= 0.85f;
                        break;
                    }

                    if (BossTime2 < 118)
                    {
                        // 阶段1：快速飞向玩家头顶
                        NPC.noTileCollide = true;
                        if (toTarget.Length() > 20f)
                        {
                            NPC.velocity = toTarget.SafeNormalize(Vector2.Zero) * 40f;
                        }
                        else
                        {
                            NPC.Center = targetPos;
                            NPC.velocity = Vector2.Zero;
                            BossTime2 = 118;
                        }
                    }
                    else if (BossTime2 < 148)
                    {
                        // 阶段2：悬停 0.5 秒
                        NPC.noTileCollide = true;
                        NPC.Center = targetPos;
                        NPC.velocity = Vector2.Zero;
                    }
                    else
                    {
                        NPC.noTileCollide = false;
                        NPC.velocity.Y = 0f;
                        NPC.position.Y += 30f;
                        NPC.netUpdate = true;

                        bool onGround = Collision.SolidCollision(
                            NPC.position + new Vector2(0, NPC.height),
                            NPC.width, 4);

                        if (onGround)
                        {
                            BossTime2 = 0;
                            ExplodeEffect();
                            Main.instance.CameraModifiers.Add(new PunchCameraModifier(NPC.Center, Vector2.UnitY, 18f, 6f, 40, 1000f));
                            style = ChooseNextSkill();
                            NPC.TargetClosest(true);
                        }
                    }
                    if (BossTime2 > 318)
                    {
                        BossTime2 = 0;
                        NPC.noTileCollide = false;
                        style = ChooseNextSkill();
                        NPC.TargetClosest(true);
                    }
                    break;

            }

            if (style != Style.TeleportSlam)
            {
                float distToPlayer = Vector2.Distance(NPC.Center, player.Center);

                // 以屏幕短边一半的 1.1 倍作为"接近屏幕边缘"的阈值
                float edgeThreshold = Math.Min(Main.screenWidth, Main.screenHeight) * 0.55f;

                if (distToPlayer > edgeThreshold)
                    TooFarTimer++;
                else
                    TooFarTimer = 0;

                // 180 帧 = 3 秒，且需要落在地面才切换，避免打断跳跃
                if (TooFarTimer > 180f && NPC.velocity.Y == 0)
                {
                    TooFarTimer = 0f;
                    BossTime2 = 0f;
                    NPC.noTileCollide = true;
                    style = Style.TeleportSlam;
                    NPC.TargetClosest(true);
                    NPC.netUpdate = true;
                }
            }
            else
            {
                TooFarTimer = 0f;
            }

            if (Phase2 && NPC.ai[0] == 0)
            {
                NPC.ai[0] = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SevenElements>(), 0, NPC.whoAmI);
                Main.npc[(int)NPC.ai[0]].active = true;
            }

            if (Math.Abs(NPC.velocity.X) < 0.5f && Math.Abs(player.Center.X - NPC.Center.X) > 100f && NPC.velocity.Y == 0)
                NPC.ai[1]++;
            else
                NPC.ai[1] = 0;

            if (NPC.ai[1] > 60 && NPC.velocity.Y == 0 && (style == Style.Jump || style == Style.BigJump))
            {
                BossTime2 = 29;
                NPC.ai[1] = 0;
            }

            if (style != Style.TeleportSlam)
            {
                NPC.noTileCollide = false;
            }



            //if (Vector2.Distance(player.Center, NPC.Center) > 3000)
            //{
            //    NPC.TargetClosest(true);
            //    if (Vector2.Distance(player.Center, NPC.Center) > 3000)
            //    {
            //        NPC.active = false;
            //        Main.NewText("它遁逃了......", Color.Purple);
            //    }
            //}
            if (!player.active && player.dead)
            {
                NPC.TargetClosest(true);
                if (!player.active && player.dead)
                {
                    NPC.active = false;
                    Main.NewText("它失去了兴趣......", Color.Purple);
                }
            }
        }
        private void ExplodeEffect(bool isBigJump = false)
        {
            SoundEngine.PlaySound(SoundID.Item62, NPC.Center);

            int ringCount = isBigJump ? 40 : 60;
            int smokeCount = isBigJump ? 15 : 25;
            float ringSpeed = isBigJump ? 10f : 6f;
            float smokeSpeed = isBigJump ? 3.5f : 2f;

            for (int i = 0; i < ringCount; i++)
            {
                float angle = MathHelper.TwoPi * i / ringCount;
                Vector2 dir = angle.ToRotationVector2();
                Vector2 spawnPos = NPC.Center + dir * 30f;

                Dust dust = Dust.NewDustDirect(spawnPos, 1, 1, DustID.Dirt, 0f, 0f, 100, default, 2f);
                dust.velocity = dir * ringSpeed;
                dust.noGravity = true;
            }

            for (int i = 0; i < smokeCount; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                dust.velocity *= smokeSpeed;
                dust.noGravity = true;
            }
        }


        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedPrimordialSlime, -1);
        }
        public override void DrawEffects(ref Color drawColor)
        {

        }
    }
    #region TestCode
#if false
    public struct Color_hsv
    {
        #region 基础字段
        public float H
        {
            get
            {
                if (H > 360)
                    H = 360;
                if (H < 0)
                    H = 0;
                return H;
            }
            set
            {
                if (value > 360)
                    value = 360;
                if (value < 0)
                    value = 0;
                H = value;
            }
        }
        public float S
        {
            get
            {
                if (S > 1)
                    S = 1;
                if (S < 0)
                    S = 0;
                return S;
            }
            set
            {
                if (value > 1)
                    value = 1;
                if (value < 0)
                    value = 0;
                S = value;
            }
        }
        public float V
        {
            get
            {
                if (V > 1)
                    V = 1;
                if (V < 0)
                    V = 0;
                return V;
            }
            set
            {
                if (value > 1)
                    value = 1;
                if (value < 0)
                    value = 0;
                V = value;
            }
        }
        #endregion

        #region 构造函数
        public Color_hsv()
        {
            H = 0;
            V = 0;
            S = 0;
        }
        public Color_hsv(float h)
        {
            H = h;
            V = 0;
            S = 0;
        }
        public Color_hsv(float h, float v)
        {
            H = h;
            V = v;
            S = 0;
        }
        public Color_hsv(float h, float v, float s)
        {
            H = h;
            V = v;
            S = s;
        }
        #endregion

        #region 转换方法
        /// <summary>
        /// 这是一个将Color_HSV转换为Color_RGB的方法，运用了HSV到RGB的转换公式
        /// </summary>
        /// <returns></returns>
        public Color ToColor_RGB()
        {
            float C = V * S;
            float X = C * (1 - Math.Abs((H / 60) % 2 - 1));
            float m = V - C;
            Color RGB_ = new Color();
            if (H == 360)
                H = 0;
            if (0 <= H && H < 60)
                RGB_ = new Color(C, X, 0);
            if (60 <= H && H < 120)
                RGB_ = new Color(X, C, 0);
            if (120 <= H && H < 180)
                RGB_ = new Color(0, C, X);
            if (180 <= H && H < 240)
                RGB_ = new Color(0, X, C);
            if (240 <= H && H < 300)
                RGB_ = new Color(X, 0, C);
            if (300 <= H && H < 360)
                RGB_ = new Color(C, 0, X);
            Color RGB = new Color((RGB_.R + m) * 255, (RGB_.G + m) * 255, (RGB_.B + m) * 255);
            return RGB;
        }
        #endregion
    }
#endif
#endregion
}