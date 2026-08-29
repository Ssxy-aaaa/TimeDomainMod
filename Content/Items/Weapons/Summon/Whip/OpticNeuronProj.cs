using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Common;
using TimeDomain.Content.Buffs;
using static System.Net.Mime.MediaTypeNames;

namespace TimeDomain.Content.Items.Weapons.Summon.Whip
{
    public class OpticNeuronProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // This makes the projectile use whip collision detection and allows flasks to be applied to it.
            //这使得射弹使用鞭状碰撞检测，并允许对其施加向量
            ProjectileID.Sets.IsAWhip[Type] = true;
        }

        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            Projectile.DefaultToWhip();
            Projectile.localNPCHitCooldown = -2;
            // use these to change from the vanilla defaults//使用这些方法改变鞭子基础属性
            Projectile.WhipSettings.Segments = 20;//这是鞭子的弹幕段数
            Projectile.WhipSettings.RangeMultiplier = 0.5f;//这是鞭子的范围乘数
        }

        private float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        private float ChargeTime
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
            //for (int i = 0; i < /*Main.rand.Next(1, 3)*/1; i++)
            //{
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center + new Vector2(Main.rand.Next(-40, 40), Main.rand.Next(-40, 40)), Vector2.Normalize(target.Center - Projectile.Center) * -5f, ModContent.ProjectileType<OpticNeuronProj2>(), Projectile.damage / 2, 5);
            //}
        }
        public void DrawLine(List<Vector2> list)
        {
            Texture2D texture = /*TextureAssets.FishingLine.Value*/ModContent.Request<Texture2D>("TimeDomain/Content/Items/Weapons/Summon/Whip/OpticNeuronProj").Value;
            Rectangle frame = texture.Frame();
            frame = new Rectangle(0,20,18,20);//20
            Vector2 origin = new Vector2(frame.Width / 2, 2);

            Vector2 pos = list[0];
            for (int i = 0; i < list.Count - 1; i++)
            {
                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2;
                Color color = Lighting.GetColor(element.ToTileCoordinates(), Color.Pink);
                Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

                pos += diff;
            }
        }

        public override void AI()
        {
            //VT2.Damage(Projectile);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            List<Vector2> list = new List<Vector2>();
            Projectile.FillWhipControlPoints(Projectile, list);

            //DrawLine(list);

            //Main.DrawWhip_WhipBland(Projectile, list);
            // The code below is for custom drawing.
            // If you don't want that, you can remove it all and instead call one of vanilla's DrawWhip methods, like above.
            // However, you must adhere to how they draw if you do.
            // 下面的代码用于自定义绘图。
            // 如果你不想要它，你可以将其全部删除，而是调用原版的 DrawWhip 方法之一，如上所示。
            // 但是，如果您这样做，您必须遵守他们的绘画方式。

            SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Main.instance.LoadProjectile(Type);
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 pos = list[0];

            for (int i = 0; i < list.Count - 1; i++)
            {
                // These two values are set to suit this projectile's sprite, but won't necessarily work for your own.
                // You can change them if they don't!
                //这两个值的设置是为了适合这个射弹的外观，但不一定适用于你自己的。
                //如果不这样做，您可以更改它们
                Rectangle frame = new Rectangle(0, 0, 18, 26);
                Vector2 origin = new Vector2(9, 10);
                float scale = 1;

                // These statements determine what part of the spritesheet to draw for the current segment.
                // They can also be changed to suit your sprite.
                // 这些语句确定要为当前段绘制弹幕图片表的哪一部分。
                // 它们也可以更改成适合您的弹幕图片。
                if (i == list.Count - 2)
                {
                    frame.Y = 82;
                    frame.Height = 20;

                    // For a more impactful look, this scales the tip of the whip up when fully extended, and down when curled up.
                    //为了获得更具冲击力的外观，这会在完全伸展时将鞭子的尖端向上缩放，在卷曲时向下缩放。
                    Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int _, out float _);
                    float t = Timer / timeToFlyOut;
                    scale = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, t, true) * Utils.GetLerpValue(0.9f, 0.7f, t, true));
                }
                //else if (i > 10)
                //{
                //    frame.Y = 58;
                //    frame.Height = 16;
                //}
                //else if (i > 5)
                //{
                //    frame.Y = 42;
                //    frame.Height = 16;
                //}
                //else if (i > 0)
                //{
                //    frame.Y = 26;
                //    frame.Height = 16;
                //}
                else
                {
                    frame.Y = 20;
                    frame.Height = 20;
                }
                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2; // This projectile's sprite faces down, so PiOver2 is used to correct rotation.//该射弹的图片面朝下，因此PiOver2用于校正旋转。
                Color color = Lighting.GetColor(element.ToTileCoordinates());

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, flip, 0);

                pos += diff;
            }
            return false;
        }
    }
}
