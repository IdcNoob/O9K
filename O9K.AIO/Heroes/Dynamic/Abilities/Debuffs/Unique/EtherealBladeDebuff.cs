namespace O9K.AIO.Heroes.Dynamic.Abilities.Debuffs.Unique
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    [AbilityId(AbilityId.item_ethereal_blade)]
    internal class EtherealBladeDebuff : OldDebuffAbility
    {
        public EtherealBladeDebuff(IDebuff ability)
            : base(ability)
        {
        }

        public override bool Use(Unit9 target)
        {
            if (!this.Ability.UseAbility(target))
            {
                return false;
            }

            this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.GetHitTime(target));
            this.AbilitySleeper.Sleep(this.Ability.Handle, this.Ability.GetHitTime(target) + 0.5f);

            return true;
        }
    }
}