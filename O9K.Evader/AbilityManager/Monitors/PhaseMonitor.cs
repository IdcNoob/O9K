namespace O9K.Evader.AbilityManager.Monitors
{
    using System;
    using System.Collections.Generic;

    using Abilities.Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Logger;
    using Core.Managers.Entity;

    internal class PhaseMonitor : IDisposable
    {
        private readonly List<EvadableAbility> evadableAbilities;

        public PhaseMonitor(List<EvadableAbility> evadableAbilities)
        {
            this.evadableAbilities = evadableAbilities;
            EntityManager9.AbilityMonitor.AbilityCastChange += this.OnAbilityCastChange;
        }

        public void Dispose()
        {
            EntityManager9.AbilityMonitor.AbilityCastChange -= this.OnAbilityCastChange;
        }

        private void OnAbilityCastChange(Ability9 ability)
        {
            try
            {
                var evadableAbility = this.evadableAbilities.Find(x => x.Ability.Handle == ability.Handle);
                if (evadableAbility?.Owner.IsVisible != true)
                {
                    return;
                }

                if (ability.IsCasting)
                {
                    evadableAbility.PhaseStart();
                }
                else if (evadableAbility.TimeSinceCasted < 0)
                {
                    evadableAbility.PhaseCancel();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}