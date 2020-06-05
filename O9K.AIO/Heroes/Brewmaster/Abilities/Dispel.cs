namespace O9K.AIO.Heroes.Brewmaster.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class Dispel : AoeAbility
    {
        public Dispel(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if (target.IsMagicImmune && !this.Ability.PiercesMagicImmunity(target))
            {
                return false;
            }

            //if (target.BaseModifiers.Any(x => x.IsPurgable && !x.IsHidden && !x.IsAura && !x.IsDebuff))
            //{
            //    return true;
            //}

            if (!target.HasModifier("modifier_brewmaster_storm_cyclone"))
            {
                return false;
            }

            var spirits = targetManager.AllyUnits.Where(x => !x.Equals(this.Owner) && x.Name.Contains("npc_dota_brewmaster")).ToList();
            if (spirits.Count == 0)
            {
                return false;
            }

            if (spirits.All(x => x.Distance(targetManager.Target) < 400))
            {
                return true;
            }

            return false;
        }
    }
}