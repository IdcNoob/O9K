namespace O9K.Evader.Abilities.Heroes.Lycan.Howl
{
    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.lycan_howl)]
    internal class HowlBase : EvaderBaseAbility
    {
        public HowlBase(Ability9 ability)
            : base(ability)
        {
            //todo add evadable modifier ?
        }
    }
}