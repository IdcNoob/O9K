namespace O9K.AIO.Heroes.Pangolier.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using Modes.Combo;

    using TargetManager;

    internal class ShieldCrash : NukeAbility
    {
        public ShieldCrash(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
        {
            if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
            {
                return false;
            }

            var target = targetManager.Target;

            if (this.Owner.HasModifier("modifier_pangolier_gyroshell") && (this.Owner.GetAngle(target.Position) < 0.75f)
                & (this.Owner.Distance(target) < this.Ability.CastRange))
            {
                return false;
            }

            return true;
        }
    }
}