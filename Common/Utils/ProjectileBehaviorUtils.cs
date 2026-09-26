using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace TimeDomain.Common.Utils
{
    public static class ProjectileBehaviorUtils
    {
        public static void HomingToNearestNPC(Projectile proj, float range)
        {
            NPC target = null;
            float bestDist = range;

            foreach (NPC npc in Main.npc)
            {
                if (!npc.CanBeChasedBy(proj)) continue;
                float dist = Vector2.Distance(proj.Center, npc.Center);
                if (dist < bestDist)
                {
                    bestDist = dist;
                    target = npc;
                }
            }

            if (target == null) return;

            Vector2 desired = (target.Center - proj.Center).SafeNormalize(proj.velocity.SafeNormalize(Vector2.UnitX));
            float speed = proj.velocity.Length();

            Vector2 newDir = Vector2.Lerp(
                proj.velocity.SafeNormalize(desired),
                desired,
                0.08f
            ).SafeNormalize(desired);

            proj.velocity = newDir * speed;
        }

        public static void Explode(Projectile proj, Vector2 position, float radius, int damage, float knockback, int owner)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;

            float radiusSq = radius * radius;

            foreach (NPC npc in Main.npc)
            {
                if (!npc.CanBeChasedBy(proj)) continue;
                if (Vector2.DistanceSquared(npc.Center, position) > radiusSq) continue;

                int direction = npc.Center.X > position.X ? 1 : -1;
                Player player = Main.player[owner];
                player.ApplyDamageToNPC(npc, damage, knockback, direction, false, proj.DamageType);
            }
        }
    }
}