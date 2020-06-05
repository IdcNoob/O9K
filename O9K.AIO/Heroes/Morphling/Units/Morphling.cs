namespace O9K.AIO.Heroes.Morphling.Units
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Abilities;

    using AIO.Abilities;
    using AIO.Abilities.Items;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    using BaseMorphling = Core.Entities.Heroes.Unique.Morphling;

    [UnitName(nameof(HeroId.npc_dota_hero_morphling))]
    internal class Morphling : ControllableUnit, IDisposable
    {
        private readonly BaseMorphling morphling;

        private readonly MultiSleeper morphlingAbilitySleeper = new MultiSleeper();

        private NukeAbility agi;

        private DisableAbility bloodthorn;

        private NukeAbility ethereal;

        private BuffAbility manta;

        //private Morph morph;

        private DisableAbility orchid;

        //private Replicate replicate;

        private DisableAbility str;

        private NukeAbility wave;

        private BlinkAbility waveBlink;

        public Morphling(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.morphling = (BaseMorphling)owner;

            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.morphling_waveform, x => this.wave = new Wave(x) },
                { AbilityId.morphling_adaptive_strike_str, x => this.str = new DisableAbility(x) },
                { AbilityId.morphling_adaptive_strike_agi, x => this.agi = new NukeAbility(x) },

                //{ AbilityId.morphling_replicate, x => this.replicate = new Replicate(x) },
                //{ AbilityId.morphling_morph_replicate, x => this.morph = new Morph(x) },

                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_ethereal_blade, x => this.ethereal = new NukeAbility(x) },
                { AbilityId.item_manta, x => this.manta = new MantaStyle(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.morphling_waveform, x => this.waveBlink = new BlinkAbility(x));

            Player.OnExecuteOrder += this.OnExecuteOrder;
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            if (this.morphling.IsMorphed)
            {
                return false;
            }

            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.orchid))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bloodthorn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.ethereal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.wave))
            {
                return true;
            }

            if (targetManager.Target.IsChanneling)
            {
                if (abilityHelper.UseAbility(this.str))
                {
                    return true;
                }
            }

            if (this.Owner.TotalAgility > this.Owner.TotalStrength)
            {
                if (abilityHelper.UseAbility(this.agi))
                {
                    return true;
                }
            }
            else
            {
                if (abilityHelper.UseAbility(this.str))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.manta))
            {
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            Player.OnExecuteOrder -= this.OnExecuteOrder;
        }

        public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            if (this.morphling.IsMorphed)
            {
                return false;
            }

            return base.Orbwalk(target, attack, move, comboMenu);
        }

        protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseBlinks(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.waveBlink))
            {
                return true;
            }

            return false;
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (!this.morphling.IsMorphed)
                {
                    return;
                }

                if (!args.Process || args.IsQueued || !args.Entities.Contains(this.Owner.BaseUnit))
                {
                    return;
                }

                var order = args.OrderId;
                if (order != OrderId.Ability && order != OrderId.AbilityLocation && order != OrderId.AbilityTarget)
                {
                    return;
                }

                var ability = args.Ability;
                this.morphlingAbilitySleeper.Sleep(ability.Handle, ability.CooldownLength);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}