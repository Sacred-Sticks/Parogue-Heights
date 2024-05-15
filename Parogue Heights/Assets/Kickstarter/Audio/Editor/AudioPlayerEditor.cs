using UnityEditor;
using UnityEngine;

namespace Kickstarter.Audio
{
    [CustomEditor(typeof(AudioPlayer))]
    public class AudioPlayerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            AudioPlayer audioPlayer = (AudioPlayer) target;

            serializedObject.Update();

            GUI.enabled = false;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));
            GUI.enabled = true;

            SerializeProperties(audioPlayer);

            serializedObject.ApplyModifiedProperties();
        }

        private void SerializeProperties(AudioPlayer audioPlayer)
        {
            SerializedProperty listenTargetProperty = serializedObject.FindProperty("listenTarget");
            if (listenTargetProperty == null)
                return;
            EditorGUILayout.PropertyField(listenTargetProperty);

            EditorGUILayout.Space(10);
            SerializedProperty clipsProperty = serializedObject.FindProperty("clips");
            if (clipsProperty == null)
                return;

            if (audioPlayer.ListenTarget == null || audioPlayer.ListenTarget.Type == null)
                return;

            System.Type notificationType = audioPlayer.ListenTarget.Type.GetNestedType("NotificationType");
            string[] enumNames = System.Enum.GetNames(notificationType);
            clipsProperty.arraySize = enumNames.Length;

            for (int i = 0; i < clipsProperty.arraySize; i++)
            {
                SerializedProperty clipProperty = clipsProperty.GetArrayElementAtIndex(i);
                string label = $"{enumNames[i]} Clips";
                EditorGUILayout.PropertyField(clipProperty, new GUIContent(label));
            }
        }
    }
}
