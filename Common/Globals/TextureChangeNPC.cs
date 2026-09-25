using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Items.Weapons.Melee;

namespace TimeDomain.Common.Globals
{ 
    public class TextureChangeNPC : GlobalNPC
    {
        public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            base.SetBestiary(npc, database, bestiaryEntry);
        }
        public override bool InstancePerEntity => true;
        public int uTime = 0;
        public override void FindFrame(NPC npc, int frameHeight)
        {

            //if (npc.type == NPCID.BrainofCthulhu)
            //{
            //    if (npc.ai[0] > 0f)
            //    {
            //        frameHeight = 200;
            //    }
            //}
            //FC++;
            //if (FC % 6 == 0)
            //{
            //    npc.frame.Y += frameHeight;
            //}
            //  if (npc.ai[0] <= 0f)
            //  {
            //    if (npc.frame.Y > frameHeight * 7)
            //    {
            //        npc.frame.Y = 0;
            //    }
            //}
            //else if (npc.ai[0] > 0)
            //{
            //    if (npc.frame.Y < frameHeight * 4)
            //    {
            //        npc.frame.Y = frameHeight * 4;
            //    }
            //    if (npc.frame.Y > frameHeight * 7)
            //    {
            //        npc.frame.Y = frameHeight * 4;
            //    }
            //}
            //if (npc.type == NPCID.BrainofCthulhu)
            //{
            //    npc.frameCounter += 1;
            //    if (npc.frameCounter > 6)
            //    {
            //        npc.frameCounter = 0;
            //        npc.frame.Y = npc.frame.Y + frameHeight;
            //    }
            //    if (npc.ai[0] <= 0f)
            //    {
            //        if (npc.frame.Y > frameHeight * 3)
            //        {
            //            npc.frame.Y = 0;
            //        }
            //    }
            //    else
            //    {
            //        if (npc.frame.Y < frameHeight * 4)
            //        {
            //            npc.frame.Y = frameHeight * 4;
            //        }
            //        if (npc.frame.Y > frameHeight * 7)
            //        {
            //            npc.frame.Y = frameHeight * 4;
            //        }
            //    }
            //}
            //算了，我回头看原版代码吧
            GetNPCFrame(npc);
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //Texture2D tex = TextureAssets.Npc[npc.type].Value;
            //int FrameCount = Main.npcFrameCount[npc.type];
            //Vector2 halfSize = new Vector2(tex.Width, tex.Height / FrameCount) / 2f;
            //Vector2 pos = npc.Center - screenPos;
            //pos -= halfSize * npc.scale;
            //pos += halfSize * npc.scale + new Vector2(0f, Main.NPCAddHeight(npc) + npc.gfxOffY - 2);
            //SpriteEffects spriteEffects = SpriteEffects.None;
            //if (npc.spriteDirection == 1) spriteEffects = SpriteEffects.FlipHorizontally;
            

            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            //Editor.EditorEffect.CurrentTechnique.Passes[0].Apply();
            //Editor.EditorEffect.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly);
            //spriteBatch.Draw(tex, pos, npc.frame, drawColor, npc.rotation, halfSize, npc.scale, spriteEffects, 0);
            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            uTime++;
            if (!TimeDomain.AncientMode)
            {
                return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
            }
            //GetNPCFrame(npc);
            int type = npc.type;
            drawColor = npc.GetNPCColorTintedByBuffs(drawColor);
            #region 史莱姆王
            if (type == 50)
            {
                Vector2 zero = Vector2.Zero;
                float num33 = 0f;
                zero.Y -= npc.velocity.Y;
                zero.X -= npc.velocity.X * 2f;
                num33 += npc.velocity.X * 0.05f;
                if (npc.frame.Y == 120)
                {
                    zero.Y += 2f;
                }
                if (npc.frame.Y == 360)
                {
                    zero.Y -= 2f;
                }
                if (npc.frame.Y == 480)
                {
                    zero.Y -= 6f;
                }
                Color color = npc.GetAlpha(drawColor);
                Texture2D texture = ModContent.Request<Texture2D>("TimeDomain/Common/Textures/OverrideKSCS3").Value;
                Texture2D texture2 = ModContent.Request<Texture2D>("TimeDomain/Common/Textures/OverrideKSH").Value;
                //spriteBatch.Draw(texture, npc.Center - screenPos + zero, new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), drawColor, num33, new Vector2((texture.Width / 2), (texture.Height / 2)), 1f, 0, 0f);
                spriteBatch.Draw(TextureAssets.Ninja.Value, npc.Center - screenPos + zero, new Rectangle?(new Rectangle(0, 0, TextureAssets.Ninja.Width(), TextureAssets.Ninja.Height())), drawColor, num33, new Vector2((float)(TextureAssets.Ninja.Width() / 2), (float)(TextureAssets.Ninja.Height() / 2)), 1f, 0, 0f);
                spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, 70), npc.scale, SpriteEffects.None, 0);


