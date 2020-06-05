namespace O9K.Hud.Modules.Particles
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes;
    using Core.Entities.Metadata;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;

    using Helpers.Notificator;

    using MainMenu;

    internal abstract class AbilityModule : IDisposable
    {
        private readonly string abilityName;

        private readonly MenuAbilityToggler toggler;

        private bool active;

        protected AbilityModule(IContext9 context, INotificator notificator, IHudMenu hudMenu)
        {
            this.Notificator = notificator;
            this.Context = context;
            this.Owner = EntityManager9.Owner;
            this.OwnerTeam = this.Owner.Team;

            this.abilityName = this.GetType().GetCustomAttribute<AbilityIdAttribute>().AbilityId.ToString();

            this.toggler = hudMenu.ParticlesMenu.GetOrAdd(new MenuAbilityToggler("Abilities"));
            this.toggler.AddTranslation(Lang.Ru, "Способности");
            this.toggler.AddTranslation(Lang.Cn, "播放声音");
        }

        protected IContext9 Context { get; }

        protected bool EnemyOnly { get; set; } = true;

        protected INotificator Notificator { get; }

        protected Owner Owner { get; }

        protected Team OwnerTeam { get; }

        public void AbilityAdded(Ability9 ability)
        {
            if (this.active)
            {
                return;
            }

            if (this.EnemyOnly && ability.Owner.Team == this.OwnerTeam)
            {
                return;
            }

            this.toggler.AddAbility(ability.Name);

            this.toggler.ValueChange += this.TogglerOnValueChange;
            this.active = true;
        }

        public void AbilityRemoved(Ability9 ability)
        {
            if (!this.active)
            {
                return;
            }

            var abilities = EntityManager9.Abilities.Where(x => x.Id == ability.Id).ToList();
            if (abilities.Count > 0 && abilities.Any(x => !this.EnemyOnly || x.Owner.Team != this.OwnerTeam))
            {
                return;
            }

            this.toggler.ValueChange -= this.TogglerOnValueChange;
            this.Disable();
            this.active = false;
        }

        public void Dispose()
        {
            if (!this.active)
            {
                return;
            }

            this.toggler.ValueChange -= this.TogglerOnValueChange;
            this.Disable();
            this.active = false;
        }

        protected abstract void Disable();

        protected abstract void Enable();

        private void TogglerOnValueChange(object sender, AbilityEventArgs e)
        {
            if (e.Ability != this.abilityName)
            {
                return;
            }

            if (e.NewValue)
            {
                this.Enable();
            }
            else
            {
                this.Disable();
            }
        }
    }
}