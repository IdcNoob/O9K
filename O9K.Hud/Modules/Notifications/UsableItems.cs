namespace O9K.Hud.Modules.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Helpers;

    using MainMenu;

    internal class UsableItems : IHudModule
    {
        private readonly List<Ability9> abilities = new List<Ability9>();

        private readonly HashSet<AbilityId> abilityIds = new HashSet<AbilityId>
        {
            AbilityId.item_trusty_shovel,
            AbilityId.item_hand_of_midas,
            AbilityId.item_pirate_hat
        };

        private readonly IContext9 context;

        private readonly IMinimap minimap;

        private readonly MenuAbilityToggler toggler;

        private Owner owner;

        [ImportingConstructor]
        public UsableItems(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
        {
            this.context = context;
            this.minimap = minimap;

            var notificationMenu = hudMenu.NotificationsMenu.GetOrAdd(new Menu("Abilities"));
            notificationMenu.AddTranslation(Lang.Ru, "Способности");
            notificationMenu.AddTranslation(Lang.Cn, "播放声音");

            var menu = notificationMenu.Add(new Menu("Available"));
            menu.AddTranslation(Lang.Ru, "Доступные");
            menu.AddTranslation(Lang.Cn, "可用的");

            this.toggler = menu.Add(new MenuAbilityToggler("Enabled", this.abilityIds.ToDictionary(x => x, _ => true)));
            this.toggler.AddTranslation(Lang.Ru, "Включено");
            this.toggler.AddTranslation(Lang.Cn, "启用");
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;

            EntityManager9.AbilityAdded += this.OnAbilityAdded;
            EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
            this.context.Renderer.Draw += this.OnDraw;
        }

        public void Dispose()
        {
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
            this.context.Renderer.Draw -= this.OnDraw;
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (ability.Owner.Team != this.owner.Team || !ability.IsControllable || ability.IsFake)
                {
                    return;
                }

                this.abilities.Add(ability);
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
                if (ability.Owner.Team != this.owner.Team || !ability.IsControllable || ability.IsFake)
                {
                    return;
                }

                this.abilities.Remove(ability);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                foreach (var ability in this.abilities)
                {
                    if (!ability.IsValid || !this.toggler.IsEnabled(ability.Name))
                    {
                        continue;
                    }

                    if (!ability.CanBeCasted(false))
                    {
                        continue;
                    }

                    var position = this.minimap.WorldToScreen(ability.Owner.Position, 35 * Hud.Info.ScreenRatio);
                    if (position.IsZero)
                    {
                        continue;
                    }

                    var scale = (Game.RawGameTime % 1) + 0.5f;
                    if (scale > 1)
                    {
                        scale = 2 - scale;
                    }

                    renderer.DrawTexture("o9k.outline_green", position * scale * 1.3f);
                    renderer.DrawTexture(ability.TextureName + "_rounded", position * scale);
                }
            }
            catch (InvalidOperationException)
            {
                // ignore
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}