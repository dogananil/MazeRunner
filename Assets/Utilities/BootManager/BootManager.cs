using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BootManager.Data;
using Cysharp.Threading.Tasks;
using Packages.ServiceLocator;
using UnityEngine;

namespace BootManager.Utility
{
    public class BootManager //: MonoBehaviour
    {
        private readonly List<IBootActionOwner> bootActionOwners = new List<IBootActionOwner>();
        public List<IBootActionOwner> GetBootActionOwners => bootActionOwners;
        public bool completedBoot;
        private BootManagerDependencyChecker bootManagerDependencyChecker;
        public string currentBootAction;
        private float bootActionsCount;
        private CancellationTokenSource cancellationTokenSource;
        private List<IBootManagerStatusReceiver> bootManagerStatusReceivers = new List<IBootManagerStatusReceiver>();
        private List<IBootManagerProcessCompleted> bootManagerCompletedStatusReceivers = new List<IBootManagerProcessCompleted>();

        public void Initialise(IBootManagerStatusReceiver bootManagerStatusReceiver = null)
        {
            AddStatusReceiver(bootManagerStatusReceiver);
            bootManagerDependencyChecker = new BootManagerDependencyChecker(this);
        }

        public void AddStatusReceiver(IBootManagerStatusReceiver bootManagerStatusReceiver)
        {
            if (bootManagerStatusReceiver == null) return;
            this.bootManagerStatusReceivers.Add(bootManagerStatusReceiver);
        }

        public void AddCompletedStatusReceiver(IBootManagerProcessCompleted bootManagerStatusReceiver)
        {
            if (bootManagerStatusReceiver == null) return;
            this.bootManagerCompletedStatusReceivers.Add(bootManagerStatusReceiver);
        }

        public void AddBootAction(IBootActionOwner bootActionOwner)
        {
            if (bootActionOwner == null || bootActionOwners.Contains(bootActionOwner))
            {
                Debug.LogError($"Adding duplicate or null bootactionowner");
                return;
            }

            if (!bootManagerDependencyChecker.ContainsAllDependencies(bootActionOwner, out string missingDepency))
            {
                Debug.LogError($"Boot process stopped since you do not have dependency {missingDepency} already added");
                cancellationTokenSource.Cancel();
                return;
            }

            AddStatusReceiver(bootActionOwner as IBootManagerStatusReceiver);
            AddCompletedStatusReceiver(bootActionOwner as IBootManagerProcessCompleted);

            bootActionOwners?.Add(bootActionOwner);
        }

        public void AddBootActions(IEnumerable<IBootActionOwner> bootActionOwner)
        {
            foreach (var actionOwner in bootActionOwner)
            {
                AddBootAction(actionOwner);
            }
        }

        public async UniTask ExecuteBootActionsAsync(CancellationTokenSource cancellationTokenSource)
        {
            cancellationTokenSource?.Token.Register(ResetBootActions);
            completedBoot = false;
            bool stopLoading = false;
            bootActionsCount = bootActionOwners.Count;
            for (var i = 0; i < bootActionOwners.Count; i++)
            {
                UpdateBootActionProgress(i, cancellationTokenSource.Token);
                var response = await bootActionOwners[i].BootActionAsync(cancellationTokenSource.Token);
                if (response == null)
                {
                    stopLoading = true;
                    break;
                }

                HandleFailedBootAction(response);
                //    Debug.LogError($"Boot Finished loading {bootActionOwners[i].ActionName} ");
            }

            if (stopLoading)
                return;

            UpdateCompleted(bootActionsCount);
            completedBoot = true;
        }

        public void MoveScenes()
        {
            // for (var i = 0; i < bootActionOwners.Count; i++)
            // {
            //     if (bootActionOwners[i] is IGameObjectMover mover)
            //         mover.MoveScene();
            // }
        }

        public void UpdateBootActionProgress(int i, CancellationToken CancellationToken)
        {
            string bootAction = bootActionOwners[i].ActionName;
            currentBootAction = bootAction;
            //    Debug.LogError($"Boot Started loading {bootAction}");
            if (CancellationToken.IsCancellationRequested) return;
            float loadingProgress = i / bootActionsCount;
            UpdateReceivers(bootAction, loadingProgress);
        }

        public void ResetBootActions()
        {
            bootActionOwners.Clear();
        }

        private void UpdateReceivers(string currentBootaction, float currentProgress)
        {
            for (int i = 0; i < bootManagerStatusReceivers.Count; i++)
                bootManagerStatusReceivers[i]?.UpdateBootProgress(currentBootaction, currentProgress);
        }

        private void UpdateCompleted(float completedCount)
        {
            for (int i = 0; i < bootManagerCompletedStatusReceivers.Count; i++)
                bootManagerCompletedStatusReceivers[i]?.OnCompletedBoot(completedCount);
        }

        private void HandleFailedBootAction(BootActionResponse response)
        {
            if (response.Status) return;
            Debug.LogError($"Boot action {response.BootActionMessage} failed to load");
        }
    }
}