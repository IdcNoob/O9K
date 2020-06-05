namespace O9K.Evader.Abilities.Heroes.AntiMage.ManaVoid
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.antimage_mana_void)]
    internal class ManaVoidBase : EvaderBaseAbility, IEvadable
    {
        public ManaVoidBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ManaVoidEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}