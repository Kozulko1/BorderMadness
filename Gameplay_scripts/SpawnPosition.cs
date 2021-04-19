using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameScripts
{
    public static class SpawnPosition
    {
        private static float minPoint = 446F;
        private static float maxPoint = 1010F;
        private static float xPosition = 3095F;

        public static Vector3 GetBasicSpawnPoint()
        {
            float yPosition = Random.Range(minPoint, maxPoint);
            return new Vector3(xPosition, yPosition);
        }
        public static Vector3 GetFacadeSpawn()
        {
            float yPosition = Random.Range(-58F, 1106F);
            return new Vector3(3465F, yPosition);
        }
        public static Vector3 GetGonzalesSpawnPoint()
        {
            return new Vector3(xPosition, 181F);
        }
    }
}