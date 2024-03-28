using UnityEngine;
using UnityEditor;
using System.IO;

namespace Kickstarter.MenuItems
{
    public class CreateBlendFileMenu
    {
        [MenuItem("Assets/Create/Kickstarter/Blend File")]
        public static void CreateBlendFile()
        {
            string selectedFolderPath = "Assets";

            foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
            {
                selectedFolderPath = AssetDatabase.GetAssetPath(obj);
                if (File.Exists(selectedFolderPath))
                {
                    selectedFolderPath = Path.GetDirectoryName(selectedFolderPath);
                }
                break;
            }

            string fileName = "New Model.blend";

            string filePath = Path.Combine(selectedFolderPath, fileName);

            FileStream fileStream = File.Create(filePath);
            fileStream.Close();

            AssetDatabase.Refresh();
        }
    }
}
