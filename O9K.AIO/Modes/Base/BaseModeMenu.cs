namespace O9K.AIO.Modes.Base
{
    using System.Collections.Generic;

    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class BaseModeMenu
    {
        private readonly Dictionary<string, string> cnLoc = new Dictionary<string, string>
        {
            { "Move", "逃跑" },
            { "Harass", "骚扰" },
            { "Combo", "连招" },
            { "Alternative combo", "替代连招" },
            { "Toss to tower", LocalizationHelper.LocalizeName(AbilityId.tiny_toss) + "到防御塔" },
            { "Glimpse tracker", LocalizationHelper.LocalizeName(AbilityId.disruptor_glimpse) + "遵循" },
            {
                "Roll+Smash combo",
                LocalizationHelper.LocalizeName(AbilityId.earth_spirit_rolling_boulder)
                + LocalizationHelper.LocalizeName(AbilityId.earth_spirit_boulder_smash) + "连招"
            },
            { "Healing ward control", "控制" + LocalizationHelper.LocalizeName(AbilityId.juggernaut_healing_ward) },
            { "Auto chains", "自动" + LocalizationHelper.LocalizeName(AbilityId.ember_spirit_searing_chains) },
            { "Auto return", "自动" + LocalizationHelper.LocalizeName(AbilityId.kunkka_return) },
            { "Stack ancients", "堆野远古" },
            {
                "Blink+Skewer combo",
                LocalizationHelper.LocalizeName(AbilityId.item_blink) + LocalizationHelper.LocalizeName(AbilityId.magnataur_skewer) + "连招"
            },
            { "Heal ally", "治疗盟友" },
            { "Sun ray ally", LocalizationHelper.LocalizeName(AbilityId.phoenix_sun_ray) + "盟友" },
            { "Hook ally", LocalizationHelper.LocalizeName(AbilityId.pudge_meat_hook) + "盟友" },
            { "Suicide", "自杀" },
            { "Charge away", LocalizationHelper.LocalizeName(AbilityId.spirit_breaker_charge_of_darkness) + "很远" },
            { "Mana calculator", "马纳计算器" },
            { "Overload charge", LocalizationHelper.LocalizeName(AbilityId.storm_spirit_overload) + "充电" },
            { "Health tracker", "生命值踪器" },
        };

        private readonly Dictionary<string, string> ruLoc = new Dictionary<string, string>
        {
            { "Move", "Убегание" },
            { "Harass", "Харас" },
            { "Combo", "Комбо" },
            { "Alternative combo", "Доп. комбо" },
            { "Toss to tower", "Тосс к вышке" },
            { "Glimpse tracker", LocalizationHelper.LocalizeName(AbilityId.disruptor_glimpse) + " трекер" },
            { "Roll+Smash combo", "Ролл+Смэш комбо" },
            { "Healing ward control", "Контролировать " + LocalizationHelper.LocalizeName(AbilityId.juggernaut_healing_ward) },
            { "Auto chains", "Авто " + LocalizationHelper.LocalizeName(AbilityId.ember_spirit_searing_chains) },
            { "Auto return", "Авто " + LocalizationHelper.LocalizeName(AbilityId.kunkka_return) },
            { "Stack ancients", "Стакать эншентов" },
            { "Blink+Skewer combo", "Блинк+скювер комбо" },
            { "Heal ally", "Хилить союзника" },
            { "Sun ray ally", "Хилить союзника" },
            { "Hook ally", "Хук союзника" },
            { "Suicide", "Суицид" },
            { "Charge away", "Чардж вдаль" },
            { "Mana calculator", "Мана калькулятор" },
            { "Overload charge", "Зарядка оверлорда" },
            { "Health tracker", "Трекер жизни" },
        };

        public BaseModeMenu(Menu rootMenu, string displayName)
        {
            this.SimplifiedName = displayName.Replace(" ", "").ToLower();
            this.Menu = new Menu(displayName, this.SimplifiedName);

            if (this.ruLoc.TryGetValue(displayName, out var loc))
            {
                this.Menu.AddTranslation(Lang.Ru, loc);
            }

            if (this.cnLoc.TryGetValue(displayName, out loc))
            {
                this.Menu.AddTranslation(Lang.Cn, loc);
            }
        }

        public string SimplifiedName { get; }

        protected Menu Menu { get; }
    }
}