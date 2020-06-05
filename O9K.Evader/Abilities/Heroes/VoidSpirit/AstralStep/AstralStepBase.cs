namespace O9K.Evader.Abilities.Heroes.VoidSpirit.AstralStep
{
    using Base;
    using Base.Evadable;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.void_spirit_astral_step)]
    internal class AstralStepBase : EvaderBaseAbility, IEvadable, IUsable<BlinkAbility>
    {
        public AstralStepBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new AstralStepEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkAbility(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}