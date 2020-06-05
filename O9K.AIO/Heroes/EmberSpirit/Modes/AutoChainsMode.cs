namespace O9K.AIO.Heroes.EmberSpirit.Modes
{
    using System;
    using System.Linq;

    using AIO.Modes.Permanent;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Logger;

    using Ensage;

    internal class AutoChainsMode : PermanentMode
    {
        private readonly AutoChainsModeMenu menu;

        private bool useChains;

        public AutoChainsMode(BaseHero baseHero, AutoChainsModeMenu menu)
            : base(baseHero, menu)
        {
            this.menu = menu;
            Player.OnExecuteOrder += this.OnExecuteOrder;
        }

        public override void Dispose()
        {
            base.Dispose();
            Player.OnExecuteOrder -= this.OnExecuteOrder;
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;
        }

        protected override void Execute()
        {
            if (!this.useChains)
            {
                return;
            }

            var hero = this.Owner.Hero;

            if (!hero.IsValid || !hero.IsAlive)
            {
                return;
            }

            var chains = hero.Abilities.FirstOrDefault(x => x.Id == AbilityId.ember_spirit_searing_chains) as ActiveAbility;
            if (chains?.IsValid != true || !chains.CanBeCasted())
            {
                return;
            }

            var enemy = this.TargetManager.EnemyHeroes.Any(x => chains.CanHit(x));
            if (!enemy)
            {
                return;
            }

            if (this.menu.fistKey)
            {
                chains.UseAbility();
            }

            this.useChains = false;
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (!args.IsPlayerInput || !args.Process || args.OrderId != OrderId.AbilityLocation)
                {
                    return;
                }

                if (args.Ability.Id == AbilityId.ember_spirit_sleight_of_fist)
                {
                    Unit.OnModifierAdded += this.OnModifierAdded;
                    Unit.OnModifierRemoved += this.OnModifierRemoved;
                }
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
                if (sender.Handle != this.Owner.HeroHandle)
                {
                    return;
                }

                if (args.Modifier.Name == "modifier_ember_spirit_sleight_of_fist_caster")
                {
                    this.useChains = true;
                }
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
                if (sender.Handle != this.Owner.HeroHandle)
                {
                    return;
                }

                if (args.Modifier.Name == "modifier_ember_spirit_sleight_of_fist_caster")
                {
                    Unit.OnModifierAdded -= this.OnModifierAdded;
                    Unit.OnModifierRemoved -= this.OnModifierRemoved;
                    this.useChains = false;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}