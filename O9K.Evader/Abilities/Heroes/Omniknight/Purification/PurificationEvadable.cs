namespace O9K.Evader.Abilities.Heroes.Omniknight.Purification
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class PurificationEvadable : LinearAreaOfEffectEvadable
    {
        public PurificationEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.BladeMail);

            this.Counters.Remove(Abilities.DarkPact);
            this.Counters.Remove(Abilities.ShadowDance);
            this.Counters.Remove(Abilities.ShadowRealm);
            this.Counters.ExceptWith(Abilities.MagicImmunity);
        }
    }
}