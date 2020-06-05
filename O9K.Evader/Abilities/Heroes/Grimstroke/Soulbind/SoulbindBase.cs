namespace O9K.Evader.Abilities.Heroes.Grimstroke.Soulbind
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.grimstroke_soul_chain)]
    internal class SoulbindBase : EvaderBaseAbility, IEvadable
    {
        public SoulbindBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SoulbindEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}