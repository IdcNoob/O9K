namespace O9K.Evader.Abilities.Items.Radiance
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Items;
    using Core.Entities.Units;

    using Ensage.SDK.Helpers;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class RadianceUsable : CounterAbility
    {
        private readonly Radiance radiance;

        public RadianceUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
            this.radiance = (Radiance)ability;
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            if (this.radiance.Enabled)
            {
                return false;
            }

            return true;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!this.radiance.UseAbility(false, true))
            {
                return false;
            }

            UpdateManager.BeginInvoke(
                () =>
                    {
                        if (!this.radiance.IsValid || !this.radiance.Enabled)
                        {
                            return;
                        }

                        this.radiance.UseAbility(false, true);
                    },
                3000);

            return true;
        }
    }
}