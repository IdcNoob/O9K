namespace O9K.Hud.Modules.Particles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;

    using Ensage;

    using Helpers.Notificator;

    using MainMenu;

    internal class ParticleAbilityManager : IHudModule
    {
        private readonly Dictionary<AbilityId, AbilityModule> abilities = new Dictionary<AbilityId, AbilityModule>();

        private readonly IContext9 context;

        private readonly IHudMenu hudMenu;

        private readonly INotificator notificator;

        [ImportingConstructor]
        public ParticleAbilityManager(IContext9 context, INotificator notificator, IHudMenu hudMenu)
        {
            this.context = context;
            this.notificator = notificator;
            this.hudMenu = hudMenu;
        }

        public void Activate()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(x => !x.IsAbstract && x.IsClass))
            {
                if (!typeof(AbilityModule).IsAssignableFrom(type))
                {
                    continue;
                }

                foreach (var attribute in type.GetCustomAttributes<AbilityIdAttribute>())
                {
                    try
                    {
                        var ability = (AbilityModule)Activator.CreateInstance(type, this.context, this.notificator, this.hudMenu);
                        this.abilities.Add(attribute.AbilityId, ability);
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                    }
                }
            }

            EntityManager9.AbilityAdded += this.OnAbilityAdded;
            EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
        }

        public void Dispose()
        {
            foreach (var abilityModule in this.abilities.Values)
            {
                abilityModule.Dispose();
            }

            this.abilities.Clear();

            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (!this.abilities.TryGetValue(ability.Id, out var abilityModule))
                {
                    return;
                }

                abilityModule.AbilityAdded(ability);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAbilityRemoved(Ability9 ability)
        {
            try
            {
                if (!this.abilities.TryGetValue(ability.Id, out var abilityModule))
                {
                    return;
                }

                abilityModule.AbilityRemoved(ability);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}