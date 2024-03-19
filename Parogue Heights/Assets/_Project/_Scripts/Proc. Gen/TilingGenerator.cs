using UnityEngine;

namespace Parogue_Heights
{
    public class TilingGenerator : MonoBehaviour
    {
        [SerializeField] private Vector3Int floorTileUnits;
        [SerializeField] private string tilePrefabPath;
        [SerializeField] private Vector3 tileDimensions;

        #region UnityEvents
        private void Start()
        {
            var origin = transform.position -
                (tileDimensions.z * floorTileUnits.z / 2 * Vector3.forward) -
                (tileDimensions.x * floorTileUnits.x / 2 * Vector3.right);
            var tilePrefab = Resources.Load<GameObject>(tilePrefabPath);
            for (int x = 0; x < floorTileUnits.x; x++)
            {
                for (int y = 0; y < floorTileUnits.y; y++)
                {
                    for (int z = 0; z < floorTileUnits.z; z++)
                    {
                        var position = origin +
                            (z * tileDimensions.z * Vector3.forward) +
                            (x * tileDimensions.x * Vector3.right) +
                            (y * tileDimensions.y * Vector3.up);
                        Instantiate(tilePrefab, position, Quaternion.identity, transform).name = $"Tile {x}, {y}, {z}";
                    }
                }
            }
            Destroy(this);
        }
        #endregion
    }
}
