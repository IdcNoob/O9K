namespace O9K.AIO.Heroes.Brewmaster.Abilities
{
    using System.Linq;

    using AIO.Abilities.Items;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class Cyclone : EulsScepterOfDivinity
    {
        public Cyclone(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            var spirits = targetManager.AllyUnits.Where(x => !x.Equals(this.Owner) && x.Name.Contains("npc_dota_brewmaster")).ToList();
            if (spirits.Count == 0)
            {
                return true;
            }

            if (spirits.Any(x => x.Distance(targetManager.Target) > 800))
            {
                return true;
            }

            return false;
        }
    }
}