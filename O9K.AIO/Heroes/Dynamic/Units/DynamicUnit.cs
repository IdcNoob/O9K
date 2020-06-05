namespace O9K.AIO.Heroes.Dynamic.Units
{
    using System.Collections.Generic;

    using Abilities;
    using Abilities.Blinks;
    using Abilities.Buffs;
    using Abilities.Debuffs;
    using Abilities.Disables;
    using Abilities.Harasses;
    using Abilities.Nukes;
    using Abilities.Shields;
    using Abilities.Specials;

    using AIO.Modes.Combo;
    using AIO.Modes.MoveCombo;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using TargetManager;

    internal class DynamicUnit : ControllableUnit
    {
        private readonly List<IOldAbilityGroup> groups = new List<IOldAbilityGroup>();

        private readonly HashSet<AbilityId> ignoredAbilities = new HashSet<AbilityId>
        {
            AbilityId.ember_spirit_activate_fire_remnant,
            AbilityId.item_tpscroll,
            AbilityId.item_recipe_travel_boots,
            AbilityId.item_recipe_travel_boots_2,
            AbilityId.item_enchanted_mango,
            AbilityId.item_ghost,
            AbilityId.item_hand_of_midas,
            AbilityId.item_dust,
            AbilityId.item_mekansm,
            AbilityId.item_guardian_greaves,
            AbilityId.item_glimmer_cape,
            AbilityId.item_sphere,
            AbilityId.item_bfury,
            AbilityId.item_quelling_blade,
            AbilityId.item_shadow_amulet,
            AbilityId.item_magic_stick,
            AbilityId.item_magic_wand,
            AbilityId.item_bloodstone,
            AbilityId.item_branches,
        };

        public DynamicUnit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu, BaseHero baseHero)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.Blinks = new BlinkAbilityGroup(baseHero);
            this.Nukes = new NukeAbilityGroup(baseHero);
            this.Debuffs = new DebuffAbilityGroup(baseHero);
            this.Disables = new DisableAbilityGroup(baseHero);
            this.Buffs = new BuffAbilityGroup(baseHero);
            this.Harasses = new HarassAbilityGroup(baseHero);
            this.Shields = new ShieldAbilityGroup(baseHero);
            this.Specials = new SpecialAbilityGroup(baseHero);

            this.Blinks.Disables = this.Disables;
            this.Blinks.Specials = this.Specials;
            this.Disables.Specials = this.Specials;
            this.Buffs.Blinks = this.Blinks;
            this.Disables.Blinks = this.Blinks;
            this.Shields.Blinks = this.Blinks;

            this.groups.Add(this.Nukes);
            this.groups.Add(this.Debuffs);
            this.groups.Add(this.Blinks);
            this.groups.Add(this.Disables);
            this.groups.Add(this.Buffs);
            this.groups.Add(this.Harasses);
            this.groups.Add(this.Shields);
            this.groups.Add(this.Specials);
        }

        public BlinkAbilityGroup Blinks { get; }

        public BuffAbilityGroup Buffs { get; }

        public DebuffAbilityGroup Debuffs { get; }

        public DisableAbilityGroup Disables { get; }

        public HarassAbilityGroup Harasses { get; }

        public NukeAbilityGroup Nukes { get; }

        public ShieldAbilityGroup Shields { get; }

        public SpecialAbilityGroup Specials { get; }

        public override void AddAbility(ActiveAbility ability, IEnumerable<ComboModeMenu> comboMenus, MoveComboModeMenu moveMenu)
        {
            if (this.ignoredAbilities.Contains(ability.Id))
            {
                return;
            }

            foreach (var group in this.groups)
            {
                if (group.AddAbility(ability))
                {
                    foreach (var comboModeMenu in comboMenus)
                    {
                        comboModeMenu.AddComboAbility(ability);
                    }
                }
            }

            this.AddMoveComboAbility(ability, moveMenu);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var target = targetManager.Target;

            if (this.Buffs.Use(target, comboModeMenu))
            {
                return true;
            }

            if (this.Shields.Use(target, comboModeMenu))
            {
                return true;
            }

            if (this.Debuffs.UseAmplifiers(target, comboModeMenu))
            {
                return true;
            }

            if (this.Disables.UseBlinkDisable(target, comboModeMenu))
            {
                return true;
            }

            if (this.Disables.Use(target, comboModeMenu))
            {
                return true;
            }

            if (this.Debuffs.Use(target, comboModeMenu))
            {
                return true;
            }

            if (this.Nukes.Use(target, comboModeMenu))
            {
                return true;
            }

            if (this.Harasses.Use(target, comboModeMenu))
            {
                return true;
            }

            if (this.Specials.Use(target, comboModeMenu))
            {
                return true;
            }

            if (this.Blinks.Use(target, comboModeMenu))
            {
                return true;
            }

            return false;
        }

        public override void RemoveAbility(ActiveAbility ability)
        {
            foreach (var group in this.groups)
            {
                group.RemoveAbility(ability);
            }
        }
    }
}