namespace O9K.Hud.Modules.Units
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Mines;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    using MainMenu;

    internal class VisibleByEnemy : IHudModule
    {
        private readonly MenuSwitcher buildings;

        private readonly MenuSwitcher creeps;

        private readonly MenuSelector<string> effectName;

        private readonly Dictionary<string, string> effects = new Dictionary<string, string>
        {
            { "Shiva", "particles/items_fx/aura_shivas.vpcf" },
            { "Radial", "particles/ui/ui_sweeping_ring.vpcf" },
            { "Beam", "particles/units/heroes/hero_omniknight/omniknight_heavenly_grace_beam.vpcf" },
            { "Beam light", "particles/units/heroes/hero_spirit_breaker/spirit_breaker_haste_owner_status.vpcf" },
            { "Dark", "particles/units/heroes/hero_spirit_breaker/spirit_breaker_haste_owner_dark.vpcf" },
            { "Purge", "particles/units/heroes/hero_oracle/oracle_fortune_purge.vpcf" },
            { "Timer", "particles/units/heroes/hero_spirit_breaker/spirit_breaker_haste_owner_timer.vpcf" },
        };

        private readonly MenuSwitcher enabled;

        private readonly MenuSwitcher heroes;

        private readonly MenuSwitcher other;

        private readonly Dictionary<Unit9, ParticleEffect> particles = new Dictionary<Unit9, ParticleEffect>();

        private readonly List<Unit9> units = new List<Unit9>();

        private Team enemyTeam;

        [ImportingConstructor]
        public VisibleByEnemy(IHudMenu hudMenu)
        {
            var menu = hudMenu.UnitsMenu.Add(new Menu("Visible by enemy"));
            menu.AddTranslation(Lang.Ru, "Вражеский обзор");
            menu.AddTranslation(Lang.Cn, "敌人的视觉");

            this.enabled = menu.Add(new MenuSwitcher("Enabled")).SetTooltip("Show when ally/neutral unit is visible by enemy");
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTooltipTranslation(Lang.Ru, "Показывать когда союзник виден для врагов");
            this.enabled.AddTranslation(Lang.Cn, "启用");
            this.enabled.AddTooltipTranslation(Lang.Cn, "显示敌方/中立单位何时可见");

            this.heroes = menu.Add(new MenuSwitcher("Heroes"));
            this.heroes.AddTranslation(Lang.Ru, "Герои");
            this.heroes.AddTranslation(Lang.Cn, "英雄");

            this.creeps = menu.Add(new MenuSwitcher("Creeps"));
            this.creeps.AddTranslation(Lang.Ru, "Крипы");
            this.creeps.AddTranslation(Lang.Cn, "兵线");

            this.buildings = menu.Add(new MenuSwitcher("Buildings"));
            this.buildings.AddTranslation(Lang.Ru, "Здания");
            this.buildings.AddTranslation(Lang.Cn, "建筑");

            this.other = menu.Add(new MenuSwitcher("Other"));
            this.other.AddTranslation(Lang.Ru, "Другое");
            this.other.AddTranslation(Lang.Cn, "其他");

            this.effectName = menu.Add(new MenuSelector<string>("Effect", this.effects.Keys));
            this.effectName.AddTranslation(Lang.Ru, "Эффект");
            this.effectName.AddTranslation(Lang.Cn, "效应");
        }

        public void Activate()
        {
            this.enemyTeam = EntityManager9.Owner.EnemyTeam;
            this.enabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.effectName.ValueChange -= this.EffectNameOnValueChange;
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
            UpdateManager.Unsubscribe(this.OnUpdate);
            this.heroes.ValueChange -= this.OptionOnValueChange;
            this.creeps.ValueChange -= this.OptionOnValueChange;
            this.buildings.ValueChange -= this.OptionOnValueChange;
            this.other.ValueChange -= this.OptionOnValueChange;

            foreach (var particle in this.particles.Values)
            {
                particle.Dispose();
            }

            this.particles.Clear();
            this.units.Clear();
        }

        private void EffectNameOnValueChange(object sender, SelectorEventArgs<string> e)
        {
            if (e.NewValue == e.OldValue)
            {
                return;
            }

            foreach (var particle in this.particles.Values)
            {
                particle.Dispose();
            }

            this.particles.Clear();
            this.units.Clear();

            UpdateManager.BeginInvoke(
                () =>
                    {
                        foreach (var unit in EntityManager9.Units)
                        {
                            this.OnUnitAdded(unit);
                        }
                    });
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                if (e.OldValue)
                {
                    //todo delete
                    if (AppDomain.CurrentDomain.GetAssemblies()
                        .Any(x => !x.GlobalAssemblyCache && x.GetName().Name.Contains("VisibleByEnemy")))
                    {
                        Hud.DisplayWarning("O9K.Hud // VisibleByEnemy is already included in O9K.Hud");
                    }
                }

                this.effectName.ValueChange += this.EffectNameOnValueChange;
                EntityManager9.UnitAdded += this.OnUnitAdded;
                EntityManager9.UnitRemoved += this.OnUnitRemoved;
                UpdateManager.Subscribe(this.OnUpdate, 300);
                this.heroes.ValueChange += this.OptionOnValueChange;
                this.creeps.ValueChange += this.OptionOnValueChange;
                this.buildings.ValueChange += this.OptionOnValueChange;
                this.other.ValueChange += this.OptionOnValueChange;
            }
            else
            {
                EntityManager9.UnitAdded -= this.OnUnitAdded;
                EntityManager9.UnitRemoved -= this.OnUnitRemoved;
                UpdateManager.Unsubscribe(this.OnUpdate);
                this.heroes.ValueChange -= this.OptionOnValueChange;
                this.creeps.ValueChange -= this.OptionOnValueChange;
                this.buildings.ValueChange -= this.OptionOnValueChange;
                this.other.ValueChange -= this.OptionOnValueChange;

                foreach (var particle in this.particles.Values)
                {
                    particle.Dispose();
                }

                this.particles.Clear();
                this.units.Clear();
            }
        }

        private void OnUnitAdded(Unit9 unit)
        {
            try
            {
                if (unit.Team == this.enemyTeam)
                {
                    return;
                }

                if (unit.IsHero)
                {
                    if (this.heroes)
                    {
                        this.units.Add(unit);
                        this.OnUpdate();
                    }

                    return;
                }

                if (unit.IsCreep)
                {
                    if (this.creeps)
                    {
                        this.units.Add(unit);
                        this.OnUpdate();
                    }

                    return;
                }

                if (unit.IsBuilding)
                {
                    if (this.buildings)
                    {
                        this.units.Add(unit);
                        this.OnUpdate();
                    }

                    return;
                }

                if (this.other)
                {
                    this.units.Add(unit);
                    this.OnUpdate();
                }
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
                if (unit.Team == this.enemyTeam)
                {
                    return;
                }

                if (this.particles.TryGetValue(unit, out var particle))
                {
                    particle.Dispose();
                    this.particles.Remove(unit);
                }

                this.units.Remove(unit);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdate()
        {
            try
            {
                foreach (var unit in this.units)
                {
                    if (!unit.IsValid)
                    {
                        return;
                    }

                    if (!unit.IsVisibleToEnemies || !unit.IsAlive)
                    {
                        if (!this.particles.TryGetValue(unit, out var particle))
                        {
                            continue;
                        }

                        particle.Dispose();
                        this.particles.Remove(unit);
                    }
                    else
                    {
                        if (unit is RemoteMine)
                        {
                            // manual position update
                            if (this.particles.TryGetValue(unit, out var particle))
                            {
                                particle.SetControlPoint(0, unit.Position);
                            }
                            else
                            {
                                this.particles.Add(unit, new ParticleEffect(this.effects[this.effectName], unit.Position));
                            }
                        }
                        else
                        {
                            if (this.particles.ContainsKey(unit))
                            {
                                continue;
                            }

                            this.particles.Add(
                                unit,
                                new ParticleEffect(this.effects[this.effectName], unit.BaseUnit, ParticleAttachment.AbsOriginFollow));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OptionOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue == e.OldValue)
            {
                return;
            }

            foreach (var particle in this.particles.Values)
            {
                particle.Dispose();
            }

            this.particles.Clear();
            this.units.Clear();

            UpdateManager.BeginInvoke(
                () =>
                    {
                        foreach (var unit in EntityManager9.Units)
                        {
                            this.OnUnitAdded(unit);
                        }
                    });
        }
    }
}