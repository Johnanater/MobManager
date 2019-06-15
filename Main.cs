using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Logger = Rocket.Core.Logging.Logger;

namespace MobManager
{
    public class Main : RocketPlugin
    {
        public static Main Instance;
        public const string version = "1.0.0.0";

        protected override void Load()
        {
            Instance = this;

            Logger.Log(string.Format("MobManager by Johnanater, version: {0}", version));
        }

        protected override void Unload()
        {
            Instance = null;
        }

        public override TranslationList DefaultTranslations => new TranslationList
        {
            {"respawned_animals", "Respawned {0} animals!"},
            {"respawned_zombies", "Respawned {0} zombies!"},
            {"not_type", "That is not an animal type!"},
            {"no_permission", "You do not have permission for that!"},

            {"killed_players", "Killed {0} players!"},
            {"killed_zombies", "Killed {0} zombies!"},
            {"killed_animals", "Killed {0} animals!"},
            {"killed_animals_type", "Killed {0} {1}s!"}
        };
    }
}
