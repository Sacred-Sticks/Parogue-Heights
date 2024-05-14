using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Kickstarter.SerializableTypes
{
    [CustomPropertyDrawer(typeof(SerializableType))]
    public class SerializeTypeDrawer : PropertyDrawer
    {
        TypeFilterAttribute typeFilter;
        string[] typeNames, typeFullNames;

        private void Initialize()
        {
            if (typeFullNames != null)
                return;

            typeFilter = (TypeFilterAttribute) Attribute.GetCustomAttribute(fieldInfo, typeof(TypeFilterAttribute));

            var filteredTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(AssemblyLoadEventArgs => AssemblyLoadEventArgs.GetTypes())
                .Where(typeFilter.Filter)
                .ToArray();

            typeNames = filteredTypes.Select(t => t.ReflectedType == null ? t.Name : $"t.ReflectedType.Name + t.Name").ToArray();
            typeFullNames = filteredTypes.Select(t => t.AssemblyQualifiedName).ToArray();
        }

        static bool DefaultFilter(Type type) => !type.IsAbstract && !type.IsInterface && !type.IsGenericType;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize();
            var typeIdProperty = property.FindPropertyRelative("assemblyQualifiedName");

            if (string.IsNullOrEmpty(typeIdProperty.stringValue))
            {
                typeIdProperty.stringValue = typeFullNames.First();
                property.serializedObject.ApplyModifiedProperties();
            }

            int currentIndex = Array.IndexOf(typeFullNames, typeIdProperty.stringValue);
            int selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, typeNames);

            if (selectedIndex < 0)
                return;
            typeIdProperty.stringValue = typeFullNames[selectedIndex];
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}
