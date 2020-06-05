namespace O9K.Hud.Helpers
{
    using System.Collections.Generic;

    using Ensage;

    using Modules;

    internal class ConsoleCommands : IHudModule
    {
        private readonly Dictionary<string, int> consoleCommands = new Dictionary<string, int>
        {
            { "dota_use_particle_fow", 0 },
            { "fog_enable", 0 },
        };

        public void Activate()
        {
            foreach (var cmd in this.consoleCommands)
            {
                Game.GetConsoleVar(cmd.Key).SetValue(cmd.Value);
            }
        }

        public void Dispose()
        {
        }
    }
}