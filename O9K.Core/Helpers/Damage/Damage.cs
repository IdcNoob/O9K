namespace O9K.Core.Helpers.Damage
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Ensage;

    public enum DamageValue
    {
        Minimum,

        Average,

        Maximum
    }

    public class Damage : IEnumerable<KeyValuePair<DamageType, float>>
    {
        protected readonly Dictionary<DamageType, float> Damages = new Dictionary<DamageType, float>();

        public virtual float this[DamageType index]
        {
            get
            {
                this.Damages.TryGetValue(index, out var damage);
                return damage;
            }
            set
            {
                this.Damages[index] = value;
            }
        }

        public static Damage operator +(Damage left, Damage right)
        {
            if (right == null)
            {
                return left;
            }

            foreach (var damage in right)
            {
                left[damage.Key] += damage.Value;
            }

            return left;
        }

        public static Damage operator *(Damage left, float right)
        {
            foreach (var damage in left.ToList())
            {
                left[damage.Key] *= right;
            }

            return left;
        }

        public IEnumerator<KeyValuePair<DamageType, float>> GetEnumerator()
        {
            return this.Damages.GetEnumerator();
        }

        public override string ToString()
        {
            var damage = new StringBuilder();

            foreach (var dmg in this.Damages)
            {
                damage.Append(dmg.Key).Append(": ").Append(dmg.Value).Append(Environment.NewLine);
            }

            return damage.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}