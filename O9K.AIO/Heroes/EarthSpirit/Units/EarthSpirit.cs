namespace O9K.AIO.Heroes.EarthSpirit.Units
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Abilities;

    using AIO.Abilities;
    using AIO.Abilities.Items;
    using AIO.Modes.Combo;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Helpers;

    using Ensage;

    using Modes;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_earth_spirit))]
    internal class EarthSpirit : ControllableUnit
    {
        private ShieldAbility bladeMail;

        private BlinkAbility blink;

        private ForceStaff force;

        private GeomagneticGrip grip;

        private DisableAbility halberd;

        private AoeAbility mag;

        private RollingBoulder rolling;

        private BlinkAbility rollingBlink;

        private DebuffAbility shiva;

        private BoulderSmash smash;

        private StoneRemnant stone;

        private StoneRemnantBlink stoneBlink;

        private DebuffAbility urn;

        private DebuffAbility veil;

        private DebuffAbility vessel;

        public EarthSpirit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.earth_spirit_boulder_smash, x => this.smash = new BoulderSmash(x) },
                { AbilityId.earth_spirit_rolling_boulder, x => this.rolling = new RollingBoulder(x) },
                { AbilityId.earth_spirit_geomagnetic_grip, x => this.grip = new GeomagneticGrip(x) },
                { AbilityId.earth_spirit_magnetize, x => this.mag = new AoeAbility(x) },
                { AbilityId.earth_spirit_stone_caller, x => this.stone = new StoneRemnant(x) },
                //{ AbilityId.earth_spirit_petrify, x => this.petrify = new NukeAbility(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_spirit_vessel, x => this.vessel = new DebuffAbility(x) },
                { AbilityId.item_urn_of_shadows, x => this.urn = new DebuffAbility(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_blade_mail, x => this.bladeMail = new ShieldAbility(x) },
                { AbilityId.item_heavens_halberd, x => this.halberd = new DisableAbility(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.earth_spirit_rolling_boulder, x => this.rollingBlink = new RollingBoulderBlink(x));
            this.MoveComboAbilities.Add(AbilityId.earth_spirit_stone_caller, x => this.stoneBlink = new StoneRemnantBlink(x));
            this.MoveComboAbilities.Add(AbilityId.earth_spirit_boulder_smash, _ => this.smash);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.blink, 400, 200))
            {
                this.rolling?.Sleeper.Sleep(0.8f);
                this.ComboSleeper.ExtendSleep(0.1f);
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 400, 200))
            {
                this.rolling?.Sleeper.Sleep(0.8f);
                this.ComboSleeper.ExtendSleep(0.1f);
                return true;
            }

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.rolling))
            {
                if (abilityHelper.UseAbility(this.rolling))
                {
                    return true;
                }

                if (abilityHelper.CanBeCasted(this.stone, false))
                {
                    if (abilityHelper.ForceUseAbility(this.stone, true))
                    {
                        this.smash?.Sleeper.Sleep(1f);
                        return true;
                    }
                }
                else
                {
                    if (abilityHelper.ForceUseAbility(this.rolling, true))
                    {
                        return true;
                    }
                }
            }

            if (abilityHelper.CanBeCasted(this.smash))
            {
                if (abilityHelper.UseAbility(this.smash))
                {
                    return true;
                }

                if (abilityHelper.CanBeCasted(this.stone, false))
                {
                    if (this.stone.UseAbility(targetManager, this.ComboSleeper, this.smash))
                    {
                        return true;
                    }
                }
                else
                {
                    if (abilityHelper.ForceUseAbility(this.smash, true))
                    {
                        return true;
                    }
                }
            }

            if (abilityHelper.CanBeCasted(this.grip))
            {
                if (abilityHelper.UseAbility(this.grip))
                {
                    return true;
                }

                if (abilityHelper.CanBeCasted(this.stone, false))
                {
                    if (this.stone.UseAbility(targetManager, this.ComboSleeper, this.grip))
                    {
                        return true;
                    }
                }
                else
                {
                    if (abilityHelper.ForceUseAbility(this.grip, true))
                    {
                        return true;
                    }
                }
            }

            if (abilityHelper.UseAbility(this.halberd))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.mag))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.vessel))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.urn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bladeMail))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.stone))
            {
                return true;
            }

            return false;
        }

        public void RollSmashCombo(TargetManager targetManager, RollSmashModeMenu menu)
        {
            if (this.ComboSleeper.IsSleeping)
            {
                return;
            }

            var target = targetManager.Target;
            var distance = this.Owner.Distance(target.Position);

            var abilityHelper = new AbilityHelper(targetManager, null, this);

            if (this.Owner.HasModifier("modifier_earth_spirit_rolling_boulder_caster"))
            {
                if (abilityHelper.ForceUseAbility(this.stone, true))
                {
                    this.ComboSleeper.Sleep(3f);
                    return;
                }
            }

            var ally = targetManager.AllyHeroes
                .Where(x => !x.Equals(this.Owner) && menu.IsAllyEnabled(x.Name) && x.Distance(target) > 300 && x.Distance(target) < 1500)
                .OrderBy(x => x.Distance(target))
                .FirstOrDefault();

            if (ally == null)
            {
                return;
            }

            if (target.HasModifier("modifier_earth_spirit_boulder_smash"))
            {
                if (abilityHelper.CanBeCasted(this.rolling, false, false) && this.rolling.SimpleUseAbility(target.Position))
                {
                    this.ComboSleeper.Sleep(0.5f);
                    return;
                }

                return;
            }

            if (this.smash?.Ability.CanBeCasted() != true)
            {
                return;
            }

            if (distance < this.smash.Ability.CastRange + 100)
            {
                if (this.Owner.Position.AngleBetween(target.Position, ally.Position) < 30)
                {
                    this.smash.Ability.UseAbility(target);
                    this.ComboSleeper.Sleep(0.3f);
                    return;
                }

                if (target.GetImmobilityDuration() > 0.5f)
                {
                    this.Owner.BaseUnit.Move(target.Position.Extend2D(ally.Position, -100));
                    this.OrbwalkSleeper.Sleep(0.1f);
                    this.ComboSleeper.Sleep(0.1f);
                    return;
                }
            }

            if (abilityHelper.CanBeCasted(this.blink))
            {
                var predicted = target.GetPredictedPosition(this.Owner.GetTurnTime(target.Position) + (Game.Ping / 2000f) + 0.2f);
                var position = predicted.Extend2D(ally.Position, -125);

                if (abilityHelper.UseAbility(this.blink, position))
                {
                    this.ComboSleeper.ExtendSleep(0.1f);
                    return;
                }

                return;
            }

            if (abilityHelper.CanBeCasted(this.rolling))
            {
                if (abilityHelper.UseAbility(this.rolling))
                {
                    return;
                }

                if (abilityHelper.CanBeCasted(this.stone, false))
                {
                    if (abilityHelper.ForceUseAbility(this.stone, true))
                    {
                        return;
                    }
                }
                else
                {
                    if (abilityHelper.ForceUseAbility(this.rolling, true))
                    {
                        return;
                    }
                }
            }
        }

        protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseBlinks(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.rollingBlink))
            {
                this.smash?.Sleeper.Sleep(2);
                return true;
            }

            if (this.Owner.HasModifier("modifier_earth_spirit_rolling_boulder_caster"))
            {
                if (abilityHelper.UseMoveAbility(this.stoneBlink))
                {
                    return true;
                }
            }

            return false;
        }

        protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
        {
            if (abilityHelper.CanBeCasted(this.smash))
            {
                if (abilityHelper.UseAbility(this.smash))
                {
                    return true;
                }

                if (abilityHelper.CanBeCasted(this.stone, false))
                {
                    if (this.stone.UseAbility(abilityHelper.TargetManager, this.ComboSleeper, this.smash))
                    {
                        return true;
                    }
                }
                else
                {
                    if (abilityHelper.ForceUseAbility(this.smash, true))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}