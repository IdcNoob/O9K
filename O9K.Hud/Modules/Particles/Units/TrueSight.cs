namespace O9K.Hud.Modules.Particles.Units
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;

    using MainMenu;

    internal class TrueSight : IHudModule
    {
        private readonly Dictionary<uint, ParticleEffect> effects = new Dictionary<uint, ParticleEffect>();

        private readonly MenuSwitcher show;

        private Team ownerTeam;

        [ImportingConstructor]
        public TrueSight(IHudMenu hudMenu)
        {
            this.show = hudMenu.ParticlesMenu.Add(new MenuSwitcher("True sight", "trueSight"));
            this.show.AddTranslation(Lang.Cn, "真实视域");
        }

        public void Activate()
        {
            this.ownerTeam = EntityManager9.Owner.Team;
            this.show.ValueChange += this.ShowOnValueChange;
        }

        public void Dispose()
        {
            this.show.ValueChange -= this.ShowOnValueChange;
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;

            foreach (var effect in this.effects)
            {
                effect.Value.Dispose();
            }

            this.effects.Clear();
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Team != this.ownerTeam)
                {
                    return;
                }

                if (!(sender is Hero))
                {
                    return;
                }

                if (args.Modifier.Name != "modifier_truesight" && args.Modifier.Name != "modifier_item_dustofappearance")
                {
                    return;
                }

                if (this.effects.ContainsKey(sender.Handle))
                {
                    return;
                }

                var effect = new ParticleEffect("particles/items2_fx/ward_true_sight.vpcf", sender, ParticleAttachment.CenterFollow);
                this.effects.Add(sender.Handle, effect);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Team != this.ownerTeam)
                {
                    return;
                }

                if (!(sender is Hero))
                {
                    return;
                }

                if (args.Modifier.Name != "modifier_truesight" && args.Modifier.Name != "modifier_item_dustofappearance")
                {
                    return;
                }

                if (!this.effects.TryGetValue(sender.Handle, out var effect))
                {
                    return;
                }

                effect.Dispose();
                this.effects.Remove(sender.Handle);
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
                Unit.OnModifierAdded += this.OnModifierAdded;
                Unit.OnModifierRemoved += this.OnModifierRemoved;
            }
            else
            {
                Unit.OnModifierAdded -= this.OnModifierAdded;
                Unit.OnModifierRemoved -= this.OnModifierRemoved;

                foreach (var effect in this.effects)
                {
                    effect.Value.Dispose();
                }

                this.effects.Clear();
            }
        }
    }
}