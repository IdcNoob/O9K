namespace O9K.ItemManager.Modules.AbilityLeveling.AbilityBuild
{
    using Ensage;

    internal class LearnableAbility
    {
        public LearnableAbility(Ability ability, string displayName, uint learnLevel)
        {
            this.Ability = ability;
            this.DisplayName = displayName;
            this.LearnLevel = learnLevel;
            this.IsUltimate = ability.AbilityType == AbilityType.Ultimate;
        }

        public Ability Ability { get; }

        public uint CurrentLevel
        {
            get
            {
                return this.Ability.Level;
            }
        }

        public string DisplayName { get; }

        public bool IsUltimate { get; }

        public uint LearnLevel { get; }

        public int MaxLevel
        {
            get
            {
                return this.Ability.MaximumLevel;
            }
        }

        public float PickRate { get; set; }

        public float WinRate { get; set; }
    }
}