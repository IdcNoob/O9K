namespace O9K.Evader.Abilities.Heroes.Doom.ScorchedEarth
{
    using Base;
    using Base.Usable.DodgeAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.doom_bringer_scorched_earth)]
    internal class ScorchedEarthBase : EvaderBaseAbility, IUsable<DodgeAbility>
    {
        public ScorchedEarthBase(Ability9 ability)
            : base(ability)
        {
        }

        DodgeAbility IUsable<DodgeAbility>.GetUsableAbility()
        {
            return new DodgeAbility(this.Ability, this.Menu);
        }
    }
}