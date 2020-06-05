namespace O9K.Evader.Abilities.Heroes.SandKing.Epicenter
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.sandking_epicenter)]
    internal class EpicenterBase : EvaderBaseAbility //, IEvadable
    {
        public EpicenterBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            //todo fix
            return new EpicenterEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}