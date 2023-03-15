using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Packages.ServiceLocator;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{

    void Start()
    {
        BootGame().Forget();
    }

    private async UniTask BootGame() 
    {
        //ServiceLocatorManager.Instance.Register<PoolManager>()
    }
}
