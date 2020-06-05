namespace O9K.Hud.Modules.Particles.Units
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;

    using MainMenu;

    internal class TowerDeny : IHudModule
    {
        private readonly MenuSwitcher show;

        private readonly Dictionary<Unit9, ParticleEffect> towers = new Dictionary<Unit9, ParticleEffect>();

        [ImportingConstructor]
        public TowerDeny(IHudMenu hudMenu)
        {
            this.show = hudMenu.ParticlesMenu.Add(new MenuSwitcher("Tower deny").SetTooltip("Show when ally/enemy tower can be denied"));
            this.show.AddTranslation(Lang.Ru, "Денай вышки");
            this.show.AddTooltipTranslation(Lang.Ru, "Показать, когда вышка может быть заденаена");
            this.show.AddTranslation(Lang.Cn, "反补防御塔");
            this.show.AddTooltipTranslation(Lang.Cn, "显示何时可以反补盟军/敌人塔");
        }

        public void Activate()
        {
            this.show.ValueChange += this.ShowOnValueChange;
        }

        public void Dispose()
        {
            this.show.ValueChange -= this.ShowOnValueChange;
            EntityManager9.UnitMonitor.UnitHealthChange -= this.UnitMonitorOnUnitHealthChange;
            EntityManager9.UnitMonitor.UnitDied -= this.UnitMonitorOnUnitDied;

            foreach (var effect in this.towers.Values)
            {
                effect.Dispose();
            }

            this.towers.Clear();
        }

        private void ShowOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                foreach (var tower in EntityManager9.Units.Where(x => x.IsTower && x.IsAlive))
                {
                    this.UnitMonitorOnUnitHealthChange(tower, tower.Health);
                }

                EntityManager9.UnitMonitor.UnitHealthChange += this.UnitMonitorOnUnitHealthChange;
                EntityManager9.UnitMonitor.UnitDied += this.UnitMonitorOnUnitDied;
            }
            else
            {
                EntityManager9.UnitMonitor.UnitHealthChange -= this.UnitMonitorOnUnitHealthChange;
                EntityManager9.UnitMonitor.UnitDied -= this.UnitMonitorOnUnitDied;

                foreach (var effect in this.towers.Values)
                {
                    effect.Dispose();
                }

                this.towers.Clear();
            }
        }

        private void UnitMonitorOnUnitDied(Unit9 unit)
        {
            try
            {
                if (!unit.IsTower)
                {
                    return;
                }

                if (!this.towers.TryGetValue(unit, out var effect))
                {
                    return;
                }

                effect.Dispose();
                this.towers.Remove(unit);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void UnitMonitorOnUnitHealthChange(Unit9 unit, float health)
        {
            try
            {
                if (!unit.IsTower)
                {
                    return;
                }

                if (health / unit.MaximumHealth > 0.1)
                {
                    if (!this.towers.TryGetValue(unit, out var effect))
                    {
                        return;
                    }

                    effect.Dispose();
                    this.towers.Remove(unit);
                }
                else
                {
                    if (this.towers.TryGetValue(unit, out var effect) && effect.IsValid)
                    {
                        return;
                    }

                    this.towers[unit] = new ParticleEffect("particles/msg_fx/msg_deniable.vpcf", unit.Position.IncreaseZ(125));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}