namespace O9K.Evader.Abilities.Heroes.Grimstroke.PhantomsEmbrace
{
    using System.Linq;

    using Base.Evadable;

    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;

    using Pathfinder.Obstacles.Abilities;

    internal class PhantomsEmbraceObstacle : AbilityObstacle
    {
        private readonly Modifier modifier;

        private readonly Unit modifierOwner;

        public PhantomsEmbraceObstacle(EvadableAbility ability, Modifier modifier, Unit ally)
            : base(ability)
        {
            this.modifier = modifier;
            this.modifierOwner = ally;
        }

        public override bool IsExpired
        {
            get
            {
                return !this.modifier.IsValid;
            }
        }

        public override void Draw()
        {
            this.Drawer.DrawCircle(this.modifierOwner.Position, 100);
            this.Drawer.UpdateCirclePosition(this.modifierOwner.Position);
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return 0;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            var inkCreature = EntityManager9.Units.Where(x => x.IsMagicImmune && x.IsAlive && x.Name == "npc_dota_grimstroke_ink_creature")
                .OrderBy(x => x.GetAngle(this.modifierOwner.Position))
                .FirstOrDefault();

            if (inkCreature == null)
            {
                return 0;
            }

            return (inkCreature.Distance(this.modifierOwner.Position) - 200) / this.EvadableAbility.ActiveAbility.Speed;
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (unit.Handle != this.modifierOwner.Handle)
            {
                return false;
            }

            if (unit.HasModifier("modifier_grimstroke_ink_creature_debuff"))
            {
                return false;
            }

            return true;
        }
    }
}