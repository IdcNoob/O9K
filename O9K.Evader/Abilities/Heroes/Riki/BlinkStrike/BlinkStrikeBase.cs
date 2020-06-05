namespace O9K.Evader.Abilities.Heroes.Riki.BlinkStrike
{
    using Base;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.riki_blink_strike)]
    internal class BlinkStrikeBase : EvaderBaseAbility, IUsable<BlinkAbility>
    {
        public BlinkStrikeBase(Ability9 ability)
            : base(ability)
        {
            //todo add disable ?
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkTargetableAbility(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}