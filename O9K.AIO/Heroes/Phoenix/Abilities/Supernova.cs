namespace O9K.AIO.Heroes.Phoenix.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using Ensage;

    using TargetManager;

    internal class Supernova : NukeAbility
    {
        public Supernova(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var mouse = Game.MousePosition;
            var ally = targetManager.AllyHeroes
                           .Where(x => !x.Equals(this.Owner) && x.HealthPercentage < 35 && x.Distance(this.Owner) < this.Ability.CastRange)
                           .OrderBy(x => x.Distance(mouse))
                           .FirstOrDefault() ?? this.Owner;

            if (!this.Ability.UseAbility(ally))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}