using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using BootManager.Actions;
using Cysharp.Threading.Tasks;
/*using Fusion;
using GameUI.Staging;*/
using Packages.ServiceLocator;
using UnityEngine;
using Main.utility;

public class ServiceLocatorsManager
{
    private const string NetworkSceneManagerBaseGameObject = "MapLoader";
    private const string LoadingGameObject = "LoadingCanvas";



    /// <summary>
    /// Create services that require prefabs that are added the addressable system.
    /// </summary>
    private async UniTask CreateNonNetworkGameObjectServices(CancellationToken cancellationToken)
    {
        /*await ServiceLocatorHelper.AddGameObjectServiceFromAddressable<MapLoader>(NetworkSceneManagerBaseGameObject, cancellationToken, false, false);
        await ServiceLocatorHelper.AddGameObjectServiceFromAddressable<LoadingManager>(LoadingGameObject, cancellationToken, false, false);*/
    }


    /// <summary>
    /// Create services so they can be easily gotten in other classes, instead of making singleton classes
    /// </summary>
    public async UniTask CreateServices(CancellationToken cancellationToken)
    {
        /*ServiceLocatorManager.Instance.Register<UIManager>(ServiceLocatorManager.AsMono<UIManager>(true));
        ServiceLocatorManager.Instance.Register<App>(ServiceLocatorManager.AsMono<App>(true));
        ServiceLocatorManager.Instance.Register<LoadAddressableUtility>(ServiceLocatorManager.AsMono<LoadAddressableUtility>(true));
        ServiceLocatorManager.Instance.Register<CastleNetworkTimer>(ServiceLocatorManager.AsMono<CastleNetworkTimer>(true));
        ServiceLocatorManager.Instance.Register<GameObjectsCreators>(ServiceLocatorManager.AsMono<GameObjectsCreators>(true));
        ServiceLocatorManager.Instance.Register<MultiplayerGameStartupBootAction>(ServiceLocatorManager.AsMono<MultiplayerGameStartupBootAction>(true));*/
        await CreateNonNetworkGameObjectServices(cancellationToken);
    }
}