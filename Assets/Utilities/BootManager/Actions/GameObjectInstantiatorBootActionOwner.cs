using System.Threading;
using BootManager.Data;
using Cysharp.Threading.Tasks;
using Packages.ServiceLocator;
using UnityEngine;
using Main.utility;

namespace BootManager.Actions
{
    public class GameObjectInstantiatorBootActionOwner : IBootActionOwner
    {
        public GameObject InstantiatedObject { private set; get; }
        public string ActionName { get; }
        private string ObjectToInstantiate;

        private LoadAddressableUtility loadAddressableUtility =>
            ServiceLocatorManager.Instance.Resolve<LoadAddressableUtility>();

        public virtual async UniTask<BootActionResponse> BootActionAsync(CancellationToken CancellationToken)
        {
            var obj = await loadAddressableUtility.LoadAddressable<GameObject>(ObjectToInstantiate,CancellationToken);
            if (obj)
            {
                InstantiatedObject = GameObject.Instantiate(obj);
                InstantiatedObject.SetActive(true);
            }
            await UniTask.DelayFrame(2);
            return new BootActionResponse {BootActionMessage = ActionName, Status = true};
        }

        /// <summary>
        /// This creates a gameobject and runs a bootaction on classes attached to that gameobject
        /// </summary>
        /// <param name="objectToInstantiate"></param>
        public GameObjectInstantiatorBootActionOwner(string objectToInstantiate)
        {
            this.ObjectToInstantiate = objectToInstantiate;
            ActionName = objectToInstantiate;
        }

        public T GetAttachedComponent<T>()
        {
            return InstantiatedObject != null ? InstantiatedObject.GetComponent<T>() : default;
        }
    }
}