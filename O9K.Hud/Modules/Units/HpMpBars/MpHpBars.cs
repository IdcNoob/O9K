namespace O9K.Hud.Modules.Units.HpMpBars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Renderer;

    using MainMenu;

    internal class MpHpBars : IHudModule
    {
        private readonly IContext9 context;

        private readonly MenuSwitcher enabledMp;

        private readonly MenuSlider hpPositionY;

        private readonly MenuSlider hpSizeY;

        private readonly MenuSlider hpTextSize;

        private readonly MenuSlider manaTextSize;

        private readonly MenuSlider mpPositionY;

        private readonly MenuSlider mpSizeY;

        private readonly MenuSwitcher showHpAmount;

        private readonly MenuSwitcher showHpRestore;

        private readonly MenuSwitcher showMpAmount;

        private readonly MenuSwitcher showMpRestore;

        private readonly List<HpMpUnit> units = new List<HpMpUnit>();

        private Team ownerTeam;

        [ImportingConstructor]
        public MpHpBars(IContext9 context, IHudMenu hudMenu)
        {
            this.context = context;

            var menuMp = hudMenu.UnitsMenu.Add(new Menu("Mana bars"));
            menuMp.AddTranslation(Lang.Ru, "Мана");
            menuMp.AddTranslation(Lang.Cn, "魔法条");

            this.enabledMp = menuMp.Add(new MenuSwitcher("Enabled"));
            this.enabledMp.AddTranslation(Lang.Ru, "Включено");
            this.enabledMp.AddTranslation(Lang.Cn, "启用");

            this.showMpRestore = menuMp.Add(new MenuSwitcher("Show restore bar"));
            this.showMpRestore.AddTranslation(Lang.Ru, "оказать панель восстановления");
            this.showMpRestore.AddTranslation(Lang.Cn, "显示还原栏");

            this.showMpAmount = menuMp.Add(new MenuSwitcher("Show amount", false));
            this.showMpAmount.AddTranslation(Lang.Ru, "Показывать количество");
            this.showMpAmount.AddTranslation(Lang.Cn, "显示金额");

            var settingsMp = menuMp.Add(new Menu("Settings"));
            settingsMp.AddTranslation(Lang.Ru, "Настройки");
            settingsMp.AddTranslation(Lang.Cn, "设置");

            this.mpPositionY = settingsMp.Add(new MenuSlider("Y coordinate", "yc4", (int)(1 * Hud.Info.ScreenRatio), -100, 100));
            this.mpPositionY.AddTranslation(Lang.Ru, "Y координата");
            this.mpPositionY.AddTranslation(Lang.Cn, "Y位置");

            this.mpSizeY = settingsMp.Add(new MenuSlider("Y size", "ys66", (int)(5 * -Hud.Info.ScreenRatio), -50, 50));
            this.mpSizeY.AddTranslation(Lang.Ru, "Y размер");
            this.mpSizeY.AddTranslation(Lang.Cn, "Y大小");

            this.manaTextSize = settingsMp.Add(new MenuSlider("Mana amount size", "manaSize", 16, 10, 35));
            this.manaTextSize.AddTranslation(Lang.Ru, "Размер текста");
            this.manaTextSize.AddTranslation(Lang.Cn, "文本大小");

            var menuHp = hudMenu.UnitsMenu.Add(new Menu("Health bars"));
            menuHp.AddTranslation(Lang.Ru, "Здоровье");
            menuHp.AddTranslation(Lang.Cn, "生命条");

            this.showHpRestore = menuHp.Add(new MenuSwitcher("Show restore bar"));
            this.showHpRestore.AddTranslation(Lang.Ru, "оказать панель восстановления");
            this.showHpRestore.AddTranslation(Lang.Cn, "显示还原栏");

            this.showHpAmount = menuHp.Add(new MenuSwitcher("Show amount", false));
            this.showHpAmount.AddTranslation(Lang.Ru, "Показывать количество");
            this.showHpAmount.AddTranslation(Lang.Cn, "显示金额");

            var settingsHp = menuHp.Add(new Menu("Settings"));
            settingsHp.AddTranslation(Lang.Ru, "Настройки");
            settingsHp.AddTranslation(Lang.Cn, "设置");

            this.hpPositionY = settingsHp.Add(new MenuSlider("Y coordinate", "yc4s", 0, -100, 100));
            this.hpPositionY.AddTranslation(Lang.Ru, "Y координата");
            this.hpPositionY.AddTranslation(Lang.Cn, "Y位置");

            this.hpSizeY = settingsHp.Add(new MenuSlider("Y size", "yssx", (int)(6 * -Hud.Info.ScreenRatio), -50, 50));
            this.hpSizeY.AddTranslation(Lang.Ru, "Y размер");
            this.hpSizeY.AddTranslation(Lang.Cn, "Y大小");

            this.hpTextSize = settingsHp.Add(new MenuSlider("Hp amount size", "hpSize", 16, 10, 35));
            this.hpTextSize.AddTranslation(Lang.Ru, "Размер текста");
            this.hpTextSize.AddTranslation(Lang.Cn, "文本大小");
        }

        public void Activate()
        {
            this.ownerTeam = EntityManager9.Owner.Team;

            EntityManager9.UnitAdded += this.OnUnitAdded;
            EntityManager9.UnitRemoved += this.OnUnitRemoved;
            EntityManager9.AbilityAdded += this.OnAbilityAdded;
            EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
            this.context.Renderer.Draw += this.OnDraw;
        }

        public void Dispose()
        {
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
            this.context.Renderer.Draw -= this.OnDraw;
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (ability.IsTalent || ability.IsFake)
                {
                    return;
                }

                var owner = this.units.Find(x => x.Handle == ability.Owner.Handle);
                if (owner == null)
                {
                    return;
                }

                if (ability is IManaRestore manaRestore)
                {
                    owner.AddAbility(manaRestore);
                }

                if (ability is IHealthRestore healthRestore)
                {
                    owner.AddAbility(healthRestore);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAbilityRemoved(Ability9 ability)
        {
            try
            {
                if (ability.IsTalent || ability.IsFake)
                {
                    return;
                }

                var owner = this.units.Find(x => x.Handle == ability.Owner.Handle);
                if (owner == null)
                {
                    return;
                }

                if (ability is IManaRestore manaRestore)
                {
                    owner.RemoveAbility(manaRestore);
                }

                if (ability is IHealthRestore healthRestore)
                {
                    owner.RemoveAbility(healthRestore);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                foreach (var unit in this.units)
                {
                    if (!unit.IsValid)
                    {
                        continue;
                    }

                    var hpBar = unit.HealthBar;
                    if (hpBar.IsZero)
                    {
                        continue;
                    }

                    if (this.enabledMp)
                    {
                        unit.DrawManaBar(
                            renderer,
                            hpBar,
                            this.mpPositionY,
                            this.mpSizeY,
                            this.showMpRestore,
                            this.showMpAmount,
                            this.manaTextSize);
                    }

                    unit.DrawHealthBar(
                        renderer,
                        hpBar,
                        this.hpPositionY,
                        this.hpSizeY,
                        this.showHpRestore,
                        this.showHpAmount,
                        this.hpTextSize);
                }
            }
            catch (InvalidOperationException)
            {
                // ignore
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitAdded(Unit9 unit)
        {
            try
            {
                if (!unit.IsHero || unit.IsIllusion || unit.Team == this.ownerTeam)
                {
                    return;
                }

                this.units.Add(new HpMpUnit(unit));
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitRemoved(Unit9 unit)
        {
            try
            {
                if (!unit.IsHero || unit.IsIllusion || unit.Team == this.ownerTeam)
                {
                    return;
                }

                var hpMpUnit = this.units.Find(x => x.Handle == unit.Handle);
                if (hpMpUnit == null)
                {
                    return;
                }

                this.units.Remove(hpMpUnit);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}