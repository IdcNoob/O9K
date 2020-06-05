namespace O9K.Core.Entities.Metadata
{
    using System;

    using Ensage;

    using Attribute = System.Attribute;

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class AbilityIdAttribute : Attribute
    {
        public AbilityIdAttribute(AbilityId abilityId)
        {
            this.AbilityId = abilityId;
        }

        public AbilityId AbilityId { get; }
    }
}