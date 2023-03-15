using System.Collections.Generic;
using System.Threading;
using BootManager.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace com.youdagames.gop_tt.client.BootManager.Actions
{
    public class TestBootActionOwner : IBootActionOwner,IContainsBootDependency
    {
        public string ActionName => "TestBootActionOwner";
        public async UniTask<BootActionResponse> BootActionAsync(CancellationToken CancellationToken)
        {
            Debug.LogError($"Passed Action: {ActionName}");
            return new BootActionResponse {BootActionMessage = ActionName, Status = true};
        }

        public List<string> Dependecies => new List<string>() {"ExternalCommunicator","SetPingSettingsBootActtionOwner"};
    }
}
