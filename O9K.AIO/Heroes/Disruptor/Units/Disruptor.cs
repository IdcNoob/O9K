namespace O9K.AIO.Heroes.Disruptor.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;

    using AIO.Abilities;
    using AIO.Abilities.Items;
    using AIO.Modes.Combo;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Helpers;

    using Ensage;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_disruptor))]
    internal class Disruptor : ControllableUnit, IDisposable
    {
        private BlinkAbility blink;

        private KineticField field;

        private ForceStaff force;

        private TargetableAbility glimpse;

        private ParticleEffect glimpseParticle;

        private StaticStorm storm;

        private NukeAbility thunder;

        private DebuffAbility veil;

        public Disruptor(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.disruptor_thunder_strike, x => this.thunder = new NukeAbility(x) },
                { AbilityId.disruptor_glimpse, x => this.glimpse = new TargetableAbility(x) },
                { AbilityId.disruptor_kinetic_field, x => this.field = new KineticField(x) },
                { AbilityId.disruptor_static_storm, x => this.storm = new StaticStorm(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.disruptor_kinetic_field, _ => this.field);

            Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.glimpse))
            {
                this.blink?.Sleeper.Sleep(3f);
                this.force?.Sleeper.Sleep(3f);
                return true;
            }

            if (abilityHelper.CanBeCasted(this.glimpse, false))
            {
                if (abilityHelper.UseAbility(this.blink, this.glimpse.Ability.CastRange, this.glimpse.Ability.CastRange - 500))
                {
                    return true;
                }
            }
            else
            {
                if (abilityHelper.UseAbility(this.blink, 800, 500))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.force, this.glimpse.Ability.CastRange, this.glimpse.Ability.CastRange - 500))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            var glimpseTime = this.glimpse.Ability.TimeSinceCasted;
            if (glimpseTime < 1.8)
            {
                if (this.glimpseParticle?.IsValid == true && !targetManager.Target.IsMagicImmune)
                {
                    var position = this.glimpseParticle.GetControlPoint(1);

                    if (abilityHelper.CanBeCasted(this.field, false))
                    {
                        if (this.field.UseAbility(position, this.ComboSleeper))
                        {
                            return true;
                        }
                    }

                    var glimpseParticleTime = this.glimpseParticle.GetControlPoint(2).X;
                    if (glimpseTime + 0.35f > glimpseParticleTime && abilityHelper.CanBeCasted(this.storm, false))
                    {
                        if (this.storm.UseAbility(position, targetManager, this.ComboSleeper))
                        {
                            return true;
                        }
                    }

                    if (this.Owner.Distance(position) > this.storm.Ability.CastRange - 100)
                    {
                        this.Owner.BaseUnit.Move(position.Extend2D(this.Owner.Position, 500));
                        return true;
                    }
                }

                this.OrbwalkSleeper.Sleep(0.1f);
                return true;
            }

            if (glimpseTime > 2 && abilityHelper.UseAbilityIfNone(this.field, this.glimpse))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.storm))
            {
                var fieldCastTime = this.field.Ability.TimeSinceCasted;
                if (fieldCastTime <= 4)
                {
                    if (this.storm.UseAbility(this.field.CastPosition, targetManager, this.ComboSleeper))
                    {
                        return true;
                    }
                }
                else
                {
                    if (abilityHelper.UseAbility(this.storm))
                    {
                        return true;
                    }
                }
            }

            if (abilityHelper.CanBeCasted(this.thunder))
            {
                var mana = this.Owner.Mana - this.thunder.Ability.ManaCost;

                if (abilityHelper.CanBeCasted(this.field))
                {
                    mana -= this.field.Ability.ManaCost;
                }

                if (abilityHelper.CanBeCasted(this.storm))
                {
                    mana -= this.storm.Ability.ManaCost;
                }

                if (mana > 0)
                {
                    abilityHelper.UseAbility(this.thunder);
                }
            }

            return false;
        }

        public void Dispose()
        {
            Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
        }

        protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseDisables(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.field))
            {
                return true;
            }

            return false;
        }

        private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
        {
            if (args.Name == "particles/units/heroes/hero_disruptor/disruptor_glimpse_targetend.vpcf")
            {
                this.glimpseParticle = args.ParticleEffect;
            }
        }
    }
}