using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace TimeDomain.Common.Globals
{
    public class TextureChangeProjectile : GlobalProjectile
    {
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (!TimeDomain.AncientMode)
            {
                return base.PreDraw(projectile, ref lightColor);
            }
            if (NPC.crimsonBoss !=-1)
            {
                if (Main.npc[NPC.crimsonBoss].ai[0] < 0)
                {
                    //for (int i = 0; i < Main.projectile.Length; i++)
                    //{
                    if (Main.getGoodWorld)
                    {
                        Player p = Main.player[Main.myPlayer];
                        Vector2 a = projectile.position - p.Center;
                        Vector2 v1 = p.Center - Main.screenPosition + new Vector2(-a.X, a.Y);
                        Vector2 v2 = p.Center - Main.screenPosition + new Vector2(a.X, -a.Y);
                        Vector2 v3 = p.Center - Main.screenPosition + new Vector2(-a.X, -a.Y);
                        //Texture2D projTex = TextureAssets.Projectile[Main.projectile[i].type].Value;
                        Texture2D projTex = TextureAssets.Projectile[projectile.type].Value;
                        float A = projectile.rotation;
                        float R1 = MathHelper.TwoPi - A;
                        float R2 = MathHelper.Pi + R1;
                        float R3 = MathHelper.Pi + A;
                        ///bool TeX = false;
                        //projectile.frameCounter
                        //Main.spriteBatch.Draw(projTex, projectile.Center, null/*new Rectangle?(projectile.frame)*/, lightColor, projectile.rotation - MathHelper.Pi / 2, new Vector2(projectile.width / 2, projectile.height / 2), projectile.scale, SpriteEffects.None, 0);
                        Vector2 DrawOrigin = new Vector2(projectile.width, projectile.height);
                        Main.spriteBatch.Draw(projTex, v1, new Rectangle?(new Rectangle(0, projectile.frameCounter * (projTex.Height / Main.projFrames[projectile.type]), projTex.Width, projTex.Height / Main.projFrames[projectile.type])), lightColor, R1/* + MathHelper.Pi / 2*/, DrawOrigin, projectile.scale, SpriteEffects.None, 0);
                        Main.spriteBatch.Draw(projTex, v2, new Rectangle?(new Rectangle(0, projectile.frameCounter * (projTex.Height / Main.projFrames[projectile.type]), projTex.Width, projTex.Height / Main.projFrames[projectile.type])), lightColor, R2/* + MathHelper.Pi / 2*/, DrawOrigin, projectile.scale, SpriteEffects.None, 0);
                        Main.spriteBatch.Draw(projTex, v3, new Rectangle?(new Rectangle(0, projectile.frameCounter * (projTex.Height / Main.projFrames[projectile.type]), projTex.Width, projTex.Height / Main.projFrames[projectile.type])), lightColor, R3/* + MathHelper.Pi / 2*/, DrawOrigin, projectile.scale, SpriteEffects.None, 0);


                        //for (int i = 0; i < Main.dust.Length; i++)
                        //{
                        //    int num2 = Main.dust[i].type / 100;
                        //    Dust dust2 = Main.dust[i];
                        //    dust2.frame.X = dust2.frame.X - 1000 * num2;
                        //    Dust dust3 = Main.dust[i];
                        //    dust3.frame.Y = dust3.frame.Y + 30 * num2;


                        //    Texture2D dustTex = TextureAssets.Dust.Value;
                        //    Vector2 Da = Main.dust[i].position - p.position;
                        //    Vector2 Dv1 = p.position - Main.screenPosition + new Vector2(-Da.X, Da.Y);
                        //    //int dust1 = Dust.NewDust(Dv1, projectile.width,projectile.height,,);
                        //    Main.spriteBatch.Draw(dustTex, Dv1, dust2.frame, lightColor, 0, new Vector2(projectile.width / 2, projectile.height / 2), projectile.scale, SpriteEffects.None, 0);
                        //    Vector2 Dv2 = p.position - Main.screenPosition + new Vector2(Da.X, -Da.Y);
                        //    Main.spriteBatch.Draw(dustTex, Dv2, dust2.frame, lightColor, 0, new Vector2(projectile.width / 2, projectile.height / 2), projectile.scale, SpriteEffects.None, 0);
                        //    Vector2 Dv3 = p.position - Main.screenPosition + new Vector2(-Da.X, -Da.Y);
                        //    Main.spriteBatch.Draw(dustTex, Dv3, dust2.frame, lightColor, 0, new Vector2(projectile.width / 2, projectile.height / 2), projectile.scale, SpriteEffects.None, 0);
                        //}
                    }
                    //}
                }
            }
            return base.PreDraw(projectile, ref lightColor);
        }
        public void InnerBOKProjDraw(Projectile npc, int type, SpriteBatch spriteBatch, Color color)
        {
            Vector2 halfSize;
            halfSize = new Vector2((TextureAssets.Projectile[type].Width() / 2), (TextureAssets.Projectile[type].Height() / Main.npcFrameCount[type] / 2));
            color = npc.GetAlpha(color);
            for (int i = 0; i < 4; i++)
            {
                Vector2 DrawPosition = npc.position;
                float AX = Math.Abs(npc.Center.X - Main.player[Main.myPlayer].Center.X);
                float AY = Math.Abs(npc.Center.Y - Main.player[Main.myPlayer].Center.Y);
                if (i == 0 || i == 2)
                {
                    DrawPosition.X = Main.player[Main.myPlayer].Center.X + AX;
                }
                else
                {
                    DrawPosition.X = Main.player[Main.myPlayer].Center.X - AX;
                }
                DrawPosition.X -= (float)(npc.width / 2);
                if (i == 0 || i == 1)
                {
                    DrawPosition.Y = Main.player[Main.myPlayer].Center.Y + AY;
                }
                else
                {
                    DrawPosition.Y = Main.player[Main.myPlayer].Center.Y - AY;
                }
                DrawPosition.Y -= (float)(npc.height / 2);
                Texture2D projTex = TextureAssets.Projectile[npc.type].Value;
                Rectangle frame = new Rectangle(0, npc.frameCounter * (projTex.Height / Main.projFrames[npc.type]), projTex.Width, projTex.Height / Main.projFrames[npc.type]);
                spriteBatch.Draw(TextureAssets.Projectile[type].Value, new Vector2(DrawPosition.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)TextureAssets.Projectile[type].Width() * npc.scale / 2f + halfSize.X * npc.scale, DrawPosition.Y - Main.screenPosition.Y + (float)npc.height - (float)TextureAssets.Projectile[type].Height() * npc.scale / (float)Main.npcFrameCount[type] + 4f + halfSize.Y * npc.scale + 50f * npc.scale + 0 + npc.gfxOffY), new Rectangle?(), color, npc.rotation, halfSize, npc.scale, SpriteEffects.None, 0f);
            }
        }
        public void InnerBOKDraw(NPC npc,int type, SpriteBatch spriteBatch,Color color  )
        {
            Vector2 halfSize;
            halfSize = new Vector2((TextureAssets.Npc[type].Width() / 2), (TextureAssets.Npc[type].Height() / Main.npcFrameCount[type] / 2));
            color = npc.GetAlpha(color);
            for (int i = 0; i < 4; i++)
            {
                Vector2 DrawPosition = npc.position;
                float AX = Math.Abs(npc.Center.X - Main.player[Main.myPlayer].Center.X);
                float AY = Math.Abs(npc.Center.Y - Main.player[Main.myPlayer].Center.Y);
                if (i == 0 || i == 2)
                {
                    DrawPosition.X = Main.player[Main.myPlayer].Center.X + AX;
                }
                else
                {
                    DrawPosition.X = Main.player[Main.myPlayer].Center.X - AX;
                }
                DrawPosition.X -= (float)(npc.width / 2);
                if (i == 0 || i == 1)
                {
                    DrawPosition.Y = Main.player[Main.myPlayer].Center.Y + AY;
                }
                else
                {
                    DrawPosition.Y = Main.player[Main.myPlayer].Center.Y - AY;
                }
                DrawPosition.Y -= (float)(npc.height / 2);
                spriteBatch.Draw(TextureAssets.Npc[type].Value, new Vector2(DrawPosition.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)TextureAssets.Npc[type].Width() * npc.scale / 2f + halfSize.X * npc.scale, DrawPosition.Y - Main.screenPosition.Y + (float)npc.height - (float)TextureAssets.Npc[type].Height() * npc.scale / (float)Main.npcFrameCount[type] + 4f + halfSize.Y * npc.scale + 50f * npc.scale + 0 + npc.gfxOffY), new Rectangle?(npc.frame), color, npc.rotation, halfSize, npc.scale, SpriteEffects.None, 0f);
            }
        }
    }
}
