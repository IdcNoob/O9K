namespace O9K.AIO.Heroes.Phoenix.Abilities
{
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Abilities;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage;

    using SharpDX;

    using TargetManager;

    using BaseIcarusDive = Core.Entities.Abilities.Heroes.Phoenix.IcarusDive;
    using BaseSunRay = Core.Entities.Abilities.Heroes.Phoenix.SunRay;

    internal class IcarusDive : NukeAbility
    {
        private readonly BaseIcarusDive icarusDive;

        private Vector3 startPosition;

        public IcarusDive(ActiveAbility ability)
            : base(ability)
        {
            this.icarusDive = (BaseIcarusDive)ability;
        }

        public Vector3 CastPosition { get; private set; }

        public bool IsFlying
        {
            get
            {
                return this.icarusDive.IsFlying;
            }
        }

        public bool AutoStop(TargetManager targetManager)
        {
            if (!this.icarusDive.IsFlying || this.CastPosition != Vector3.Zero)
            {
                return false;
            }

            var modifier = this.Owner.GetModifier("modifier_phoenix_icarus_dive");
            if (modifier == null)
            {
                return false;
            }

            if (targetManager != null)
            {
                var elapsed = modifier.ElapsedTime;
                if (elapsed < 1)
                {
                    return false;
                }

                var distance = this.Owner.Distance(targetManager.Target);

                if (targetManager.Target.Distance(this.startPosition) > this.Ability.CastRange - 200 && distance > 300)
                {
                    if (this.Ability.UseAbility())
                    {
                        return true;
                    }
                }

                if (distance > 300 && distance < 500)
                {
                    if (this.Ability.UseAbility())
                    {
                        return true;
                    }
                }
            }
            else if (modifier.ElapsedTime > 1)
            {
                if (this.Ability.UseAbility())
                {
                    return true;
                }
            }

            return false;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (this.CastPosition == Vector3.Zero && this.Owner.Distance(targetManager.Target) < 600)
            {
                return false;
            }

            return true;
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var nova = usableAbilities.Find(x => x.Ability.Id == AbilityId.phoenix_supernova);
            if (nova != null && this.Owner.HasAghanimsScepter)
            {
                var mouse = Game.MousePosition;
                var ally = targetManager.AllyHeroes
                    .Where(x => !x.Equals(this.Owner) && x.HealthPercentage < 35 && x.Distance(this.Owner) < this.Ability.CastRange)
                    .OrderBy(x => x.Distance(mouse))
                    .FirstOrDefault();

                if (ally != null)
                {
                    this.CastPosition = ally.Position;
                    return true;
                }
            }

            this.CastPosition = Vector3.Zero;

            var ray = this.Owner.Abilities.FirstOrDefault(x => x.Id == AbilityId.phoenix_sun_ray) as BaseSunRay;
            if (ray?.IsSunRayActive == true)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            this.startPosition = this.Owner.Position;

            if (!this.CastPosition.IsZero)
            {
                if (!this.Ability.UseAbility(this.CastPosition))
                {
                    return false;
                }
            }
            else if (!this.Ability.UseAbility(targetManager.Target, targetManager.EnemyHeroes, HitChance.Low))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            if (this.Ability is IDisable disable)
            {
                targetManager.Target.SetExpectedUnitState(disable.AppliesUnitState, this.Ability.GetHitTime(targetManager.Target));
            }

            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}