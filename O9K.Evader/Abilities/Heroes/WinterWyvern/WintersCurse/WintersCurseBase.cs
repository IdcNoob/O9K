namespace O9K.Evader.Abilities.Heroes.WinterWyvern.WintersCurse
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.winter_wyvern_winters_curse)]
    internal class WintersCurseBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public WintersCurseBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new WintersCurseEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}