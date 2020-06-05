namespace O9K.Core.Plugins.OrderBlocker.Modules
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;

    using Helpers;

    using SharpDX;

    internal class SpamBlock
    {
        private const float AbilityDelay = 0.1f;

        private const float AttackMoveDelay = 0.15f;

        private const float DefaultDelay = 0.05f;

        private const float DuplicateAttackMoveDelay = 0.3f;

        private const float RuneDelay = 0.3f;

        private readonly MultiSleeper abilitySleeper = new MultiSleeper();

        private readonly HashSet<AbilityId> ignoredSpamCheck = new HashSet<AbilityId>
        {
            AbilityId.invoker_quas,
            AbilityId.invoker_wex,
            AbilityId.invoker_exort,
            AbilityId.item_armlet,
            AbilityId.item_power_treads
        };

        private readonly OrderBlockerMenu menu;

        private readonly Sleeper orderSleeper = new Sleeper();

        private readonly MultiSleeper runeSleeper = new MultiSleeper();

        private readonly MultiSleeper unitAttackSleeper = new MultiSleeper();

        private readonly Dictionary<uint, (uint lastTarget, Sleeper sleeper)> unitAttackTargets = new Dictionary<uint, (uint, Sleeper)>();

        private readonly Dictionary<uint, (Vector3 lastPosition, Sleeper sleeper)> unitMovePositions =
            new Dictionary<uint, (Vector3, Sleeper)>();

        private readonly MultiSleeper unitMoveSleeper = new MultiSleeper();

        public SpamBlock(OrderBlockerMenu menu)
        {
            this.menu = menu;
        }

        public bool ShouldBlock(ExecuteOrderEventArgs args)
        {
            if (!this.menu.SpamBlock || !args.Entities.Any())
            {
                return false;
            }

            switch (args.OrderId)
            {
                case OrderId.MoveToDirection:
                case OrderId.MoveLocation:
                case OrderId.AttackLocation:
                {
                    if (this.orderSleeper)
                    {
                        return true;
                    }

                    var handle = args.Entities.First().Handle;

                    if (this.unitMoveSleeper.IsSleeping(handle))
                    {
                        return true;
                    }

                    var position = args.TargetPosition;

                    if (this.unitMovePositions.TryGetValue(handle, out var data) && data.sleeper && data.lastPosition == position)
                    {
                        return true;
                    }

                    this.unitMovePositions[handle] = (position, new Sleeper(DuplicateAttackMoveDelay));
                    this.unitAttackTargets.Remove(handle);

                    this.unitAttackSleeper.Remove(handle);

                    this.orderSleeper.Sleep(DefaultDelay);
                    this.unitMoveSleeper.Sleep(handle, AttackMoveDelay);
                    break;
                }
                case OrderId.AttackTarget:
                case OrderId.MoveTarget:
                {
                    if (this.orderSleeper)
                    {
                        return true;
                    }

                    var handle = args.Entities.First().Handle;

                    if (this.unitAttackSleeper.IsSleeping(handle))
                    {
                        return true;
                    }

                    var target = args.Target.Handle;

                    if (this.unitAttackTargets.TryGetValue(handle, out var data) && data.sleeper && data.lastTarget == target)
                    {
                        return true;
                    }

                    this.unitAttackTargets[handle] = (target, new Sleeper(DuplicateAttackMoveDelay));
                    this.unitMovePositions.Remove(handle);

                    this.unitMoveSleeper.Remove(handle);

                    this.orderSleeper.Sleep(DefaultDelay);
                    this.unitAttackSleeper.Sleep(handle, AttackMoveDelay);
                    break;
                }

                case OrderId.ConsumeRune:
                {
                    var handle = args.Entities.First().Handle;

                    if (this.runeSleeper.IsSleeping(handle))
                    {
                        return true;
                    }

                    this.runeSleeper.Sleep(handle, RuneDelay);
                    break;
                }

                case OrderId.Ability:
                case OrderId.AbilityTarget:
                case OrderId.AbilityLocation:
                case OrderId.AbilityTargetRune:
                case OrderId.AbilityTargetTree:
                case OrderId.ToggleAbility:
                {
                    var ability = args.Ability;

                    if (this.ignoredSpamCheck.Contains(ability.Id))
                    {
                        return false;
                    }

                    var handle = ability.Handle;

                    if (this.abilitySleeper.IsSleeping(handle))
                    {
                        return true;
                    }

                    this.unitAttackTargets.Remove(ability.Owner.Handle);
                    this.unitMovePositions.Remove(ability.Owner.Handle);

                    this.abilitySleeper.Sleep(handle, AbilityDelay);
                    break;
                }

                //case OrderId.DropItem:
                //case OrderId.PickItem:
            }

            return false;
        }
    }
}