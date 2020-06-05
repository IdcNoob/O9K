namespace O9K.AIO.Heroes.Dynamic.Abilities.Buffs.Unique
{
    using AIO.Modes.Combo;

    using Blinks;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    [AbilityId(AbilityId.item_necronomicon)]
    [AbilityId(AbilityId.item_necronomicon_2)]
    [AbilityId(AbilityId.item_necronomicon_3)]
    internal class NecroBuffAbility : OldBuffAbility
    {
        public NecroBuffAbility(IBuff ability)
            : base(ability)
        {
        }

        protected override bool ShouldCastBuff(Unit9 target, BlinkAbilityGroup blinks, ComboModeMenu menu)
        {
            var distance = this.Ability.Owner.Distance(target);

            if (distance > 300)
            {
                return false;
            }

            return true;
        }
    }
}