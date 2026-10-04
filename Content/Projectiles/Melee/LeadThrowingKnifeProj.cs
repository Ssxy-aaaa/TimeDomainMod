using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Buffs;

namespace TimeDomain.Content.Projectiles.Melee
{
    public class LeadThrowingKnifeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 6;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            // 暗影焰刀
            Projectile.CloneDefaults(ProjectileID.ShadowFlameKnife);

            Projectile.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // buff
            target.AddBuff(ModContent.BuffType<TetanusBuff>(), 180);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = texture.Size() / 2f;

            for (int i = 1; i < Projectile.oldPos.Length; i++)
            {
                if (Projectile.oldPos[i] == Vector2.Zero)
                    continue;

                float progress = 1f - (float)i / Projectile.oldPos.Length;
                Color color = lightColor * progress * 0.4f;

                Vector2 drawPos = Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition;

                Main.EntitySpriteDraw(
                    texture,
                    drawPos,
                    null,
                    color,
                    Projectile.oldRot[i],
                    origin,
                    Projectile.scale,
                    SpriteEffects.None,
                    0
                );
            }

            return true;
        }
    }
}