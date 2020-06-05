namespace O9K.Evader.Abilities.Heroes.Clockwerk.Hookshot
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.rattletrap_hookshot)]
    internal class HookshotBase : EvaderBaseAbility, IEvadable
    {
        public HookshotBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new HookshotEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}