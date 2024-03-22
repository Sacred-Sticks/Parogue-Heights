using System.Collections.Generic;
using UnityEngine;

namespace Parogue_Heights
{
    public class WallManagement : MonoBehaviour
    {
        private static readonly List<IWallGenerator> wallGenerators = new List<IWallGenerator>();

        private void Awake()
        {
            wallGenerators.Clear();
        }

        public static void RegisterWallGenerator(IWallGenerator wallGenerator)
        {
            wallGenerators.Add(wallGenerator);
        }

        public static void GenerateWalls(float targetHeight)
        {
            foreach (var wallGenerator in wallGenerators)
                wallGenerator.GenerateWall(targetHeight);
        }
    }
}
