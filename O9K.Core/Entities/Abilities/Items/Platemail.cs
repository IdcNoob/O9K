﻿namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_platemail)]
    public class Platemail : PassiveAbility
    {
        public Platemail(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}