using UnityEngine;

namespace Parogue_Heights
{
    public class FloorGenerator : MonoBehaviour
    {
        [SerializeField] private Vector3Int floorTileUnits;

        // Constants
        private const string FLOOR_TILE_PREFAB_PATH = "Prefabs/Platforms/Floor Tile";
        private readonly Vector3 FLOOR_TILE_SIZE = new Vector3(3, 0.25f, 3);

        #region UnityEvents
        private void Start()
        {
            var origin = transform.position - 
                (FLOOR_TILE_SIZE.x * floorTileUnits.x / 2 * Vector3.right) - 
                (FLOOR_TILE_SIZE.z * floorTileUnits.z / 2 * Vector3.forward) -
                (FLOOR_TILE_SIZE.y * floorTileUnits.y * Vector3.up);
            var floorTilePrefab = Resources.Load<GameObject>(FLOOR_TILE_PREFAB_PATH);
            for (int x = 0; x < floorTileUnits.x; x++)
            {
                for (int z = 0; z < floorTileUnits.z; z++)
                {
                    var position = origin + x * FLOOR_TILE_SIZE.x * Vector3.right + z * FLOOR_TILE_SIZE.z * Vector3.forward;
                    Instantiate(floorTilePrefab, position, Quaternion.identity, transform).name = $"Floor Tile ({x}, {z})";
                }
            }
            Destroy(this);
        }
        #endregion
    }
}
