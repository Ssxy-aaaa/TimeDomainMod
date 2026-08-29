using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TimeDomain.Content.Items.Materials;
using Microsoft.Xna.Framework;

namespace TimeDomain.Content.Items.Armors
{
    [AutoloadEquip(EquipType.Head)]
    public class GraniteHelmet : ModItem
    {
        public static Player player = Main.player[Main.myPlayer];
        public static GranitePlayer modPlayer = player.GetModPlayer<GranitePlayer>();
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0,1,0,0);
            Item.defense = 5;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<GraniteHelmet>()
                && body.type == ModContent.ItemType<GraniteBreastplate>()
                && legs.type == ModContent.ItemType<GraniteLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            modPlayer.IsArmorSet = true;
            if (modPlayer.IsDefenseMode && modPlayer.DefenseModeTimeLeft > 0)
            {
                player.endurance += 0.5f;
                modPlayer.ThornsMode = true;
            }
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed -= 0.05f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Granite, 50)
                .AddIngredient(ModContent.ItemType<GraniteCore>(), 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    public class GranitePlayer : ModPlayer
    {
        public bool IsDefenseMode = false;
        public int EnergyBallCount = 0;
        public float DefenseModeTimeLeft = 0;
        public bool ThornsMode = false;
        public bool IsArmorSet = false;
        public float CD = 0;
        public override void PostUpdate()
        {
            if (EnergyBallCount >= 5)
            {
                
            }

            if (DefenseModeTimeLeft > 0)
                DefenseModeTimeLeft--;
            if (DefenseModeTimeLeft < 0)
                DefenseModeTimeLeft = 0;

            if (CD > 0)
                CD--;
            if (CD < 0)
                CD = 0;


            if (DefenseModeTimeLeft <= 0)
                IsDefenseMode = false;

            if (IsDefenseMode && DefenseModeTimeLeft > 0)
            {
                int d = Dust.NewDust(Player.Center + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), 5, 5, DustID.Granite, Main.rand.Next(-1, 1), Main.rand.Next(-1, 1));
                Main.dust[d].noGravity = true;
            }
            if (!IsDefenseMode)
            {
                ThornsMode = false;
            }
        }
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (ThornsMode)
            {
                NPC.HitInfo HitInfo = new NPC.HitInfo();
                HitInfo.Damage = (int)(hurtInfo.Damage * 2.5f);
                npc.StrikeNPC(HitInfo);
            }
        }
        public override void ArmorSetBonusHeld(int holdTime)
        {
            bool flag5 = (Player.controlDown);
            if (IsArmorSet && CD == 0)
            {
                if (Player.doubleTapCardinalTimer[0] > 0)
                {
                    if (flag5)
                    {
                        IsDefenseMode = true;
                        DefenseModeTimeLeft = 600;
                        CD = 3000;
                    }
                }
            }
        }
    }
}
