namespace O9K.Evader.Abilities.Heroes.Pugna.NetherWard
{
    using System;

    using Base;

    using Core.Entities.Abilities.Heroes.Pugna;
    using Core.Entities.Units;
    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;

    using Pathfinder.Obstacles.Modifiers;

    internal class NetherWardModifierObstacle : ModifierAllyObstacle, IDisposable
    {
        private readonly NetherWard netherWard;

        public NetherWardModifierObstacle(IModifierCounter ability, Modifier modifier, Unit9 modifierOwner)
            : base(ability, modifier, modifierOwner)
        {
            if (!modifierOwner.IsControllable)
            {
                return;
            }

            this.netherWard = (NetherWard)this.EvadableAbility.Ability;
            Player.OnExecuteOrder += this.OnExecuteOrder;
        }

        public void Dispose()
        {
            Player.OnExecuteOrder -= this.OnExecuteOrder;
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (!args.Process)
                {
                    return;
                }

                var order = args.OrderId;
                if (order != OrderId.Ability && order != OrderId.AbilityLocation && order != OrderId.AbilityTarget)
                {
                    return;
                }

                var ability = EntityManager9.GetAbility(args.Ability.Handle);
                if (ability == null)
                {
                    return;
                }

                var owner = ability.Owner;
                if (!owner.Equals(this.ModifierOwner))
                {
                    return;
                }

                var damage = this.netherWard.GetDamage(owner, ability.ManaCost);
                if (damage <= 0)
                {
                    return;
                }

                if (damage > 300 || owner.Health - damage <= 0)
                {
                    args.Process = false;
                }
            }
            catch (Exception e)
            {
                Player.OnExecuteOrder -= this.OnExecuteOrder;
                Logger.Error(e);
            }
        }
    }
}