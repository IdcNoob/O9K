namespace O9K.Core.Extensions
{
    using Ensage;

    using Entities.Units;

    public static class DamageExtensions
    {
        public static float GetMeleeDamageMultiplier(Unit9 source, Unit9 target)
        {
            switch (target.ArmorType)
            {
                case ArmorType.Basic:
                    switch (source.AttackType)
                    {
                        case AttackDamageType.Pierce:
                            return 1.5f;
                    }

                    break;
                case ArmorType.Structure:
                    switch (source.AttackType)
                    {
                        case AttackDamageType.Basic:
                            return 0.7f;
                        case AttackDamageType.Pierce:
                            return 0.35f;
                        case AttackDamageType.Siege:
                            return 2.5f;
                        case AttackDamageType.Hero:
                            return 0.5f;
                    }

                    break;
                case ArmorType.Hero:
                    switch (source.AttackType)
                    {
                        case AttackDamageType.Basic:
                            return 0.75f;
                        case AttackDamageType.Pierce:
                            return 0.5f;
                    }

                    break;
            }

            return 1;
        }
    }
}