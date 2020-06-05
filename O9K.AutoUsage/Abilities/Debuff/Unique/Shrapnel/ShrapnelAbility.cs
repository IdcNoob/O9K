namespace O9K.AutoUsage.Abilities.Debuff.Unique.Shrapnel
{
    using System.Collections.Generic;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using Settings;

    [AbilityId(AbilityId.sniper_shrapnel)]
    internal class ShrapnelAbility : DebuffAbility
    {
        private readonly Sleeper sleeper = new Sleeper();

        public ShrapnelAbility(IDebuff debuff, GroupSettings settings)
            : base(debuff, settings)
        {
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            if (this.sleeper.IsSleeping)
            {
                return false;
            }

            if (base.UseAbility(heroes))
            {
                this.sleeper.Sleep(2);
                return true;
            }

            return false;
        }
    }
}