namespace O9K.AIO.Heroes.Enigma.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;

    using AIO.Abilities;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_enigma))]
    internal class Enigma : ControllableUnit
    {
        private ShieldAbility bkb;

        private BlackHole blackHole;

        private BlinkDaggerEnigma blink;

        private ShieldAbility ghost;

        private DisableAbility malefice;

        private AoeAbility pulse;

        private UntargetableAbility refresher;

        private UntargetableAbility refresherShard;

        private DebuffAbility shiva;

        public Enigma(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.enigma_malefice, x => this.malefice = new DisableAbility(x) },
                { AbilityId.enigma_midnight_pulse, x => this.pulse = new AoeAbility(x) },
                { AbilityId.enigma_black_hole, x => this.blackHole = new BlackHole(x) },

                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
                { AbilityId.item_ghost, x => this.ghost = new ShieldAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkDaggerEnigma(x) },
                { AbilityId.item_refresher, x => this.refresher = new UntargetableAbility(x) },
                { AbilityId.item_refresher_shard, x => this.refresherShard = new UntargetableAbility(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.CanBeCasted(this.blackHole))
            {
                if (abilityHelper.CanBeCasted(this.bkb, false, false))
                {
                    if (abilityHelper.ForceUseAbility(this.bkb))
                    {
                        return true;
                    }
                }

                if (abilityHelper.HasMana(this.pulse, this.blackHole) && abilityHelper.UseAbility(this.pulse))
                {
                    return true;
                }

                if (abilityHelper.HasMana(this.shiva, this.blackHole) && abilityHelper.UseAbility(this.shiva))
                {
                    return true;
                }

                if (abilityHelper.UseAbility(this.blackHole))
                {
                    return true;
                }
            }

            if (abilityHelper.CanBeCastedIfCondition(this.blink, this.blackHole))
            {
                if (abilityHelper.CanBeCasted(this.bkb, false, false))
                {
                    if (abilityHelper.ForceUseAbility(this.bkb))
                    {
                        return true;
                    }
                }

                if (abilityHelper.CanBeCasted(this.ghost, false, false))
                {
                    if (abilityHelper.ForceUseAbility(this.ghost))
                    {
                        return true;
                    }
                }
            }

            if (abilityHelper.UseAbilityIfCondition(this.blink, this.blackHole))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.refresher) || abilityHelper.CanBeCasted(this.refresherShard))
            {
                if (abilityHelper.CanBeCasted(this.blackHole, true, true, true, false) && !this.blackHole.Ability.IsReady)
                {
                    var useRefresher = abilityHelper.CanBeCasted(this.refresherShard) ? this.refresherShard : this.refresher;

                    if (abilityHelper.HasMana(this.blackHole, useRefresher))
                    {
                        if (abilityHelper.UseAbility(useRefresher))
                        {
                            return true;
                        }
                    }
                }
            }

            if (abilityHelper.UseAbility(this.malefice))
            {
                return true;
            }

            return false;
        }
    }
}