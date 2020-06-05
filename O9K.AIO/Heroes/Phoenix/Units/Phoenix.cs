namespace O9K.AIO.Heroes.Phoenix.Units
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
    using Core.Helpers;
    using Core.Logger;
    using Core.Prediction.Data;

    using Ensage;

    using SharpDX;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_phoenix))]
    internal class Phoenix : ControllableUnit
    {
        private DisableAbility atos;

        private IcarusDive dive;

        private DisableAbility halberd;

        private DisableAbility hex;

        private Supernova nova;

        private SunRay ray;

        private ShivasGuard shiva;

        private FireSpirits spirits;

        private DebuffAbility urn;

        private DebuffAbility veil;

        private DebuffAbility vessel;

        public Phoenix(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.phoenix_icarus_dive, x => this.dive = new IcarusDive(x) },
                { AbilityId.phoenix_launch_fire_spirit, x => this.spirits = new FireSpirits(x) },
                { AbilityId.phoenix_sun_ray, x => this.ray = new SunRay(x) },
                { AbilityId.phoenix_supernova, x => this.nova = new Supernova(x) },

                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new ShivasGuard(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
                { AbilityId.item_spirit_vessel, x => this.vessel = new DebuffAbility(x) },
                { AbilityId.item_urn_of_shadows, x => this.urn = new DebuffAbility(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_heavens_halberd, x => this.halberd = new DisableAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.phoenix_icarus_dive, _ => this.dive);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbilityIfCondition(this.dive, this.nova))
            {
                if (abilityHelper.CanBeCasted(this.shiva, false))
                {
                    this.shiva.ForceUseAbility(targetManager, this.ComboSleeper);
                }

                return true;
            }

            if (!comboModeMenu.IsHarassCombo)
            {
                if (this.dive.AutoStop(targetManager))
                {
                    this.ComboSleeper.Sleep(0.1f);
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.halberd))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.atos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.urn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.vessel))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.spirits))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.nova, false) && (this.dive.IsFlying || this.Owner.Distance(targetManager.Target) < 600))
            {
                var spiritsCount = this.Owner.GetModifier("modifier_phoenix_fire_spirit_count")?.StackCount ?? 0;
                var enemies = targetManager.EnemyHeroes.Where(x => x.Distance(this.Owner) < this.spirits.Ability.CastRange).ToList();
                var count = Math.Min(spiritsCount, enemies.Count);

                for (var i = 0; i < count; i++)
                {
                    this.spirits.Ability.UseAbility(enemies[i], HitChance.Low, i != 0);
                }

                if (count > 0)
                {
                    this.ComboSleeper.Sleep(0.2f);
                    return true;
                }

                if ((this.Owner.Distance(targetManager.Target) < 600
                     || (this.dive.CastPosition != Vector3.Zero
                         && this.Owner.Distance(this.dive.CastPosition) < this.nova.Ability.CastRange))
                    && abilityHelper.UseAbility(this.nova))
                {
                    return true;
                }
            }

            if (!this.dive.IsFlying
                && (!abilityHelper.CanBeCasted(this.spirits) || this.Owner.HasModifier("modifier_phoenix_fire_spirit_count")))
            {
                if (comboModeMenu.IsAbilityEnabled(this.ray.Ability))
                {
                    if (this.ray.AutoControl(targetManager, this.ComboSleeper, 0.6f))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            try
            {
                if (this.ray.IsActive)
                {
                    return false;
                }

                return base.Orbwalk(target, attack, move, comboMenu);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }
        }

        public void SunRayAllyCombo(TargetManager targetManager)
        {
            if (this.ComboSleeper.IsSleeping)
            {
                return;
            }

            if (this.ray.Ability.CanBeCasted())
            {
                this.ray.UseAbility(targetManager, this.ComboSleeper, true);
                return;
            }

            if (this.ray.IsActive)
            {
                if (this.ray.AutoControl(targetManager, this.ComboSleeper, 0.85f))
                {
                    return;
                }
            }

            return;
        }

        protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseBlinks(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.dive, false, false))
            {
                this.dive.Ability.UseAbility(Game.MousePosition);
                this.ComboSleeper.Sleep(0.3f);
                this.dive.Sleeper.Sleep(0.3f);
                return true;
            }

            if (this.dive.AutoStop(null))
            {
                this.ComboSleeper.Sleep(0.1f);
                return true;
            }

            return false;
        }
    }
}