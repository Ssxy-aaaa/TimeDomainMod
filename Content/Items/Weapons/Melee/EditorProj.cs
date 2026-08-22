using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static tModPorter.ProgressUpdate;

namespace yourmod.Content.Items.Weapons.Melee
{
    public class EditorProj : ModProjectile
    {
        public override string Texture => "yourmod/Content/Items/Weapons/Melee/Editor";
        private Player Owner => Main.player[Projectile.owner];
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
            //ProjectileID.Sets.AllowsContactDamageFromJellyfish[Type] = true;
            //ProjectileID.Sets.TrailingMode[Type] = 2;
            //ProjectileID.Sets.TrailCacheLength[Type] = 10;
        }
        public override void SetDefaults()
        {
            Projectile.width = 114;
            Projectile.height = 114;
            Projectile.friendly = true;
            Projectile.timeLeft = 999;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 2;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 80;
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = 1000;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        private const float _SwingRange = 1.67f * (float)Math.PI; // 摆动攻击覆盖的角度（300度）
        private const float _FirstHalfSwing = 0.45f; // 在到达目标角度之前摆动的比例（相对于摆动范围）
        private const float _SpinRange = 3.5f * (float)Math.PI; // 旋转攻击覆盖的角度（630度）
        private const float _WindUp = 0.15f; // 玩家蓄力攻击时手的后摆距离（相对于摆动范围）
        private const float _UnWind = 0.4f; // 剑开始消失的时机
        private const float _SpinTime = 1; // 旋转攻击比摆动攻击持续时间更长

        private enum AttackType // Which attack is being performed
        {
            // 挥击是可以略微瞄准的普通剑击
            // 挥击会经历完整的动画循环
            Swing,
            // 旋转是可以完整转一圈的摆动
            // 它们速度较慢，但会造成更大的击退
            Spin,
        }

        private enum AttackStage //攻击的执行阶段，详见AI中功能
        {
            Prepare,
            Execute,
            Unwind
        }
        // 这些属性封装了常用的 ai 和 localAI 数组，使代码更清晰、更易理解。
        private AttackType CurrentAttack
        {
            get => (AttackType)Projectile.ai[0];
            set => Projectile.ai[0] = (float)value;
        }

        private AttackStage CurrentStage
        {
            get => (AttackStage)Projectile.localAI[0];
            set
            {
                Projectile.localAI[0] = (float)value;
                Timer = 0; // 当弹丸切换状态时重置计时器
            }
        }

        // 在运行时需要跟踪的变量
        private ref float InitialAngle => ref Projectile.ai[1]; // 瞄准角度（有约束条件）
        private ref float Timer => ref Projectile.ai[2]; // 用于跟踪每个阶段进展的计时器
        private ref float Progress => ref Projectile.localAI[1]; // 剑相对于初始角度的位置
        private ref float Size => ref Projectile.localAI[2]; // 剑的尺寸

        // 我们为每个阶段定义了时间函数，同时考虑了近战攻击速度
        // 注意，你可以根据你的投射物需求更改此设置
        float a = 27;//54f;
        private float prepTime => a / Owner.GetTotalAttackSpeed(Projectile.DamageType);
        private float execTime => a / Owner.GetTotalAttackSpeed(Projectile.DamageType);
        private float hideTime => a / Owner.GetTotalAttackSpeed(Projectile.DamageType);

        public int UseItemCount = 0;
        public static int Style = 1;
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.spriteDirection = Main.MouseWorld.X > Owner.MountedCenter.X ? 1 : -1;
            float targetAngle = (Main.MouseWorld - Owner.MountedCenter).ToRotation();

            if (CurrentAttack == AttackType.Spin)
            {
                InitialAngle = (float)((-Math.PI / 2 + Math.PI * 1 / 3) * Projectile.spriteDirection); // For the spin, starting angle is designated based on direction of hit
            }
            else
            {
                if (Projectile.spriteDirection == 1)
                {
                    // However, we limit the rangle of possible directions so it does not look too ridiculous
                    targetAngle = MathHelper.Clamp(targetAngle, (float)-Math.PI * 1 / 3, (float)Math.PI * 1 / 6);
                }
                else
                {
                    if (targetAngle < 0)
                    {
                        targetAngle += 2 * (float)Math.PI; // This makes the range continuous for easier operations
                    }

                    targetAngle = MathHelper.Clamp(targetAngle, (float)Math.PI * 5 / 6, (float)Math.PI * 4 / 3);
                }

                InitialAngle = targetAngle - _FirstHalfSwing * _SwingRange * Projectile.spriteDirection; // Otherwise, we calculate the angle
            }


            //UseItemCount++;
            //UseItemCount = UseItemCount > 1 ? 0 : UseItemCount;
            Style = -Style;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((sbyte)Projectile.spriteDirection);
            writer.Write(Style);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.spriteDirection = reader.ReadSByte();
            Style = reader.ReadInt32();
        }

