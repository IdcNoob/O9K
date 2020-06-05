namespace O9K.Core.Entities.Abilities.Heroes.Undying
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.undying_tombstone)]
    public class Tombstone : CircleAbility, IDebuff
    {
        public Tombstone(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string DebuffModifierName { get; set; }
    }
}