namespace O9K.AutoUsage.Abilities.HealthRestore.Unique.AttributeShiftStrengthGain
{
    using System;
    using System.Collections.Generic;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Abilities.Heroes.Morphling;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Settings;

    [AbilityId(AbilityId.morphling_morph_str)]
    internal class AttributeShiftStrengthGainAbility : HealthRestoreAbility
    {
        private readonly AttributeShiftStrengthGainSettings settings;

        private readonly AttributeShiftAgilityGain shiftAgi;

        private readonly AttributeShiftStrengthGain shiftStr;

        private readonly Sleeper sleeper = new Sleeper();

        private float balanceHealth;

        private bool manualToggle;

        public AttributeShiftStrengthGainAbility(IHealthRestore healthRestore, GroupSettings settings)
            : base(healthRestore)
        {
            this.settings = new AttributeShiftStrengthGainSettings(settings.Menu, healthRestore);
            this.shiftStr = (AttributeShiftStrengthGain)healthRestore;
            this.shiftAgi = this.shiftStr.AttributeShiftAgilityGain;

            this.balanceHealth = this.Owner.Health;
        }

        public override void Enabled(bool enabled)
        {
            base.Enabled(enabled);

            if (enabled)
            {
                Player.OnExecuteOrder += this.OnExecuteOrder;
                Unit.OnModifierAdded += this.OnModifierAdded;
                UpdateManager.Subscribe(this.OnUpdate, 100);
            }
            else
            {
                Player.OnExecuteOrder -= this.OnExecuteOrder;
                Unit.OnModifierAdded -= this.OnModifierAdded;
                UpdateManager.Unsubscribe(this.OnUpdate);
            }
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            return false;
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (args.OrderId != OrderId.ToggleAbility || !args.IsPlayerInput || !args.Process)
                {
                    return;
                }

                if (args.Ability.Id != this.shiftStr.Id && args.Ability.Id != this.shiftAgi.Id)
                {
                    return;
                }

                if (!args.Ability.IsToggled)
                {
                    this.manualToggle = true;
                    return;
                }

                var delay = (int)Game.Ping + 100;
                UpdateManager.BeginInvoke(
                    () =>
                        {
                            this.balanceHealth = this.Owner.Health;
                            this.manualToggle = false;
                        },
                    delay);

                this.sleeper.Sleep(delay / 1000f);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Handle != this.Owner.Handle || args.Modifier.Name != "modifier_pudge_meat_hook")
                {
                    return;
                }

                this.sleeper.Sleep(2f);
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
                if (this.manualToggle || this.sleeper.IsSleeping || !this.settings.AutoBalance)
                {
                    return;
                }

                if (!this.shiftStr.CanBeCasted())
                {
                    return;
                }

                if (!this.Owner.CanBeHealed)
                {
                    if (this.shiftAgi.Enabled)
                    {
                        this.shiftAgi.Enabled = false;
                        this.sleeper.Sleep(0.1f);
                    }
                    else if (this.shiftStr.Enabled)
                    {
                        this.shiftAgi.Enabled = false;
                        this.sleeper.Sleep(0.1f);
                    }

                    return;
                }

                var health = this.Owner.Health;

                if (health > this.balanceHealth + 150)
                {
                    if (!this.shiftAgi.Enabled)
                    {
                        this.shiftAgi.Enabled = true;
                        this.sleeper.Sleep(0.1f);
                    }
                }
                else if (health < this.balanceHealth)
                {
                    if (!this.shiftStr.Enabled)
                    {
                        this.shiftStr.Enabled = true;
                        this.sleeper.Sleep(0.1f);
                    }
                }
                else
                {
                    if (this.shiftAgi.Enabled)
                    {
                        this.shiftAgi.Enabled = false;
                        this.sleeper.Sleep(0.1f);
                    }
                    else if (this.shiftStr.Enabled)
                    {
                        this.shiftStr.Enabled = false;
                        this.sleeper.Sleep(0.1f);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}