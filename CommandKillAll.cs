using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace MobManager
{
    public class CommandKillAll : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "killall";

        public string Help => "Kill all of a mob";

        public string Syntax => "/killall <type>";

        public List<string> Aliases => new List<string> { "ka" };

        public List<string> Permissions => new List<string> { "mobmanager.killall" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length < 1)
            {
                UnturnedChat.Say(caller, Syntax);
                return;
            }

            string typeString = command[0].ToLower();

            // If player
            if (typeString.Equals("player") || typeString.Equals("players") || typeString.Equals("p"))
            {
                if (!caller.HasPermission("mobmanager.killall.players"))
                {
                    UnturnedChat.Say(caller, Main.Instance.Translate("no_permission"));
                    return;
                }

                int killedPlayers = 0;

                foreach (SteamPlayer player in Provider.clients)
                {
                    UnturnedPlayer untPlayer = UnturnedPlayer.FromSteamPlayer(player);

                    if (!untPlayer.Id.Equals(caller.Id))
                    {
                        untPlayer.Player.life.tellDead(untPlayer.CSteamID, Vector3.zero, 0);
                        killedPlayers++;
                    }
                    UnturnedChat.Say(caller, Main.Instance.Translate("killed_players", killedPlayers));
                }
            }

            // If zombie
            else if (typeString.Equals("zombie") || typeString.Equals("zombies") || typeString.Equals("z"))
            {
                if (!caller.HasPermission("mobmanager.killall.zombies"))
                {
                    UnturnedChat.Say(caller, Main.Instance.Translate("no_permission"));
                    return;
                }

                int killedZombies = 0;

                foreach (Zombie zombie in Utils.GetZombies())
                {
                    if (!zombie.isDead)
                    {
                        ZombieManager.sendZombieDead(zombie, Vector3.zero);
                        killedZombies++;
                    }
                }
                UnturnedChat.Say(caller, Main.Instance.Translate("killed_zombies", killedZombies));
            }

            // If animal
            else if (typeString.Equals("animal") || typeString.Equals("animals") || typeString.Equals("a"))
            {
                if (!caller.HasPermission("mobmanager.killall.animals"))
                {
                    UnturnedChat.Say(caller, Main.Instance.Translate("no_permission"));
                    return;
                }

                int killedAnimals = 0;

                foreach (Animal animal in Utils.GetAnimals())
                {
                    if (!animal.isDead)
                    {
                        AnimalManager.sendAnimalDead(animal, Vector3.zero);
                        killedAnimals++;
                    }
                }
                UnturnedChat.Say(caller, Main.Instance.Translate("killed_animals", killedAnimals));
            }

            // Else try get animal name
            else
            {
                if (!caller.HasPermission("mobmanager.killall.animaltypes"))
                {
                    UnturnedChat.Say(caller, Main.Instance.Translate("no_permission"));
                    return;
                }

                ushort animalType = Utils.GetAnimalType(typeString);
                int killedAnimals = 0;

                if (animalType == 0)
                {
                    UnturnedChat.Say(caller, Main.Instance.Translate("not_type"));
                    return;
                }

                foreach (Animal animal in Utils.GetAnimals())
                {
                    if (!animal.isDead && animal.asset.id == animalType)
                    {
                        AnimalManager.sendAnimalDead(animal, Vector3.zero);
                        killedAnimals++;
                    }
                }
                UnturnedChat.Say(caller, Main.Instance.Translate("killed_animals_type", killedAnimals, typeString));
            }
        }
    }
}
