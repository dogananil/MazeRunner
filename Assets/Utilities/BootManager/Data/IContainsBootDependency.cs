using System.Collections.Generic;

namespace BootManager.Data
{
    public interface IContainsBootDependency
    {
        public List<string> Dependecies { get; }
    }
}