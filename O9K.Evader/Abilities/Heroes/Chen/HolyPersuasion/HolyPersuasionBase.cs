namespace O9K.Evader.Abilities.Heroes.Chen.HolyPersuasion
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.chen_holy_persuasion)]
    internal class HolyPersuasionBase : EvaderBaseAbility, IEvadable
    {
        public HolyPersuasionBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new HolyPersuasionEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}