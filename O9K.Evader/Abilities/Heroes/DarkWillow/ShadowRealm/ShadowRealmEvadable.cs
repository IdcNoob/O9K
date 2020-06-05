namespace O9K.Evader.Abilities.Heroes.DarkWillow.ShadowRealm
{
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class ShadowRealmEvadable : ProjectileEvadable, IAutoAttack
    {
        public ShadowRealmEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.BallLightning);
        }

        public void AttackEnd()
        {
        }

        public void AttackStart()
        {
        }
    }
}