namespace O9K.Evader.Abilities.Heroes.Windranger.FocusFire
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.windrunner_focusfire)]
    internal class FocusFireBase : EvaderBaseAbility, IEvadable
    {
        public FocusFireBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FocusFireEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}