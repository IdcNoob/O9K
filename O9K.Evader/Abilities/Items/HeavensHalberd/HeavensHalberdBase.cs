namespace O9K.Evader.Abilities.Items.HeavensHalberd
{
    using Base;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_heavens_halberd)]
    internal class HeavensHalberdBase : EvaderBaseAbility, IUsable<DisableAbility>
    {
        public HeavensHalberdBase(Ability9 ability)
            : base(ability)
        {
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}