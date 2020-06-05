namespace O9K.Core.Extensions
{
    using Ensage;

    public static class ProjectileExtensions
    {
        public static bool IsAutoAttackProjectile(this TrackingProjectile projectile)
        {
            return (projectile.Flags & 256) != 0;
        }
    }
}