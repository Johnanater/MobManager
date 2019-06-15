using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace MobManager
{
    public class CommandSpawnAnimal : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "spawnanimal";

        public string Help => "Spawn an animal";

        public string Syntax => "/spawnanimal <type>";

        public List<string> Aliases => new List<string> { "sa" };

        public List<string> Permissions => new List<string> { "mobmanager.spawnanimal" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length < 1)
            {
                UnturnedChat.Say(caller, Syntax);
                return;
            }

            UnturnedPlayer untPlayer = (UnturnedPlayer)caller;
            ushort type = Utils.GetAnimalType(command[0].ToLower());

            if (type == 0)
            {
                UnturnedChat.Say(caller, Main.Instance.Translate("not_type"));
                return;
            }

            Vector3? eyePos = Utils.GetEyePosition(Mathf.Infinity, untPlayer);

            Vector3 spawnPoint = new Vector3();
            if (eyePos.HasValue)
            {
                spawnPoint = eyePos.Value;
            }
            else
            {
                spawnPoint = untPlayer.Position;
            }

            AnimalManager.spawnAnimal(type, spawnPoint, new Quaternion());
        }
    }
}
