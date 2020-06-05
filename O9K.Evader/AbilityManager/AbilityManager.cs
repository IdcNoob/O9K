namespace O9K.Evader.AbilityManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Abilities.Base;
    using Abilities.Base.Evadable;
    using Abilities.Base.Evadable.Components;
    using Abilities.Base.Usable;
    using Abilities.Base.Usable.BlinkAbility;
    using Abilities.Base.Usable.CounterAbility;
    using Abilities.Base.Usable.DisableAbility;
    using Abilities.Base.Usable.DodgeAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes;
    using Core.Entities.Metadata;
    using Core.Logger;
    using Core.Managers.Assembly;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu.EventArgs;

    using Ensage;

    using Metadata;

    using Monitors;

    [Export(typeof(IAbilityManager))]
    internal class AbilityManager : IAbilityManager, IEvaderService
    {
        private readonly Dictionary<AbilityId, Type> abilityTypes = new Dictionary<AbilityId, Type>();

        private readonly IActionManager actionManager;

        private readonly IAssemblyEventManager9 assemblyEventManager;

        private readonly IMainMenu menu;

        private readonly IPathfinder pathfinder;

        private AttackMonitor attackMonitor;

        private ModifierMonitor modifierMonitor;

        private Owner owner;

        private ParticleMonitor particleMonitor;

        private PhaseMonitor phaseMonitor;

        private ProjectileMonitor projectileMonitor;

        private UnitMonitor unitMonitor;

        [ImportingConstructor]
        public AbilityManager(IContext9 context, IPathfinder pathfinder, IActionManager actionManager, IMainMenu menu)
        {
            foreach (var type in Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass && typeof(EvaderBaseAbility).IsAssignableFrom(x)))
            {
                foreach (var attribute in type.GetCustomAttributes<AbilityIdAttribute>())
                {
                    this.abilityTypes.Add(attribute.AbilityId, type);
                }
            }

            this.assemblyEventManager = context.AssemblyEventManager;
            this.pathfinder = pathfinder;
            this.actionManager = actionManager;
            this.menu = menu;
        }

        public List<EvadableAbility> EvadableAbilities { get; } = new List<EvadableAbility>();

        public LoadOrder LoadOrder { get; } = LoadOrder.AbilityManager;

        public IEnumerable<UsableAbility> UsableAbilities
        {
            get
            {
                return this.UsableBlinkAbilities.Cast<UsableAbility>()
                    .Concat(this.UsableCounterAbilities)
                    .Concat(this.UsableDodgeAbilities)
                    .Concat(this.UsableDisableAbilities);
            }
        }

        public List<BlinkAbility> UsableBlinkAbilities { get; } = new List<BlinkAbility>();

        public List<CounterAbility> UsableCounterAbilities { get; } = new List<CounterAbility>();

        public List<DisableAbility> UsableDisableAbilities { get; } = new List<DisableAbility>();

        public List<DodgeAbility> UsableDodgeAbilities { get; } = new List<DodgeAbility>();

        public void Activate()
        {
            this.owner = EntityManager9.Owner;

            this.particleMonitor = new ParticleMonitor(this.EvadableAbilities);
            this.phaseMonitor = new PhaseMonitor(this.EvadableAbilities);
            this.projectileMonitor = new ProjectileMonitor(this.EvadableAbilities);
            this.unitMonitor = new UnitMonitor(this.EvadableAbilities);
            this.modifierMonitor = new ModifierMonitor(this.menu, this.EvadableAbilities);
            this.attackMonitor = new AttackMonitor(this.EvadableAbilities);

            EntityManager9.AbilityAdded += this.OnAbilityAdded;
            EntityManager9.AbilityRemoved += this.OnAbilityRemoved;

            this.menu.Hotkeys.ProactiveEvade.ValueChange += this.ProactiveEvadeOnValueChange;

            this.assemblyEventManager.OnAssemblyLoad += this.OnAssemblyLoad;
        }

        public void Dispose()
        {
            this.assemblyEventManager.OnAssemblyLoad -= this.OnAssemblyLoad;

            this.menu.Hotkeys.ProactiveEvade.ValueChange -= this.ProactiveEvadeOnValueChange;

            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;

            this.unitMonitor.Dispose();
            this.phaseMonitor.Dispose();
            this.modifierMonitor.Dispose();
            this.particleMonitor.Dispose();
            this.projectileMonitor.Dispose();
            this.attackMonitor.Dispose();

            this.EvadableAbilities.Clear();
            this.UsableCounterAbilities.Clear();
            this.UsableDodgeAbilities.Clear();
            this.UsableDisableAbilities.Clear();
            this.UsableBlinkAbilities.Clear();
        }

        private void AddAllyEvadableAbility(IAllyEvadable allyEvadable)
        {
            var evadableAbility = allyEvadable.GetEvadableAbility();
            this.EvadableAbilities.Add(evadableAbility);
            this.menu.EnemySettings.AddAbility(evadableAbility);
        }

        private void AddBlinkAbility(IUsable<BlinkAbility> blink)
        {
            var blinkAbility = blink.GetUsableAbility();

            this.UsableBlinkAbilities.Add(blinkAbility);
            this.menu.AbilitySettings.AddBlinkAbility(blinkAbility.Ability);
            this.AddEvadableAbilities(blinkAbility);
        }

        private void AddCounterAbility(IUsable<CounterAbility> counter)
        {
            var counterAbility = counter.GetUsableAbility();

            this.UsableCounterAbilities.Add(counterAbility);
            this.menu.AbilitySettings.AddCounterAbility(counterAbility.Ability);
            this.AddEvadableAbilities(counterAbility);
        }

        private void AddDisableAbility(IUsable<DisableAbility> disable)
        {
            var disableAbility = disable.GetUsableAbility();

            this.UsableDisableAbilities.Add(disableAbility);
            this.menu.AbilitySettings.AddDisableAbility(disableAbility.Ability);
            this.AddEvadableAbilities(disableAbility);
        }

        private void AddDodgeAbility(IUsable<DodgeAbility> dodge)
        {
            var dodgeAbility = dodge.GetUsableAbility();

            this.UsableDodgeAbilities.Add(dodgeAbility);
            this.menu.AbilitySettings.AddDodgeAbility(dodgeAbility.Ability);
            this.AddEvadableAbilities(dodgeAbility);
        }

        private void AddEvadableAbilities(UsableAbility usableAbility)
        {
            foreach (var evadableAbility in this.EvadableAbilities)
            {
                usableAbility.AddEvadableAbility(evadableAbility);
            }
        }

        private void AddEvadableAbility(IEvadable evadable)
        {
            var evadableAbility = evadable.GetEvadableAbility();
            this.EvadableAbilities.Add(evadableAbility);
            this.menu.EnemySettings.AddAbility(evadableAbility);

            foreach (var usableAbility in this.UsableAbilities)
            {
                usableAbility.AddEvadableAbility(evadableAbility);
            }

            if (!this.menu.Hotkeys.ProactiveEvade)
            {
                return;
            }

            if (evadableAbility is IProactiveCounter proactive)
            {
                proactive.AddProactiveObstacle();
            }
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (ability.IsTalent || ability.IsFake)
                {
                    return;
                }

                if (!this.abilityTypes.TryGetValue(ability.Id, out var type))
                {
                    return;
                }

                var evaderAbility = (EvaderBaseAbility)Activator.CreateInstance(type, ability);

                evaderAbility.Pathfinder = this.pathfinder;
                evaderAbility.Menu = this.menu;

                var isAllyAbility = ability.Owner.IsAlly(this.owner.Team);

                if (isAllyAbility && evaderAbility is IAllyEvadable allyEvadable)
                {
                    this.AddAllyEvadableAbility(allyEvadable);
                }

                if (isAllyAbility && ability.IsControllable)
                {
                    evaderAbility.ActionManager = this.actionManager;

                    if (evaderAbility is IUsable<CounterAbility> counter)
                    {
                        this.AddCounterAbility(counter);
                    }

                    if (evaderAbility is IUsable<DodgeAbility> dodge)
                    {
                        this.AddDodgeAbility(dodge);
                    }

                    if (evaderAbility is IUsable<BlinkAbility> blink)
                    {
                        this.AddBlinkAbility(blink);
                    }

                    if (evaderAbility is IUsable<DisableAbility> disable)
                    {
                        this.AddDisableAbility(disable);
                    }
                }
                else if (!isAllyAbility && evaderAbility is IEvadable evadable)
                {
                    this.AddEvadableAbility(evadable);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAbilityRemoved(Ability9 ability)
        {
            try
            {
                if (ability.IsTalent || ability.IsFake)
                {
                    return;
                }

                var evadable = this.EvadableAbilities.Find(x => x.Ability.Handle == ability.Handle);
                if (evadable != null)
                {
                    if (evadable is IProactiveCounter proactive)
                    {
                        this.pathfinder.CancelObstacle(proactive.Ability.Handle, true);
                    }

                    if (evadable is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }

                    this.EvadableAbilities.Remove(evadable);
                    return;
                }

                var counter = this.UsableCounterAbilities.Find(x => x.Ability.Handle == ability.Handle);
                if (counter != null)
                {
                    if (counter is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }

                    this.UsableCounterAbilities.Remove(counter);
                }

                var dodge = this.UsableDodgeAbilities.Find(x => x.Ability.Handle == ability.Handle);
                if (dodge != null)
                {
                    this.UsableDodgeAbilities.Remove(dodge);
                }

                var blink = this.UsableBlinkAbilities.Find(x => x.Ability.Handle == ability.Handle);
                if (blink != null)
                {
                    this.UsableBlinkAbilities.Remove(blink);
                }

                var disable = this.UsableDisableAbilities.Find(x => x.Ability.Handle == ability.Handle);
                if (disable != null)
                {
                    this.UsableDisableAbilities.Remove(disable);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAssemblyLoad(object sender, string args)
        {
            if (args == "O9K.ItemManager")
            {
                this.menu.AbilitySettings.AddGoldSpenderAbility();
            }
        }

        private void ProactiveEvadeOnValueChange(object sender, KeyEventArgs e)
        {
            try
            {
                foreach (var proactive in this.EvadableAbilities.OfType<IProactiveCounter>())
                {
                    if (e.NewValue)
                    {
                        proactive.AddProactiveObstacle();
                    }
                    else
                    {
                        this.pathfinder.CancelObstacle(proactive.Ability.Handle, true);
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }
    }
}