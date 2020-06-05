namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_bottle)]
    public class Bottle : RangedAbility, IHealthRestore, IManaRestore
    {
        private readonly Ensage.Items.Bottle bottle;

        private readonly SpecialData healthRestoreData;

        private readonly SpecialData manaRestoreData;

        public Bottle(Ability ability)
            : base(ability)
        {
            this.bottle = (Ensage.Items.Bottle)ability;
            this.healthRestoreData = new SpecialData(ability, "health_restore");
            this.manaRestoreData = new SpecialData(ability, "mana_restore");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                return base.AbilityBehavior | AbilityBehavior.UnitTarget;
            }
        }

        public bool InstantRestore { get; } = false;

        public override bool IsInvisibility
        {
            get
            {
                return this.bottle.StoredRune == RuneType.Invisibility;
            }
        }

        public string RestoreModifierName { get; } = "modifier_bottle_regeneration";

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public RuneType StoredRune
        {
            get
            {
                return this.bottle.StoredRune;
            }
        }

        public override bool TargetsEnemy { get; } = false;

        public override string TextureName
        {
            get
            {
                switch (this.bottle.StoredRune)
                {
                    case RuneType.None:
                        return base.TextureName;
                    case RuneType.DoubleDamage:
                        return "item_bottle_doubledamage";
                    case RuneType.Haste:
                        return "item_bottle_haste";
                    case RuneType.Illusion:
                        return "item_bottle_illusion";
                    case RuneType.Invisibility:
                        return "item_bottle_invisibility";
                    case RuneType.Regeneration:
                        return "item_bottle_regeneration";
                    case RuneType.Bounty:
                        return "item_bottle_bounty";
                    case RuneType.Arcane:
                        return "item_bottle_arcane";
                }

                return base.TextureName;
            }
        }

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return this.Charges > 0 && base.CanBeCasted(checkChanneling);
        }

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)this.healthRestoreData.GetValue(this.Level);
        }

        public int GetManaRestore(Unit9 unit)
        {
            return (int)this.manaRestoreData.GetValue(this.Level);
        }
    }
}