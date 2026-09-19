using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using static System.Net.Mime.MediaTypeNames;

namespace TimeDomain.Content.Items.Weapons.Melee
{
    public class Editor : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 80;
            Item.damage = 276*10;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTime = Item.useAnimation = 10;
            Item.knockBack = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = null;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<EditorProj>();
            Item.shootSpeed = 10f;
        }
        public override void SetStaticDefaults()
        {
            EditorEffect = ModContent.Request<Effect>("TimeDomain/Content/Effects/Editor", AssetRequestMode.ImmediateLoad).Value;
        }
        public static Effect EditorEffect;
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.Mod == "Terraria" && line.Name == "ItemName")
            {
                SpriteBatch sb = Main.spriteBatch;
                sb.End();
                sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.SamplerStateForCursor, null, null, null, Main.UIScaleMatrix);
                //GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(3556), Item);
                //float O = (Main.GlobalTimeWrappedHourly % 60) / 60f;
                //float O2 = (Main.GlobalTimeWrappedHourly % 120) / 60f;
                EditorEffect.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly);
                EditorEffect.CurrentTechnique.Passes[0].Apply();

                //ChatManager.DrawColorCodedStringWithShadow(sb, line.Font, line.Text, new Vector2((float)line.X, (float)line.Y), line.Color, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);

                TextSnippet[] snippets = ChatManager.ParseMessage(line.Text, line.Color).ToArray();
                ChatManager.ConvertNormalSnippets(snippets);
                int hoveredSnippet;
                ChatManager.DrawColorCodedString(sb, line.Font, snippets, new Vector2((float)line.X, (float)line.Y), Color.White, line.Rotation, line.Origin, line.BaseScale, out hoveredSnippet, line.MaxWidth);
                sb.End();
                sb.Begin(0, BlendState.AlphaBlend, Main.SamplerStateForCursor, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.GameUpdateCount % 3 == 0)
            {
                int d1 = Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.Shadowflame);
                int d2 = Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.ShadowbeamStaff);
                Main.dust[d1].noGravity = true;
                Main.dust[d2].noGravity = true;
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback);
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            //
            if (player.ownedProjectileCounts[ModContent.ProjectileType<EditorProj>()] > 0)
                return false;
            return base.CanUseItem(player);
        }
    }
}
