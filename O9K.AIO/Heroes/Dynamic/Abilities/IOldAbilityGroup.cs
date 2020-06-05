namespace O9K.AIO.Heroes.Dynamic.Abilities
{
    using Core.Entities.Abilities.Base;

    internal interface IOldAbilityGroup
    {
        bool AddAbility(Ability9 ability);

        void RemoveAbility(Ability9 ability);
    }
}