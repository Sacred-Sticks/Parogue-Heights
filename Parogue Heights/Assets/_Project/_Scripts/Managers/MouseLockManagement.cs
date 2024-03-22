using UnityEngine;

namespace Parogue_Heights
{
    public class MouseLockManagement : MonoBehaviour
    {
        private void Start()
        {
            SetCursorLock(true);
        }

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
