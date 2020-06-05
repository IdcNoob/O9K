namespace O9K.AIO.Heroes.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Abilities;
    using Abilities.Items;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Extensions;

    using FailSafe;

    using Modes.Combo;
    using Modes.MoveCombo;

    using PlaySharp.Toolkit.Helper.Annotations;

    using SharpDX;

    using TargetManager;

    internal class ControllableUnit
    {
        private readonly MultiSleeper abilitySleeper;

        private DisableAbility moveAbyssal;

        private DisableAbility moveAtos;

        private ShieldAbility moveBkb;

        private ShieldAbility moveBladeMail;

        private BlinkAbility moveBlink;

        private DisableAbility moveBloodthorn;

        private DebuffAbility moveDiffusal;

        private DebuffAbility moveEthereal;

        private BlinkAbility moveForceStaff;

        private ShieldAbility moveGlimmer;

        private DisableAbility moveHex;

        private ShieldAbility moveHood;

        private ShieldAbility moveLotus;

        private DisableAbility moveOrchid;

        private MoveBuffAbility movePhaseBoots;

        private BlinkAbility movePike;

        private MoveBuffAbility moveShadowBlade;

        private MoveBuffAbility moveSilverEdge;

        public ControllableUnit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
        {
            this.Owner = owner;
            this.abilitySleeper = abilitySleeper;
            this.OrbwalkSleeper = orbwalkSleeper;
            this.Menu = menu;
            this.Handle = owner.Handle;

            this.MoveComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.item_blink, x => this.moveBlink = new BlinkAbility(x) },
                { AbilityId.item_force_staff, x => this.moveForceStaff = new ForceStaff(x) },
                { AbilityId.item_hurricane_pike, x => this.movePike = new ForceStaff(x) },

                { AbilityId.item_butterfly, x => this.movePhaseBoots = new MoveBuffAbility(x) },
                { AbilityId.item_invis_sword, x => this.moveShadowBlade = new MoveBuffAbility(x) },
                { AbilityId.item_silver_edge, x => this.moveSilverEdge = new MoveBuffAbility(x) },
                { AbilityId.item_glimmer_cape, x => this.moveGlimmer = new ShieldAbility(x) },

                { AbilityId.item_black_king_bar, x => this.moveBkb = new ShieldAbility(x) },
                { AbilityId.item_blade_mail, x => this.moveBladeMail = new ShieldAbility(x) },
                { AbilityId.item_hood_of_defiance, x => this.moveHood = new ShieldAbility(x) },
                { AbilityId.item_lotus_orb, x => this.moveLotus = new ShieldAbility(x) },

                { AbilityId.item_diffusal_blade, x => this.moveDiffusal = new DebuffAbility(x) },
                { AbilityId.item_abyssal_blade, x => this.moveAbyssal = new DisableAbility(x) },
                { AbilityId.item_rod_of_atos, x => this.moveAtos = new DisableAbility(x) },
                { AbilityId.item_orchid, x => this.moveOrchid = new DisableAbility(x) },
                { AbilityId.item_sheepstick, x => this.moveHex = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.moveBloodthorn = new Bloodthorn(x) },
                { AbilityId.item_ethereal_blade, x => this.moveEthereal = new DebuffAbility(x) },
            };
        }

        public Sleeper AttackSleeper { get; } = new Sleeper();

        public bool CanBeControlled
        {
            get
            {
                return this.Menu.Control && (this.Owner.UnitState & UnitState.CommandRestricted) == 0;
            }
        }

        public bool CanBodyBlock
        {
            get
            {
                return this.Menu.BodyBlock && (this.Owner.UnitState & UnitState.NoCollision) == 0
                                           && this.Owner.MoveCapability == MoveCapability.Ground && this.Owner.CanMove()
                                           && !this.Owner.IsInvulnerable;
            }
        }

        public Sleeper ComboSleeper { get; } = new Sleeper();

        public FailSafe FailSafe { get; set; }

        public uint Handle { get; }

        public virtual bool IsInvisible
        {
            get
            {
                return this.Owner.IsInvisible;
            }
        }

        public bool IsValid
        {
            get
            {
                return this.Owner.IsValid && this.Owner.IsAlive;
            }
        }

        public Vector3 LastMovePosition { get; set; }

        public Unit9 LastTarget { get; protected set; }

        public string MorphedUnitName { get; set; }

        public Sleeper MoveSleeper { get; } = new Sleeper();

        public virtual bool OrbwalkEnabled
        {
            get
            {
                return this.Menu.Orbwalk;
            }
        }

        public Sleeper OrbwalkSleeper { get; }

        public Unit9 Owner { get; }

        public virtual bool ShouldControl
        {
            get
            {
                return !this.Owner.IsCasting;
            }
        }

        protected virtual int BodyBlockRange { get; } = 150;

        protected Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>> ComboAbilities { get; set; }

        protected ControllableUnitMenu Menu { get; }

        protected Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>> MoveComboAbilities { get; }

        public virtual void AddAbility(ActiveAbility ability, IEnumerable<ComboModeMenu> comboMenus, MoveComboModeMenu moveMenu)
        {
            if (this.ComboAbilities != null && this.ComboAbilities.TryGetValue(ability.Id, out var func1))
            {
                var usableAbility = func1(ability);
                usableAbility.Sleeper = this.abilitySleeper[usableAbility.Ability.Handle];
                usableAbility.OrbwalkSleeper = this.OrbwalkSleeper;

                foreach (var comboMenu in comboMenus)
                {
                    comboMenu.AddComboAbility(usableAbility);
                }
            }

            this.AddMoveComboAbility(ability, moveMenu);
        }

        public bool? BodyBlock(TargetManager targetManager, Vector3 blockPosition, ComboModeMenu menu)
        {
            if (!this.Owner.CanMove() || this.MoveSleeper.IsSleeping)
            {
                return false;
            }

            var target = targetManager.Target;
            var angle = target.GetAngle(this.Owner.Position);

            if (angle > 1.2)
            {
                if (this.Owner.Speed <= target.Speed + 35)
                {
                    return null;
                }

                var delta = angle * 0.6f;
                var side1 = target.InFront(this.BodyBlockRange, MathUtil.RadiansToDegrees(delta));
                var side2 = target.InFront(this.BodyBlockRange, MathUtil.RadiansToDegrees(-delta));

                if (this.Move(this.Owner.Distance(side1) < this.Owner.Distance(side2) ? side1 : side2))
                {
                    this.MoveSleeper.Sleep(0.1f);
                    return true;
                }

                return false;
            }

            if (angle < 0.36)
            {
                if (!target.IsMoving && this.CanAttack(target, 400))
                {
                    return this.Attack(target, menu);
                }

                if (this.Owner.IsMoving && !this.AttackSleeper.IsSleeping)
                {
                    this.MoveSleeper.Sleep(0.2f);
                    return this.Owner.BaseUnit.Stop();
                }

                return false;
            }

            if (this.Move(target.Position.Extend2D(blockPosition, this.BodyBlockRange)))
            {
                this.MoveSleeper.Sleep(0.1f);
                return true;
            }

            return false;
        }

        public virtual bool CanAttack(Unit9 target, float additionalRange = 0)
        {
            if (target == null)
            {
                return false;
            }

            if (!this.Owner.CanAttack(target, additionalRange))
            {
                return false;
            }

            if (this.Menu.DangerRange > 0 && this.Menu.DangerMoveToMouse
                                          && this.Owner.Distance(target) < Math.Min(
                                              (int)this.Owner.GetAttackRange(),
                                              this.Menu.DangerRange))
            {
                return false;
            }

            var delay = this.Owner.GetTurnTime(target.Position) + (Game.Ping / 2000f);
            if (delay <= 0)
            {
                return !this.AttackSleeper.IsSleeping;
            }

            return this.AttackSleeper.RemainingSleepTime <= delay;
        }

        public virtual bool CanMove()
        {
            if (!this.Owner.CanMove())
            {
                return false;
            }

            return !this.MoveSleeper.IsSleeping;
        }

        public virtual bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            return false;
        }

        public virtual void EndCombo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            this.LastMovePosition = Vector3.Zero;
        }

        public bool Move(Vector3 movePosition)
        {
            if (!this.CanMove())
            {
                return false;
            }

            if (movePosition == this.LastMovePosition)
            {
                return false;
            }

            if (!this.Owner.BaseUnit.Move(movePosition))
            {
                return false;
            }

            this.LastMovePosition = movePosition;
            return true;
        }

        public virtual bool MoveCombo(TargetManager targetManager, MoveComboModeMenu comboModeMenu)
        {
            if (!this.Owner.CanUseAbilities || this.Owner.IsInvisible)
            {
                return false;
            }

            if (this.Owner.IsMyHero)
            {
                var target = targetManager.EnemyHeroes.Where(x => !x.IsStunned && !x.IsHexed && !x.IsSilenced && !x.IsRooted)
                    .OrderBy(x => x.Distance(this.Owner))
                    .FirstOrDefault(x => x.Distance(this.Owner) < 1000);

                if (target != null)
                {
                    targetManager.ForceSetTarget(target);
                }
            }

            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (this.MoveComboUseBlinks(abilityHelper))
            {
                return true;
            }

            if (targetManager.HasValidTarget)
            {
                if (this.MoveComboUseDisables(abilityHelper))
                {
                    return true;
                }
            }

            if (this.MoveComboUseBuffs(abilityHelper))
            {
                return true;
            }

            if (targetManager.HasValidTarget && targetManager.Target.Distance(this.Owner) < 600)
            {
                if (this.MoveComboUseShields(abilityHelper))
                {
                    return true;
                }
            }

            return false;
        }

        public void OnAttackStart()
        {
            var ping = Game.Ping / 2000f;
            var attackPoint = this.Owner.GetAttackPoint(this.LastTarget);

            if (this.Owner.Abilities.Any(x => x.Id == AbilityId.item_echo_sabre && x.CanBeCasted()))
            {
                attackPoint *= 2.5f;
            }

            this.MoveSleeper.Sleep((attackPoint - ping) + 0.06f + (this.Menu.AdditionalDelay / 1000f));
            this.AttackSleeper.Sleep((attackPoint + this.Owner.GetAttackBackswing(this.LastTarget)) - ping - 0.06f);
        }

        public virtual bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            if (this.OrbwalkSleeper.IsSleeping)
            {
                return false;
            }

            this.LastTarget = target;

            if (attack && this.CanAttack(target))
            {
                this.LastMovePosition = Vector3.Zero;
                return this.Attack(target, comboMenu);
            }

            if (target != null && this.Menu.OrbwalkerStopOnStanding && !target.IsMoving
                && this.Owner.Distance(target) < this.Owner.GetAttackRange(target))
            {
                return false;
            }

            if (move && this.CanMove())
            {
                return this.ForceMove(target, attack);
            }

            return false;
        }

        public bool Orbwalk(Unit9 target, ComboModeMenu comboMenu)
        {
            var move = comboMenu.Move.IsEnabled;
            if (target != null && this.Owner.IsRanged && this.Owner.HasModifier("modifier_item_hurricane_pike_range"))
            {
                move = false;
            }

            return this.Orbwalk(target, comboMenu.Attack, move, comboMenu);
        }

        public virtual void RemoveAbility(ActiveAbility ability)
        {
        }

        protected void AddMoveComboAbility(ActiveAbility ability, MoveComboModeMenu moveMenu)
        {
            if (this.MoveComboAbilities.TryGetValue(ability.Id, out var func2))
            {
                var usableAbility = func2(ability);
                usableAbility.Sleeper = this.abilitySleeper[usableAbility.Ability.Handle];
                usableAbility.OrbwalkSleeper = this.OrbwalkSleeper;
                moveMenu.AddComboAbility(usableAbility);
            }
        }

        protected virtual bool Attack(Unit9 target, [CanBeNull] ComboModeMenu comboMenu)
        {
            if (this.Owner.Name == nameof(HeroId.npc_dota_hero_rubick))
            {
                //hack
                var q = (IActiveAbility)this.Owner.Abilities.FirstOrDefault(x => x.Id == AbilityId.rubick_telekinesis);
                if (q?.CanBeCasted() == true && comboMenu?.IsAbilityEnabled(q) == true)
                {
                    return false;
                }
            }

            if (!this.UseOrbAbility(target, comboMenu) && !this.Owner.BaseUnit.Attack(target.BaseUnit))
            {
                return false;
            }

            var ping = (Game.Ping / 2000) + 0.06f;
            var turnTime = this.Owner.GetTurnTime(target.Position);
            var distance = Math.Max(this.Owner.Distance(target) - this.Owner.GetAttackRange(target), 0) / this.Owner.BaseUnit.MovementSpeed;
            var delay = turnTime + distance + ping;

            var attackPoint = this.Owner.GetAttackPoint(target);
            if (this.Owner.Abilities.Any(x => x.Id == AbilityId.item_echo_sabre && x.CanBeCasted()))
            {
                attackPoint *= 2.5f;
            }

            this.AttackSleeper.Sleep((this.Owner.GetAttackPoint(target) + this.Owner.GetAttackBackswing(target) + delay) - 0.1f);
            this.MoveSleeper.Sleep(attackPoint + delay + 0.25f + (this.Menu.AdditionalDelay / 1000f));

            return true;
        }

        protected virtual bool ForceMove(Unit9 target, bool attack)
        {
            var mousePosition = Game.MousePosition;
            var movePosition = mousePosition;

            if (target != null && attack)
            {
                var targetPosition = target.Position;

                if (this.Menu.OrbwalkingMode == "Move to target" || this.CanAttack(target, 400))
                {
                    movePosition = targetPosition;
                }

                if (this.Menu.DangerRange > 0)
                {
                    var dangerRange = Math.Min((int)this.Owner.GetAttackRange(), this.Menu.DangerRange);
                    var targetDistance = this.Owner.Distance(target);

                    if (this.Menu.DangerMoveToMouse)
                    {
                        if (targetDistance < dangerRange)
                        {
                            movePosition = mousePosition;
                        }
                    }
                    else
                    {
                        if (targetDistance < dangerRange)
                        {
                            var angle = (targetPosition - this.Owner.Position).AngleBetween(movePosition - targetPosition);
                            if (angle < 90)
                            {
                                if (angle < 30)
                                {
                                    movePosition = targetPosition.Extend2D(movePosition, (dangerRange - 25) * -1);
                                }
                                else
                                {
                                    var difference = mousePosition - targetPosition;
                                    var rotation = difference.Rotated(MathUtil.DegreesToRadians(90));
                                    var end = rotation.Normalized() * (dangerRange - 25);
                                    var right = targetPosition + end;
                                    var left = targetPosition - end;

                                    movePosition = this.Owner.Distance(right) < this.Owner.Distance(left) ? right : left;
                                }
                            }
                            else if (target.Distance(movePosition) < dangerRange)
                            {
                                movePosition = targetPosition.Extend2D(movePosition, dangerRange - 25);
                            }
                        }
                    }
                }
            }

            if (this.Menu.OrbwalkingMode == "Move to target")
            {
                if (this.Owner.Distance(movePosition) < 100)
                {
                    return false;
                }
            }
            else
            {
                if (this.Owner.Distance(movePosition) < 10)
                {
                    return false;
                }
            }

            if (movePosition == this.LastMovePosition && this.AttackSleeper.IsSleeping)
            {
                return false;
            }

            if (!this.Owner.BaseUnit.Move(movePosition))
            {
                return false;
            }

            this.LastMovePosition = movePosition;
            return true;
        }

        protected virtual bool MoveComboUseBlinks(AbilityHelper abilityHelper)
        {
            if (abilityHelper.UseMoveAbility(this.moveBlink))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.moveForceStaff))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.movePike))
            {
                return true;
            }

            return false;
        }

        protected virtual bool MoveComboUseBuffs(AbilityHelper abilityHelper)
        {
            if (abilityHelper.UseMoveAbility(this.movePhaseBoots))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.moveSilverEdge))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.moveShadowBlade))
            {
                return true;
            }

            return false;
        }

        protected virtual bool MoveComboUseDisables(AbilityHelper abilityHelper)
        {
            if (abilityHelper.UseAbility(this.moveHex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.moveAbyssal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.moveEthereal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.moveAtos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.moveBloodthorn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.moveOrchid))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.moveDiffusal))
            {
                return true;
            }

            return false;
        }

        protected virtual bool MoveComboUseShields(AbilityHelper abilityHelper)
        {
            if (abilityHelper.UseMoveAbility(this.moveBkb))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.moveGlimmer))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.moveBladeMail))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.moveHood))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.moveLotus))
            {
                return true;
            }

            return false;
        }

        protected virtual bool UseOrbAbility(Unit9 target, ComboModeMenu comboMenu)
        {
            if (!this.Owner.CanUseAbilities)
            {
                return false;
            }

            var orbAbility = this.Owner.Abilities.OfType<OrbAbility>()
                .FirstOrDefault(x => comboMenu?.IsAbilityEnabled(x) == true && !x.Enabled && x.CanBeCasted() && x.CanHit(target));

            if (orbAbility != null)
            {
                return orbAbility.UseAbility(target);
            }

            return false;
        }
    }
}