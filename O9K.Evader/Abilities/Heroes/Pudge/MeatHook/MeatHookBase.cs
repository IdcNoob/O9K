namespace O9K.Evader.Abilities.Heroes.Pudge.MeatHook
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.pudge_meat_hook)]
    internal class MeatHookBase : EvaderBaseAbility, IEvadable
    {
        public MeatHookBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MeatHookEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}