namespace O9K.ItemManager.Modules.AutoActions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes;
    using Core.Entities.Mines;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using Metadata;

    internal class MinesDestroyer : IModule
    {
        private readonly Sleeper actionBlockSleeper = new Sleeper();

        private readonly MenuSwitcher attack;

        private readonly HashSet<OrderId> blockedOrders = new HashSet<OrderId>
        {
            OrderId.MoveLocation,
            OrderId.MoveTarget,
            OrderId.MoveToDirection,
            OrderId.AttackLocation,
            OrderId.AttackTarget,
            OrderId.Ability,
            OrderId.AbilityLocation,
            OrderId.AbilityTarget,
            OrderId.AbilityTargetRune,
            OrderId.AbilityTargetTree,
            OrderId.ToggleAbility,
            OrderId.Hold,
            OrderId.Stop,
            OrderId.Continue,
            OrderId.ConsumeRune,
            OrderId.DropItem,
        };

        private readonly HashSet<AbilityId> mineAbilities = new HashSet<AbilityId>
        {
            AbilityId.techies_land_mines,
            AbilityId.techies_remote_mines,
            AbilityId.techies_stasis_trap
        };

        private readonly Sleeper sleeper = new Sleeper();

        private IUpdateHandler handler;

        private Owner owner;

        [ImportingConstructor]
        public MinesDestroyer(IMainMenu mainMenu)
        {
            var menu = mainMenu.AutoActionsMenu.Add(new Menu("Mines destroyer"));
            menu.AddTranslation(Lang.Ru, "Уничтожение мин");
            menu.AddTranslation(Lang.Cn, "破坏地雷");

            this.attack = menu.Add(new MenuSwitcher("Attack"));
            this.attack.AddTranslation(Lang.Ru, "Атаковать");
            this.attack.AddTranslation(Lang.Cn, "进攻");
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;

            this.handler = UpdateManager.Subscribe(this.OnUpdate, 300, false);
            this.attack.ValueChange += this.AttackOnValueChange;
        }

        public void Dispose()
        {
            this.attack.ValueChange -= this.AttackOnValueChange;
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            Player.OnExecuteOrder -= this.OnExecuteOrder;
            UpdateManager.Unsubscribe(this.handler);
        }

        private void AttackOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                EntityManager9.AbilityAdded += this.OnAbilityAdded;
            }
            else
            {
                EntityManager9.AbilityAdded -= this.OnAbilityAdded;
                Player.OnExecuteOrder -= this.OnExecuteOrder;
                this.handler.IsEnabled = false;
            }
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (!this.mineAbilities.Contains(ability.Id) || ability.Owner.Team == this.owner.Team)
                {
                    return;
                }

                EntityManager9.AbilityAdded -= this.OnAbilityAdded;

                if (!this.handler.IsEnabled)
                {
                    this.handler.IsEnabled = true;
                    Player.OnExecuteOrder += this.OnExecuteOrder;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (!this.actionBlockSleeper || !this.blockedOrders.Contains(args.OrderId)
                                             || !args.Entities.Contains(this.owner.Hero.BaseHero))
                {
                    return;
                }

                args.Process = false;
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
                if (Game.IsPaused || this.sleeper)
                {
                    return;
                }

                var hero = this.owner.Hero;
                if (hero?.IsValid != true || !hero.IsAlive)
                {
                    return;
                }

                var mines = EntityManager9.Units.Where(x => x is Mine && x.IsVisible && x.IsAlive && !x.IsAlly(this.owner.Team)).ToList();

                foreach (var mine in mines)
                {
                    if (!hero.CanAttack(mine, 200))
                    {
                        continue;
                    }

                    if (hero.GetAttackDamage(mine) < mine.Health)
                    {
                        continue;
                    }

                    if (mines.Count(x => x.Distance(mine) < 400) > 1)
                    {
                        continue;
                    }

                    if (hero.Attack(mine))
                    {
                        var moveDistance = Math.Max(0, hero.Distance(mine) - hero.GetAttackRange(mine));
                        var delay = hero.GetAttackPoint(mine) + hero.GetTurnTime(mine.Position);
                        var attackTime = (moveDistance / hero.Speed) + delay;

                        this.actionBlockSleeper.Sleep(attackTime + 0.2f);
                        this.sleeper.Sleep(1);
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}