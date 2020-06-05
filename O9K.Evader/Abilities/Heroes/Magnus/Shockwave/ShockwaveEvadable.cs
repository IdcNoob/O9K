namespace O9K.Evader.Abilities.Heroes.Magnus.Shockwave
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class ShockwaveEvadable : LinearProjectileEvadable
    {
        public ShockwaveEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo add aghs obstacle

            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.BladeMail);
        }

        protected override void AddObstacle()
        {
            this.EndCastTime -= 0.1f;
            base.AddObstacle();
        }
    }
}