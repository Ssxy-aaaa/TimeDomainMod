using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Common.Globals.VanillaNPCAIOverrides;

namespace TimeDomain.Common.Globals
{
    public class VanillaBossAIChangeNPC : GlobalNPC
    {
        public override void SetDefaults(NPC npc)
        {
            if (!TimeDomain.AncientMode)
            {
                return;
            }
            if (npc.type == NPCID.Creeper)
            {
                npc.lifeMax = 150;
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                npc.knockBackResist = 0;
                npc.damage = 45;
                npc.lifeMax = 1800;
            }
            if (npc.type == NPCID.KingSlime)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.2f);
                npc.damage = (int)(npc.damage * 1.2f);
                npc.noGravity = false;
            }
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (!TimeDomain.AncientMode)
            {
                return;
            }
            if (npc.type == NPCID.Creeper)
            {
                return;
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                BrainofCthulhuAI.OverrideOnSpawn(npc);
                return;
            }
            //if (npc.type == NPCID.KingSlime)
            //{
            //    KingSlimeAI.OverrideOnSpawn(npc);
            //    return;
            //}
        }
        public override bool InstancePerEntity => true;


        public override bool PreAI(NPC npc)
        {
            if (!TimeDomain.AncientMode)
            {
                return base.PreAI(npc);
            }
            if (npc.type == NPCID.Creeper)
            {
                return CreeperAI.BuffedAI(npc);
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                return BrainofCthulhuAI.BuffedAI(npc);
            }
            if (npc.type == NPCID.KingSlime)
            {
                KingSlimeAI.ChangeVanillaAI(npc);
                return false;
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                if (!NPC.downedBoss2)
                {
                    npc.active = false;
                    NPC.NewNPC(npc.GetSource_FromAI(), (int)Main.player[npc.target].Center.X, (int)Main.player[npc.target].Center.Y, NPCID.Guide);
                }
            }
            return base.PreAI(npc);
        }

        public override void PostAI(NPC npc)
        {
        //    if (!TimeDomain.AncientMode)
        //    {
        //        return;
        //    }
        //    if (npc.type == NPCID.KingSlime)
        //    {
        //        if (npc.velocity.Y != 0)
        //        {
        //            if (npc.velocity.Y < 0)
        //            {
        //                KSTime++;
        //                npc.TargetClosest(true);
        //                if (KSTime == 1)
        //                {
        //                    KSTargetVector2 = Main.player[npc.target].Center;
        //                }
        //                float c = 0;
        //                if ((KSTargetVector2 - npc.Center).X > 0)
        //                {
        //                    c = -64;
        //                }
        //                else
        //                {
        //                    c = 64;
        //                }
        //                float tx = (KSTargetVector2 + new Vector2(c, 0) - npc.Center).X / 10;
        //                if (tx >= 50)
        //                {
        //                    tx = 50;
        //                }
        //                npc.velocity.X = tx;
        //            }
        //            else
        //            {
        //                KSTime = 0;
        //            }
        //        }
        //    }
        }
        public override void AI(NPC npc)
        {
            if (!TimeDomain.AncientMode)
            {
                return;
            }
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (!TimeDomain.AncientMode)
            {
                return;
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                BrainofCthulhuAI.PostDraw(npc, spriteBatch, screenPos, drawColor);
            }
        }
        public override bool CheckDead(NPC npc)
        {
            if (!TimeDomain.AncientMode)
            {
                return true;
            }
            if (npc.type == NPCID.Creeper)
            {
                return CreeperAI.CheckDead(npc);
            }
            return base.CheckDead(npc);
        }
        public bool oldai()
        {
            return true;
        }
    }
}
