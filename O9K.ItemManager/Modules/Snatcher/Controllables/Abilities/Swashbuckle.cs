namespace O9K.ItemManager.Modules.Snatcher.Controllables.Abilities
{
    using Core.Data;
    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;

    using SharpDX;

    internal class Swashbuckle : ControllableAbility
    {
        private readonly Vector3 direction = new Vector3(-2179, 1649, 159);

        private readonly Core.Entities.Abilities.Heroes.Pangolier.Swashbuckle swashbuckle;

        public Swashbuckle(ActiveAbility ability)
            : base(ability)
        {
            this.swashbuckle = (Core.Entities.Abilities.Heroes.Pangolier.Swashbuckle)ability;
        }

        public override void UseAbility(Unit9 roshan)
        {
            if (roshan?.IsVisible == true)
            {
                if (this.Ability.Owner.Distance(roshan.Position) > this.Ability.CastRange)
                {
                    return;
                }

                if (this.Ability.GetDamage(roshan) / 2f < roshan.Health)
                {
                    return;
                }

                this.swashbuckle.UseAbility(roshan.Position.Extend2D(this.direction, -100), this.direction);
            }
            else
            {
                if (this.Ability.Owner.Distance(GameData.RoshanPosition) > this.Ability.CastRange)
                {
                    return;
                }

                this.swashbuckle.UseAbility(GameData.RoshanPosition, this.direction);
            }

            this.Sleeper.Sleep(0.5f);
        }
    }
}