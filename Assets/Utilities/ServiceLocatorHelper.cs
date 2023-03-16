using System.Threading;
using Packages.ServiceLocator;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Main.utility
{
    public class ServiceLocatorHelper 
    {
        /// <summary>
        /// This utily method loads a gameobject from addressables and addes it to the service locator, so the gameobject can be gotten via resolve
        /// </summary>
        /// <param name="addressableName"></param>
        /// <typeparam name="T"></typeparam>
        public static async UniTask AddGameObjectServiceFromAddressable<T>(string addressableName, CancellationToken cancellationToken, bool onNetwork, bool dontDestroyOnLoad)
        {
            var add = ServiceLocatorManager.Instance.Resolve<LoadAddressableUtility>();
            var obj = await add.LoadAddressable<GameObject>(addressableName, cancellationToken);
            if (obj == null) return;

            var prefabLocator = new ServiceLocatorPrefab()
            {
                ObjectToInstantiate = obj,
                //OnNetwork = onNetwork
            };
            ServiceLocatorManager.Instance.Register<T>(prefabLocator, dontDestroyOnLoad);
        }
    }
}
