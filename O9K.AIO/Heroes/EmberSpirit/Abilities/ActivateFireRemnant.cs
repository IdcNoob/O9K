namespace O9K.AIO.Heroes.EmberSpirit.Abilities
{
    using System.Linq;

    using AIO.Abilities;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Managers.Entity;

    using TargetManager;

    internal class ActivateFireRemnant : BlinkAbility
    {
        public ActivateFireRemnant(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            var remnants = EntityManager9.Units.Any(
                x => x.IsAlly(this.Owner) && x.Name == "npc_dota_ember_spirit_remnant"
                                          && x.Distance(
                                              targetManager.Target.GetPredictedPosition(this.Owner.Distance(x) / this.Ability.Speed))
                                          < this.Ability.Radius);

            return remnants;
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            if (!EntityManager9.Units.Any(
                    x => x.IsAlly(this.Owner) && x.Name == "npc_dota_ember_spirit_remnant" && x.Distance(targetManager.Target) < 800))
            {
                return false;
            }

            return base.ForceUseAbility(targetManager, comboSleeper);
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (this.Owner.IsInvulnerable)
            {
                return false;
            }

            return true;
        }
    }
}