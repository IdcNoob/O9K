namespace O9K.Evader.Abilities.Heroes.MonkeyKing.JinguMastery
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.monkey_king_jingu_mastery)]
    internal class JinguMasteryBase : EvaderBaseAbility, IEvadable
    {
        public JinguMasteryBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new JinguMasteryEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}