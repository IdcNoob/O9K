namespace O9K.Core.Managers.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Ensage;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using Entities;
    using Entities.Abilities.Base;
    using Entities.Buildings;
    using Entities.Heroes;
    using Entities.Metadata;
    using Entities.Units;

    using Helpers;

    using Logger;

    using Monitors;

    using Prediction;

    using SharpDX;

    public static class EntityManager9
    {
        private static EventHandler<Ability9> abilityAdded;

        private static EventHandler<Unit9> unitAdded;

        static EntityManager9()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(x => !x.IsAbstract && x.IsClass))
            {
                if (typeof(Ability9).IsAssignableFrom(type))
                {
                    foreach (var attribute in type.GetCustomAttributes<AbilityIdAttribute>())
                    {
                        abilityTypes.Add(attribute.AbilityId, type);
                    }
                }
                else if (typeof(Hero9).IsAssignableFrom(type))
                {
                    foreach (var attribute in type.GetCustomAttributes<HeroIdAttribute>())
                    {
                        heroTypes.Add(attribute.HeroId, type);
                    }
                }
                else if (typeof(Unit9).IsAssignableFrom(type))
                {
                    foreach (var attribute in type.GetCustomAttributes<UnitNameAttribute>())
                    {
                        unitTypes.Add(attribute.Name, type);
                    }
                }
            }

            Owner = new Owner();
            UnitMonitor = new UnitMonitor();
            AbilityMonitor = new AbilityMonitor();
            delayedEntityHandler = UpdateManager.Subscribe(DelayedEntitiesOnUpdate, 1000, false);

            AddCurrentUnits();
            AddCurrentAbilities();

            ObjectManager.OnAddEntity += OnAddEntity;
            ObjectManager.OnRemoveEntity += OnRemoveEntity;

            UpdateManager.BeginInvoke(DemoModeCheck, 2000);
            UpdateManager.BeginInvoke(LoadCheck, 5000);
        }

        public delegate void EventHandler<in T>(T entity)
            where T : Entity9;

        public static event EventHandler<Ability9> AbilityAdded
        {
            add
            {
                abilityAdded += value;

                foreach (var ability in Abilities)
                {
                    //if (abilityAdded?.GetInvocationList().Contains(value) != true)
                    //{
                    //    break;
                    //}

                    try
                    {
                        value(ability);
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                    }
                }
            }
            remove
            {
                abilityAdded -= value;
            }
        }

        public static event EventHandler<Ability9> AbilityRemoved;

        public static event EventHandler<Unit9> UnitAdded
        {
            add
            {
                unitAdded += value;

                foreach (var unit in Units)
                {
                    //if (unitAdded?.GetInvocationList().Contains(value) != true)
                    //{
                    //    break;
                    //}

                    try
                    {
                        value(unit);
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                    }
                }
            }
            remove
            {
                unitAdded -= value;
            }
        }

        public static event EventHandler<Unit9> UnitRemoved;

        public static IEnumerable<Ability9> Abilities
        {
            get
            {
                return abilitiesList.Where(x => x.IsValid);
            }
        }

        public static AbilityMonitor AbilityMonitor { get; }

        public static Vector3 AllyFountain { get; private set; }

        public static List<Unit9> AllyHeroes
        {
            get
            {
                return Units.Where(x => x.IsHero && x.IsAlive && !x.IsIllusion && !x.IsInvulnerable && x.IsAlly(Owner)).ToList();
            }
        }

        public static List<Unit9> AllyUnits
        {
            get
            {
                return Units.Where(x => x.IsAlive && !x.IsIllusion && !x.IsInvulnerable && x.IsAlly(Owner)).ToList();
            }
        }

        public static Vector3 DireOutpost { get; private set; }

        public static Vector3 EnemyFountain { get; private set; }

        public static List<Unit9> EnemyHeroes
        {
            get
            {
                return Units.Where(x => x.IsHero && x.IsAlive && !x.IsIllusion && x.IsVisible && !x.IsInvulnerable && !x.IsAlly(Owner))
                    .ToList();
            }
        }

        public static List<Unit9> EnemyUnits
        {
            get
            {
                return Units.Where(x => x.IsAlive && !x.IsIllusion && x.IsVisible && !x.IsInvulnerable && !x.IsAlly(Owner)).ToList();
            }
        }

        public static IEnumerable<Hero9> Heroes
        {
            get
            {
                return unitsList.Where(x => x.IsHero && x.IsValid).Cast<Hero9>();
            }
        }

        public static Owner Owner { get; }

        public static Vector3 RadiantOutpost { get; private set; }

        public static IEnumerable<Tree> Trees
        {
            get
            {
                return ObjectManager.GetEntitiesFast<Tree>().Where(x => x.IsAlive).ToArray();
            }
        }

        public static UnitMonitor UnitMonitor { get; }

        public static IEnumerable<Unit9> Units
        {
            get
            {
                return unitsList.Where(x => x.IsValid);
            }
        }

        internal static IEnumerable<Ability> BaseAbilities
        {
            get
            {
                return ObjectManager.GetEntities<Ability>().Concat(ObjectManager.GetDormantEntities<Ability>()).Where(x => x.IsValid);
            }
        }

        public static void ForceReload()
        {
            try
            {
                foreach (var unit in Units)
                {
                    try
                    {
                        UnitRemoved?.Invoke(unit);
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                    }
                    finally
                    {
                        if (unit is IDisposable disposable)
                        {
                            disposable.Dispose();
                        }
                    }
                }

                foreach (var ability in Abilities)
                {
                    try
                    {
                        AbilityRemoved?.Invoke(ability);
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                    }
                    finally
                    {
                        ability.Dispose();
                    }
                }

                unitsDictionary.Clear();
                unitsList.Clear();

                abilitiesDictionary.Clear();
                abilitiesList.Clear();

                delayedAbilities.Clear();
                delayedHeroes.Clear();

                AddCurrentUnits();
                AddCurrentAbilities();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public static Ability9 GetAbility(uint handle)
        {
            if (abilitiesDictionary.TryGetValue(handle, out var ability) && ability.IsValid)
            {
                return ability;
            }

            return null;
        }

        public static T GetAbility<T>(Unit9 owner)
            where T : Ability9
        {
            return (T)Abilities.FirstOrDefault(x => x is T && x.Owner.Equals(owner));
        }

        public static T GetAbility<T>(uint ownerHandle)
            where T : Ability9
        {
            return (T)Abilities.FirstOrDefault(x => x is T && x.Owner.Handle == ownerHandle);
        }

        public static Unit9 GetUnit(uint handle)
        {
            if (unitsDictionary.TryGetValue(handle, out var unit) && unit.IsValid)
            {
                return unit;
            }

            return null;
        }

        public static Unit9 GetUnit(uint? handle)
        {
            return handle == null ? null : GetUnit(handle.Value);
        }

        internal static Ability9 AddAbility(Ability ability)
        {
            try
            {
                if (!abilityTypes.TryGetValue(ability.Id, out var type))
                {
                    return null;
                }

                var newAbility = GetAbilityFast(ability.Handle);
                if (newAbility != null)
                {
                    return newAbility;
                }

                var abilityOwner = GetUnitFast(ability.Owner?.Handle);
                if (abilityOwner == null)
                {
                    DelayedAdd(ability);
                    return null;
                }

                if (ability is Item item)
                {
                    if (item.PurchaseTime < 0)
                    {
                        // created by lotus
                        return null;
                    }
                }
                else if (ability.AbilitySlot < 0 && !ability.IsHidden)
                {
                    // created by lotus
                    return null;
                }

                newAbility = (Ability9)Activator.CreateInstance(type, ability);
                newAbility.SetPrediction(predictionManager);
                AbilityMonitor.SetOwner(newAbility, abilityOwner);
                SaveAbility(newAbility);
                abilityAdded?.Invoke(newAbility);

                return newAbility;
            }
            catch (Exception e)
            {
                Logger.Error(e, ability);
                return null;
            }
        }

        internal static Unit9 AddUnit(Unit unit)
        {
            try
            {
                if (ignoredUnits.Contains(unit.Name))
                {
                    return null;
                }

                var newUnit = GetUnitFast(unit.Handle);
                if (newUnit != null)
                {
                    return newUnit;
                }

                if (unit is Hero hero)
                {
                    if (hero.HeroId == HeroId.npc_dota_hero_base || hero.Inventory == null)
                    {
                        DelayedAdd(hero);
                        return null;
                    }

                    if (heroTypes.TryGetValue(hero.HeroId, out var type))
                    {
                        newUnit = (Hero9)Activator.CreateInstance(type, hero);
                    }
                    else
                    {
                        newUnit = new Hero9(hero);
                    }

                    if (!newUnit.IsIllusion && newUnit.Handle == ObjectManager.LocalHero.Handle)
                    {
                        Owner.SetHero(newUnit);
                    }
                }
                else
                {
                    if (unitTypes.TryGetValue(unit.Name, out var type))
                    {
                        newUnit = (Unit9)Activator.CreateInstance(type, unit);
                    }
                    else
                    {
                        newUnit = new Unit9(unit);
                    }
                }

                SaveUnit(newUnit);

                if (newUnit.BaseOwner is Unit owner && owner.IsValid)
                {
                    newUnit.Owner = AddUnit(owner);
                }

                UnitMonitor.CheckModifiers(newUnit);
                unitAdded?.Invoke(newUnit);

                return newUnit;
            }
            catch (Exception e)
            {
                Logger.Error(e, unit);
                return null;
            }
        }

        internal static void ChangeEntityControl(Entity entity)
        {
            try
            {
                var unit = GetUnit(entity.Handle);
                if (unit == null)
                {
                    return;
                }

                var entityOwner = entity.Owner;
                if (entityOwner?.IsValid != true)
                {
                    return;
                }

                if (!unit.IsHero && unit.BaseOwner?.IsValid == true && unit.BaseOwner.Handle == entityOwner.Handle)
                {
                    return;
                }

                UnitRemoved?.Invoke(unit);

                foreach (var ability in unit.Abilities)
                {
                    AbilityRemoved?.Invoke(ability);
                }

                unit.IsControllable = unit.BaseUnit.IsControllable;
                unit.Team = unit.BaseUnit.Team;
                unit.EnemyTeam = unit.Team == Team.Radiant ? Team.Dire : Team.Radiant;
                unit.BaseOwner = entityOwner;

                if (entityOwner is Unit owner)
                {
                    unit.Owner = AddUnit(owner);
                }

                unitAdded?.Invoke(unit);

                foreach (var ability in unit.Abilities)
                {
                    abilityAdded?.Invoke(ability);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, entity, "Change owner");
            }
        }

        internal static Ability9 GetAbilityFast(uint handle)
        {
            return abilitiesDictionary.TryGetValue(handle, out var ability) ? ability : null;
        }

        internal static Unit9 GetUnitFast(uint handle)
        {
            return unitsDictionary.TryGetValue(handle, out var unit) ? unit : null;
        }

        internal static Unit9 GetUnitFast(uint? handle)
        {
            return handle == null ? null : GetUnitFast(handle.Value);
        }

        internal static void RemoveAbility(Ability ability)
        {
            try
            {
                var ability9 = GetAbilityFast(ability.Handle);
                if (ability9 == null)
                {
                    return;
                }

                ability9.Dispose();

                abilitiesDictionary.Remove(ability9.Handle);
                abilitiesList.Remove(ability9);

                AbilityRemoved?.Invoke(ability9);

                ability9.IsValid = false;
            }
            catch (Exception e)
            {
                Logger.Error(e, ability);
            }
        }

        private static void AddBuilding(Unit building)
        {
            try
            {
                var newUnit = GetUnitFast(building.Handle);
                if (newUnit != null)
                {
                    return;
                }

                switch (building.NetworkName)
                {
                    case "CDOTA_BaseNPC_Tower":
                    {
                        newUnit = new Tower9((Tower)building);
                        break;
                    }
                    case "CDOTA_BaseNPC_Barracks":
                    {
                        newUnit = new Building9(building)
                        {
                            IsBarrack = true
                        };
                        break;
                    }
                    case "CDOTA_BaseNPC_Watch_Tower":
                    {
                        newUnit = new Building9(building);

                        if (building.Position.X < 0)
                        {
                            RadiantOutpost = newUnit.Position;
                        }
                        else
                        {
                            DireOutpost = newUnit.Position;
                        }

                        break;
                    }
                    case "CDOTA_Unit_Fountain":
                    {
                        newUnit = new Building9(building)
                        {
                            IsFountain = true
                        };

                        if (newUnit.Team == Owner.Team)
                        {
                            AllyFountain = newUnit.Position;
                        }
                        else
                        {
                            EnemyFountain = newUnit.Position;
                        }

                        break;
                    }
                    case "CDOTA_BaseNPC_Shop":
                    {
                        // ignore
                        return;
                    }
                    default:
                    {
                        newUnit = new Building9(building);
                        break;
                    }
                }

                SaveUnit(newUnit);
                unitAdded?.Invoke(newUnit);
            }
            catch (Exception e)
            {
                Logger.Error(e, building);
            }
        }

        private static void AddCurrentAbilities()
        {
            foreach (var entity in ObjectManager.GetEntities<Ability>().Concat(ObjectManager.GetDormantEntities<Ability>()))
            {
                try
                {
                    if (!entity.IsValid)
                    {
                        continue;
                    }

                    AddAbility(entity);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }

        private static void AddCurrentUnits()
        {
            AddUnit(ObjectManager.LocalHero);

            foreach (var entity in ObjectManager.GetEntities<Unit>().Concat(ObjectManager.GetDormantEntities<Unit>()))
            {
                try
                {
                    if (!entity.IsValid || entity.Position.IsZero)
                    {
                        continue;
                    }

                    if (entity is Building)
                    {
                        AddBuilding(entity);
                    }
                    else
                    {
                        AddUnit(entity);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }

        private static void AutoReload()
        {
            if (Heroes.Any(x => x.IsMyHero && x.IsValid && x.Id != HeroId.npc_dota_hero_base))
            {
                return;
            }

            ForceReload();
        }

        private static void DelayedAdd(Ability ability)
        {
            if (!delayedAbilities.Contains(ability))
            {
                delayedAbilities.Add(ability);
            }

            delayedEntityHandler.IsEnabled = true;
        }

        private static void DelayedAdd(Hero hero)
        {
            if (!delayedHeroes.Contains(hero))
            {
                delayedHeroes.Add(hero);
            }

            delayedEntityHandler.IsEnabled = true;
        }

        private static void DelayedEntitiesOnUpdate()
        {
            try
            {
                for (var i = delayedHeroes.Count - 1; i > -1; i--)
                {
                    var hero = delayedHeroes[i];

                    if (!hero.IsValid)
                    {
                        delayedHeroes.RemoveAt(i);
                        continue;
                    }

                    if (hero.HeroId == HeroId.npc_dota_hero_base || hero.Inventory == null)
                    {
                        continue;
                    }

                    delayedHeroes.RemoveAt(i);
                    AddUnit(hero);
                }

                for (var i = delayedAbilities.Count - 1; i > -1; i--)
                {
                    var ability = delayedAbilities[i];

                    if (!ability.IsValid)
                    {
                        delayedAbilities.RemoveAt(i);
                        continue;
                    }

                    if (GetUnitFast(ability.Owner?.Handle) == null)
                    {
                        continue;
                    }

                    delayedAbilities.RemoveAt(i);
                    AddAbility(ability);
                }

                if (delayedAbilities.Count == 0 && delayedHeroes.Count == 0)
                {
                    delayedEntityHandler.IsEnabled = false;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private static void DemoModeCheck()
        {
            if (Game.GameMode != GameMode.Demo)
            {
                return;
            }

            Hud.DisplayWarning("O9K // Some assemblies will not work correctly in demo mode", 600);
            Hud.DisplayWarning("O9K // Use custom lobby instead", 600);
        }

        private static void LoadCheck()
        {
            var myHero = Units.FirstOrDefault(x => x.IsMyHero);
            if (myHero?.IsValid == true && myHero.MoveCapability != MoveCapability.None)
            {
                UpdateManager.Subscribe(AutoReload, 3000);
                return;
            }

            Logger.Warn("O9K was not loaded successfully, reloading...");
            ForceReload();

            UpdateManager.Subscribe(AutoReload, 3000);
        }

        private static void OnAddEntity(EntityEventArgs args)
        {
            switch (args.Entity)
            {
                case Building building:
                {
                    UpdateManager.BeginInvoke(
                        () =>
                            {
                                if (building.IsValid)
                                {
                                    AddBuilding(building);
                                }
                            },
                        100);
                    return;
                }
                case Unit unit:
                {
                    UpdateManager.BeginInvoke(
                        () =>
                            {
                                if (unit.IsValid)
                                {
                                    AddUnit(unit);
                                }
                            },
                        100);
                    return;
                }
                case Ability ability:
                {
                    UpdateManager.BeginInvoke(
                        () =>
                            {
                                if (ability.IsValid)
                                {
                                    AddAbility(ability);
                                }
                            },
                        300);
                    return;
                }
            }
        }

        private static void OnRemoveEntity(EntityEventArgs args)
        {
            switch (args.Entity)
            {
                case Unit unit:
                {
                    RemoveUnit(unit);
                    return;
                }
                case Ability ability:
                {
                    RemoveAbility(ability);
                    return;
                }
            }
        }

        private static void RemoveUnit(Entity unit)
        {
            try
            {
                var unit9 = GetUnitFast(unit.Handle);
                if (unit9 == null)
                {
                    return;
                }

                if (unit9 is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                unitsDictionary.Remove(unit9.Handle);
                unitsList.Remove(unit9);

                UnitRemoved?.Invoke(unit9);

                unit9.IsValid = false;
            }
            catch (Exception e)
            {
                Logger.Error(e, unit);
            }
        }

        private static void SaveAbility(Ability9 ability)
        {
            if (abilitiesDictionary.Remove(ability.Handle))
            {
                abilitiesList.Remove(ability);
            }

            abilitiesList.Add(ability);
            abilitiesDictionary[ability.Handle] = ability;
        }

        private static void SaveUnit(Unit9 unit)
        {
            if (unitsDictionary.Remove(unit.Handle))
            {
                unitsList.Remove(unit);
            }

            unitsList.Add(unit);
            unitsDictionary[unit.Handle] = unit;
        }

        // ReSharper disable InconsistentNaming

        private static readonly Dictionary<uint, Ability9> abilitiesDictionary = new Dictionary<uint, Ability9>(300);

        internal static readonly List<Ability9> abilitiesList = new List<Ability9>(300);

        private static readonly Dictionary<AbilityId, Type> abilityTypes = new Dictionary<AbilityId, Type>();

        private static readonly Dictionary<HeroId, Type> heroTypes = new Dictionary<HeroId, Type>();

        private static readonly Dictionary<string, Type> unitTypes = new Dictionary<string, Type>();

        private static readonly IPredictionManager9 predictionManager = new PredictionManager9();

        private static readonly IUpdateHandler delayedEntityHandler;

        private static readonly Dictionary<uint, Unit9> unitsDictionary = new Dictionary<uint, Unit9>(100);

        internal static readonly List<Unit9> unitsList = new List<Unit9>(100);

        private static readonly List<Ability> delayedAbilities = new List<Ability>();

        private static readonly List<Hero> delayedHeroes = new List<Hero>();

        private static readonly HashSet<string> ignoredUnits = new HashSet<string>
        {
            "",
            "ent_dota_halloffame",
            "dota_portrait_entity",
            "portrait_world_unit",
            "npc_dota_wisp_spirit",
            "npc_dota_companion",
            "npc_dota_hero_announcer",
            "npc_dota_neutral_caster",
            "npc_dota_hero_announcer_killing_spree",
            "npc_dota_base",
            "npc_dota_thinker",
        };

        // ReSharper restore InconsistentNaming
    }
}