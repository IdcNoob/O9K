namespace O9K.Evader.Abilities.Heroes.Rubick.SpellSteal
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.rubick_spell_steal)]
    internal class SpellStealBase : EvaderBaseAbility, IEvadable
    {
        public SpellStealBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SpellStealEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}