                Texture2D value74 = TextureAssets.Extra[ExtrasID.KingSlimeCrown].Value;
                value74 = texture2;
                Vector2 center3 = npc.Center;
                float num267 = 0f;
                switch (npc.frame.Y / (TextureAssets.Npc[type].Height() / Main.npcFrameCount[type]))
                {
                    case 0:
                        num267 = 2f;
                        break;
                    case 1:
                        num267 = -6f;
                        break;
                    case 2:
                        num267 = 2f;
                        break;
                    case 3:
                        num267 = 10f;
                        break;
                    case 4:
                        num267 = 2f;
                        break;
                    case 5:
                        num267 = 0f;
                        break;
                }
                center3.Y += npc.gfxOffY - (70f - num267) * npc.scale;
                spriteBatch.Draw(value74, center3 - screenPos, null, drawColor, 0f, value74.Size() / 2f, 1f, SpriteEffects.None, 0f);

                return false;
            }
            #endregion

            #region 克苏鲁之脑
            if (npc.type == NPCID.BrainofCthulhu)
            {
                Color color = npc.GetAlpha(drawColor);
                if (npc.ai[2] == 1)
                {
                    color = new Color(200, 120, 120);
                }
                Texture2D texture;
                //不是
                //因为这是原版boss，有个我说不出的问题，
                //我代码写的问题
                //反正就是与原版不同导致的
                //if (npc.ai[0] <= 0)
                //{
                texture = ModContent.Request<Texture2D>("TimeDomain/Common/Textures/OverrideBOKAll").Value;
                //}
                //else
                //{
                //    texture = ModContent.Request<Texture2D>("TimeDomain/Common/Textures/OverrideBOKPhase2").Value;
                //}
                //spriteBatch.Draw(texture, npc.position - Main.screenPosition, new Rectangle(texture.Width / 2, texture.Height / Main.npcFrameCount[npc.type] / 2, texture.Width, texture.Height / Main.npcFrameCount[npc.type]), Color.White, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 2), npc.scale, SpriteEffects.None, 0);
                VanillaBossAIChangeNPC VanillaBossAIChangeNPC = new VanillaBossAIChangeNPC();


                spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 16), npc.scale, SpriteEffects.None, 0);

                //color = npc.GetAlpha(drawColor) * 0.5f;
                if (npc.ai[2] == 1)
                {
                    float RT = MathHelper.Pi / 2;
                    Player P = Main.player[npc.target];
                    if (Main.masterMode)
                    {
                        if (Main.masterMode && Main.getGoodWorld)
                        {
                            Vector2 A = npc.Center - P.Center;
                            Vector2 V1 = P.Center + (A.ToRotation() + RT).ToRotationVector2() * A.Length() - Main.screenPosition;
                            Vector2 V2 = P.Center + (A.ToRotation() + RT * 2).ToRotationVector2() * A.Length() - Main.screenPosition;
                            Vector2 V3 = P.Center + (A.ToRotation() + RT * 3).ToRotationVector2() * A.Length() - Main.screenPosition;
                            spriteBatch.Draw(texture, V1, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 16), npc.scale, SpriteEffects.None, 0);
                            spriteBatch.Draw(texture, V2, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 16), npc.scale, SpriteEffects.None, 0);
                            spriteBatch.Draw(texture, V3, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 16), npc.scale, SpriteEffects.None, 0);
                        }

                        Player p = Main.player[npc.target];
                        Vector2 a = npc.Center - p.Center;
                        Vector2 v1 = p.Center + new Vector2(-a.X, a.Y) - Main.screenPosition;
                        Vector2 v2 = p.Center + new Vector2(a.X, -a.Y) - Main.screenPosition;
                        Vector2 v3 = p.Center + new Vector2(-a.X, -a.Y) - Main.screenPosition;
                        spriteBatch.Draw(texture, v1, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 16), npc.scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(texture, v2, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 16), npc.scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(texture, v3, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 16), npc.scale, SpriteEffects.None, 0);
                    }
                }
                if (npc.ai[0] < 0)//我暂时把幻象去了
                {
                    if (npc.ai[2] == 1)
                    {
                        return false;
                    }
                    if (npc.ai[3] == 1)
                    {
                        float RT = MathHelper.Pi / 2;
                        Player P = Main.player[npc.target];

                        Vector2 A = npc.Center - P.Center;
                        Vector2 V1 = P.Center + (A.ToRotation() + RT).ToRotationVector2() * A.Length() - Main.screenPosition;
                        Vector2 V2 = P.Center + (A.ToRotation() + RT * 2).ToRotationVector2() * A.Length() - Main.screenPosition;
                        Vector2 V3 = P.Center + (A.ToRotation() + RT * 3).ToRotationVector2() * A.Length() - Main.screenPosition;
                        spriteBatch.Draw(texture, V1, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 16), npc.scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(texture, V2, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 16), npc.scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(texture, V3, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 16), npc.scale, SpriteEffects.None, 0);
                    }
                    else
                    {
                        Player p = Main.player[npc.target];
                        Vector2 a = npc.Center - p.Center;
                        Vector2 v1 = p.Center + new Vector2(-a.X, a.Y) - Main.screenPosition;
                        Vector2 v2 = p.Center + new Vector2(a.X, -a.Y) - Main.screenPosition;
                        Vector2 v3 = p.Center + new Vector2(-a.X, -a.Y) - Main.screenPosition;
                        spriteBatch.Draw(texture, v1, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 16), npc.scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(texture, v2, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 16), npc.scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(texture, v3, new Rectangle?(npc.frame), color, npc.rotation, new Vector2(texture.Width / 2, texture.Height / 16), npc.scale, SpriteEffects.None, 0);
                    }
                }
                return false;
            }
            #endregion

            #region 克苏鲁之眼
            if (npc.type == NPCID.EyeofCthulhu)
            {
                Texture2D texture1 = ModContent.Request<Texture2D>("TimeDomain/Common/Textures/OverrideEOK_Phase1").Value;
                Texture2D texture2 = ModContent.Request<Texture2D>("TimeDomain/Common/Textures/OverrideEOK_Phase2").Value;
                bool flag = npc.ai[0] > 1;
                float ROoff = -MathHelper.PiOver2;
                if (flag)
                    spriteBatch.Draw(texture2, npc.Center - screenPos, NPCRectangle, drawColor, npc.rotation + ROoff, new Vector2(texture2.Width / 2, texture2.Height / 8), 1f, SpriteEffects.None, 0f);
                else
                    spriteBatch.Draw(texture1, npc.Center - screenPos, NPCRectangle, drawColor, npc.rotation + ROoff, new Vector2(texture1.Width / 2, texture1.Height / 8), 1f, SpriteEffects.None, 0f);
                return false;
            }
            #endregion

            #region d
            if (npc.type == NPCID.BrainofCthulhu)
            {
                Texture2D npcTexture = TextureAssets.Npc[npc.type].Value;
                for (int i = 0; i < 6; i++)
                {
                    float ro = -MathHelper.Pi + (i / 6f) * MathHelper.TwoPi + (uTime / 100f);
                    Color newColor = drawColor;
                    newColor.A /= 3;
                    newColor *= (float)((Math.Sin(uTime / 100f)));
                    spriteBatch.Draw(npcTexture, npc.position + ro.ToRotationVector2() * 5 - Main.screenPosition, new Rectangle?(npc.frame), newColor, npc.rotation, Vector2.Zero, npc.scale, 0, 0f);
                }
            }
            #endregion

            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
        public double NPCFrameCount = 0;

        public Rectangle NPCRectangle = new Rectangle(0, 0, 0, 0);
        public int A;
        public void GetNPCFrame(NPC npc)
        {
            int a = A + 1;
            int Height = TextureAssets.Npc[npc.type].Height();
            int npcFrameCount = Main.npcFrameCount[npc.type];
            int num = Height / npcFrameCount;
            NPCRectangle.Width = TextureAssets.Npc[npc.type].Width();
            
            if (npc.type == NPCID.EyeofCthulhu)
            {
                num = 464 / 4;
                NPCFrameCount++;
                if (NPCFrameCount < 7.0)
                {
                    NPCRectangle.Y = 0;
                }
                else if (NPCFrameCount < 14.0)
                {
                    NPCRectangle.Y = num;
                }
                else if (NPCFrameCount < 21.0)
                {
                    NPCRectangle.Y = num * 2;
                }
                else if (NPCFrameCount < 28.0)
                {
                    NPCRectangle.Y = num * 3;
                }
                else
                {
                    NPCFrameCount = 0.0;
                    NPCRectangle.Y = 0;
                }
                NPCRectangle.Width = 160;
                //if (npc.ai[0] > 1f)
                //{
                //    NPCRectangle.Y = NPCRectangle.Y + num * 3;
                //}
            }
            NPCRectangle.Height = num;
        }
    }
}
