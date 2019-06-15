using System.Collections.Generic;
using System.Linq;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace MobManager
{
    public static class Utils
    {
        // Get all zombies on the map
        public static List<Zombie> GetZombies()
        {
            return Object.FindObjectsOfType<Zombie>()?.ToList() ?? new List<Zombie>(0);
        }

        // Get all animals on the map
        public static List<Animal> GetAnimals()
        {
            return Object.FindObjectsOfType<Animal>()?.ToList() ?? new List<Animal>(0);
        }

        // Get an animal id
        public static ushort GetAnimalType(string typeString)
        {
            ushort type = 0;

            if (typeString.Equals("deer"))
                type = 1;
            else if (typeString.Equals("moose"))
                type = 2;
            else if (typeString.Equals("wolf"))
                type = 3;
            else if (typeString.Equals("pig"))
                type = 4;
            else if (typeString.Equals("bear"))
                type = 5;
            else if (typeString.Equals("cow"))
                type = 6;
            else if (typeString.Equals("reindeer"))
                type = 7;
            else
                ushort.TryParse(typeString, out type);

            return type;
        }

        /*
         * Get where a player is looking
         * (From: https://github.com/MrKwabs/Airstrikes/blob/master/CommandBoom.cs#L75)
         * Thanks MrKwabs :)
         */       
        public static Vector3? GetEyePosition(float distance, UnturnedPlayer untPlayer)
        {
            int Masks = RayMasks.BLOCK_COLLISION & ~(1 << 0x15);
            PlayerLook Look = untPlayer.Player.look;

            Physics.Raycast(Look.aim.position, Look.aim.forward, out RaycastHit Raycast, distance, Masks);

            if (Raycast.transform == null)
                return null;

            return Raycast.point;
        }
    }
}
