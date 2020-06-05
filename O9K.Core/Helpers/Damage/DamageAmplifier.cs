namespace O9K.Core.Helpers.Damage
{
    using Ensage;

    public class DamageAmplifier : Damage
    {
        public override float this[DamageType index]
        {
            get
            {
                if (this.Damages.TryGetValue(index, out var damage))
                {
                    return damage;
                }

                return 1;
            }
            set
            {
                this.Damages[index] = value;
            }
        }
    }
}