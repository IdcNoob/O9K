namespace O9K.Evader.Abilities.Heroes.Bristleback.QuillSpray
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.bristleback_quill_spray)]
    internal class QuillSprayBase : EvaderBaseAbility, IEvadable
    {
        public QuillSprayBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new QuillSprayEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}