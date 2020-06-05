namespace O9K.AIO.Heroes.Morphling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes.Unique;
    using Core.Logger;

    using Dynamic.Units;

    using Ensage;

    using Modes.Combo;

    using SharpDX;

    using UnitManager;

    internal class MorphlingUnitManager : UnitManager
    {
        public MorphlingUnitManager(BaseHero baseHero)
            : base(baseHero)
        {
        }

        public override void ExecuteCombo(ComboModeMenu comboModeMenu)
        {
            var controlMorphedUnitName = this.owner.Hero is Morphling morphling && morphling.IsMorphed
                                             ? morphling.MorphedHero.Name
                                             : this.owner.HeroName;

            foreach (var controllable in this.ControllableUnits.Where(
                x => x.MorphedUnitName == null || x.MorphedUnitName == controlMorphedUnitName))
            {
                if (controllable.ComboSleeper.IsSleeping)
                {
                    continue;
                }

                if (!comboModeMenu.IgnoreInvisibility && controllable.IsInvisible)
                {
                    return;
                }

                if (controllable.Combo(this.targetManager, comboModeMenu))
                {
                    controllable.LastMovePosition = Vector3.Zero;
                }
            }
        }

        public override void Orbwalk(ComboModeMenu comboModeMenu)
        {
            if (this.issuedAction.IsSleeping)
            {
                return;
            }

            var controlMorphedUnitName = this.owner.Hero is Morphling morphling && morphling.IsMorphed
                                             ? morphling.MorphedHero.Name
                                             : this.owner.HeroName;

            var allUnits = this.ControllableUnits.Where(x => x.MorphedUnitName == null || x.MorphedUnitName == controlMorphedUnitName)
                .OrderBy(x => this.IssuedActionTime(x.Handle))
                .ToList();

            if (this.BodyBlock(allUnits, comboModeMenu))
            {
                this.issuedAction.Sleep(0.05f);
                return;
            }

            var noOrbwalkUnits = new List<ControllableUnit>();
            foreach (var controllable in allUnits)
            {
                if (!controllable.OrbwalkEnabled)
                {
                    noOrbwalkUnits.Add(controllable);
                    continue;
                }

                if (this.unitIssuedAction.IsSleeping(controllable.Handle))
                {
                    continue;
                }

                if (!controllable.Orbwalk(this.targetManager.Target, comboModeMenu))
                {
                    continue;
                }

                this.issuedActionTimings[controllable.Handle] = Game.RawGameTime;
                this.unitIssuedAction.Sleep(controllable.Handle, 0.2f);
                this.issuedAction.Sleep(0.05f);
                return;
            }

            if (noOrbwalkUnits.Count > 0 && !this.unitIssuedAction.IsSleeping(uint.MaxValue))
            {
                this.ControlAllUnits(noOrbwalkUnits);
            }
        }

        protected override void OnAbilityAdded(Ability9 entity)
        {
            try
            {
                if (!entity.IsControllable || entity.IsFake || !entity.Owner.IsAlly(this.owner.Team)
                    || !(entity is ActiveAbility activeAbility))
                {
                    return;
                }

                var abilityOwner = entity.Owner;
                var morph = entity.Owner as Morphling;

                if (morph?.IsMorphed == true)
                {
                    ControllableUnit morphedUnit;

                    if (this.unitTypes.TryGetValue(morph.MorphedHero.Name, out var type))
                    {
                        morphedUnit = this.controllableUnits.Find(x => x.Handle == abilityOwner.Handle && x.GetType() == type);

                        if (morphedUnit == null)
                        {
                            morphedUnit = (ControllableUnit)Activator.CreateInstance(
                                type,
                                abilityOwner,
                                this.abilitySleeper,
                                this.orbwalkSleeper[abilityOwner.Handle],
                                this.GetUnitMenu(abilityOwner));
                            morphedUnit.FailSafe = this.BaseHero.FailSafe;
                            morphedUnit.MorphedUnitName = morph.MorphedHero.Name;

                            foreach (var item in abilityOwner.Abilities.Where(x => x.IsItem).OfType<ActiveAbility>())
                            {
                                morphedUnit.AddAbility(item, this.BaseHero.ComboMenus, this.BaseHero.MoveComboModeMenu);
                            }

                            this.controllableUnits.Add(morphedUnit);
                        }
                    }
                    else
                    {
                        morphedUnit = this.controllableUnits.Find(x => x.Handle == abilityOwner.Handle && x is DynamicUnit);

                        if (morphedUnit == null)
                        {
                            morphedUnit = new DynamicUnit(
                                abilityOwner,
                                this.abilitySleeper,
                                this.orbwalkSleeper[abilityOwner.Handle],
                                this.GetUnitMenu(abilityOwner),
                                this.BaseHero)
                            {
                                FailSafe = this.BaseHero.FailSafe,
                                MorphedUnitName = morph.MorphedHero.Name
                            };
                            foreach (var item in abilityOwner.Abilities.Where(x => x.IsItem).OfType<ActiveAbility>())
                            {
                                morphedUnit.AddAbility(item, this.BaseHero.ComboMenus, this.BaseHero.MoveComboModeMenu);
                            }

                            this.controllableUnits.Add(morphedUnit);
                        }
                    }

                    if (activeAbility.IsItem)
                    {
                        foreach (var controllableUnit in this.controllableUnits.Where(x => x.Handle == abilityOwner.Handle))
                        {
                            controllableUnit.AddAbility(activeAbility, this.BaseHero.ComboMenus, this.BaseHero.MoveComboModeMenu);
                        }
                    }
                    else
                    {
                        morphedUnit.AddAbility(activeAbility, this.BaseHero.ComboMenus, this.BaseHero.MoveComboModeMenu);
                    }

                    return;
                }

                if (activeAbility.IsItem)
                {
                    foreach (var controllable in this.controllableUnits.Where(x => x.Handle == abilityOwner.Handle))
                    {
                        controllable.AddAbility(activeAbility, this.BaseHero.ComboMenus, this.BaseHero.MoveComboModeMenu);
                    }
                }
                else
                {
                    var controllable = this.controllableUnits.Find(x => x.Handle == entity.Owner.Handle);
                    controllable?.AddAbility(activeAbility, this.BaseHero.ComboMenus, this.BaseHero.MoveComboModeMenu);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        protected override void OnAbilityRemoved(Ability9 entity)
        {
            try
            {
                if (!entity.IsControllable || entity.IsFake || !entity.Owner.IsAlly(this.owner.Team)
                    || !(entity is ActiveAbility activeAbility))
                {
                    return;
                }

                foreach (var controllable in this.controllableUnits.Where(x => x.Handle == entity.Owner.Handle))
                {
                    controllable.RemoveAbility(activeAbility);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}