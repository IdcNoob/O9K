namespace O9K.Core.Entities.Abilities.Items
{
    using System;

    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Logger;

    using Managers.Entity;

    using Metadata;

    using SharpDX;

    [AbilityId(AbilityId.item_hurricane_pike)]
    public class HurricanePike : RangedAbility, IBlink, IHasRangeIncrease
    {
        private readonly SpecialData attackRange;

        private Unit9 pikeTarget;

        private bool subbed;

        public HurricanePike(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackRange = new SpecialData(baseAbility, "base_attack_range");
            this.RangeData = new SpecialData(baseAbility, "push_length");
        }

        public BlinkType BlinkType { get; } = BlinkType.Leap;

        public bool IsRangeIncreasePermanent { get; } = true;

        public override float Range
        {
            get
            {
                return this.RangeData.GetValue(this.Level);
            }
        }

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_item_hurricane_pike";

        public override float Speed { get; } = 1200;

        public override bool CanHit(Unit9 target)
        {
            if (!base.CanHit(target))
            {
                return false;
            }

            if (!this.Owner.IsAlly(target) && this.Owner.Distance(target) > this.CastRange / 2)
            {
                return false;
            }

            return true;
        }

        public override void Dispose()
        {
            base.Dispose();

            Player.OnExecuteOrder -= this.OnExecuteOrder;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;
            Unit.OnModifierAdded -= this.OnModifierAdded;
        }

        public override float GetHitTime(Vector3 position)
        {
            return this.GetCastDelay(position) + this.ActivationDelay + (this.Range / this.Speed);
        }

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            if (!this.IsUsable || !this.Owner.IsRanged)
            {
                return 0;
            }

            return this.attackRange.GetValue(this.Level);
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            if (this.IsControllable)
            {
                Player.OnExecuteOrder += this.OnExecuteOrder;
            }
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (this.subbed || args.OrderId != OrderId.AbilityTarget || args.Ability.Id != this.Id || !args.Process)
                {
                    return;
                }

                this.pikeTarget = EntityManager9.GetUnit(args.Target.Handle);
                if (this.pikeTarget == null || this.pikeTarget.IsLinkensProtected || this.pikeTarget.IsSpellShieldProtected)
                {
                    return;
                }

                Unit.OnModifierAdded += this.OnModifierAdded;
                this.subbed = true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Handle != this.Owner.Handle || args.Modifier.Name != "modifier_item_hurricane_pike_range")
                {
                    return;
                }

                this.Owner.HurricanePikeTarget = this.pikeTarget;

                Unit.OnModifierAdded -= this.OnModifierAdded;
                Unit.OnModifierRemoved += this.OnModifierRemoved;
            }
            catch (Exception e)
            {
                Unit.OnModifierAdded -= this.OnModifierAdded;
                Logger.Error(e);
            }
        }

        private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Handle != this.Owner.Handle || args.Modifier.Name != "modifier_item_hurricane_pike_range")
                {
                    return;
                }

                this.Owner.HurricanePikeTarget = null;

                Unit.OnModifierRemoved -= this.OnModifierRemoved;
                this.subbed = false;
            }
            catch (Exception e)
            {
                Unit.OnModifierRemoved -= this.OnModifierRemoved;
                Logger.Error(e);
            }
        }
    }
}