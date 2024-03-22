using Kickstarter.DependencyInjection;
using UnityEngine;

namespace Parogue_Heights
{
    public class WallGenerator : MonoBehaviour, IDependencyProvider, IWallGenerator
    {
        [SerializeField] private int tilesNeeded;

        private GameObject wallPrefab;
        private readonly Vector2 wallTileSize = new Vector2(3, 3);
        private int wallLayersGenerated;

        #region UnityEvents
        private void Awake()
        {
            wallPrefab = Resources.Load<GameObject>(ResourcePaths.WallTile);
        }

        private void Start()
        {
            WallManagement.RegisterWallGenerator(this);
        }
        #endregion

        #region WallGenerator
        public void GenerateWall(float yTargetPosition)
        {
            var position = transform.position + transform.right * (tilesNeeded * wallTileSize.x / 2);
            for (float y = transform.position.y + wallLayersGenerated * wallTileSize.y; y < yTargetPosition; y += wallTileSize.y)
            {
                wallLayersGenerated++;
                position.y = y;
                for (int i = 0; i < tilesNeeded; i++)
                {
                    var offset = -transform.right * (i * wallTileSize.x + wallTileSize.x / 2);
                    Instantiate(wallPrefab, position + offset, transform.rotation, transform);
                }
            }
        }
        #endregion
    }

    public interface IWallGenerator
    {
        public void GenerateWall(float yTargetPosition);
    }
}
