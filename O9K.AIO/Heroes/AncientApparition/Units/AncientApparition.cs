namespace O9K.AIO.Heroes.AncientApparition.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_ancient_apparition))]
    internal class AncientApparition : ControllableUnit
    {
        private DisableAbility atos;

        private IceBlast blast;

        private DebuffAbility coldFeet;

        private DisableAbility eul;

        private DisableAbility hex;

        private TargetableAbility touch;

        private DebuffAbility urn;

        private DebuffAbility veil;

        private DebuffAbility vessel;

        private DebuffAbility vortex;

        public AncientApparition(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.ancient_apparition_cold_feet, x => this.coldFeet = new DebuffAbility(x) },
                { AbilityId.ancient_apparition_ice_vortex, x => this.vortex = new IceVortex(x) },
                { AbilityId.ancient_apparition_chilling_touch, x => this.touch = new ChillingTouch(x) },
                { AbilityId.ancient_apparition_ice_blast, x => this.blast = new IceBlast(x) },

                { AbilityId.item_cyclone, x => this.eul = new DisableAbility(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
                { AbilityId.item_spirit_vessel, x => this.vessel = new DebuffAbility(x) },
                { AbilityId.item_urn_of_shadows, x => this.urn = new DebuffAbility(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (this.blast?.Release(targetManager, this.ComboSleeper) == true)
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.coldFeet))
            {
                this.ComboSleeper.ExtendSleep(0.3f);
                return true;
            }

            if (abilityHelper.UseAbility(this.vortex))
            {
                return true;
            }

            var coldFeedModifier = targetManager.Target.GetModifier("modifier_cold_feet");
            if (coldFeedModifier?.ElapsedTime < 1)
            {
                if (abilityHelper.UseAbility(this.eul))
                {
                    this.ComboSleeper.ExtendSleep(0.5f);
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.touch))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.atos))
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

            if (abilityHelper.UseAbility(this.blast))
            {
                return true;
            }

            return true;
        }

        protected override bool UseOrbAbility(Unit9 target, ComboModeMenu comboMenu)
        {
            return false;
        }
    }
}