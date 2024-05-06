using System.Collections;
using UnityEngine;

namespace Parogue_Heights
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineRendererMediator : MonoBehaviour
    {
        [SerializeField] private string registryKey;

        private LineRenderer lineRenderer;
        private Transform target;
        private Vector3 offset;

        private void Awake() => lineRenderer = GetComponent<LineRenderer>();

        private void Start()
        {
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

            Registry.Register(registryKey, this);
            StopRender();
        }

        public void WithInitialPoint(Transform target) => this.target = target;

        public void WithInitialOffset(Vector3 offset) => this.offset = offset;

        public void RenderLine(Vector3 endPoint)
        {
            StartCoroutine(RenderForOneFrame(endPoint));
        }

        private IEnumerator RenderForOneFrame(Vector3 endPoint)
        {
            StartRender();
            lineRenderer.SetPosition(0, target.position + offset);
            lineRenderer.SetPosition(1, endPoint);
            yield return new WaitForEndOfFrame();
            StopRender();
        }

        private void StartRender() => lineRenderer.enabled = true;

        public void StopRender() => lineRenderer.enabled = false;
    }
}
