using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace TimeDomain.Common
{
    public static class ModUtil
    {
        public static void SetNPCDamageAndLifeMax(NPC npc, int classicDamage, int expertDamage, int masterDamage, int classicLifeMax, int expertLifeMax, int masterLifeMax)
        {
            npc.damage = classicDamage;
            npc.lifeMax = classicLifeMax;
            if (Main.expertMode)
            {
                npc.damage = expertDamage / 2;
                npc.lifeMax = expertLifeMax / 2;

            }
            if (Main.masterMode)
            {
                npc.damage = masterDamage / 3;
                npc.lifeMax = masterLifeMax / 3;
            }
        }
        public static void SetNPCDamageAndLifeMax_InBossFight(NPC npc, int classicDamage, int expertDamage, int masterDamage, int classicLifeMax, int expertLifeMax, int masterLifeMax)
        {
            npc.damage = Main.masterMode ? masterDamage : (Main.expertMode ? expertDamage : classicDamage);
            npc.lifeMax = Main.masterMode ? masterLifeMax : (Main.expertMode ? expertLifeMax : classicLifeMax);
        }
        public static int SetProjectileDamage(int classicDamage, int expertDamage, int masterDamage)
        {
            Projectile projectile = new Projectile();
            projectile.damage = classicDamage / 2;
            if (Main.expertMode)
            {
                projectile.damage = expertDamage / 4;
            }
            if (Main.masterMode)
            {
                projectile.damage = masterDamage / 6;
            }
            return projectile.damage;
        }
        //public static void SetDamageDown(Projectile projectile, double multiple)
        //{
        //    for (int i = 0; i < Main.maxNPCs; i++)
        //    {
        //        //NPC npc = Main.npc[i];
        //        bool flag10 = projectile.Colliding(Damage_GetHitbox(projectile), Main.npc[i].getRect());
        //        if (flag10) projectile.damage = (int)((double)projectile.damage * multiple);
        //    }
        //}
        public static Rectangle Damage_GetHitbox(Projectile projectile)
        {
            Rectangle result = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
            if (projectile.type == ProjectileID.EyeFire)
            {
                result.Inflate(30, 30);
            }
            if (projectile.type == ProjectileID.Flames)
            {
                int num = (int)Utils.Remap(projectile.localAI[0], 0f, 72f, 10f, 40f, true);
                result.Inflate(num, num);
            }
            if (projectile.type == ProjectileID.FlamesTrap)
            {
                result.Inflate(20, 20);
            }
            if (projectile.aiStyle == ProjAIStyleID.GemStaffBolt)
            {
                result.Inflate(4, 4);
            }
            if (projectile.type == ProjectileID.HoundiusShootiusFireball)
            {
                result.Inflate(10, 10);
            }
            ProjectileLoader.ModifyDamageHitbox(projectile, ref result);
            return result;
        }
    }
}
