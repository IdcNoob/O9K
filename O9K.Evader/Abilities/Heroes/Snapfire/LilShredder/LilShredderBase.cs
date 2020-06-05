namespace O9K.Evader.Abilities.Heroes.Snapfire.LilShredder
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.snapfire_lil_shredder)]
    internal class LilShredderBase : EvaderBaseAbility, IEvadable
    {
        public LilShredderBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new LilShredderEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}