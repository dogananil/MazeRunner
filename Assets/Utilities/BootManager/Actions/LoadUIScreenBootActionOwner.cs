using System;
using System.Collections.Generic;
using System.Threading;
using BootManager.Data;
using Cysharp.Threading.Tasks;
using Packages.ServiceLocator;
using UnityEngine;
using Main.utility;

namespace BootManager.Actions
{
    public class UILoaderInterface //: IUIData
    {
        public Action OnCompletedBoot;
        public CancellationToken CancellationToken;
    }
    
    public class LoadUIScreenBootActionOwner : IBootActionOwner
    {
       // private UIManager UIManager => ServiceLocatorManager.Instance.Resolve<UIManager>();
        private Action OnCompleted;
        private CancellationToken Token;
        public LoadUIScreenBootActionOwner(string name)
        {
            bootName = name;
        }

        private string bootName;
        public string ActionName => bootName;
        public async UniTask<BootActionResponse> BootActionAsync(CancellationToken CancellationToken)
        {
            Token.ThrowIfCancellationRequested();
            this.Token = CancellationToken;
            await Loadable.CreateLoader(LoadUI, ref OnCompleted, null, CancellationToken);
            return new BootActionResponse {BootActionMessage = ActionName, Status = true};
        }

        private void LoadUI()
        {
            var data = new UILoaderInterface {OnCompletedBoot = OnCompleted,CancellationToken = Token};
           // UIManager.Show(bootName, data: data);
        }
    }
}
