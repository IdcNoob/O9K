namespace O9K.ItemManager.Modules.Snatcher.Controllables.Abilities
{
    using Core.Data;
    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;

    using SharpDX;

    internal class SleightOfFist : ControllableAbility
    {
        public SleightOfFist(ActiveAbility ability)
            : base(ability)
        {
        }

        public override void UseAbility(Unit9 roshan)
        {
            Vector3 position;

            if (roshan?.IsVisible == true)
            {
                if (this.Ability.GetDamage(roshan) / 2f < roshan.Health)
                {
                    return;
                }

                position = roshan.Position.Extend2D(this.Ability.Owner.Position, this.Ability.Radius - 100);
            }
            else
            {
                position = GameData.RoshanPosition.Extend2D(this.Ability.Owner.Position, this.Ability.Radius - 100);
            }

            if (this.Ability.Owner.Distance(position) > this.Ability.CastRange)
            {
                return;
            }

            this.Ability.UseAbility(position);
            this.Sleeper.Sleep(0.5f);
        }
    }
}