namespace O9K.Evader.Abilities.Items.PhaseBoots
{
    using Base;
    using Base.Usable.DodgeAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_phase_boots)]
    internal class PhaseBootsBase : EvaderBaseAbility, IUsable<DodgeAbility>
    {
        public PhaseBootsBase(Ability9 ability)
            : base(ability)
        {
        }

        public DodgeAbility GetUsableAbility()
        {
            return new DodgeAbility(this.Ability, this.Menu);
        }
    }
}