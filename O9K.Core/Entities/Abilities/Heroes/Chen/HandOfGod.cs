namespace O9K.Core.Entities.Abilities.Heroes.Chen
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.chen_hand_of_god)]
    public class HandOfGod : AreaOfEffectAbility, IHealthRestore
    {
        private readonly SpecialData healthRestoreData;

        public HandOfGod(Ability baseAbility)
            : base(baseAbility)
        {
            this.healthRestoreData = new SpecialData(baseAbility, "heal_amount");
        }

        public bool InstantRestore { get; } = true;

        public override float Radius { get; } = 9999999;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)this.healthRestoreData.GetValue(this.Level);
        }
    }
}