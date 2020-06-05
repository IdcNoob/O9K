namespace O9K.Evader.Abilities.Heroes.Enigma.BlackHole
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class BlackHoleEvadable : LinearAreaOfEffectEvadable, IModifierCounter
    {
        public BlackHoleEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);
            this.Disables.UnionWith(Abilities.StrongDisable);

            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.Supernova);
            this.Counters.Add(Abilities.HurricanePike);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.Add(Abilities.Bulwark);

            this.Counters.Remove(Abilities.DarkPact);

            this.ModifierDisables.UnionWith(Abilities.Disable);
            this.ModifierDisables.UnionWith(Abilities.ChannelDisable);
            this.ModifierDisables.UnionWith(Abilities.Invulnerability);
            this.ModifierDisables.Add(Abilities.NetherSwap);
            this.ModifierDisables.UnionWith(Abilities.StrongDisable);
            this.ModifierDisables.Add(Abilities.ReapersScythe);

            this.ModifierCounters.Add(Abilities.SongOfTheSiren);
            this.ModifierCounters.Add(Abilities.InkSwell);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.StrongMagicShield);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner)
            {
                IgnoreModifierRemainingTime = true
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}