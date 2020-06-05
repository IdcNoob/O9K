namespace O9K.AIO.Heroes.TemplarAssassin.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Entity;

    using Ensage;

    using TargetManager;

    internal class PsionicTrap : DebuffAbility
    {
        public PsionicTrap(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            if (EntityManager9.Units.Any(
                x => x.IsAlly(this.Owner) && (x.IsAlive || x.DeathTime + 0.5f > Game.RawGameTime)
                                          && x.Name == "npc_dota_templar_assassin_psionic_trap"
                                          && x.Distance(targetManager.Target) < this.Ability.Radius))
            {
                return false;
            }

            return true;
        }
    }
}