        public override void AI()
        {
            // Extend use animation until projectile is killed
            Owner.itemAnimation = 2;
            Owner.itemTime = 2;

            // Kill the projectile if the player dies or gets crowd controlled
            if (!Owner.active || Owner.dead || Owner.noItems || Owner.CCed)
            {
                Projectile.Kill();
                return;
            }

            // AI depends on stage and attack
            // Note that these stages are to facilitate the scaling effect at the beginning and end
            // If this is not desirable for you, feel free to simplify
            switch (CurrentStage)
            {
                case AttackStage.Prepare:
                    PrepareStrike();
                    break;
                case AttackStage.Execute:
                    ExecuteStrike();
                    break;
                default:
                    UnwindStrike();
                    break;
            }
            //Main.NewText(CurrentAttack);
            SetSwordPosition();
            DustH();
            Timer++;
        }
        public void DustH()
        {
            if (Timer % 3 == 0)
            {
                int d1 = Dust.NewDust(Projectile.Center, 3, 3, DustID.Shadowflame, 0, 0);
                int d2 = Dust.NewDust(Projectile.Center, 3, 3, DustID.ShadowbeamStaff, 0, 0);
                Main.dust[d1].noGravity = true;
                Main.dust[d2].noGravity = true;
            }
        }
        public Player player => Main.player[Projectile.owner];
        int d = 12;
        public override bool PreDraw(ref Color lightColor)
        {
            // Calculate origin of sword (hilt) based on orientation and offset sword rotation (as sword is angled in its sprite)
            Vector2 origin;
            float rotationOffset;
            SpriteEffects effects;

            if (Projectile.spriteDirection > 0)
            {
                origin = new Vector2(0, Projectile.height);
                rotationOffset = MathHelper.ToRadians(45f);
                effects = SpriteEffects.None;
            }
            else
            {
                origin = new Vector2(Projectile.width, Projectile.height);
                rotationOffset = MathHelper.ToRadians(135f);
                effects = SpriteEffects.FlipHorizontally;
            }

            Texture2D texture = TextureAssets.Projectile[Type].Value;


            {
                #region L
                //Vector2 tipPos = Owner.MountedCenter + new Vector2(-1.5f, -1.5f).RotatedBy(Projectile.rotation + (MathHelper.Pi / 2 * 3) * Projectile.spriteDirection) * 80;
                //Texture2D Tex = ModContent.Request<Texture2D>("yourmod/Assets/Textures/Misc/Effect_7").Value;
                //Texture2D Tex2 = ModContent.Request<Texture2D>("yourmod/Assets/Textures/Misc/slash1").Value;
                //Texture2D Tex3 = ModContent.Request<Texture2D>("yourmod/Assets/Textures/Misc/slash2").Value;
                ////Texture2D Tex4 = ModContent.Request<Texture2D>("yourmod/Assets/Textures/Misc/Particle_Slash_05").Value;
                //Color c = /*Color.LimeGreen*/new Color(90, 90, 140);
                //Color c2 = Color.Lerp(/*Color.Blue*/new Color(87, 86, 136), /*Color.Cyan*/new Color(214, 71, 214), 0.5f) * 0.8f;
                //Color c3 = /*Color.CadetBlue*/new Color(220, 80, 220);
                //c.A = 0;

                ////if (/*CurrentAttackType == SpearAttackType.Spin*/)
                //{
                //    float Progress = this.Progress * 0.5f;
                //    //Main.spriteBatch.Draw(Tex, tipPos - Main.screenPosition, null, c * (Progress - 0.5f) * 5, 0, Tex.Size() / 2, new Vector2(2, 2), 0, 0);
                //    if (Owner.direction == 1)
                //    {
                //        Main.spriteBatch.Draw(Tex2, Owner.Center - Main.screenPosition, null, c * Progress, (float)(Projectile.rotation/* - Math.PI / 2 - Math.PI / 4*/), Tex2.Size() / 2, 2f, 0, 0);
                //        Main.spriteBatch.Draw(Tex2, Owner.Center - Main.screenPosition, null, c2 * Progress, (float)(Projectile.rotation/* - Math.PI / 2 - Math.PI / 4*/ - 0.4f), Tex2.Size() / 2, 2f, 0, 0);
                //        Main.spriteBatch.Draw(Tex3, Owner.Center - Main.screenPosition, null, c3 * Progress/* * 3*/, (float)(Projectile.rotation/* - Math.PI / 2 - Math.PI / 4*/), Tex3.Size() / 2, 2f, 0, 0);
                //        Main.spriteBatch.Draw(Tex3, Owner.Center - Main.screenPosition, null, c3 * Progress/* * 2*/, (float)(Projectile.rotation/* - Math.PI / 2 - Math.PI / 4*/), Tex3.Size() / 2, 1.5f, 0, 0);
                //        Main.spriteBatch.Draw(Tex3, Owner.Center - Main.screenPosition, null, c3 * Progress/* * 2*/, (float)(Projectile.rotation/* - Math.PI / 2 - Math.PI / 4*/), Tex3.Size() / 2, 1.2f, 0, 0);
                //        //Main.spriteBatch.Draw(Tex4, Owner.Center - Main.screenPosition, null, c3 * Progress * 5, (float)(Projectile.rotation - Math.PI - 1.8f - Math.PI / 2), Tex4.Size() / 2, 1.35f, 0, 0);

                //    }
                //    else
                //    {
                //        Main.spriteBatch.Draw(Tex2, Owner.Center - Main.screenPosition, null, c * Progress, (float)(Projectile.rotation/* - Math.PI / 2 - Math.PI / 4*/ + Math.PI), Tex2.Size() / 2, 2f, SpriteEffects.FlipHorizontally, 0);
                //        Main.spriteBatch.Draw(Tex2, Owner.Center - Main.screenPosition, null, c2 * Progress, (float)(Projectile.rotation/* - Math.PI / 2 - Math.PI / 4*/ + Math.PI + 0.4f), Tex2.Size() / 2, 2f, SpriteEffects.FlipHorizontally, 0);
                //        Main.spriteBatch.Draw(Tex3, Owner.Center - Main.screenPosition, null, c3 * Progress/* * 3*/, (float)(Projectile.rotation/* - Math.PI / 2 - Math.PI / 4*/ + Math.PI), Tex3.Size() / 2, 2f, SpriteEffects.FlipHorizontally, 0);
                //        Main.spriteBatch.Draw(Tex3, Owner.Center - Main.screenPosition, null, c3 * Progress/* * 2*/, (float)(Projectile.rotation/* - Math.PI / 2 - Math.PI / 4*/ + Math.PI), Tex3.Size() / 2, 1.5f, SpriteEffects.FlipHorizontally, 0);
                //        Main.spriteBatch.Draw(Tex3, Owner.Center - Main.screenPosition, null, c3 * Progress/* * 2*/, (float)(Projectile.rotation/* - Math.PI / 2 - Math.PI / 4*/ + Math.PI), Tex3.Size() / 2, 1.2f, SpriteEffects.FlipHorizontally, 0);
                //        //Main.spriteBatch.Draw(Tex4, Owner.Center - Main.screenPosition, null, c3 * Progress * 5, (float)(Projectile.rotation/* + Math.PI + 1.8f*/), Tex4.Size() / 2, 1.35f, SpriteEffects.FlipHorizontally, 0);
                //    }

                //}
                #endregion
            }




            //Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, default, lightColor * Projectile.Opacity, Projectile.rotation + rotationOffset, origin, Projectile.scale, effects, 0);
            // Since we are doing a custom draw, prevent it from normally drawing













            {
                ////缩写这俩 我懒得在后面打长长的东西
                //SpriteBatch sb = Main.spriteBatch;
                //GraphicsDevice gd = Main.graphics.GraphicsDevice;

                ////end 和 begin里和顶点的东西建议照抄 然后慢慢理解

                //sb.End();
                //sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                ////开始顶点绘制

                //List<Vertex> ve = new List<Vertex>();

                //for (int i = 0; i < 9; i++)
                //{
                //    Color b = Color.Lerp(Color.Green, Color.Blue, i / 9);

                //    //存顶点																										从这一—————————————到这里都是乱弄的 你可以随便改改数据看看能发生什么
                //    ve.Add(
                //        new Vertex(
                //            Projectile.Center - Main.screenPosition + new Vector2(80, -80).RotatedBy(Projectile.oldRot[i]) * (1 + (float)Math.Cos(Projectile.oldRot[i] - MathHelper.PiOver2) * player.direction),
                //            new Vector3(i / 9, 1, 1),
                //            b
                //        )
                //    );
                //    ve.Add(
                //        new Vertex(
                //            Projectile.Center - Main.screenPosition + new Vector2(20, -20).RotatedBy(Projectile.oldRot[i]) * (1 + (float)Math.Cos(Projectile.oldRot[i] - MathHelper.PiOver2) * player.direction),
                //            new Vector3(i / 9, 0, 1),
                //            b
                //        )
                //    );
                //}

                //if (ve.Count >= 3)//因为顶点需要围成一个三角形才能画出来 所以需要判顶点数>=3 否则报错
                //{
                //    gd.Textures[0] = ModContent.Request<Texture2D>("yourmod/Assets/Textures/Misc/Extra_209").Value;//获取刀光的拖尾贴图
                //    gd.DrawUserPrimitives(PrimitiveType.TriangleStrip, ve.ToArray(), 0, ve.Count - 2);//画
                //}

                ////结束顶点绘制    
                //sb.End();
                //sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);


                //////画出这把 剑 的样子
                ////Main.spriteBatch.Draw(TextureAssets.Projectile[Type].Value,
                ////             Projectile.Center - Main.screenPosition,
                ////             null,
                ////             lightColor,
                ////             Projectile.rotation - MathHelper.PiOver4,
                ////             new Vector2(0, 40),
                ////             1.5f,
                ////             SpriteEffects.None,
                ////             0);
            }
            {
                //缩写这俩 我懒得在后面打长长的东西
                SpriteBatch sb = Main.spriteBatch;
                GraphicsDevice gd = Main.graphics.GraphicsDevice;

                //end 和 begin里和顶点的东西建议照抄 然后慢慢理解

                sb.End();
                sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                //开始顶点绘制

                List<Vertex> ve = new List<Vertex>();

                for (int i = 0; i < 9; i++)
                {
                    Color b = Color.Lerp(Color.Red, Color.Blue, i / 9);
                    float Ro = (1 + (float)Math.Cos(Projectile.oldRot[i] - MathHelper.PiOver2) * player.direction);
                    Ro = 1;
                    //存顶点																										从这一—————————————到这里都是乱弄的 你可以随便改改数据看看能发生什么
                    ve.Add(new Vertex(Projectile.Center - Main.screenPosition + new Vector2(0, -160).RotatedBy(Projectile.oldRot[i] + MathHelper.PiOver2) * Ro,
                          new Vector3(i / 9, 1, 1),
                          b));
                    ve.Add(new Vertex(Projectile.Center - Main.screenPosition + new Vector2(0, -20).RotatedBy(Projectile.oldRot[i] + MathHelper.PiOver2) * Ro,
                          new Vector3(i / 9, 0, 1),
                          b));
                }

                if (ve.Count >= 3)//因为顶点需要围成一个三角形才能画出来 所以需要判顶点数>=3 否则报错
                {
                    gd.Textures[0] = ModContent.Request<Texture2D>("yourmod/Assets/Textures/Misc/Extra_210").Value;//获取刀光的拖尾贴图
                    gd.DrawUserPrimitives(PrimitiveType.TriangleStrip, ve.ToArray(), 0, ve.Count - 2);//画
                }
                
                //结束顶点绘制
                sb.End();
                sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, default, lightColor * Projectile.Opacity, Projectile.rotation + rotationOffset, origin, Projectile.scale, effects, 0);















            return false;
        }

