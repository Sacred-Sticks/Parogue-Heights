using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kickstarter.Extensions
{
    public static class DictionaryExtensions
    {
        public static void LoadDictionary<TEnum, TType>(this Dictionary<TEnum, TType> dictionary, TType[] items) where TEnum : Enum
        {
            var keys = Enum.GetValues(typeof(TEnum));
            LoadValues(dictionary, keys, items);
        }

        public static void LoadDictionary<TType>(this Dictionary<Enum, TType> dictionary, TType[] items, Type enumType)
        {
            if (!enumType.IsEnum)
            {
                Debug.LogError($"{enumType} is not an Enum");
                return;
            }
            var keys = Enum.GetValues(enumType);
            LoadValues(dictionary, keys, items);
        }

        private static void LoadValues<TEnum, TType>(Dictionary<TEnum, TType> dictionary, Array keys, TType[] items) where TEnum : Enum
        {
            if (keys.Length != items.Length)
            {
                Debug.LogError("Invalid Use of LoadDictionary Extension Method. Items must have the same length as the enum.");
                return;
            }
            var valuesArray = new TEnum[keys.Length];
            keys.CopyTo(valuesArray, 0);
            for (int i = 0; i < keys.Length; i++)
                dictionary.Add(valuesArray[i], items[i]);
        }
    }

    public static class VisualElementExtensions
    {
        public static T CreateChild<T>(this VisualElement parent, params string[] classes) where T : VisualElement, new()
        {
            var child = new T();
            foreach (var @class in classes)
            {
                child.AddToClassList(@class);
            }
            parent.AddChild(child);
            return child;
        }

        public static VisualElement AddChild(this VisualElement parent, VisualElement child)
        {
            parent.hierarchy.Add(child);
            return child;
        }
    }

    public static class TypeExtensions
    {
        private static Type ResolveGenericType(Type type)
        {
            if (type is not { IsGenericType: true })
                return type;

            var genericType = type.GetGenericTypeDefinition();
            return genericType != type ? genericType : type;
        }

        private static bool HasAnyInterfaces(Type type, Type interfaceType)
        {
            return type.GetInterfaces().Any(i => ResolveGenericType(i) == interfaceType);
        }

        public static bool InheritsOrImplements(this Type type, Type baseType)
        {
            type = ResolveGenericType(type);
            baseType = ResolveGenericType(baseType);

            while (type != typeof(object))
            {
                if (baseType == type || HasAnyInterfaces(type, baseType))
                    return true;

                type = ResolveGenericType(type.BaseType);
                if (type == null) return false;
            }

            return false;
        }
    }
}
