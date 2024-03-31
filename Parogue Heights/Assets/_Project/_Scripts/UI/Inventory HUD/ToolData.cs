using UnityEngine;

namespace Parogue_Heights
{
    [CreateAssetMenu(fileName = "New ToolPopupData", menuName = "Parogue Heights/Tool Popup Data")]
    public class ToolData : ScriptableObject
    {
        [SerializeField] private ITool.ToolType toolType;
        [SerializeField] private string toolDescription;
        [SerializeField] private Texture2D toolSprite;

        public ITool.ToolType ToolType => toolType;
        public string ToolDescription => toolDescription;
        public Texture2D ToolSprite => toolSprite;
    }
}
