namespace O9K.AIO.Heroes.Dynamic.Abilities.Disables.Unique
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    [AbilityId(AbilityId.pudge_dismember)]
    internal class DismemberDisable : OldDisableAbility
    {
        public DismemberDisable(IDisable ability)
            : base(ability)
        {
        }

        protected override bool ChainStun(Unit9 target)
        {
            return true;
        }
    }
}