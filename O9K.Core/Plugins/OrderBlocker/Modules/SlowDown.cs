namespace O9K.Core.Plugins.OrderBlocker.Modules
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;

    using Entities.Abilities.Base;

    using Helpers;

    using Managers.Entity;

    internal class SlowDown
    {
        private const float AbilityDelay = 0.2f;

        private const float AbilityLowDelay = 0.05f;

        private readonly MultiSleeper abilitySleeper = new MultiSleeper();

        private readonly HashSet<AbilityId> lowDelayAbilities = new HashSet<AbilityId>
        {
            AbilityId.invoker_quas,
            AbilityId.invoker_wex,
            AbilityId.invoker_exort,
            AbilityId.invoker_invoke,
            AbilityId.item_power_treads,
            AbilityId.item_armlet,
        };

        private readonly OrderBlockerMenu menu;

        public SlowDown(OrderBlockerMenu menu)
        {
            this.menu = menu;
        }

        public bool ShouldBlock(ExecuteOrderEventArgs args)
        {
            if (!this.menu.SlowDown)
            {
                return false;
            }

            if (!args.Entities.Any())
            {
                return false;
            }

            switch (args.OrderId)
            {
                case OrderId.Ability:
                case OrderId.AbilityTarget:
                case OrderId.AbilityLocation:
                case OrderId.AbilityTargetRune:
                case OrderId.AbilityTargetTree:
                {
                    var handle = args.Entities.First().Handle;
                    if (this.abilitySleeper.IsSleeping(handle))
                    {
                        return true;
                    }

                    var ability = (ActiveAbility)EntityManager9.GetAbilityFast(args.Ability.Handle);
                    if (ability == null)
                    {
                        return false;
                    }

                    if (this.lowDelayAbilities.Contains(ability.Id))
                    {
                        this.abilitySleeper.Sleep(handle, ability.CastPoint + AbilityLowDelay);
                    }
                    else
                    {
                        this.abilitySleeper.Sleep(handle, ability.CastPoint + AbilityDelay);
                    }

                    return false;
                }
                case OrderId.Hold:
                case OrderId.Stop:
                    //case OrderId.MoveLocation:
                    //case OrderId.AttackTarget:
                {
                    var handle = args.Entities.First().Handle;
                    this.abilitySleeper.Reset(handle);

                    break;
                }
            }

            return false;
        }
    }
}