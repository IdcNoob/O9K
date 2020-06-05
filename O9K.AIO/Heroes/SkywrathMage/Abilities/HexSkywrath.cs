namespace O9K.AIO.Heroes.SkywrathMage.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;

    using TargetManager;

    internal class HexSkywrath : DisableAbility
    {
        public HexSkywrath(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            var target = targetManager.Target;

            if (target.IsRooted)
            {
                if (target.Abilities.Any(x => (x is IShield || x is IDisable || x is IBlink) && x.CanBeCasted(false)))
                {
                    return true;
                }

                return false;
            }

            return true;
        }
    }
}