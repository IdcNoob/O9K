namespace O9K.Evader.Abilities.Heroes.TrollWarlord.BerserkersRage
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DodgeAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.troll_warlord_berserkers_rage)]
    internal class BerserkersRageBase : EvaderBaseAbility, IEvadable, IUsable<DodgeAbility>
    {
        public BerserkersRageBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BerserkersRageEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DodgeAbility GetUsableAbility()
        {
            return new DodgeAbility(this.Ability, this.Menu);
        }
    }
}