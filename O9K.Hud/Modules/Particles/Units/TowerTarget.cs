namespace O9K.Hud.Modules.Particles.Units
{
    using System;
    using System.ComponentModel.Composition;

    using Core.Entities.Heroes;
    using Core.Entities.Units;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using MainMenu;

    using SharpDX;

    internal class TowerTarget : IHudModule
    {
        private readonly MenuSwitcher show;

        private ParticleEffect effect;

        private IUpdateHandler handler;

        private Owner owner;

        private Tower tower;

        private Unit9 towerTarget;

        [ImportingConstructor]
        public TowerTarget(IHudMenu hudMenu)
        {
            this.show = hudMenu.ParticlesMenu.Add(new MenuSwitcher("Tower target").SetTooltip("Show when tower's target is hero"));
            this.show.AddTranslation(Lang.Ru, "Цель вышки");
            this.show.AddTooltipTranslation(Lang.Ru, "Показать, когда вышка атакует героя");
            this.show.AddTranslation(Lang.Cn, "塔目标");
            this.show.AddTooltipTranslation(Lang.Cn, "顯示塔何時攻擊您的英雄");
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;

            this.show.ValueChange += this.ShowOnValueChange;
        }

        public void Dispose()
        {
            this.show.ValueChange -= this.ShowOnValueChange;
            UpdateManager.Unsubscribe(this.handler);
            Entity.OnHandlePropertyChange -= this.OnHandlePropertyChange;
        }

        private void OnHandlePropertyChange(Entity sender, HandlePropertyChangeEventArgs args)
        {
            try
            {
                if (args.NewValue == args.OldValue || args.PropertyName != "m_hTowerAttackTarget")
                {
                    return;
                }

                var unit = EntityManager9.GetUnit(args.NewValue);
                if (unit == null || !unit.IsHero || unit.IsIllusion || unit.Distance(this.owner) > 1000)
                {
                    return;
                }

                this.effect?.Dispose();

                this.effect = new ParticleEffect(@"materials\ensage_ui\particles\target.vpcf", unit.Position);
                this.effect.SetControlPoint(2, sender.Position);
                this.effect.SetControlPoint(5, new Vector3(255, 0, 0));
                this.effect.SetControlPoint(6, new Vector3(255));

                this.tower = (Tower)sender;
                this.towerTarget = unit;
                this.handler.IsEnabled = true;
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
                if (!this.tower.IsValid || !this.effect.IsValid || !this.tower.IsAlive
                    || this.tower.AttackTarget?.Handle != this.towerTarget.Handle)
                {
                    this.effect.Dispose();
                    this.effect = null;
                    this.handler.IsEnabled = false;
                    return;
                }

                this.effect.SetControlPoint(7, this.towerTarget.Position);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void ShowOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.handler = UpdateManager.Subscribe(this.OnUpdate, 0, false);
                Entity.OnHandlePropertyChange += this.OnHandlePropertyChange;
            }
            else
            {
                Entity.OnHandlePropertyChange -= this.OnHandlePropertyChange;
                UpdateManager.Unsubscribe(this.handler);
                this.effect?.Dispose();
                this.effect = null;
            }
        }
    }
}