namespace O9K.Core.Helpers.Damage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Entities.Abilities.Base;
    using Entities.Abilities.Base.Components;

    using Managers.Entity;

    internal class DamageFactory : IDisposable
    {
        private readonly Dictionary<string, IHasDamageAmplify> amplifiers = new Dictionary<string, IHasDamageAmplify>();

        private readonly Dictionary<string, IHasDamageBlock> blockers = new Dictionary<string, IHasDamageBlock>();

        private readonly Dictionary<string, IAppliesImmobility> disablers = new Dictionary<string, IAppliesImmobility>();

        private readonly Dictionary<string, IHasPassiveDamageIncrease> passives = new Dictionary<string, IHasPassiveDamageIncrease>();

        public DamageFactory()
        {
            EntityManager9.AbilityAdded += this.OnAbilityAdded;
            EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
        }

        public void Dispose()
        {
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
        }

        public IHasDamageAmplify GetAmplifier(string name)
        {
            if (this.amplifiers.TryGetValue(name, out var amplifier) && amplifier.IsValid)
            {
                return amplifier;
            }

            return null;
        }

        public IHasDamageBlock GetBlocker(string name)
        {
            if (this.blockers.TryGetValue(name, out var blocker) && blocker.IsValid)
            {
                return blocker;
            }

            return null;
        }

        public IAppliesImmobility GetDisable(string name)
        {
            if (this.disablers.TryGetValue(name, out var passive) && passive.IsValid)
            {
                return passive;
            }

            return null;
        }

        public IHasPassiveDamageIncrease GetPassive(string name)
        {
            if (this.passives.TryGetValue(name, out var passive) && passive.IsValid)
            {
                return passive;
            }

            return null;
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            if (ability is IHasDamageAmplify amplifier)
            {
                if (amplifier.IsAmplifierPermanent)
                {
                    ability.Owner.Amplifier(amplifier, true);
                    return;
                }

                if (amplifier.IsAmplifierAddedToStats)
                {
                    return;
                }

                foreach (var amplifierModifierName in amplifier.AmplifierModifierNames)
                {
                    if (this.amplifiers.ContainsKey(amplifierModifierName))
                    {
                        continue;
                    }

                    this.amplifiers.Add(amplifierModifierName, amplifier);

                    foreach (var unit in EntityManager9.Units.Where(x => x.HasModifier(amplifierModifierName)))
                    {
                        unit.Amplifier(amplifier, true);
                    }
                }
            }

            if (ability is IHasPassiveDamageIncrease passive)
            {
                if (passive.IsPassiveDamagePermanent)
                {
                    ability.Owner.Passive(passive, true);
                    return;
                }

                if (this.passives.ContainsKey(passive.PassiveDamageModifierName))
                {
                    return;
                }

                this.passives.Add(passive.PassiveDamageModifierName, passive);

                foreach (var unit in EntityManager9.Units.Where(x => x.HasModifier(passive.PassiveDamageModifierName)))
                {
                    unit.Passive(passive, true);
                }
            }

            if (ability is IHasDamageBlock block)
            {
                if (block.IsDamageBlockPermanent)
                {
                    ability.Owner.Blocker(block, true);
                    return;
                }

                if (this.blockers.ContainsKey(block.BlockModifierName))
                {
                    return;
                }

                this.blockers.Add(block.BlockModifierName, block);

                foreach (var unit in EntityManager9.Units.Where(x => x.HasModifier(block.BlockModifierName)))
                {
                    unit.Blocker(block, true);
                }
            }

            if (ability is IAppliesImmobility disable)
            {
                if (this.disablers.ContainsKey(disable.ImmobilityModifierName))
                {
                    return;
                }

                this.disablers.Add(disable.ImmobilityModifierName, disable);
            }
        }

        private void OnAbilityRemoved(Ability9 ability)
        {
            if (ability is IHasDamageAmplify amplifier)
            {
                if (amplifier.IsAmplifierPermanent)
                {
                    ability.Owner.Amplifier(amplifier, false);
                    return;
                }

                if (amplifier.IsAmplifierAddedToStats)
                {
                    return;
                }

                foreach (var unit in EntityManager9.Units)
                {
                    unit.Amplifier(amplifier, false);
                }

                var sameAmplifier = EntityManager9.Abilities.OfType<IHasDamageAmplify>()
                    .FirstOrDefault(x => x.AmplifierModifierNames[0] == amplifier.AmplifierModifierNames[0]);

                if (sameAmplifier != null)
                {
                    foreach (var amplifierModifierName in sameAmplifier.AmplifierModifierNames)
                    {
                        this.amplifiers[amplifierModifierName] = sameAmplifier;

                        foreach (var unit in EntityManager9.Units.Where(x => x.HasModifier(amplifierModifierName)))
                        {
                            unit.Amplifier(sameAmplifier, true);
                        }
                    }
                }
                else
                {
                    foreach (var amplifierModifierName in amplifier.AmplifierModifierNames)
                    {
                        this.amplifiers.Remove(amplifierModifierName);
                    }
                }
            }

            if (ability is IHasPassiveDamageIncrease passive)
            {
                if (passive.IsPassiveDamagePermanent)
                {
                    ability.Owner.Passive(passive, false);
                    return;
                }

                foreach (var unit in EntityManager9.Units)
                {
                    unit.Passive(passive, false);
                }

                var samePassive = EntityManager9.Abilities.OfType<IHasPassiveDamageIncrease>()
                    .FirstOrDefault(x => x.PassiveDamageModifierName == passive.PassiveDamageModifierName);

                if (samePassive != null)
                {
                    this.passives[samePassive.PassiveDamageModifierName] = samePassive;

                    foreach (var unit in EntityManager9.Units.Where(x => x.HasModifier(passive.PassiveDamageModifierName)))
                    {
                        unit.Passive(samePassive, true);
                    }
                }
                else
                {
                    this.passives.Remove(passive.PassiveDamageModifierName);
                }
            }

            if (ability is IHasDamageBlock block)
            {
                if (block.IsDamageBlockPermanent)
                {
                    ability.Owner.Blocker(block, false);
                    return;
                }

                foreach (var unit in EntityManager9.Units)
                {
                    unit.Blocker(block, false);
                }

                var sameBlocker = EntityManager9.Abilities.OfType<IHasDamageBlock>()
                    .FirstOrDefault(x => x.BlockModifierName == block.BlockModifierName);

                if (sameBlocker != null)
                {
                    this.blockers[sameBlocker.BlockModifierName] = sameBlocker;

                    foreach (var unit in EntityManager9.Units.Where(x => x.HasModifier(block.BlockModifierName)))
                    {
                        unit.Blocker(block, true);
                    }
                }
                else
                {
                    this.blockers.Remove(block.BlockModifierName);
                }
            }

            if (ability is IAppliesImmobility disable)
            {
                var sameDisable = EntityManager9.Abilities.OfType<IAppliesImmobility>()
                    .FirstOrDefault(x => x.ImmobilityModifierName == disable.ImmobilityModifierName);

                if (sameDisable == null)
                {
                    this.disablers.Remove(disable.ImmobilityModifierName);
                }
                else
                {
                    this.disablers[disable.ImmobilityModifierName] = sameDisable;
                }
            }
        }
    }
}