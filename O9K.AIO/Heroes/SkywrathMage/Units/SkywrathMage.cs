namespace O9K.AIO.Heroes.SkywrathMage.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_skywrath_mage))]
    internal class SkywrathMage : ControllableUnit
    {
        private DisableAbility atos;

        private BlinkAbility blink;

        private NukeAbility bolt;

        private DebuffAbility concussive;

        private NukeAbility dagon;

        private EtherealBlade ethereal;

        private MysticFlare flare;

        private ForceStaff force;

        private HexSkywrath hex;

        private Nullifier nullifier;

        private DebuffAbility seal;

        private DebuffAbility veil;

        public SkywrathMage(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.skywrath_mage_arcane_bolt, x => this.bolt = new NukeAbility(x) },
                { AbilityId.skywrath_mage_ancient_seal, x => this.seal = new DebuffAbility(x) },
                { AbilityId.skywrath_mage_concussive_shot, x => this.concussive = new DebuffAbility(x) },
                { AbilityId.skywrath_mage_mystic_flare, x => this.flare = new MysticFlare(x) },

                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_sheepstick, x => this.hex = new HexSkywrath(x) },
                { AbilityId.item_ethereal_blade, x => this.ethereal = new EtherealBlade(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_dagon_5, x => this.dagon = new NukeAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.skywrath_mage_ancient_seal, _ => this.seal);
            this.MoveComboAbilities.Add(AbilityId.skywrath_mage_concussive_shot, _ => this.concussive);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.atos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.nullifier))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.seal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.ethereal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.dagon))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.concussive))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blink, 850, 600))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 850, 600))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfNone(this.flare, this.atos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bolt))
            {
                return true;
            }

            return false;
        }

        protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseDisables(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.seal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.concussive))
            {
                return true;
            }

            return false;
        }
    }
}