        // Find the start and end of the sword and use a line collider to check for collision with enemies
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 start = Owner.MountedCenter;
            Vector2 end = start + Projectile.rotation.ToRotationVector2() * ((Projectile.Size.Length()) * Projectile.scale);
            float collisionPoint = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 15f * Projectile.scale, ref collisionPoint);
        }

        // Do a similar collision check for tiles
        public override void CutTiles()
        {
            Vector2 start = Owner.MountedCenter;
            Vector2 end = start + Projectile.rotation.ToRotationVector2() * (Projectile.Size.Length() * Projectile.scale);
            Utils.PlotTileLine(start, end, 15 * Projectile.scale, DelegateMethods.CutTiles);
        }

        // We make it so that the projectile can only do damage in its release and unwind phases
        public override bool? CanDamage()
        {
            if (CurrentStage == AttackStage.Prepare)
                return false;
            return base.CanDamage();
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            // Make knockback go away from player
            modifiers.HitDirectionOverride = target.position.X > Owner.MountedCenter.X ? 1 : -1;

            // If the NPC is hit by the spin attack, increase knockback slightly
            if (CurrentAttack == AttackType.Spin)
                modifiers.Knockback += 1;
        }

        // Function to easily set projectile and arm position
        public void SetSwordPosition()
        {
            Projectile.rotation = InitialAngle + Projectile.spriteDirection * Progress; // Set projectile rotation
            Vector2 armPosition = Owner.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, Projectile.rotation - (float)Math.PI / 2); // get position of hand

            if (Style == -1)
            {
                Projectile.rotation = 0f - Projectile.rotation;
                armPosition.Y = Owner.Bottom.Y + (Owner.position.Y - armPosition.Y);
            }
            // Set composite arm allows you to set the rotation of the arm and stretch of the front and back arms independently
            Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (Projectile.rotation - MathHelper.ToRadians(90f))* 1); // set arm position (90 degree offset since arm starts lowered)


            // Adjust the position for reversed gravity.
            if (Owner.gravDir == -1f)
            {
                Projectile.rotation = 0f - Projectile.rotation;
                armPosition.Y = Owner.Bottom.Y + (Owner.position.Y - armPosition.Y);
            }


            armPosition.Y += Owner.gfxOffY;
            Projectile.Center = armPosition; // Set projectile to arm position
            //Projectile.scale = Size * 1.2f * Owner.GetAdjustedItemScale(Owner.HeldItem); // Slightly scale up the projectile and also take into account melee size modifiers

            Owner.heldProj = Projectile.whoAmI; // set held projectile to this projectile
        }

        // Function facilitating the taking out of the sword
        private void PrepareStrike()
        {
            Progress = _WindUp * _SwingRange * (1f - Timer / prepTime); // Calculates rotation from initial angle
            Size = MathHelper.SmoothStep(0, 1, Timer / prepTime); // Make sword slowly increase in size as we prepare to strike until it reaches max

            if (Timer >= prepTime)
            {
                SoundEngine.PlaySound(SoundID.Item1); // Play sword sound here since playing it on spawn is too early
                CurrentStage = AttackStage.Execute; // If attack is over prep time, we go to next stage
            }
        }

        // Function facilitating the first half of the swing
        private void ExecuteStrike()
        {
            if (CurrentAttack == AttackType.Swing)
            {
                Progress = MathHelper.SmoothStep(0, _SwingRange, (1f - _UnWind) * Timer / execTime);

                if (Timer >= execTime)
                {
                    CurrentStage = AttackStage.Unwind;
                }
            }
            else
            {
                Progress = MathHelper.SmoothStep(0, _SpinRange, (1f - _UnWind / 2) * Timer / (execTime * _SpinTime));

                if (Timer == (int)(execTime * _SpinTime * 3 / 4))
                {
                    SoundEngine.PlaySound(SoundID.Item1); // Play sword sound again
                    Projectile.ResetLocalNPCHitImmunity(); // Reset the local npc hit immunity for second half of spin
                }

                if (Timer >= execTime * _SpinTime)
                {
                    CurrentStage = AttackStage.Unwind;
                }
            }
        }

        // Function facilitating the latter half of the swing where the sword disappears
        private void UnwindStrike()
        {
            if (CurrentAttack == AttackType.Swing)
            {
                Progress = MathHelper.SmoothStep(0, _SwingRange, (1f - _UnWind) + _UnWind * Timer / hideTime);
                Size = 1f - MathHelper.SmoothStep(0, 1, Timer / hideTime); // Make sword slowly decrease in size as we end the swing to make a smooth hiding animation

                if (Timer >= hideTime)
                {
                    Projectile.Kill();
                }
            }
            else
            {
                Progress = MathHelper.SmoothStep(0, _SpinRange, (1f - _UnWind / 2) + _UnWind / 2 * Timer / (hideTime * _SpinTime / 2));
                Size = 1f - MathHelper.SmoothStep(0, 1, Timer / (hideTime * _SpinTime / 2));

                if (Timer >= hideTime * _SpinTime / 2)
                {
                    Projectile.Kill();
                }
            }
        }
    }

    public struct Vertex : IVertexType
    {
        private static VertexDeclaration _vertexDeclaration = new VertexDeclaration(new VertexElement[3]
        {
            new VertexElement(0,VertexElementFormat.Vector2,VertexElementUsage.Position,0),
            new VertexElement(8,VertexElementFormat.Color,VertexElementUsage.Color,0),
            new VertexElement(12,VertexElementFormat.Vector3,VertexElementUsage.TextureCoordinate,0)
        });
        public Vector2 Position;
        public Color Color;
        public Vector3 TexCoord;
        public Vertex(Vector2 position, Vector3 texCoord, Color color)
        {
            Position = position;
            TexCoord = texCoord;
            Color = color;
        }
        public VertexDeclaration VertexDeclaration
        {
            get => _vertexDeclaration;
        }
    }
}
