namespace O9K.AIO.Abilities.Items
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    internal class Bloodthorn : DisableAbility
    {
        public Bloodthorn(ActiveAbility ability)
            : base(ability)
        {
        }

        protected override bool ChainStun(Unit9 target, bool invulnerability)
        {
            return true;
        }
    }
}