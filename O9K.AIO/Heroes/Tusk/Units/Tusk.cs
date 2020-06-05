namespace O9K.AIO.Heroes.Tusk.Units
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
    using Core.Managers.Entity;

    using Ensage;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_tusk))]
    internal class Tusk : ControllableUnit
    {
        private ShieldAbility bladeMail;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private NukeAbility kick;

        private DebuffAbility medallion;

        private DisableAbility orchid;

        private NukeAbility punch;

        private IceShards shards;

        private TargetableAbility snowball;

        private DebuffAbility solar;

        private DebuffAbility tag;

        private DebuffAbility urn;

        private DebuffAbility vessel;

        public Tusk(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.tusk_ice_shards, x => this.shards = new IceShards(x) },
                { AbilityId.tusk_snowball, x => this.snowball = new Snowball(x) },
                { AbilityId.tusk_tag_team, x => this.tag = new DebuffAbility(x) },
                { AbilityId.tusk_walrus_punch, x => this.punch = new NukeAbility(x) },
                { AbilityId.tusk_walrus_kick, x => this.kick = new Kick(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_spirit_vessel, x => this.vessel = new DebuffAbility(x) },
                { AbilityId.item_urn_of_shadows, x => this.urn = new DebuffAbility(x) },
                { AbilityId.item_solar_crest, x => this.solar = new DebuffAbility(x) },
                { AbilityId.item_medallion_of_courage, x => this.medallion = new DebuffAbility(x) },
                { AbilityId.item_blade_mail, x => this.bladeMail = new ShieldAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            if (comboModeMenu.SimplifiedName == "kicktoallycombo")
            {
                return this.KickToAllyCombo(targetManager, comboModeMenu);
            }

            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.bloodthorn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.orchid))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blink))
            {
                this.ComboSleeper.ExtendSleep(0.2f);
                return true;
            }

            if (abilityHelper.UseAbility(this.solar))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.medallion))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.tag))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.punch))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.snowball))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shards))
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

            if (abilityHelper.UseAbility(this.bladeMail, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.kick))
            {
                return true;
            }

            return false;
        }

        public bool KickToAllyCombo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (this.Owner.HasModifier("modifier_tusk_snowball_movement"))
            {
                var ally = EntityManager9.Heroes.FirstOrDefault(
                    x => !x.Equals(this.Owner) && x.IsAlly(this.Owner) && x.IsAlive && !x.IsInvulnerable && x.Distance(this.Owner) < 200);

                if (ally != null)
                {
                    if (this.Owner.Attack(ally))
                    {
                        this.ComboSleeper.Sleep(0.1f);
                        return true;
                    }
                }
            }

            if (abilityHelper.UseAbility(this.punch))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.kick))
            {
                return true;
            }

            if (targetManager.Target.HasModifier("modifier_tusk_walrus_kick_slow"))
            {
                if (abilityHelper.UseAbilityIfNone(this.snowball))
                {
                    return true;
                }
            }

            if (!this.Owner.IsInvulnerable)
            {
                if (abilityHelper.UseAbilityIfNone(this.shards, this.snowball, this.punch, this.kick))
                {
                    return true;
                }

                if (abilityHelper.UseAbilityIfNone(this.tag, this.snowball, this.punch, this.kick))
                {
                    return true;
                }
            }

            if (abilityHelper.CanBeCasted(this.blink) && abilityHelper.CanBeCasted(this.kick, false)
                                                      && !abilityHelper.CanBeCasted(this.kick))
            {
                var position = targetManager.Target.Position.Extend2D(EntityManager9.EnemyFountain, 100);
                if (this.Owner.Distance(position) > this.blink.Ability.Range)
                {
                    return false;
                }

                if (abilityHelper.UseAbility(this.blink, position))
                {
                    return true;
                }
            }

            return false;
        }
    }
}