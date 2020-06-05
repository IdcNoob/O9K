namespace O9K.Core.Entities.Metadata
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class UnitNameAttribute : Attribute
    {
        public UnitNameAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}