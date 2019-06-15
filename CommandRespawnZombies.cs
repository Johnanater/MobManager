using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using UnityEngine;

namespace MobManager
{
    public class CommandRespawnZombies : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "respawnzombies";

        public string Help => "Respawn all zombies";

        public string Syntax => "/respawnzombies";

        public List<string> Aliases => new List<string> { "rz" };

        public List<string> Permissions => new List<string> { "mobmanager.respawnzombies" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            int respawnedZombies = 0;

            foreach (Zombie zombie in Utils.GetZombies())
            {
                if (zombie.isDead)
                {
                    zombie.sendRevive
                    (
                        zombie.type,
                        (byte)zombie.speciality, 
                        zombie.shirt, 
                        zombie.pants, 
                        zombie.hat, 
                        zombie.gear, 
                        zombie.transform.position,
                        Random.Range(0f, 360f)
                    );
                    respawnedZombies++;
                }
            }
            UnturnedChat.Say(caller, Main.Instance.Translate("respawned_zombies", respawnedZombies));
        }
    }
}
