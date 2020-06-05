namespace O9K.AIO.Heroes.Razor.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_razor))]
    internal class Razor : ControllableUnit
    {
        private DisableAbility atos;

        private ShieldAbility bkb;

        private ShieldAbility bladeMail;

        private ForceStaff force;

        private DisableAbility halberd;

        private DebuffAbility link;

        private SpeedBuffAbility phase;

        private HurricanePike pike;

        private NukeAbility plasma;

        private DebuffAbility shiva;

        private AoeAbility storm;

        public Razor(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.razor_plasma_field, x => this.plasma = new NukeAbility(x) },
                { AbilityId.razor_static_link, x => this.link = new StaticLink(x) },
                { AbilityId.razor_eye_of_the_storm, x => this.storm = new AoeAbility(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_hurricane_pike, x => this.pike = new HurricanePike(x) },
                { AbilityId.item_blade_mail, x => this.bladeMail = new ShieldAbility(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_heavens_halberd, x => this.halberd = new DisableAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.bkb, 600))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bladeMail, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.plasma))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.atos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.halberd))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 550, 300))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.pike, 550, 300))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.link))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.storm))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            return false;
        }
    }
}