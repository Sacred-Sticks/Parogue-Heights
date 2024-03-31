using UnityEngine;

namespace Parogue_Heights
{
    public class CursorLockManager : MonoBehaviour
    {
        #region UnityEvents
        private void Start()
        {
            SetCursorLock(true);
        }

        private void OnDestroy()
        {
            SetCursorLock(false);
        }
        #endregion

        public static void ToggleMouseLock()
        {
            SetCursorLock(Cursor.lockState == CursorLockMode.None);
        }

        public static void SetCursorLock(bool isLocked)
        {
            Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
