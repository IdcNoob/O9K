using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O9K.AutoUsage.Abilities.HealthRestore.Unique.Tango
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    class TangoSettings
    {
        public TangoSettings(Menu settings, IHealthRestore ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.HpThreshold = menu.GetOrAdd(new MenuSlider("HP% threshold", 80, 1, 100));
            this.HpThreshold.SetTooltip("Use when hero hp% is lower");
            this.HpThreshold.AddTranslation(Lang.Ru, "Порог ХП%");
            this.HpThreshold.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда у героя меньше здоровья%");
            this.HpThreshold.AddTranslation(Lang.Cn, "生命值％");
            this.HpThreshold.AddTooltipTranslation(Lang.Cn, "当英雄健康百分比较低时使用");
            
            this.HappyLittleTreeOnly = menu.GetOrAdd(new MenuSwitcher("Use only on happy little tree"));
            this.HappyLittleTreeOnly.AddTranslation(
                Lang.Ru,
                "Использовать только на дерево из " + LocalizationHelper.LocalizeAbilityName(nameof(AbilityId.item_branches)));
            this.HappyLittleTreeOnly.AddTranslation(Lang.Cn, "仅适用于" + LocalizationHelper.LocalizeAbilityName(nameof(AbilityId.item_branches)));

            this.EnemiesCount = menu.GetOrAdd(new MenuSlider("Number of enemies", 1, 0, 5));
            this.EnemiesCount.SetTooltip("Use only when there are equals/more enemies near target");
            this.EnemiesCount.AddTranslation(Lang.Ru, "Число врагов");
            this.EnemiesCount.AddTooltipTranslation(Lang.Ru, "Использовать способность, если врагов больше или равно");
            this.EnemiesCount.AddTranslation(Lang.Cn, "敌人数量");
            this.EnemiesCount.AddTooltipTranslation(Lang.Cn, "仅在附近有相等/更多敌人的情况下使用");

            this.Distance = menu.GetOrAdd(new MenuSlider("Distance", 1200, 0, 2000));
            this.Distance.SetTooltip("Use ability only when enemy is closer");
            this.Distance.AddTranslation(Lang.Ru, "Дистанция");
            this.Distance.AddTooltipTranslation(Lang.Ru, "Использовать способность, если враг находится ближе");
            this.Distance.AddTranslation(Lang.Cn, "距离");
            this.Distance.AddTooltipTranslation(Lang.Cn, "如果敌人更近，使用能力");
        }
        
        public MenuSwitcher HappyLittleTreeOnly { get; }
        public MenuSlider Distance { get; }

        public MenuSlider EnemiesCount { get; }
        public MenuSlider HpThreshold { get; }

    }
}
