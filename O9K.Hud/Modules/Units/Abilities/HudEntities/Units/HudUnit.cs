namespace O9K.Hud.Modules.Units.Abilities.HudEntities.Units
{
    using System.Collections.Generic;
    using System.Linq;

    using Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Managers.Entity;
    using Core.Managers.Menu.Items;

    using SharpDX;

    internal class HudUnit
    {
        private readonly List<HudAbility> abilities = new List<HudAbility>();

        private readonly List<HudAbility> items = new List<HudAbility>();

        public HudUnit(Unit9 unit)
        {
            this.Unit = unit;
            this.IsAlly = EntityManager9.Owner.Team == unit.Team;
        }

        public IEnumerable<HudAbility> Abilities
        {
            get
            {
                return this.abilities.Where(x => x.ShouldDisplay && x.Ability.BaseAbility.AbilitySlot >= 0)
                    .OrderBy(x => x.Ability.BaseAbility.AbilitySlot);
            }
        }

        public IEnumerable<HudAbility> AllAbilities
        {
            get
            {
                return this.abilities.Concat(this.items);
            }
        }

        public Vector2 HealthBarPosition
        {
            get
            {
                return this.Unit.HealthBarPosition;
            }
        }

        public Vector2 HealthBarSize
        {
            get
            {
                return this.Unit.HealthBarSize;
            }
        }

        public bool IsAlly { get; }

        public bool IsValid
        {
            get
            {
                return this.Unit.IsValid && this.Unit.IsVisible && this.Unit.IsAlive && !this.Unit.HideHud;
            }
        }

        public IEnumerable<HudAbility> Items
        {
            get
            {
                return this.items.Where(x => x.ShouldDisplay);
            }
        }

        public Unit9 Unit { get; }

        public void AddAbility(Ability9 ability, MenuAbilityToggler itemsToggler)
        {
            var hudAbility = new HudAbility(ability);

            if (ability.IsItem)
            {
                itemsToggler.AddAbility(ability.Name);
                hudAbility.ChangeEnabled(itemsToggler.IsEnabled(ability.Name));
                this.items.Add(hudAbility);
            }
            else
            {
                this.abilities.Add(hudAbility);
            }
        }

        public void RemoveAbility(Ability9 entity)
        {
            if (entity.IsItem)
            {
                var item = this.items.Find(x => x.Ability.Handle == entity.Handle);
                if (item != null)
                {
                    this.items.Remove(item);
                }
            }
            else
            {
                var ability = this.abilities.Find(x => x.Ability.Handle == entity.Handle);
                if (ability != null)
                {
                    this.abilities.Remove(ability);
                }
            }
        }
    }
}