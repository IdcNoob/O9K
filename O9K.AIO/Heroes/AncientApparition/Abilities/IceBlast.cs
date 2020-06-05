namespace O9K.AIO.Heroes.AncientApparition.Abilities
{
    using System.Linq;

    using AIO.Abilities;
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage;
    using Ensage.SDK.Geometry;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    internal class IceBlast : AoeAbility
    {
        private readonly Core.Entities.Abilities.Heroes.AncientApparition.IceBlast iceBlast;

        private Vector3 direction;

        public IceBlast(ActiveAbility ability)
            : base(ability)
        {
            this.iceBlast = (Core.Entities.Abilities.Heroes.AncientApparition.IceBlast)ability;
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            var menu = comboMenu.GetAbilitySettingsMenu<IceBlastMenu>(this);
            if (menu.StunOnly && targetManager.Target.GetImmobilityDuration() < 1.5f)
            {
                return false;
            }

            return base.CanHit(targetManager, comboMenu);
        }

        public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
        {
            return new IceBlastMenu(this.Ability, simplifiedName);
        }

        public bool Release(TargetManager targetManager, Sleeper comboSleeper)
        {
            if (this.iceBlast.IsUsable)
            {
                return false;
            }

            var iceBlastUnit = ObjectManager.GetEntitiesFast<Unit>()
                .FirstOrDefault(
                    x => x.IsValid && x.NetworkName == "CDOTA_BaseNPC" && x.DayVision == 550 && x.Health == 150
                         && x.Team == this.Owner.Team);

            if (iceBlastUnit == null)
            {
                return true;
            }

            var currentPosition = iceBlastUnit.Position;
            var nextPosition = iceBlastUnit.Position.Extend2D(this.direction, 50);
            var targetPosition = targetManager.Target.GetPredictedPosition(this.iceBlast.GetReleaseFlyTime(iceBlastUnit.Position));

            if (currentPosition.Distance2D(targetPosition) > nextPosition.Distance2D(targetPosition))
            {
                return false;
            }

            if (!this.Ability.UseAbility())
            {
                return false;
            }

            this.Sleeper.Sleep(0.3f);
            this.OrbwalkSleeper.Sleep(0.1f);
            comboSleeper.Sleep(0.1f);
            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.iceBlast.IsUsable)
            {
                return false;
            }

            var input = this.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
            input.Delay += this.iceBlast.GetReleaseFlyTime(targetManager.Target.Position);
            var output = this.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low)
            {
                return false;
            }

            if (!this.Ability.UseAbility(output.CastPosition))
            {
                return false;
            }

            this.direction = this.Owner.Position.Extend2D(output.CastPosition, 9999);

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.1f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        protected override bool ChainStun(Unit9 target, bool invulnerability)
        {
            var immobile = invulnerability ? target.GetInvulnerabilityDuration() : target.GetImmobilityDuration();
            if (immobile <= 0)
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(target) + this.iceBlast.GetReleaseFlyTime(target.Position);
            if (target.IsInvulnerable)
            {
                hitTime -= 0.1f;
            }

            return hitTime > immobile;
        }
    }
}