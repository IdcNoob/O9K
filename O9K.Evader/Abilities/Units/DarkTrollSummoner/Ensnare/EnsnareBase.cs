﻿namespace O9K.Evader.Abilities.Units.DarkTrollSummoner.Ensnare
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dark_troll_warlord_ensnare)]
    internal class EnsnareBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public EnsnareBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EnsnareEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}