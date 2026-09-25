using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace TimeDomain.Content.Items.Weapons.Melee
{
    public class EditorProj2 : ModProjectile
    {
        public override string Texture => "TimeDomain/Content/Items/Weapons/Melee/Editor";
        public static Effect EditorEffect;
        public override void SetStaticDefaults()
        {
            EditorEffect = ModContent.Request<Effect>("TimeDomain/Content/Effects/Editor", AssetRequestMode.ImmediateLoad).Value;
        }
        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 120;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 180;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        public override void AI()
        {
            Projectile.velocity = Projectile.velocity.Length() < 48f ? Projectile.velocity * 1.03f : Projectile.velocity;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            //Projectile.position = Main.MouseWorld;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            //缩写这俩 我懒得在后面打长长的东西
            SpriteBatch sb = Main.spriteBatch;
            GraphicsDevice gd = Main.graphics.GraphicsDevice;

            //end 和 begin里和顶点的东西建议照抄 然后慢慢理解

            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            //开始顶点绘制

            List<Vertex> ve = new List<Vertex>();
            //GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(3556), Projectile);
            float c = 10;
            //if (Timer <= 20)
            //{
            //    goto End;
            //}
            for (int i = 0; i < c; i++)
            {
                Color b = Color.Lerp(Color.Red, Color.Blue, i / c);
                float Ro = (1 + (float)Math.Cos(Projectile.oldRot[i] - MathHelper.PiOver2) * player.direction);
                Ro = 1;
                if (Projectile.oldRot[i] == 0)
                {
                    break;
                }
                //存顶点																										从这一—————————————到这里都是乱弄的 你可以随便改改数据看看能发生什么
                ve.Add(new Vertex(Projectile.Center - Main.screenPosition + new Vector2(0, 20).RotatedBy(Projectile.oldRot[i] + MathHelper.PiOver4) * Ro,
                      new Vector3(i / c, 1, 1),
                      b));
                ve.Add(new Vertex(Projectile.Center - Main.screenPosition + new Vector2(0, -20).RotatedBy(Projectile.oldRot[i] + MathHelper.PiOver4) * Ro,
                      new Vector3(i / c, 0, 1),
                      b));
            }
            //EditorEffect.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly);
            //EditorEffect.CurrentTechnique.Passes[0].Apply();
            if (ve.Count >= 3)//因为顶点需要围成一个三角形才能画出来 所以需要判顶点数>=3 否则报错
            {
                //gd.Textures[0] = ModContent.Request<Texture2D>("TimeDomain/Assets/Textures/Misc/Extra_210").Value;//获取刀光的拖尾贴图
                gd.Textures[0] = ModContent.Request<Texture2D>("Terraria/Images/Extra_98").Value;
                gd.DrawUserPrimitives(PrimitiveType.TriangleStrip, ve.ToArray(), 0, ve.Count - 2);//画
            }
            //End:;
            //结束顶点绘制
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            //EditorEffect.Parameters["uTime"].SetValue(Main.GameUpdateCount);
            //EditorEffect.CurrentTechnique.Passes[0].Apply();
            sb.Draw(texture, Projectile.Center, null, Color.White, Projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), Projectile.scale, 0, 0);
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
    }
}
