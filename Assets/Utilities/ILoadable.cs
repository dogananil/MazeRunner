using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Main.utility
{

    public static class Loadable {
        public static UniTask<bool> CreateLoader(Action start, ref Action when, Action call, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new UniTaskCompletionSource<bool>();
            cancellationToken.Register(() => taskCompletionSource.TrySetCanceled(cancellationToken));

            when += delegate
            {
                call?.Invoke();
                call = null;
                taskCompletionSource.TrySetResult(true);
            };
            start?.Invoke();
            return taskCompletionSource.Task;
        }

        public static UniTask<bool> CreateLoader<T>(Action start, ref Action<T> when, Action<T> call, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new UniTaskCompletionSource<bool>();
            cancellationToken.Register(() => taskCompletionSource.TrySetCanceled(cancellationToken));
            
            void WhenListener(T result)
            {
                call?.Invoke(result);
                call = null;
                taskCompletionSource.TrySetResult(true);
            }

            when += WhenListener;
            start?.Invoke();
            return taskCompletionSource.Task;
        }
    }
}