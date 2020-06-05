namespace O9K.Core.Managers.Menu
{
    using EnsageSharp.Sandbox;

    public enum Lang
    {
        En,

        Ru,

        Cn
    }

    internal static class Language
    {
        public static Lang GetLanguage()
        {
            switch (SandboxConfig.Language)
            {
                case "ru":
                    return Lang.Ru;
                case "zh-Hans":
                case "zh-Hant":
                    return Lang.Cn;
                default:
                    return Lang.En;
            }
        }
    }
}