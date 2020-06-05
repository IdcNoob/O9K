namespace O9K.AIO.Heroes.DarkSeer.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;

    using AIO.Abilities;
    using AIO.Abilities.Items;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_dark_seer))]
    internal class DarkSeer : ControllableUnit
    {
        private ShieldAbility bladeMail;

        private BlinkAbility blink;

        private ForceStaff force;

        private BuffAbility shell;

        private DebuffAbility shiva;

        private BuffAbility surge;

        private Vacuum vacuum;

        private WallOfReplica wall;

        public DarkSeer(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                {
                    AbilityId.dark_seer_vacuum, x =>
                        {
                            this.vacuum = new Vacuum(x);
                            if (this.wall != null)
                            {
                                this.wall.Vacuum = this.vacuum;
                            }

                            return this.vacuum;
                        }
                },
                { AbilityId.dark_seer_ion_shell, x => this.shell = new IonShell(x) },
                { AbilityId.dark_seer_surge, x => this.surge = new BuffAbility(x) },
                {
                    AbilityId.dark_seer_wall_of_replica, x =>
                        {
                            this.wall = new WallOfReplica(x);
                            if (this.vacuum != null)
                            {
                                this.wall.Vacuum = this.vacuum;
                            }

                            return this.wall;
                        }
                },

                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_blade_mail, x => this.bladeMail = new ShieldAbility(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.dark_seer_surge, _ => this.surge);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bladeMail, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.vacuum))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfNone(this.wall, this.vacuum))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blink, 600, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 600, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shell))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.surge, false, false))
            {
                if (abilityHelper.ForceUseAbility(this.surge))
                {
                    return true;
                }
            }

            return false;
        }

        protected override bool MoveComboUseBuffs(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseBuffs(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.surge, false, false))
            {
                abilityHelper.ForceUseAbility(this.surge);
                return true;
            }

            return false;
        }
    }
}