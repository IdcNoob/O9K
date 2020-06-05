namespace O9K.Core.Entities
{
    using System;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Extensions;

    using Units;

    public abstract class Entity9 : IEquatable<Entity9>
    {
        private string defaultName;

        private string displayName;

        protected Entity9(Entity entity)
        {
            this.Name = entity.Name;
            this.TextureName = entity.Name;
            this.BaseEntity = entity;
            this.Handle = entity.Handle;
        }

        public Entity BaseEntity { get; }

        public float DeathTime { get; internal set; }

        public string DefaultName
        {
            get
            {
                if (this.defaultName == null)
                {
                    this.defaultName = this.Name.RemoveAbilityLevel();
                }

                return this.defaultName;
            }
        }

        public virtual string DisplayName
        {
            get
            {
                if (this.displayName == null)
                {
                    try
                    {
                        this.displayName = LocalizationHelper.LocalizeName(this.Name);
                    }
                    catch
                    {
                        this.displayName = this.Name;
                    }
                }

                return this.displayName;
            }
        }

        public uint Handle { get; }

        public bool IsValid { get; internal set; } = true;

        public string Name { get; protected set; }

        public Unit9 Owner { get; internal set; }

        public virtual string TextureName { get; }

        public static bool operator ==(Entity9 obj1, Entity9 obj2)
        {
            return obj1?.Handle == obj2?.Handle;
        }

        public static implicit operator Entity(Entity9 entity)
        {
            return entity.BaseEntity;
        }

        public static bool operator !=(Entity9 obj1, Entity9 obj2)
        {
            return obj1?.Handle != obj2?.Handle;
        }

        public bool Equals(Entity9 other)
        {
            return this.Handle == other?.Handle;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Entity9);
        }

        public override int GetHashCode()
        {
            return (int)this.Handle;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}