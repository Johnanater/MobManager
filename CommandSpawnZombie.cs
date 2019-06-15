using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace MobManager
{
    /* 
     * Disabled, doesn't work
     * Tries to spawn zombies, doesn't fully work
     * The zombie spawns, but is invisible to the client             
     */
    public class CommandSpawnZombie /*: IRocketCommand */
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "spawnzombie";

        public string Help => "Spawn a zombie";

        public string Syntax => "/spawnzombie";

        public List<string> Aliases => new List<string> { "sz" };

        public List<string> Permissions => new List<string> { "mobmanager.spawnzombie" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            // Try to spawn the zombie just like ZombieManager does
            // In a finished version, this would also need to be configurable
            UnturnedPlayer untPlayer = (UnturnedPlayer)caller;
            EZombieSpeciality speciality = EZombieSpeciality.NORMAL;
            byte type = 1;
            byte shirt = (byte)Random.Range(0, 1);
            byte pants = (byte)Random.Range(0, 1);
            byte hat = (byte)Random.Range(0, 1);
            byte gear = (byte)Random.Range(0, 1);
            byte move = (byte)Random.Range(0, 4);
            byte idle = (byte)Random.Range(0, 3);
            Vector3 point = untPlayer.Position;
            point.y += 0.1f;
            byte bound = untPlayer.Player.movement.bound;
            // The newewst zombie will have this id
            ushort zombieId = (ushort)ZombieManager.regions[bound].zombies.Count;

            // Add the zombie
            ZombieManager.instance.addZombie(bound, type, (byte)speciality, shirt, pants, hat, gear, move, idle, point, Random.Range(0f, 360f), false);

            // Get the Zombie by it's new id
            Zombie zombie = ZombieManager.getZombie(point, zombieId);

            /*zombie.sendRevive
            (
                type,
                (byte)speciality,
                shirt,
                pants,
                hat,
                gear,
                untPlayer.Position,
                0
            );*/

            UnturnedChat.Say("Spawned zombie with id " + zombieId);
        }
    }
}
