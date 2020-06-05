namespace O9K.ItemManager.Modules.AbilityLeveling.Names
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Core.Entities.Heroes;

    using Ensage;

    internal class NameManager
    {
        public readonly Dictionary<string, string> MyAbilityNames = new Dictionary<string, string>();

        private readonly Owner owner;

        public NameManager(Owner owner)
        {
            this.owner = owner;

            this.GetHeroName();
            this.GetAbilityNames();
        }

        public string HeroDotaBuffName { get; private set; }

        public string HeroName { get; private set; }

        private void GetAbilityNames()
        {
            var abilities = new Abilities();

            foreach (var abilityName in this.owner.Hero.BaseSpellbook.Spells
                .Where(x => (x.AbilityBehavior & (AbilityBehavior.NotLearnable | AbilityBehavior.Hidden)) == 0)
                .Select(x => x.Name))
            {
                this.MyAbilityNames.Add(abilityName, abilities.GetAbilityName(abilityName));
            }
        }

        private void GetHeroName()
        {
            var heroes = new Heroes();
            var name = heroes.GetHeroName(this.owner.Hero.Name);
            var dotaBuffName = new StringBuilder(name.Length);

            foreach (var ch in name)
            {
                if (ch == '\'')
                {
                    continue;
                }

                dotaBuffName.Append(char.IsWhiteSpace(ch) ? '-' : char.ToLower(ch));
            }

            this.HeroName = name;
            this.HeroDotaBuffName = dotaBuffName.ToString();
        }
    }
}