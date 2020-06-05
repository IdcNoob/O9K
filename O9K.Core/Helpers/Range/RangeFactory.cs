namespace O9K.Core.Helpers.Range
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Entities.Abilities.Base;
    using Entities.Abilities.Base.Components;

    using Managers.Entity;

    internal class RangeFactory : IDisposable
    {
        private readonly Dictionary<string, IHasRangeIncrease> ranges = new Dictionary<string, IHasRangeIncrease>();

        public RangeFactory()
        {
            EntityManager9.AbilityAdded += this.OnAbilityAdded;
            EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
        }

        public void Dispose()
        {
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
        }

        public IHasRangeIncrease GetRange(string name)
        {
            if (this.ranges.TryGetValue(name, out var range) && range.IsValid)
            {
                return range;
            }

            return null;
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            if (ability is IHasRangeIncrease range)
            {
                if (range.IsRangeIncreasePermanent)
                {
                    ability.Owner.Range(range, true);
                    return;
                }

                if (this.ranges.ContainsKey(range.RangeModifierName))
                {
                    return;
                }

                this.ranges.Add(range.RangeModifierName, range);

                foreach (var unit in EntityManager9.Units.Where(x => x.HasModifier(range.RangeModifierName)))
                {
                    unit.Range(range, true);
                }
            }
        }

        private void OnAbilityRemoved(Ability9 ability)
        {
            if (ability is IHasRangeIncrease range)
            {
                if (range.IsRangeIncreasePermanent)
                {
                    ability.Owner.Range(range, false);
                    return;
                }

                foreach (var unit in EntityManager9.Units)
                {
                    unit.Range(range, false);
                }

                var sameRange = EntityManager9.Abilities.OfType<IHasRangeIncrease>()
                    .FirstOrDefault(x => x.RangeModifierName == range.RangeModifierName);

                if (sameRange != null)
                {
                    this.ranges[sameRange.RangeModifierName] = sameRange;

                    foreach (var unit in EntityManager9.Units.Where(x => x.HasModifier(range.RangeModifierName)))
                    {
                        unit.Range(range, true);
                    }
                }
                else
                {
                    this.ranges.Remove(range.RangeModifierName);
                }
            }
        }
    }
}