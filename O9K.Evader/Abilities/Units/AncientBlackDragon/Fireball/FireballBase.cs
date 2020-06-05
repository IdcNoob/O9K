namespace O9K.Evader.Abilities.Units.AncientBlackDragon.Fireball
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.black_dragon_fireball)]
    internal class FireballBase : EvaderBaseAbility, IEvadable
    {
        public FireballBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FireballEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}