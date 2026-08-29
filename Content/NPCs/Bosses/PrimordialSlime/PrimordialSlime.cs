using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TimeDomain.Content.Items.Consumables;
using TimeDomain.Common;
using TimeDomain.Common.Systems;
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
            Shoot
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
        public int CurrentSkill = 0;

        public int JumpMaxX
        {
            get
            {
                if (Main.masterMode && Main.getGoodWorld)
                    return 20*16;//14
                if (Main.masterMode)
                    return 17*16;//10
                if (Main.expertMode)
                    return 12*16;//9
                return 7*16;//7
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
        public override bool PreDraw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.Vector2 screenPos, Color drawColor)
        {
            if (Main.GameUpdateCount % 50 == 0)
            {
                CurrentCount++;
                if (CurrentCount >= colors.Length)
                {
                    CurrentCount = 0;
                }
            }
            color = Color.Lerp(color, colors[CurrentCount], 0.02f);
            Texture2D tex = TextureAssets.Extra[ExtrasID.SharpTears].Value;//贴图
            Color c = Color.White;//颜色
            //spriteBatch.Draw(
            //    tex,
            //    NPC.Center + new Vector2(0, -NPC.height) - Main.screenPosition,
            //    null,
            //    color,
            //    0,
            //    new Vector2(tex.Width / 2, tex.Height / 2),
            //    1,
            //    SpriteEffects.None,
            //    0);
            return true;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(BossTime);
            writer.Write(BossTime2);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            BossTime = reader.ReadSingle();
            BossTime2 = reader.ReadSingle();
        }
        public override void AI()
        {
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
            int ElseJump = (NPC.lifeMax - NPC.life) / NPC.lifeMax * 5;
            switch (style)
            {
                case Style.Jump:
                    BossTime2++;
                    if (BossTime2 == 1)
                        CurrentSkill++;
                    if (BossTime2 == 30)
                    {
                        int dx = (int)(player.Center.X - NPC.Center.X);
                        int dy = (int)(player.Center.Y - NPC.Center.Y);
                        if (Math.Abs(dx) > JumpMaxX)
                            dx = dx < 0 ? -JumpMaxX : JumpMaxX;
                        if (Math.Abs(dy) > JumpMaxY)
                            dy = JumpMaxY;
                        dy = (dy + JumpMaxY) / 2;
                        NPC.velocity = new Vector2(dx, -JumpMaxY * 2 - ElseJump) / 30;
                    }

                    if (NPC.velocity.Y == 0)
                    {
                        NPC.velocity.X *= 0.9f;
                    }

                    if (BossTime2 > 40 && NPC.velocity.Y == 0)
                    {
                        BossTime2 = 0;
                        if (CurrentSkill == 1 || CurrentSkill == 4)
                            style = Style.Jump;
                        if (CurrentSkill == 2)
                            style = Style.BigJump;
                        if (CurrentSkill == 5)
                            style = Style.Shoot;
                    }
                    break;
                case Style.BigJump:
                    BossTime2++;
                    if (BossTime2 == 1)
                        CurrentSkill++;
                    if (BossTime2 == 30)
                    {
                        int dx = (int)(player.Center.X - NPC.Center.X);
                        int dy = (int)(player.Center.Y - NPC.Center.Y);
                        if (Math.Abs(dx) > BigJumpMaxX)
                            dx = dx < 0 ? -BigJumpMaxX : BigJumpMaxX;
                        if (Math.Abs(dy) > JumpMaxY)
                            dy = JumpMaxY;
                        dy = (dy + JumpMaxY) / 2;
                        NPC.velocity = new Vector2(dx, -JumpMaxY * 3 - ElseJump) / 30;
                    }

                    if (NPC.velocity.Y == 0)
                    {
                        NPC.velocity.X *= 0.9f;
                    }

                    if (BossTime2 > 40 && NPC.velocity.Y == 0)
                    {
                        BossTime2 = 0;
                        if (CurrentSkill == 3)
                            style = Style.Jump;
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
                    if (BossTime2 == 40)
                    {
                        NPC.velocity = new Vector2(0, -JumpMaxY * 2) / 30;
                    }
                    if (BossTime2 > 40 && NPC.velocity.Y == 0 && CanShoot)
                    {
                        Vector2 velocity = new Vector2(0, -1);
                        for (float r = -MathHelper.Pi; r <= MathHelper.Pi; r += MathHelper.Pi / 4)
                        {
                            float r2 = r + velocity.ToRotation(); // 加上发射向量所代表的角度
                            Vector2 v = new Vector2((float)Math.Cos(r2), (float)Math.Sin(r2)) * 10f;// 使用三角函数将其转换成向量
                            int proj = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, v, ModContent.ProjectileType<ElementalAura>(), ModUtil.SetProjectileDamage(110, 140, 162), 0, player.whoAmI);
                        }
                        CanShoot = false;
                    }

                    if (BossTime2 > 150 && !CanShoot)
                    {
                        BossTime2 = 0;
                        if (CurrentSkill == 6)
                            style = Style.Jump;
                        CurrentSkill = 0;
                        CanShoot = true;
                        NPC.TargetClosest(true);
                    }
                    break;
            }
            if (Phase2 && NPC.ai[0] == 0)
            {
                NPC.ai[0] = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SevenElements>(), 0, NPC.whoAmI);
                Main.npc[(int)NPC.ai[0]].active = true;
            }
            int PX = (int)(130 * NPC.scale);
            int bx = (int)(NPC.Bottom.X / 16) + 1;
            int by = (int)(NPC.Bottom.Y / 16) + 1;
            int ic = PX / 16 + 1;
            bool CanDown = false;
            for (int i = -ic / 2; i < ic / 2 + 1; i++)
            {
                Tile tile = Main.tile[bx + i, by];
                bool a = tile.HasTile == false || (Main.tileSolidTop[tile.TileType] && !Main.tileSolid[tile.TileType]);
                if (a)
                {
                    CanDown = true;
                }
                else
                {
                    CanDown = false;
                    break;
                }

            }

            NPC.noTileCollide = (NPC.Center.Y < player.Center.Y && (player.Center.Y - NPC.Center.Y) > 80) && CanDown;



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
        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedPrimordialSlime, -1);
        }
        public override void DrawEffects(ref Color drawColor)
        {

        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            float Rotation = NPC.velocity.X * 0.05f;
            Texture2D texture = ModContent.Request<Texture2D>("TimeDomain/Content/NPCs/Bosses/PrimordialSlime/SevenElements").Value;
            //Main.spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, texture.Width, 46)), new Color(255, 255, 255, 80), Rotation, new Vector2((texture.Width / 2), 23), 1, 0, 0);
        }
    }
    #region TestCode
    /*
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
    */
    #endregion
}
