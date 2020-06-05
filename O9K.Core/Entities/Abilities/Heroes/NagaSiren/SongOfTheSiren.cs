namespace O9K.Core.Entities.Abilities.Heroes.NagaSiren
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.naga_siren_song_of_the_siren)]
    public class SongOfTheSiren : AreaOfEffectAbility, IShield
    {
        public SongOfTheSiren(Ability baseAbility)
            : base(baseAbility)
        {
            //todo add to immobile ?
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = 0;

        public string ShieldModifierName { get; } = "modifier_naga_siren_song_of_the_siren_aura";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;
    }
}