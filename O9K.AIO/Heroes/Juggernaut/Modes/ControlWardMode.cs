namespace O9K.AIO.Heroes.Juggernaut.Modes
{
    using System;
    using System.Linq;

    using AIO.Modes.Permanent;

    using Base;

    using Core.Entities.Abilities.Heroes.Juggernaut;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    using Ensage;

    internal class ControlWardMode : PermanentMode
    {
        private readonly Sleeper sleeper = new Sleeper();

        private Unit9 ward;

        public ControlWardMode(BaseHero baseHero, PermanentModeMenu menu)
            : base(baseHero, menu)
        {
        }

        public override void Disable()
        {
            base.Disable();
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
        }

        public override void Dispose()
        {
            base.Dispose();
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
        }

        public override void Enable()
        {
            base.Enable();
            EntityManager9.UnitAdded += this.OnUnitAdded;
            EntityManager9.UnitRemoved += this.OnUnitRemoved;
        }

        protected override void Execute()
        {
            if (this.sleeper.IsSleeping)
            {
                return;
            }

            if (this.ward?.IsValid != true || !this.ward.IsAlive)
            {
                return;
            }

            var fountain = this.TargetManager.Fountain;
            var juggernaut = this.ward.Owner;
            var wardAbility = (HealingWard)juggernaut.Abilities.First(x => x.Id == AbilityId.juggernaut_healing_ward);
            var allies = this.TargetManager.AllyHeroes.Where(x => x.HealthPercentage < 90 || x.IsMyHero)
                .OrderBy(x => x.HealthPercentage)
                .ToList();
            var mainTarget = juggernaut.HealthPercentage < 80 && juggernaut.IsAlive ? juggernaut : allies.FirstOrDefault();

            if (mainTarget?.IsAlive != true)
            {
                return;
            }

            var input = wardAbility.GetPredictionInput(mainTarget, allies);
            input.CastRange = 2000;
            var output = wardAbility.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low)
            {
                return;
            }

            var movePosition = output.CastPosition;

            if (output.AoeTargetsHit.Count == 1)
            {
                //  movePosition = output.TargetPosition.Extend2D(fountain, 200);
                movePosition = output.TargetPosition;
            }

            if (!mainTarget.Equals(juggernaut) || juggernaut.HealthPercentage > 50)
            {
                foreach (var enemy in this.TargetManager.EnemyHeroes.OrderByDescending(x => x.GetAttackRange()))
                {
                    var attackRange = enemy.GetAttackRange() * (enemy.IsRanged ? 2f : 3f);
                    var distance = enemy.Distance(movePosition);

                    if (distance > attackRange)
                    {
                        continue;
                    }

                    movePosition = mainTarget.InFront(5, 0, false);

                    //if (this.ward.Distance(enemy) - 100 < this.ward.Distance(mainTarget))
                    //{
                    //    movePosition = enemy.InFront(attackRange + enemy.Distance(mainTarget));
                    //}
                    //else
                    //{
                    //    movePosition = enemy.Position.Extend2D(mainTarget.Position, attackRange);
                    //}

                    break;
                }
            }

            if (this.ward.Position == movePosition)
            {
                return;
            }

            this.ward.BaseUnit.Move(movePosition);
            this.sleeper.Sleep(0.15f);
        }

        private void OnUnitAdded(Unit9 entity)
        {
            try
            {
                if (!entity.IsControllable || !entity.IsAlly(this.Owner) || entity.Name != "npc_dota_juggernaut_healing_ward")
                {
                    return;
                }

                this.ward = entity;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitRemoved(Unit9 entity)
        {
            try
            {
                if (entity != this.ward)
                {
                    return;
                }

                this.ward = null;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}