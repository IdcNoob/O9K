namespace O9K.Core.Entities.Abilities.Heroes.OgreMagi
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.ogre_magi_multicast)]
    public class Multicast : PassiveAbility
    {
        public Multicast(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}