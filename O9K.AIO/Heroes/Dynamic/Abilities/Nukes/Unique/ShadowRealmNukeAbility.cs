namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes.Unique
{
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Abilities.Heroes.DarkWillow;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    [AbilityId(AbilityId.dark_willow_shadow_realm)]
    internal class ShadowRealmNukeAbility : OldNukeAbility
    {
        private readonly ShadowRealm shadowRealm;

        public ShadowRealmNukeAbility(INuke ability)
            : base(ability)
        {
            this.shadowRealm = (ShadowRealm)ability;
        }

        public override bool CanBeCasted(ComboModeMenu menu)
        {
            if (menu.IsAbilityEnabled(this.Ability))
            {
                return false;
            }

            if (this.AbilitySleeper.IsSleeping(this.Ability.Handle) || this.OrbwalkSleeper.IsSleeping(this.Ability.Owner.Handle))
            {
                return false;
            }

            if (this.shadowRealm.Casted)
            {
                return this.Ability.Owner.CanAttack();
            }

            return this.Ability.CanBeCasted();
        }

        public override bool ShouldCast(Unit9 target)
        {
            if (this.Ability.UnitTargetCast && !target.IsVisible)
            {
                return false;
            }

            if (!this.shadowRealm.Casted)
            {
                return true;
            }

            if (target.IsReflectingDamage)
            {
                return false;
            }

            if (target.IsInvulnerable)
            {
                return false;
            }

            var damage = this.Nuke.GetDamage(target);

            if (damage <= 0)
            {
                return false;
            }

            if (!this.shadowRealm.DamageMaxed && damage < target.Health)
            {
                return false;
            }

            return true;
        }

        public override bool Use(Unit9 target)
        {
            if (this.shadowRealm.Casted)
            {
                if (!this.Ability.Owner.BaseUnit.Attack(target.BaseUnit))
                {
                    return false;
                }

                this.AbilitySleeper.Sleep(this.Ability.Handle, this.Ability.GetHitTime(target) + 0.5f);
                this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.Owner.GetAttackPoint());

                return true;
            }

            if (!this.Ability.UseAbility())
            {
                return false;
            }

            this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.GetCastDelay(this.Ability.Owner));
            this.AbilitySleeper.Sleep(this.Ability.Handle, this.Ability.GetHitTime(this.Ability.Owner) + 0.5f);

            return true;
        }
    }
}