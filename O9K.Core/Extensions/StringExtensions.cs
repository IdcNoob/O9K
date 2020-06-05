namespace O9K.Core.Extensions
{
    using System.Linq;
    using System.Text;

    public static class StringExtensions
    {
        internal static string RemoveAbilityLevel(this string name)
        {
            try
            {
                var length = name.Length - 1;
                if (!char.IsDigit(name[length]))
                {
                    return name;
                }

                return name.Substring(0, length).TrimEnd('_');
            }
            catch
            {
                return string.Empty;
            }
        }

        internal static string ToSentenceCase(this string name)
        {
            var result = new StringBuilder().Append(name[0]);

            foreach (var c in name.Skip(1))
            {
                if (char.IsUpper(c))
                {
                    result.Append(" ").Append(char.ToLower(c));
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }
}