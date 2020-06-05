namespace O9K.AIO.Heroes.Morphling.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    internal class Replicate : TargetableAbility
    {
        private Core.Entities.Abilities.Heroes.Morphling.Morph baseReplicate;

        public Replicate(ActiveAbility ability)
            : base(ability)
        {
            this.baseReplicate = (Core.Entities.Abilities.Heroes.Morphling.Morph)ability;
        }
    }
}