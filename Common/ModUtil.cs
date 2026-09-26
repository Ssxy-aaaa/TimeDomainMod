using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Common.Utils;

namespace TimeDomain.Common
{
    public static class ModUtil
    {
        /// <summary>
        /// 按当前难度直接设置 NPC 伤害和最大生命
        /// </summary>
        public static void SetNPCDamageAndLifeMax(NPC npc,
            int classicDamage, int expertDamage, int masterDamage,
            int classicLifeMax, int expertLifeMax, int masterLifeMax)
        {
            npc.damage = Main.masterMode ? masterDamage : (Main.expertMode ? expertDamage : classicDamage);
            npc.lifeMax = Main.masterMode ? masterLifeMax : (Main.expertMode ? expertLifeMax : classicLifeMax);
        }

        /// <summary>
        /// 返回当前难度下的弹幕伤害值
        /// </summary>
        public static int SetProjectileDamage(int classicDamage, int expertDamage, int masterDamage)
        {
            if (Main.masterMode) return masterDamage;
            if (Main.expertMode) return expertDamage;
            return classicDamage;
        }

        /// <summary>
        /// 计算弹幕的伤害判定矩形，对部分原版火焰弹幕做额外扩张
        /// </summary>
        public static Rectangle GetDamageHitbox(Projectile projectile)
        {
            Rectangle result = new Rectangle(
                (int)projectile.position.X,
                (int)projectile.position.Y,
                projectile.width,
                projectile.height);

            if (projectile.type == ProjectileID.EyeFire)
                result.Inflate(30, 30);

            if (projectile.type == ProjectileID.Flames)
            {
                int num = (int)MathUtils.Remap(projectile.localAI[0], 0f, 72f, 10f, 40f, true);
                result.Inflate(num, num);
            }

            if (projectile.type == ProjectileID.FlamesTrap)
                result.Inflate(20, 20);

            if (projectile.aiStyle == ProjAIStyleID.GemStaffBolt)
                result.Inflate(4, 4);

            if (projectile.type == ProjectileID.HoundiusShootiusFireball)
                result.Inflate(10, 10);

            ProjectileLoader.ModifyDamageHitbox(projectile, ref result);
            return result;
        }

        internal static void SetNPCDamageAndLifeMax_InBossFight(NPC nPC, int v1, int v2, int v3, int v4, int v5, int v6)
        {
            throw new NotImplementedException();
        }
    }
}