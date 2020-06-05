namespace O9K.AIO.Heroes.Base
{
    using Core.Entities.Units;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;

    internal class ControllableUnitMenu
    {
        public ControllableUnitMenu(Unit9 owner, Menu rootMenu)
        {
            Menu menu;
            string menuName;

            if (owner.IsIllusion)
            {
                menuName = owner.DefaultName + "illusion";
                menu = new Menu(owner.DisplayName + " (illusion)", menuName).SetTexture(owner.DefaultName);
                menu.AddTranslation(Lang.Ru, owner.DisplayName + " (иллюзия)");
                menu.AddTranslation(Lang.Cn, owner.DisplayName + " (幻象)");
            }
            else
            {
                menuName = owner.DefaultName;
                menu = new Menu(owner.DisplayName, menuName).SetTexture(owner.DefaultName);
            }

            var defaultOrbwalk = DefaultOrbwalkValue(owner);
            this.MenuCreate(rootMenu, menu, menuName, defaultOrbwalk);
        }

        public MenuSlider AdditionalDelay { get; private set; }

        public MenuSwitcher BodyBlock { get; private set; }

        public MenuSwitcher Control { get; private set; }

        public MenuSwitcher DangerMoveToMouse { get; private set; }

        public MenuSlider DangerRange { get; private set; }

        public MenuSwitcher Orbwalk { get; private set; }

        public MenuSwitcher OrbwalkerStopOnStanding { get; private set; }

        public MenuSelector OrbwalkingMode { get; private set; }

        private static bool DefaultOrbwalkValue(Unit9 owner)
        {
            if (owner.IsIllusion)
            {
                if (owner.Name == nameof(HeroId.npc_dota_hero_phantom_lancer))
                {
                    return false;
                }
            }

            if (!owner.IsHero)
            {
                if (owner.DefaultName == "npc_dota_shadow_shaman_ward")
                {
                    return false;
                }
            }

            return true;
        }

        private void MenuCreate(Menu rootMenu, Menu menu, string menuName, bool defaultOrbwalk)
        {
            this.Control = menu.Add(new MenuSwitcher("Control", "orbwalkControl" + menuName).SetTooltip("Control unit in combo"));
            this.Control.AddTranslation(Lang.Ru, "Управление");
            this.Control.AddTooltipTranslation(Lang.Ru, "Управлять юнитом в комбо");
            this.Control.AddTranslation(Lang.Cn, "控制");
            this.Control.AddTooltipTranslation(Lang.Cn, "在组合中控制单位");

            this.Orbwalk = menu.Add(new MenuSwitcher("Orbwalk", "orbwalk" + menuName, defaultOrbwalk).SetTooltip("Orbwalk or just attack"));
            this.Orbwalk.AddTranslation(Lang.Ru, "Орбвалк");
            this.Orbwalk.AddTooltipTranslation(Lang.Ru, "Использовать орбвалкер или просто атаковать");
            this.Orbwalk.AddTranslation(Lang.Cn, "走A");
            this.Orbwalk.AddTooltipTranslation(Lang.Cn, "走A或者只是攻击");

            this.OrbwalkingMode = menu.Add(
                new MenuSelector("Orbwalk mode", "orbwalkMode" + menuName, new[] { "Move to mouse", "Move to target" }));
            this.OrbwalkingMode.AddTranslation(Lang.Ru, "Режим орбвалка");
            this.OrbwalkingMode.AddValuesTranslation(Lang.Ru, new[] { "Двигаться к мышке", "Двигатся к цели" });
            this.OrbwalkingMode.AddTranslation(Lang.Cn, "走A方式");
            this.OrbwalkingMode.AddValuesTranslation(Lang.Cn, new[] { "移动到鼠标", "移动到目标" });

            this.BodyBlock = menu.Add(
                new MenuSwitcher("Body block", "orbBodyBlock" + menuName, false).SetTooltip(
                    "Unit(s) will try to body block the target in combo"));
            this.BodyBlock.AddTranslation(Lang.Ru, "Боди блок");
            this.BodyBlock.AddTooltipTranslation(Lang.Ru, "Юнит будет блокировать путь цели");
            this.BodyBlock.AddTranslation(Lang.Cn, "卡位");
            this.BodyBlock.AddTooltipTranslation(Lang.Cn, "单位将阻止目标的路径");

            this.OrbwalkerStopOnStanding = menu.Add(
                new MenuSwitcher("Stop orbwalk if standing", "orbwalkStopStanding" + menuName, false).SetTooltip(
                    "Unit will not orbwalk if target is standing"));
            this.OrbwalkerStopOnStanding.AddTranslation(Lang.Ru, "Останавливать орбвалк");
            this.OrbwalkerStopOnStanding.AddTooltipTranslation(Lang.Ru, "Не использовать орбвалк, если цель не двигается");
            this.OrbwalkerStopOnStanding.AddTranslation(Lang.Cn, "如果敌人静止不动停止走A");
            this.OrbwalkerStopOnStanding.AddTooltipTranslation(Lang.Cn, "如果目标站立，单位将不会走A");

            this.DangerRange = menu.Add(
                new MenuSlider("Danger range", "orbwalkerDanger" + menuName, 0, 0, 1200).SetTooltip(
                    "Unit will not move closer to the target"));
            this.DangerRange.AddTranslation(Lang.Ru, "Радиус опасности");
            this.DangerRange.AddTooltipTranslation(Lang.Ru, "Юнит не будет двигаться ближе к цели");
            this.DangerRange.AddTranslation(Lang.Cn, "危险范围");
            this.DangerRange.AddTooltipTranslation(Lang.Cn, "单位不会向目标靠拢");

            this.DangerMoveToMouse = menu.Add(
                new MenuSwitcher("Danger move to mouse", "orbwalkerDangerMove" + menuName, false).SetTooltip(
                    "Unit will move to mouse without attacking when in danger range"));
            this.DangerMoveToMouse.AddTranslation(Lang.Ru, "В опасности двигаться к мышке");
            this.DangerMoveToMouse.AddTooltipTranslation(Lang.Ru, "В радиусе опасности юнит будет двигаться к мышке не атакуя врага");
            this.DangerMoveToMouse.AddTranslation(Lang.Cn, "如危险移至鼠标");
            this.DangerMoveToMouse.AddTooltipTranslation(Lang.Cn, "在危险范围内时，单位会移动到鼠标上而不会受到攻击");

            this.AdditionalDelay = menu.Add(
                new MenuSlider("Additional delay", "orbwalkerDelay" + menuName, 0, 0, 500).SetTooltip(
                    "Set additional delay if unit cancels auto attack"));
            this.AdditionalDelay.AddTranslation(Lang.Cn, "额外延迟");
            this.AdditionalDelay.AddTooltipTranslation(Lang.Cn, "如果单位取消自动攻击，则设置其他延迟");

            rootMenu.Add(menu);
        }
    }
}