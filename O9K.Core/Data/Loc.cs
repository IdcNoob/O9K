namespace O9K.Core.Data
{
    using System.Collections.Generic;

    using Ensage.SDK.Helpers;

    using EnsageSharp.Sandbox;

    using Managers.Menu;

    public static class Loc
    {
        public enum Lang2
        {
            En,

            Ru,

            Cn
        }

        private static readonly Dictionary<string, (string Ru, string Cn)> Localization = new Dictionary<string, (string, string)>
        {
            { "Units", ("Юниты", "单位") }
        };

        static Loc()
        {
            switch (SandboxConfig.Language)
            {
                case "ru":
                    Lang = Lang.Ru;
                    break;
                case "zh-Hans":
                case "zh-Hant":
                    Lang = Lang.Cn;
                    break;
                default:
                    Lang = Lang.En;
                    break;
            }
        }

        public static Lang Lang { get; }

        public static string A(string name)
        {
            return LocalizationHelper.LocalizeAbilityName(name);
        }

        public static string E(string name)
        {
            return LocalizationHelper.LocalizeName(name);
        }

        public static string S(string name)
        {
            if (Lang == Lang.En || !Localization.TryGetValue(name, out var loc))
            {
                return name;
            }

            return Lang == Lang.Ru ? loc.Ru : loc.Cn;
        }
    }
}