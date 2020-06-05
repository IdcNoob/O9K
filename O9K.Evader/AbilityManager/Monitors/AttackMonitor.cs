namespace O9K.Evader.AbilityManager.Monitors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Abilities.Base.Evadable;
    using Abilities.Base.Evadable.Components;

    using Core.Entities.Heroes;
    using Core.Entities.Units;
    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;

    internal class AttackMonitor : IDisposable
    {
        private readonly List<EvadableAbility> evadableAbilities;

        private readonly Team myTeam;

        public AttackMonitor(List<EvadableAbility> evadableAbilities)
        {
            this.myTeam = EntityManager9.Owner.Team;
            this.evadableAbilities = evadableAbilities;

            EntityManager9.UnitMonitor.AttackStart += this.OnAttackStart;
            EntityManager9.UnitMonitor.AttackEnd += this.OnAttackEnd;
        }

        public void Dispose()
        {
            EntityManager9.UnitMonitor.AttackStart -= this.OnAttackStart;
            EntityManager9.UnitMonitor.AttackEnd -= this.OnAttackEnd;
        }

        private void OnAttackEnd(Unit9 unit)
        {
            try
            {
                var hero = unit as Hero9;
                if (hero == null || hero.Team == this.myTeam)
                {
                    return;
                }

                foreach (var ability in this.evadableAbilities.Where(x => x.Owner.Equals(hero)).OfType<IAutoAttack>())
                {
                    ability.AttackEnd();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAttackStart(Unit9 unit)
        {
            try
            {
                var hero = unit as Hero9;
                if (hero == null || hero.Team == this.myTeam)
                {
                    return;
                }

                foreach (var ability in this.evadableAbilities.Where(x => x.Owner.Equals(hero)).OfType<IAutoAttack>())
                {
                    ability.AttackStart();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}