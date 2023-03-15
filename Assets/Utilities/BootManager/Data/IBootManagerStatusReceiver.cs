using System.Threading;
using Cysharp.Threading.Tasks;

namespace BootManager.Data
{
    /// <summary>
    /// If a bootaction implements this interface, they get the status of the boot process
    /// </summary>
    public interface IBootManagerStatusReceiver
    {
        void UpdateBootProgress(string currentBootaction, float currentProgress);
    }
    
    
    /// <summary>
    /// If a bootaction owner implements this interface, they get feedback when boot is completed
    /// </summary>
    public interface IBootManagerProcessCompleted
    {
        void OnCompletedBoot(float actionsCompleted);
    }
}
