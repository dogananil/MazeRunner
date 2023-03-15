using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BootManager.Data
{
    public interface IBootActionOwner
    {
        string ActionName { get; }
        UniTask<BootActionResponse> BootActionAsync(CancellationToken CancellationToken);
    }

    public interface IGameObjectMover
    {
        bool Move { get; }
        GameObject ObjectToInstantiate { get; }

        // void MoveScene()
        // {
        //     if (!Move) return;
        //     var scene = SceneManager.GetSceneByBuildIndex((int) SceneType.Main);
        //     SceneManager.MoveGameObjectToScene(ObjectToInstantiate, scene);
        // }
    }
}