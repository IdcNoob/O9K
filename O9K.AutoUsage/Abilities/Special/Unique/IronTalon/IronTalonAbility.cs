namespace O9K.AutoUsage.Abilities.Special.Unique.IronTalon
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Entities.Abilities.NeutralItems;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;

    using Settings;

    [AbilityId(AbilityId.item_iron_talon)]
    internal class IronTalonAbility : SpecialAbility
    {
        private readonly IronTalon ironTalon;

        private readonly IronTalonSettings settings;

        public IronTalonAbility(IActiveAbility ability, GroupSettings settings)
            : base(ability)
        {
            this.ironTalon = (IronTalon)ability;
            this.settings = new IronTalonSettings(settings.Menu, ability);
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            if (!this.Owner.IsAttacking)
            {
                return false;
            }

            var target = this.Owner.Target;
            if (target == null || !target.IsCreep || target.Team != Team.Neutral)
            {
                return false;
            }

            var creeps = EntityManager9.Units
                .Where(x => x.IsCreep && x.Team == Team.Neutral && x.IsVisible && x.IsAlive && this.Ability.CanHit(x))
                .OrderByDescending(x => x.Health);

            foreach (var creep in creeps)
            {
                if (this.ironTalon.GetDamage(creep) < this.settings.Damage)
                {
                    continue;
                }

                return this.Ability.UseAbility(creep);
            }

            return false;
        }
    }
}