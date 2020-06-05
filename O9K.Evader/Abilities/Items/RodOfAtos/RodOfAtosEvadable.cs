namespace O9K.Evader.Abilities.Items.RodOfAtos
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class RodOfAtosEvadable : ProjectileEvadable, IModifierCounter
    {
        public RodOfAtosEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);
            this.Blinks.Remove(Abilities.ForceBoots);

            this.Counters.UnionWith(Abilities.VsDisableProjectile);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invisibility);
            this.Counters.UnionWith(Abilities.SlowHeal);

            this.Counters.ExceptWith(Abilities.MagicImmunity);
            this.Counters.Remove(Abilities.ShadowDance);
            this.Counters.Remove(Abilities.SpikedCarapace);
            this.Counters.Remove(Abilities.Refraction);
            this.Counters.Remove(Abilities.EulsScepterOfDivinity);
            this.Counters.Remove(Abilities.MantaStyle);
            this.Counters.Remove(Abilities.Enrage);

            this.ModifierBlinks.Add(Abilities.ForceBoots);

            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.Add(Abilities.Enrage);
            this.ModifierCounters.Add(Abilities.ChemicalRage);
            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.Add(Abilities.PressTheAttack);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}