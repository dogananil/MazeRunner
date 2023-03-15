using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainGame.Utility
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, object> services = new();
        private static ServiceLocator instance;

        public T Resolve<T>()
        {
            var type = typeof(T);
            if (services.TryGetValue(type, out var result))
            {
                return (T)result;
            }

            return default;
        }

        public void Reset()
        {
            foreach (KeyValuePair<Type, object> keyValuePair in services)
            {
                if (keyValuePair.Value is MonoBehaviour behaviour)
                {
                    Object.DestroyImmediate(behaviour.gameObject);
                }
            }

            services.Clear();
        }

        public static T AsMono<T>(bool destroyOnLoad = false) where T : Component
        {
            GameObject gameObject = new GameObject(typeof(T).Name);
            var obj = gameObject.AddComponent<T>();
            if (!destroyOnLoad)
            {
                Object.DontDestroyOnLoad(gameObject);
            }

            return obj;
        }

        public static ServiceLocator Instance
        {
            get => instance ??= new ServiceLocator();
            set => instance = value;
        }
    }
}