namespace O9K.AIO.Heroes.DarkSeer.Abilities
{
    using AIO.Abilities;
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    internal class Vacuum : DisableAbility
    {
        public Vacuum(ActiveAbility ability)
            : base(ability)
        {
        }

        public Vector3 CastPosition { get; protected set; }

        public float CastTime { get; protected set; }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            if (!base.CanHit(targetManager, comboMenu))
            {
                return false;
            }

            if (!this.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu)))
            {
                return false;
            }

            return true;
        }

        public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
        {
            return new UsableAbilityHitCountMenu(this.Ability, simplifiedName);
        }

        public int TargetsToHit(IComboModeMenu comboMenu)
        {
            var menu = comboMenu.GetAbilitySettingsMenu<UsableAbilityHitCountMenu>(this);
            return menu.HitCount;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var input = this.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
            var output = this.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low)
            {
                return false;
            }

            if (!this.Ability.UseAbility(output.CastPosition))
            {
                return false;
            }

            this.CastPosition = output.CastPosition;
            this.CastTime = Game.RawGameTime;

            var hitTime = this.Ability.GetHitTime(targetManager.Target) + 0.5f;
            var delay = this.Ability.GetCastDelay(targetManager.Target);

            targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            comboSleeper.Sleep(delay);
            this.OrbwalkSleeper.Sleep(delay);
            this.Sleeper.Sleep(hitTime);

            return true;
        }
    }
}