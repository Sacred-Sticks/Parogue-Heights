using DG.Tweening;
using UnityEngine;

namespace Parogue_Heights
{
    public class ToolSpawner : MonoBehaviour, IPlatform
    {
        [SerializeField] private GameObject tokenPrefab;

        private Transform token;

        private const float tokenHeight = 1f;
        private const float tokenMoveHeight = 0.5f;
        private const float tweenDuration = 2f;

        #region UnityEvents
        private void Start()
        {
            PlatformManager.RegisterPlatform(transform.position, this);
            SpawnTool();
        }
        #endregion

        private void SpawnTool()
        {
            token = Instantiate(tokenPrefab, transform.position + Vector3.up * tokenHeight, Quaternion.identity, transform).transform;
            token.DOMove(token.position + Vector3.up * tokenMoveHeight, tweenDuration / 2)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
            token.DORotate(new Vector3(0, 360, 0), tweenDuration, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }

        private void ProvideTool()
        {
            token.DOPause();
            Destroy(token.gameObject);
            var tool = ToolFactory.CreateRandomTool();
            Inventory.Instance.CollectTool(tool);
            PlatformManager.DeregisterPlatform(transform.position);
        }

        #region Platform
        public GameObject GameObject => gameObject;

        public void OnPlayerEnter(Rigidbody body)
        {
            ProvideTool();
        }
        #endregion
    }
}
