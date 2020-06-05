namespace O9K.AIO.TargetManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Heroes;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu.EventArgs;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Menu;

    using SharpDX;

    internal class TargetManager : IDisposable
    {
        private readonly Vector3 orangeColor = new Vector3(Color.Orange.R, Color.Orange.G, Color.Orange.B);

        private readonly Vector3 orangeRedColor = new Vector3(Color.OrangeRed.R, Color.OrangeRed.G, Color.OrangeRed.B);

        private readonly HashSet<string> targetableUnits = new HashSet<string>
        {
            "npc_dota_hero_target_dummy",
            "npc_dota_courier",
            "npc_dota_phoenix_sun",
            "npc_dota_juggernaut_healing_ward",
            "npc_dota_templar_assassin_psionic_trap",
            "npc_dota_techies_land_mine",
            "npc_dota_techies_stasis_trap",
            "npc_dota_techies_remote_mine",
            "npc_dota_weaver_swarm",
            "npc_dota_grimstroke_ink_creature",
            "npc_dota_sentry_wards",
            "npc_dota_observer_wards",
            "npc_dota_lone_druid_bear1",
            "npc_dota_lone_druid_bear2",
            "npc_dota_lone_druid_bear3",
            "npc_dota_lone_druid_bear4",
            "npc_dota_unit_tombstone1",
            "npc_dota_unit_tombstone2",
            "npc_dota_unit_tombstone3",
            "npc_dota_unit_tombstone4",
            "npc_dota_pugna_nether_ward_1",
            "npc_dota_pugna_nether_ward_2",
            "npc_dota_pugna_nether_ward_3",
            "npc_dota_pugna_nether_ward_4",

            "npc_dota_brewmaster_earth_1",
            "npc_dota_brewmaster_earth_2",
            "npc_dota_brewmaster_earth_3",
            "npc_dota_brewmaster_fire_1",
            "npc_dota_brewmaster_fire_2",
            "npc_dota_brewmaster_fire_3",
            "npc_dota_brewmaster_storm_1",
            "npc_dota_brewmaster_storm_2",
            "npc_dota_brewmaster_storm_3",

            // "npc_dota_venomancer_plague_ward_1",
            // "npc_dota_venomancer_plague_ward_2",
            // "npc_dota_venomancer_plague_ward_3",
            // "npc_dota_venomancer_plague_ward_4"
        };

        private readonly TargetManagerMenu targetManagerMenu;

        private bool drawTargetParticle;

        private Vector3 fountain;

        private Func<Unit9, float, IEnumerable<Unit9>> getTargetsFunc;

        private ParticleEffect targetParticleEffect;

        public TargetManager(MenuManager menu)
        {
            this.Owner = EntityManager9.Owner;
            this.targetManagerMenu = new TargetManagerMenu(menu.GeneralSettingsMenu);

            this.targetManagerMenu.FocusTarget.ValueChange += this.FocusTargetOnValueChanged;
            this.targetManagerMenu.DrawTargetParticle.ValueChange += this.DrawTargetParticleOnValueChanged;
        }

        public List<Unit9> AllEnemyHeroes
        {
            get
            {
                return EntityManager9.Units.Where(x => x.IsHero && x.IsAlive && x.IsVisible && !x.IsAlly(this.Owner.Team)).ToList();
            }
        }

        public List<Unit9> AllEnemyUnits
        {
            get
            {
                return EntityManager9.Units
                    .Where(x => x.IsUnit && x.IsAlive && !x.IsInvulnerable && x.IsVisible && !x.IsAlly(this.Owner.Team))
                    .ToList();
            }
        }

        public List<Unit9> AllyHeroes
        {
            get
            {
                return EntityManager9.Units.Where(
                        x => x.IsHero && x.IsAlive && !x.IsIllusion && x.IsVisible && !x.IsInvulnerable && x.IsAlly(this.Owner.Team))
                    .ToList();
            }
        }

        public List<Unit9> AllyUnits
        {
            get
            {
                return EntityManager9.Units.Where(
                        x => x.IsUnit && x.IsAlive && !x.IsIllusion && x.IsVisible && !x.IsInvulnerable && x.IsAlly(this.Owner.Team))
                    .ToList();
            }
        }

        public Vector3 EnemyFountain
        {
            get
            {
                if (this.fountain == Vector3.Zero)
                {
                    this.fountain = EntityManager9.Units.First(x => x.IsFountain && x.IsEnemy(this.Owner.Team)).Position;
                }

                return this.fountain;
            }
        }

        public List<Unit9> EnemyHeroes
        {
            get
            {
                return EntityManager9.Units.Where(
                        x => x.IsHero && x.IsAlive && !x.IsIllusion && x.IsVisible && !x.IsInvulnerable && !x.IsAlly(this.Owner.Team))
                    .ToList();
            }
        }

        public List<Unit9> EnemyUnits
        {
            get
            {
                return EntityManager9.Units.Where(
                        x => x.IsUnit && x.IsAlive && !x.IsIllusion && x.IsVisible && !x.IsInvulnerable && !x.IsAlly(this.Owner.Team))
                    .ToList();
            }
        }

        public Vector3 Fountain
        {
            get
            {
                if (this.fountain == Vector3.Zero)
                {
                    this.fountain = EntityManager9.Units.First(x => x.IsFountain && x.IsAlly(this.Owner.Team)).Position;
                }

                return this.fountain;
            }
        }

        public bool HasValidTarget
        {
            get
            {
                return this.Target?.IsValid == true && this.Target.IsAlive;
            }
        }

        public Owner Owner { get; }

        public Unit9 Target { get; private set; }

        public float TargetDistance { get; set; } = 2500;

        public bool TargetLocked { get; set; }

        public Sleeper TargetSleeper { get; } = new Sleeper();

        public Unit9 ClosestAllyHeroToMouse(Unit9 unit, bool ignoreSelf = true)
        {
            var mouse = Game.MousePosition;
            return this.AllyHeroes.Where(x => (!ignoreSelf || !x.Equals(unit)) && x.Distance(mouse) < 500)
                .OrderBy(x => x.DistanceSquared(mouse))
                .FirstOrDefault();
        }

        public void Disable()
        {
            this.targetParticleEffect?.Dispose();
            UpdateManager.Unsubscribe(this.OnUpdate);
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
        }

        public void Dispose()
        {
            this.targetManagerMenu.FocusTarget.ValueChange -= this.FocusTargetOnValueChanged;
            this.targetManagerMenu.DrawTargetParticle.ValueChange -= this.DrawTargetParticleOnValueChanged;

            UpdateManager.Unsubscribe(this.OnUpdate);
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
        }

        public void Enable()
        {
            UpdateManager.Subscribe(this.OnUpdate);

            if (ObjectManager.GetEntities<Player>().Any(x => x.SelectedHeroId == HeroId.npc_dota_hero_phoenix))
            {
                EntityManager9.UnitAdded += this.OnUnitAdded;
                EntityManager9.UnitRemoved += this.OnUnitRemoved;
            }
        }

        public void ForceSetTarget(Unit9 target)
        {
            this.Target = target;
        }

        private IEnumerable<Unit9> ClosestToMouse(Unit9 unit, float range)
        {
            var mouse = Game.MousePosition;

            return EntityManager9.Units
                .Where(
                    x => (x.IsHero || (this.targetManagerMenu.AdditionalTargets && !x.IsBuilding && this.targetableUnits.Contains(x.Name)))
                         && x.IsAlive && !x.IsIllusion && x.IsVisible && x.IsEnemy(unit) && x.Distance(mouse) < 750)
                .OrderBy(x => x.DistanceSquared(mouse))
                .ToList();
        }

        private IEnumerable<Unit9> ClosestToUnit(Unit9 unit, float range)
        {
            return EntityManager9.Heroes
                .Where(
                    x => (x.IsHero || (this.targetManagerMenu.AdditionalTargets && !x.IsBuilding && this.targetableUnits.Contains(x.Name)))
                         && x.IsAlive && !x.IsIllusion && x.IsVisible && x.IsEnemy(unit) && x.Distance(unit) <= range)
                .OrderBy(x => x.DistanceSquared(unit))
                .ToList();
        }

        private void DrawTargetParticle()
        {
            if (this.targetParticleEffect?.IsValid != true)
            {
                this.targetParticleEffect = new ParticleEffect(@"materials\ensage_ui\particles\target.vpcf", this.Target.Position);
                this.targetParticleEffect.SetControlPoint(6, new Vector3(255));
            }

            this.targetParticleEffect.SetControlPoint(2, this.Owner.Hero.Position);
            this.targetParticleEffect.SetControlPoint(5, this.TargetLocked ? this.orangeRedColor : this.orangeColor);
            this.targetParticleEffect.SetControlPoint(7, this.Target.IsVisible ? this.Target.Position : this.Target.GetPredictedPosition());
        }

        private void DrawTargetParticleOnValueChanged(object sender, SwitcherEventArgs e)
        {
            this.drawTargetParticle = e.NewValue;

            if (!this.drawTargetParticle)
            {
                this.RemoveTargetParticle();
            }
        }

        private void FocusTargetOnValueChanged(object sender, SelectorEventArgs<string> e)
        {
            switch (e.NewValue)
            {
                case "Near mouse":
                {
                    this.getTargetsFunc = this.ClosestToMouse;
                    break;
                }
                case "Near hero":
                {
                    this.getTargetsFunc = this.ClosestToUnit;
                    break;
                }
                case "Lowest health":
                {
                    this.getTargetsFunc = this.LowestHealth;
                    break;
                }
            }
        }

        private void GetTarget()
        {
            this.Target = this.getTargetsFunc(this.Owner.Hero, this.TargetDistance)
                .FirstOrDefault(
                    x => (x.UnitState & (UnitState.Unselectable | UnitState.CommandRestricted))
                         != (UnitState.Unselectable | UnitState.CommandRestricted));
        }

        private IEnumerable<Unit9> LowestHealth(Unit9 unit, float range)
        {
            return EntityManager9.Heroes
                .Where(
                    x => (x.IsHero || (this.targetManagerMenu.AdditionalTargets && !x.IsBuilding && this.targetableUnits.Contains(x.Name)))
                         && x.IsAlive && !x.IsIllusion && x.IsVisible && x.IsEnemy(unit) && x.Distance(unit) <= range)
                .OrderBy(x => x.Health)
                .ToList();
        }

        private void OnUnitAdded(Unit9 unit)
        {
            try
            {
                if (!this.TargetLocked || unit.IsAlly(this.Owner.Team) || unit.Name != "npc_dota_phoenix_sun")
                {
                    return;
                }

                if (this.Target?.Name == nameof(HeroId.npc_dota_hero_phoenix))
                {
                    this.Target = unit;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitRemoved(Unit9 unit)
        {
            try
            {
                if (!this.TargetLocked || unit.IsAlly(this.Owner.Team) || unit.Name != "npc_dota_phoenix_sun")
                {
                    return;
                }

                if (this.Target?.Equals(unit) == true)
                {
                    this.Target = EntityManager9.Heroes.FirstOrDefault(x => x.Id == HeroId.npc_dota_hero_phoenix);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdate()
        {
            try
            {
                if (this.TargetLocked && this.targetManagerMenu.LockTarget)
                {
                    if (this.targetManagerMenu.DeathSwitch && !this.HasValidTarget)
                    {
                        this.GetTarget();
                    }
                }
                else
                {
                    this.GetTarget();
                }

                if (!this.drawTargetParticle)
                {
                    return;
                }

                if (!this.HasValidTarget)
                {
                    this.RemoveTargetParticle();
                    return;
                }

                this.DrawTargetParticle();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void RemoveTargetParticle()
        {
            if (this.targetParticleEffect == null)
            {
                return;
            }

            this.targetParticleEffect.Dispose();
            this.targetParticleEffect = null;
        }
    }
}