using System.Threading;

namespace Geeky.VanityRemover.Core
{
    public interface IContextAware
    {
        SynchronizationContext Context { get; set; }
    }
}