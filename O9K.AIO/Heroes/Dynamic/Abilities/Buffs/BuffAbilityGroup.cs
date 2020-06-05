namespace O9K.AIO.Heroes.Dynamic.Abilities.Buffs
{
    using System.Linq;

    using AIO.Modes.Combo;

    using Base;

    using Blinks;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;

    using Ensage;

    internal class BuffAbilityGroup : OldAbilityGroup<IBuff, OldBuffAbility>
    {
        public BuffAbilityGroup(BaseHero baseHero)
            : base(baseHero)
        {
        }

        public BlinkAbilityGroup Blinks { get; set; }

        public override bool Use(Unit9 target, ComboModeMenu menu, params AbilityId[] except)
        {
            foreach (var ability in this.Abilities)
            {
                if (!ability.Ability.IsValid)
                {
                    continue;
                }

                if (except.Contains(ability.Ability.Id))
                {
                    continue;
                }

                if (!ability.CanBeCasted(ability.Buff.Owner, target, menu, this.Blinks))
                {
                    continue;
                }

                if (ability.Use(ability.Buff.Owner))
                {
                    return true;
                }
            }

            return false;
        }
    }
}