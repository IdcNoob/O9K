namespace O9K.AutoUsage.Abilities.HealthRestore.Unique.Tango
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Settings;

    [AbilityId(AbilityId.item_tango)]
    [AbilityId(AbilityId.item_tango_single)]
    internal class TangoAbility : HealthRestoreAbility, IDisposable
    {
        private readonly TangoSettings settings;

        private readonly List<Tree> trees = new List<Tree>();

        public TangoAbility(IHealthRestore healthRestore, GroupSettings settings)
            : base(healthRestore)
        {
            this.settings = new TangoSettings(settings.Menu, healthRestore);
        }

        public void Dispose()
        {
            ObjectManager.OnAddEntity -= this.OnAddEntity;
            ObjectManager.OnRemoveEntity -= this.OnRemoveEntity;
        }

        public override void Enabled(bool enabled)
        {
            base.Enabled(enabled);

            if (enabled)
            {
                ObjectManager.OnAddEntity += this.OnAddEntity;
                ObjectManager.OnRemoveEntity += this.OnRemoveEntity;
            }
            else
            {
                ObjectManager.OnAddEntity -= this.OnAddEntity;
                ObjectManager.OnRemoveEntity -= this.OnRemoveEntity;
            }
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            if (!this.Owner.CanBeHealed)
            {
                return false;
            }

            if (this.Owner.HasModifier(this.HealthRestore.RestoreModifierName))
            {
                return false;
            }

            var enemies = heroes.Where(x => !x.IsInvulnerable && x.IsEnemy(this.Owner)).ToList();

            if (this.Owner.HealthPercentage > this.settings.HpThreshold)
            {
                return false;
            }

            if (enemies.Count(x => x.Distance(this.Owner) < this.settings.Distance) < this.settings.EnemiesCount)
            {
                return false;
            }

            var position = this.Owner.Position;
            var range = Math.Pow(this.Ability.CastRange + 100, 2);
            var target = this.settings.HappyLittleTreeOnly
                             ? this.trees.Find(x => x.IsValid && x.Position.DistanceSquared(position) < range)
                             : EntityManager9.Trees.FirstOrDefault(x => x.Position.DistanceSquared(position) < range);

            if (target != null)
            {
                return this.Ability.UseAbility(target);
            }

            return false;
        }

        private void OnAddEntity(EntityEventArgs args)
        {
            try
            {
                if (!(args.Entity is Tree tree) || tree.Name != "dota_temp_tree")
                {
                    return;
                }

                this.trees.Add(tree);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnRemoveEntity(EntityEventArgs args)
        {
            try
            {
                if (!(args.Entity is Tree tree) || tree.Name != "dota_temp_tree")
                {
                    return;
                }

                this.trees.Remove(tree);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}