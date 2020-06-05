namespace O9K.Evader.Abilities.Heroes.Kunkka.XMark
{
    using System;
    using System.Linq;

    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Logger;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class XMarkUsable : CounterAbility
    {
        public XMarkUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
            this.CanBeCastedOnAlly = true;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return base.GetRequiredTime(ally, enemy, obstacle) + 0.1f;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.Use(ally, enemy, obstacle))
            {
                return false;
            }

            if (obstacle.EvadableAbility.Ability.Id == AbilityId.disruptor_glimpse)
            {
                var xReturn = this.Owner.Abilities.FirstOrDefault(x => x.Id == AbilityId.kunkka_return) as ActiveAbility;
                if (xReturn != null)
                {
                    var delay = (int)(obstacle.GetEvadeTime(ally, false) * 1000);
                    if (!ally.Equals(this.Owner))
                    {
                        delay -= (int)(xReturn.GetCastDelay() * 1000) - 100;
                    }

                    UpdateManager.BeginInvoke(
                        () =>
                            {
                                try
                                {
                                    if (!xReturn.IsValid || !xReturn.CanBeCasted())
                                    {
                                        return;
                                    }

                                    xReturn.UseAbility(false, true);
                                }
                                catch (Exception e)
                                {
                                    Logger.Error(e);
                                }
                            },
                        delay);
                }
            }

            return true;
        }
    }
}