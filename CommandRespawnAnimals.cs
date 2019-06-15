using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using UnityEngine;

namespace MobManager
{
    public class CommandRespawnAnimals : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "respawnanimals";

        public string Help => "Respawn all animals";

        public string Syntax => "/respawnanimals [<type>]";

        public List<string> Aliases => new List<string> { "ra" };

        public List<string> Permissions => new List<string> { "mobmanager.respawnanimals" };

        public void Execute(IRocketPlayer caller, string[] command) 
        {
            int respawnedAnimals = 0;

            foreach (Animal animal in Utils.GetAnimals())
            {
                // If type
                if (command.Length > 1)
                {
                    ushort animalType = Utils.GetAnimalType(command[0]);

                    if (animalType == 0)
                    {
                        UnturnedChat.Say(caller, Main.Instance.Translate("not_type"));
                        return;
                    }

                    if (animal.asset.id.Equals(animalType) && animal.isDead)
                    {
                        animal.sendRevive
                        (
                            animal.transform.position,
                            Random.Range(0f, 360f)
                        );
                        respawnedAnimals++;
                    }
                }

                // Else all animals
                else if (animal.isDead)
                {
                    animal.sendRevive
                    (
                        animal.transform.position,
                        Random.Range(0f, 360f)
                    );
                    respawnedAnimals++;
                }
            }
            UnturnedChat.Say(caller, Main.Instance.Translate("respawned_animals", respawnedAnimals));
        }
    }
}
