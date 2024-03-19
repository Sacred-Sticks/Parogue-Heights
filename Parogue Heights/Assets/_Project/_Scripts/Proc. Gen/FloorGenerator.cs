using UnityEngine;

namespace Parogue_Heights
{
    public class FloorGenerator : MonoBehaviour
    {
        [SerializeField] private Vector2Int floorTileUnits;

        // Constants
        private const string FLOOR_TILE_PREFAB_PATH = "Prefabs/Platforms/Floor Tile";
        private readonly Vector2 FLOOR_TILE_SIZE = new Vector2(3, 3);
        private GameObject floorTilePrefab;

        #region UnityEvents
        private void Start()
        {
            var origin = transform.position - 
                (FLOOR_TILE_SIZE.x * floorTileUnits.x / 2 * Vector3.right) - 
                (FLOOR_TILE_SIZE.y * floorTileUnits.y / 2 * Vector3.forward);
            floorTilePrefab = Resources.Load<GameObject>(FLOOR_TILE_PREFAB_PATH);
            for (int x = 0; x < floorTileUnits.x; x++)
            {
                for (int z = 0; z < floorTileUnits.y; z++)
                {
                    var position = origin + x * FLOOR_TILE_SIZE.x * Vector3.right + z * FLOOR_TILE_SIZE.y * Vector3.forward;
                    Instantiate(floorTilePrefab, position, Quaternion.identity, transform).name = $"Floor Tile ({x}, {z})";
                }
            }
        }
        #endregion
    }
}
