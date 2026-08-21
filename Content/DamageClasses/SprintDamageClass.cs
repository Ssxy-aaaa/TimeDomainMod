using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace yourmod.Content.DamageClasses
{
    public class SprintDamageClass : DamageClass
    {
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Generic)
            {
                return StatInheritanceData.Full;
            }
            return StatInheritanceData.None;
        }
        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            //if (damageClass == DamageClass.Melee)
            //    return true;
            //if (damageClass == DamageClass.Magic)
            //    return true;

            return false;
        }
        public override void SetDefaultStats(Player player)
        {
            // 此方法让你设置此伤害类型的默认属性加成 (像原版的伤害默认有+4%暴击率)
            // 此处我们使其默认拥有+4%暴击率和+10盔甲穿透
            player.GetCritChance<SprintDamageClass>() += 4;
            //player.GetArmorPenetration<HolyDamage>() += 10;
            // 你也可以在这里写伤害 (GetDamage), 击退 (GetKnockback), 和攻速 (GetAttackSpeed)
        }
        // 此属性决定此伤害类型是否使用标准的暴击计算公式
        // 请注意将其设为 false 会阻止描述中 "暴击率" 一行的显示
        // 并且即使你在 ShowStatTooltipLine 返回 true 也不行, 所以要小心!
        public override bool UseStandardCritCalcs => true;
        public override bool ShowStatTooltipLine(Player player, string lineName)
        {
            // 此方法允许你隐藏物品描述中特定伤害类型的数据显示
            // 四个可用的名称是 "Damage", "CritChance", "Speed", 和 "Knockback"
            // 这四行描述默认返回 true, 因此会显示出来 (废话), 但如果我们...
            if (lineName == "Speed")
                return false;

            return true;
        }
    }
}
