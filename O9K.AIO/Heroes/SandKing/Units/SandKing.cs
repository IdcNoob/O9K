namespace O9K.AIO.Heroes.SandKing.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_sand_king))]
    internal class SandKing : ControllableUnit
    {
        private BlinkAbility blink;

        private Burrowstrike burrow;

        private BlinkAbility burrowBlink;

        private Epicenter epicenter;

        private ForceStaff force;

        private DisableAbility hex;

        private UntargetableAbility sandstorm;

        private DebuffAbility shiva;

        private DebuffAbility veil;

        public SandKing(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.sandking_burrowstrike, x => this.burrow = new Burrowstrike(x) },
                { AbilityId.sandking_sand_storm, x => this.sandstorm = new UntargetableAbility(x) },
                { AbilityId.sandking_epicenter, x => this.epicenter = new Epicenter(x) },

                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.sandking_burrowstrike, x => this.burrowBlink = new BlinkAbility(x));
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.CanBeCastedIfCondition(this.epicenter, this.blink, this.burrow))
            {
                if (abilityHelper.UseAbility(this.sandstorm))
                {
                    return true;
                }

                if (abilityHelper.UseAbility(this.epicenter))
                {
                    return true;
                }
            }

            if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink, 300))
            {
                return true;
            }

            if (abilityHelper.UseBlinkLineCombo(this.blink, this.burrow))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.burrow))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 800, this.burrow.Ability.CastRange))
            {
                return true;
            }

            var distance = this.Owner.Distance(targetManager.Target);

            if (distance < 450 && abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (distance < 450 && abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (this.Owner.HasModifier("modifier_sand_king_epicenter") && distance < 450)
            {
                if (abilityHelper.UseAbilityIfNone(this.sandstorm, this.epicenter, this.burrow))
                {
                    this.ComboSleeper.Sleep(0.2f);
                    return true;
                }
            }

            return false;
        }

        protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseBlinks(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.burrowBlink))
            {
                return true;
            }

            return false;
        }
    }
}