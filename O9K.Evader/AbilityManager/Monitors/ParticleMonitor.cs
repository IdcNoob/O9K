namespace O9K.Evader.AbilityManager.Monitors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Abilities.Base.Evadable;
    using Abilities.Base.Evadable.Components;

    using Core.Entities.Heroes;
    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;

    internal class ParticleMonitor : IDisposable
    {
        private readonly Dictionary<string, AbilityId> addedParticles = new Dictionary<string, AbilityId>
        {
            { "particles/units/heroes/hero_pudge/pudge_meathook.vpcf", AbilityId.pudge_meat_hook },
            { "particles/econ/items/pudge/pudge_trapper_beam_chain/pudge_nx_meathook.vpcf", AbilityId.pudge_meat_hook },
            { "particles/econ/items/pudge/pudge_hook_whale/pudge_meathook_whale.vpcf", AbilityId.pudge_meat_hook },
            { "particles/econ/items/pudge/pudge_ti6_immortal/pudge_ti6_meathook.vpcf", AbilityId.pudge_meat_hook },
            { "particles/econ/items/pudge/pudge_ti6_immortal_gold/pudge_ti6_meathook_gold.vpcf", AbilityId.pudge_meat_hook },
            { "particles/econ/items/pudge/pudge_ti6_immortal/pudge_ti6_witness_meathook.vpcf", AbilityId.pudge_meat_hook },
            { "particles/econ/items/pudge/pudge_scorching_talon/pudge_scorching_talon_meathook.vpcf", AbilityId.pudge_meat_hook },
            { "particles/econ/items/pudge/pudge_dragonclaw/pudge_meathook_dragonclaw.vpcf", AbilityId.pudge_meat_hook },
            { "particles/econ/items/pudge/pudge_harvester/pudge_meathook_harvester.vpcf", AbilityId.pudge_meat_hook },
            { "particles/units/heroes/hero_pudge/pudge_meathook_impact.vpcf", AbilityId.pudge_meat_hook },
            { "particles/econ/items/pudge/pudge_ti6_immortal/pudge_meathook_impact_ti6.vpcf", AbilityId.pudge_meat_hook },
            { "particles/econ/items/pudge/pudge_ti6_immortal/pudge_meathook_witness_impact_ti6.vpcf", AbilityId.pudge_meat_hook },
            { "particles/units/heroes/hero_dark_willow/dark_willow_wisp_spell.vpcf", AbilityId.dark_willow_terrorize },
            { "particles/units/heroes/hero_invoker/invoker_emp.vpcf", AbilityId.invoker_emp },
            { "particles/units/heroes/hero_batrider/batrider_flamebreak.vpcf", AbilityId.batrider_flamebreak },
            { "particles/units/heroes/hero_slark/slark_pounce_trail.vpcf", AbilityId.slark_pounce },
            { "particles/econ/items/slark/slark_ti6_blade/slark_ti6_pounce_trail.vpcf", AbilityId.slark_pounce },
            { "particles/econ/items/slark/slark_ti6_blade/slark_ti6_pounce_trail_gold.vpcf", AbilityId.slark_pounce },
            { "particles/units/heroes/hero_windrunner/windrunner_spell_powershot_channel.vpcf", AbilityId.windrunner_powershot },
            {
                "particles/econ/items/windrunner/windrunner_ti6/windrunner_spell_powershot_channel_ti6.vpcf", AbilityId.windrunner_powershot
            },
            { "particles/units/heroes/hero_venomancer/venomancer_venomous_gale_mouth.vpcf", AbilityId.venomancer_venomous_gale },
            {
                "particles/econ/items/venomancer/veno_ti8_immortal_head/veno_ti8_immortal_gale_mouth.vpcf",
                AbilityId.venomancer_venomous_gale
            },
            { "particles/units/heroes/hero_rattletrap/rattletrap_hookshot.vpcf", AbilityId.rattletrap_hookshot },
            { "particles/units/heroes/hero_rattletrap/rattletrap_rocket_flare.vpcf", AbilityId.rattletrap_rocket_flare },
            { "particles/econ/items/clockwerk/clockwerk_paraflare/clockwerk_para_rocket_flare.vpcf", AbilityId.rattletrap_rocket_flare },
            { "particles/units/heroes/hero_disruptor/disruptor_glimpse_targetstart.vpcf", AbilityId.disruptor_glimpse },
            { "particles/units/heroes/hero_grimstroke/grimstroke_darkartistry_proj.vpcf", AbilityId.grimstroke_dark_artistry },
            //  { "particles/econ/items/grimstroke/ti9_immortal/gs_ti9_artistry_proj.vpcf", AbilityId.grimstroke_dark_artistry },
            { "particles/units/heroes/hero_pangolier/pangolier_tailthump_cast.vpcf", AbilityId.pangolier_shield_crash },
            {
                "particles/econ/items/pangolier/pangolier_ti8_immortal/pangolier_ti8_immortal_shield_crash_cast.vpcf",
                AbilityId.pangolier_shield_crash
            },
            { "particles/units/heroes/hero_phoenix/phoenix_fire_spirit_launch.vpcf", AbilityId.phoenix_launch_fire_spirit },
            { "particles/units/heroes/hero_phoenix/phoenix_fire_spirit_ground.vpcf", AbilityId.phoenix_launch_fire_spirit },
            { "particles/units/heroes/hero_void_spirit/pulse/void_spirit_pulse.vpcf", AbilityId.void_spirit_resonant_pulse },
            { "particles/units/heroes/hero_techies/techies_blast_off_trail.vpcf", AbilityId.techies_suicide },
        };

        private readonly List<EvadableAbility> evadableAbilities;

        private readonly HashSet<string> knownParticles = new HashSet<string>
        {
            "particles/units/heroes/hero_pudge/pudge_meathook.vpcf",
            "particles/econ/items/pudge/pudge_trapper_beam_chain/pudge_nx_meathook.vpcf",
            "particles/econ/items/pudge/pudge_hook_whale/pudge_meathook_whale.vpcf",
            "particles/econ/items/pudge/pudge_ti6_immortal/pudge_ti6_meathook.vpcf",
            "particles/econ/items/pudge/pudge_ti6_immortal_gold/pudge_ti6_meathook_gold.vpcf",
            "particles/econ/items/pudge/pudge_ti6_immortal/pudge_ti6_witness_meathook.vpcf",
            "particles/econ/items/pudge/pudge_scorching_talon/pudge_scorching_talon_meathook.vpcf",
            "particles/econ/items/pudge/pudge_dragonclaw/pudge_meathook_dragonclaw.vpcf",
            "particles/econ/items/pudge/pudge_harvester/pudge_meathook_harvester.vpcf",
            "particles/units/heroes/hero_pudge/pudge_meathook_impact.vpcf",
            "particles/econ/items/pudge/pudge_ti6_immortal/pudge_meathook_impact_ti6.vpcf",
            "particles/econ/items/pudge/pudge_ti6_immortal/pudge_meathook_witness_impact_ti6.vpcf",
            "particles/units/heroes/hero_bristleback/bristleback_quill_spray.vpcf",
            "particles/econ/items/bristleback/bristle_spikey_spray/bristle_spikey_quill_spray.vpcf",
            "particles/econ/items/bristleback/bristle_spikey_spray/bristle_spikey_quill_spray_hit_creep.vpcf", //ignore
            "particles/units/heroes/hero_bristleback/bristleback_quill_spray_impact.vpcf", //ignore
            "particles/units/heroes/hero_bristleback/bristleback_quill_spray_hit.vpcf", //ignore
            "particles/units/heroes/hero_bristleback/bristleback_quill_spray_hit_creep.vpcf", //ignore
            "particles/units/heroes/hero_dark_willow/dark_willow_wisp_spell.vpcf",
            "particles/units/heroes/hero_dark_willow/dark_willow_wisp_spell_channel.vpcf", //ignore
            "particles/units/heroes/hero_invoker/invoker_emp.vpcf",
            "particles/units/heroes/hero_batrider/batrider_flamebreak.vpcf",
            "particles/units/heroes/hero_slark/slark_pounce_trail.vpcf",
            "particles/units/heroes/hero_venomancer/venomancer_poison_nova.vpcf",
            "particles/econ/items/slark/slark_ti6_blade/slark_ti6_pounce_trail.vpcf",
            // "particles/econ/items/grimstroke/ti9_immortal/gs_ti9_artistry_proj.vpcf",
            "particles/econ/items/slark/slark_ti6_blade/slark_ti6_pounce_trail_gold.vpcf",
            "particles/units/heroes/hero_windrunner/windrunner_spell_powershot_channel.vpcf",
            "particles/econ/items/windrunner/windrunner_ti6/windrunner_spell_powershot_channel_ti6.vpcf",
            "particles/units/heroes/hero_venomancer/venomancer_venomous_gale_mouth.vpcf",
            "particles/econ/items/venomancer/veno_ti8_immortal_head/veno_ti8_immortal_gale_mouth.vpcf",
            "particles/units/heroes/hero_ancient_apparition/ancient_apparition_ice_blast_final.vpcf",
            "particles/econ/items/ancient_apparition/aa_blast_ti_5/ancient_apparition_ice_blast_final_ti5.vpcf",
            "particles/units/heroes/hero_rattletrap/rattletrap_hookshot.vpcf",
            "particles/units/heroes/hero_rattletrap/rattletrap_rocket_flare.vpcf",
            "particles/econ/items/clockwerk/clockwerk_paraflare/clockwerk_para_rocket_flare.vpcf",
            "particles/econ/items/clockwerk/clockwerk_paraflare/clockwerk_para_rocket_flare_illumination.vpcf", //ignore
            "particles/units/heroes/hero_rattletrap/rattletrap_rocket_flare_illumination.vpcf", //ignore
            "particles/units/heroes/hero_disruptor/disruptor_glimpse_targetstart.vpcf",
            "particles/units/heroes/hero_invoker/invoker_chaos_meteor_fly.vpcf",
            "particles/units/heroes/hero_grimstroke/grimstroke_darkartistry_proj.vpcf",
            "particles/units/heroes/hero_gyrocopter/gyro_calldown_first.vpcf",
            "particles/units/heroes/hero_pangolier/pangolier_tailthump_cast.vpcf",
            "particles/econ/items/pangolier/pangolier_ti8_immortal/pangolier_ti8_immortal_shield_crash_cast.vpcf",
            "particles/units/heroes/hero_phoenix/phoenix_fire_spirit_launch.vpcf",
            "particles/units/heroes/hero_phoenix/phoenix_fire_spirits.vpcf", //ignore
            "particles/units/heroes/hero_phoenix/phoenix_fire_spirit_ground.vpcf",
            "particles/units/heroes/hero_phoenix/phoenix_fire_spirit_burn.vpcf", //ignore
            "particles/units/heroes/hero_phoenix/phoenix_fire_spirit_burn_creep.vpcf", //ignore
            "particles/units/heroes/hero_sandking/sandking_burrowstrike.vpcf",
            "particles/econ/items/sand_king/sandking_barren_crown/sandking_rubyspire_burrowstrike.vpcf",
        };

        private readonly Owner owner;

        private readonly Dictionary<string, AbilityId> particles = new Dictionary<string, AbilityId>
        {
            { "burrowstrike", AbilityId.pudge_meat_hook },
            { "meathook", AbilityId.pudge_meat_hook },
            { "quill_spray.vpcf", AbilityId.bristleback_quill_spray },
            { "wisp_spell.vpcf", AbilityId.dark_willow_terrorize },
            { "_emp.vpcf", AbilityId.invoker_emp },
            { "_flamebreak.vpcf", AbilityId.batrider_flamebreak },
            { "pounce_trail", AbilityId.slark_pounce },
            { "powershot_channel", AbilityId.windrunner_powershot },
            { "gale_mouth", AbilityId.venomancer_venomous_gale },
            { "poison_nova", AbilityId.venomancer_poison_nova },
            //{ "whirling_axe_melee", "troll_warlord_whirling_axes_melee" },
            { "ice_blast_final", AbilityId.ancient_apparition_ice_blast },
            { "hookshot", AbilityId.rattletrap_hookshot },
            { "rocket_flare", AbilityId.rattletrap_rocket_flare },
            { "glimpse_targetstart", AbilityId.disruptor_glimpse },
            { "chaos_meteor_fly", AbilityId.invoker_chaos_meteor },
            { "darkartistry_proj", AbilityId.grimstroke_dark_artistry },
            { "calldown_first", AbilityId.gyrocopter_call_down },
            { "tailthump_cast", AbilityId.pangolier_shield_crash },
            { "shield_crash_cast", AbilityId.pangolier_shield_crash },
            { "sprout", AbilityId.furion_sprout },
            //{ "bouldersmash_caster", "earth_spirit_boulder_smash" },
            //{ "bouldersmash_target", "earth_spirit_boulder_smash" },
            //{ "rollingboulder", "earth_spirit_rolling_boulder" },
            //{ "geomagentic_grip_target", "earth_spirit_geomagnetic_grip" },
            //{ "sleight_of_fist_cast", "ember_spirit_sleight_of_fist" },
            //{ "_illuminate.vpcf", "keeper_of_the_light_illuminate" },
            //{ "_illuminate_charge.vpcf", "keeper_of_the_light_illuminate" },
            { "fire_spirit", AbilityId.phoenix_launch_fire_spirit },
            //{ "spring_channel", "monkey_king_primal_spring" },
            //{ "jump_treelaunch_ring", "monkey_king_primal_spring" },
            //{ "_cloud.vpcf", "zuus_cloud" },
        };

        private readonly Dictionary<string, AbilityId> releasedParticles = new Dictionary<string, AbilityId>
        {
            { "particles/units/heroes/hero_bristleback/bristleback_quill_spray.vpcf", AbilityId.bristleback_quill_spray },
            { "particles/units/heroes/hero_venomancer/venomancer_poison_nova.vpcf", AbilityId.venomancer_poison_nova },
            { "particles/econ/items/bristleback/bristle_spikey_spray/bristle_spikey_quill_spray.vpcf", AbilityId.bristleback_quill_spray },
            {
                "particles/units/heroes/hero_ancient_apparition/ancient_apparition_ice_blast_final.vpcf",
                AbilityId.ancient_apparition_ice_blast
            },
            {
                "particles/econ/items/ancient_apparition/aa_blast_ti_5/ancient_apparition_ice_blast_final_ti5.vpcf",
                AbilityId.ancient_apparition_ice_blast
            },
            { "particles/units/heroes/hero_invoker/invoker_chaos_meteor_fly.vpcf", AbilityId.invoker_chaos_meteor },
            { "particles/units/heroes/hero_gyrocopter/gyro_calldown_first.vpcf", AbilityId.gyrocopter_call_down },
            { "particles/units/heroes/hero_sandking/sandking_burrowstrike.vpcf", AbilityId.sandking_burrowstrike },
            {
                "particles/econ/items/sand_king/sandking_barren_crown/sandking_rubyspire_burrowstrike.vpcf", AbilityId.sandking_burrowstrike
            },
            { "particles/units/heroes/hero_furion/furion_sprout.vpcf", AbilityId.furion_sprout },
        };

        public ParticleMonitor(List<EvadableAbility> evadable)
        {
            this.evadableAbilities = evadable;
            this.owner = EntityManager9.Owner;

            Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
            Entity.OnParticleEffectReleased += this.OnParticleEffectReleased;

            //Entity.OnParticleEffectAdded += this.OnParticleEffectAddedTemp;
        }

        public void Dispose()
        {
            Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
            Entity.OnParticleEffectReleased -= this.OnParticleEffectReleased;
        }

        private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
        {
            try
            {
                var particleName = args.Name;

                if (!this.addedParticles.TryGetValue(particleName, out var abilityId))
                {
                    return;
                }

                if (!(this.evadableAbilities.Find(x => x.Ability.Id == abilityId) is IParticle ability))
                {
                    return;
                }

                var allyAbility = EntityManager9.Heroes.Any(
                    x => x.IsAlly(this.owner.Team) && (x.Id == HeroId.npc_dota_hero_rubick || x.Id == HeroId.npc_dota_hero_morphling)
                                                   && x.IsAlive && x.Abilities.Any(z => z.Id == abilityId && z.TimeSinceCasted < 0.5f));

                if (allyAbility)
                {
                    return;
                }

                ability.AddParticle(args.ParticleEffect, particleName);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnParticleEffectAddedTemp(Entity sender, ParticleEffectAddedEventArgs args)
        {
            //todo delete

            try
            {
                var name = args.Name;

                foreach (var key in this.particles.Keys)
                {
                    if (!name.Contains(key) || this.knownParticles.Contains(name))
                    {
                        continue;
                    }

                    Logger.Error("Particle", name);
                    break;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnParticleEffectReleased(Entity sender, ParticleEffectReleasedEventArgs args)
        {
            try
            {
                if (!args.ParticleEffect.IsValid)
                {
                    return;
                }

                var particleName = args.ParticleEffect.Name;

                if (!this.releasedParticles.TryGetValue(particleName, out var abilityId))
                {
                    return;
                }

                if (!(this.evadableAbilities.Find(x => x.Ability.Id == abilityId) is IParticle ability))
                {
                    return;
                }

                var allyAbility = EntityManager9.Heroes.Any(
                    x => x.IsAlly(this.owner.Team) && (x.Id == HeroId.npc_dota_hero_rubick || x.Id == HeroId.npc_dota_hero_morphling)
                                                   && x.IsAlive && x.Abilities.Any(z => z.Id == abilityId && z.TimeSinceCasted < 0.5f));

                if (allyAbility)
                {
                    return;
                }

                ability.AddParticle(args.ParticleEffect, particleName);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}