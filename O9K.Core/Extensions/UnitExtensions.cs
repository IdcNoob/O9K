namespace O9K.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Entities.Units;

    using SharpDX;

    public static class UnitExtensions
    {
        public static Vector3 GetCenterPosition(this IEnumerable<Unit9> units)
        {
            var array = units.ToArray();
            if (array.Length == 0)
            {
                return Vector3.Zero;
            }

            var position = array[0].Position;

            for (var i = 1; i < array.Length; i++)
            {
                position = (position + array[i].Position) / 2;
            }

            return position;
        }
    }
}