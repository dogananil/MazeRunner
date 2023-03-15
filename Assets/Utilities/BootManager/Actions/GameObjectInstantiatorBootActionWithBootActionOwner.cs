using System.Threading;
using BootManager.Data;
using Main.utility;
using Cysharp.Threading.Tasks;
using Packages.ServiceLocator;
using UnityEngine;

namespace BootManager.Actions
{
    public class GameObjectInstantiatorBootActionWithBootActionOwner : GameObjectInstantiatorBootActionOwner,
        IBootManagerStatusReceiver, IBootManagerProcessCompleted
    {
        public override async UniTask<BootActionResponse> BootActionAsync(CancellationToken CancellationToken)
        {
            await base.BootActionAsync(CancellationToken);
          
            if (InstantiatedObject)
            {
                var bootAction = InstantiatedObject.GetComponent<IBootActionOwner>();
                await bootAction.BootActionAsync(CancellationToken);
            }

            return new BootActionResponse {BootActionMessage = ActionName, Status = true};
        }

        public GameObjectInstantiatorBootActionWithBootActionOwner(string objectToInstantiate) : base(
            objectToInstantiate)
        {
        }

        public void UpdateBootProgress(string currentBootaction, float currentProgress)
        {
            if (InstantiatedObject == null) return;
            var bootManagerStatus = InstantiatedObject.GetComponent<IBootManagerStatusReceiver>();
            bootManagerStatus?.UpdateBootProgress(currentBootaction, currentProgress);
        }

        public void OnCompletedBoot(float actionsCompleted)
        {
            if (InstantiatedObject == null) return;
            var bootManagerStatus = InstantiatedObject.GetComponent<IBootManagerProcessCompleted>();
            bootManagerStatus?.OnCompletedBoot(actionsCompleted);
        }
    }
}