namespace O9K.AIO.Heroes.DarkWillow.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_dark_willow))]
    internal class DarkWillow : ControllableUnit
    {
        private DisableAbility atos;

        private NukeAbility bedlam;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private DebuffAbility crown;

        private EulsScepterOfDivinityDarkWillow eul;

        private ForceStaff force;

        private DisableAbility hex;

        private BrambleMaze maze;

        private DisableAbility moveCrown;

        private ShieldAbility moveRealm;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private ShadowRealm realm;

        private DisableAbility terror;

        private DebuffAbility urn;

        private DebuffAbility veil;

        private DebuffAbility vessel;

        public DarkWillow(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.dark_willow_bramble_maze, x => this.maze = new BrambleMaze(x) },
                { AbilityId.dark_willow_shadow_realm, x => this.realm = new ShadowRealm(x) },
                { AbilityId.dark_willow_cursed_crown, x => this.crown = new DebuffAbility(x) },
                { AbilityId.dark_willow_bedlam, x => this.bedlam = new NukeAbility(x) },
                { AbilityId.dark_willow_terrorize, x => this.terror = new DisableAbility(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_cyclone, x => this.eul = new EulsScepterOfDivinityDarkWillow(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_spirit_vessel, x => this.vessel = new DebuffAbility(x) },
                { AbilityId.item_urn_of_shadows, x => this.urn = new DebuffAbility(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.dark_willow_shadow_realm, x => this.moveRealm = new ShieldAbility(x));
            this.MoveComboAbilities.Add(AbilityId.dark_willow_cursed_crown, x => this.moveCrown = new DisableAbility(x));
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.blink, 600, 450))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 600, 450))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.atos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.orchid))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bloodthorn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.nullifier))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.crown))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.eul, this.crown))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.realm))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.maze))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bedlam))
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

            if (abilityHelper.UseAbility(this.terror))
            {
                return true;
            }

            return false;
        }

        public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            if (target != null && !this.Owner.HasAghanimsScepter)
            {
                if (this.realm.Casted)
                {
                    return base.Orbwalk(target, this.realm.ShouldAttack(target), move, comboMenu);
                }

                if (this.Owner.HasModifier("modifier_dark_willow_bedlam")
                    && this.Owner.Distance(target) > this.bedlam.Ability.Radius * 0.5f)
                {
                    return this.ForceMove(target, true);
                }
            }

            return base.Orbwalk(target, attack, move, comboMenu);
        }

        protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseDisables(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.moveCrown))
            {
                return true;
            }

            return false;
        }

        protected override bool MoveComboUseShields(AbilityHelper abilityHelper)
        {
            if (abilityHelper.UseMoveAbility(this.moveRealm))
            {
                return true;
            }

            return base.MoveComboUseShields(abilityHelper);
        }
    }
}