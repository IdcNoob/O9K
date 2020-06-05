namespace O9K.AIO.Heroes.CrystalMaiden.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_crystal_maiden))]
    internal class CrystalMaiden : ControllableUnit
    {
        private DisableAbility atos;

        private ShieldAbility bkb;

        private BlinkAbility blink;

        private NukeAbility field;

        private ForceStaff force;

        private DisableAbility frostbite;

        private ShieldAbility glimmer;

        private NukeAbility nova;

        public CrystalMaiden(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.crystal_maiden_crystal_nova, x => this.nova = new NukeAbility(x) },
                { AbilityId.crystal_maiden_frostbite, x => this.frostbite = new DisableAbility(x) },
                { AbilityId.crystal_maiden_freezing_field, x => this.field = new FreezingField(x) },

                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkDaggerCM(x) },
                { AbilityId.item_glimmer_cape, x => this.glimmer = new ShieldAbility(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.crystal_maiden_frostbite, _ => this.frostbite);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.CanBeCasted(this.field))
            {
                if (abilityHelper.CanBeCasted(this.bkb, false, false))
                {
                    if (abilityHelper.ForceUseAbility(this.bkb))
                    {
                        return true;
                    }
                }

                if (abilityHelper.HasMana(this.field, this.glimmer) && abilityHelper.CanBeCasted(this.glimmer, false, false))
                {
                    if (abilityHelper.ForceUseAbility(this.glimmer))
                    {
                        return true;
                    }
                }

                if (abilityHelper.HasMana(this.field, this.atos) && abilityHelper.UseAbility(this.atos))
                {
                    return true;
                }

                if (abilityHelper.HasMana(this.field, this.frostbite) && abilityHelper.UseAbility(this.frostbite))
                {
                    return true;
                }

                if (abilityHelper.UseAbility(this.field))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbilityIfCondition(this.blink, this.field))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 600, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.atos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.frostbite))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.nova))
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

            if (abilityHelper.UseMoveAbility(this.frostbite))
            {
                return true;
            }

            return false;
        }
    }
}