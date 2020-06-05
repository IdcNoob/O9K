namespace O9K.Evader.Abilities.Heroes.KeeperOfTheLight.WilloWisp
{
    using Base;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.keeper_of_the_light_will_o_wisp)]
    internal class WilloWispBase : EvaderBaseAbility, IUsable<DisableAbility>
    {
        public WilloWispBase(Ability9 ability)
            : base(ability)
        {
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}