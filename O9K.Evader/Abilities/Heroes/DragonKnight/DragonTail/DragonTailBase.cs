namespace O9K.Evader.Abilities.Heroes.DragonKnight.DragonTail
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dragon_knight_dragon_tail)]
    internal class DragonTailBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public DragonTailBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DragonTailEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}