using System.Linq;
using BootManager.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BootManager.Utility
{
    public class BootManagerDependencyChecker
    {
        private BootManager bootManager;

        public BootManagerDependencyChecker(BootManager bootManager)
        {
            this.bootManager = bootManager;
        }
        
        public bool ContainsAllDependencies(IBootActionOwner bootActionOwner, out string missingDepency)
        {
            missingDepency = "";
            if (!(bootActionOwner is IContainsBootDependency {Dependecies: { }} dependency) || dependency.Dependecies.Count == 0)
            {
                return true;
            }

            var dependencies = dependency.Dependecies;
            for (int i = 0; i < dependencies.Count; i++)
            {
                if (bootManager.GetBootActionOwners.Select(x => x.ActionName).Contains(dependencies[i]))
                {
                    continue;
                }

                missingDepency = dependencies[i];
                return false;
            }

            return true;
        }
    }
}
