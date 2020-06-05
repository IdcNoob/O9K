namespace O9K.Evader.Abilities.Heroes.Alchemist.UnstableConcoctionThrow
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.alchemist_unstable_concoction_throw)]
    internal class UnstableConcoctionThrowBase : EvaderBaseAbility, IEvadable, IAllyEvadable
    {
        public UnstableConcoctionThrowBase(Ability9 ability)
            : base(ability)
        {
        }

        EvadableAbility IAllyEvadable.GetEvadableAbility()
        {
            return new UnstableConcoctionThrowAllyEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        EvadableAbility IEvadable.GetEvadableAbility()
        {
            return new UnstableConcoctionThrowEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}