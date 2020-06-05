namespace O9K.Core.Entities.Abilities.Base
{
    using Ensage;

    public abstract class Talent : Ability9
    {
        protected Talent(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override bool IsTalent { get; } = true;

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return true;
        }
    }
}