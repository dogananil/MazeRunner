using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class CancellationTokenExtensionGop
{
    public static CancellationTokenSource GetCancellationTokenSource(this GameObject gameObject, CancellationTokenSource cancelThisSource, bool addOnDestroyTrigger = true)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        if (addOnDestroyTrigger) cancellationTokenSource.RegisterRaiseCancelOnDestroy(gameObject);
        
        if (cancelThisSource != null && !cancelThisSource.IsCancellationRequested)
        {
            cancelThisSource.Cancel();
        }
        
        return cancellationTokenSource;
    }
    
    public static CancellationTokenSource GetCancellationTokenSource(this Component gameObject, CancellationTokenSource cancelThisSource, bool addOnDestroyTrigger = true)
    {
        return GetCancellationTokenSource(gameObject.gameObject, cancelThisSource, addOnDestroyTrigger);
    }
}
