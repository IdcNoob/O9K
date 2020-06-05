namespace O9K.Core.Entities.Abilities.Heroes.Tinker
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.tinker_march_of_the_machines)]
    public class MarchOfTheMachines : AreaOfEffectAbility
    {
        public MarchOfTheMachines(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }
    }
}