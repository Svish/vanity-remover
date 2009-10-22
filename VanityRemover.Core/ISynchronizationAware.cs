using System.Threading;

namespace Geeky.VanityRemover.Core
{
    /// <summary>
    /// Implementors use the <see cref="Context"/> when for
    /// example raising events.
    /// </summary>
    public interface ISynchronizationAware
    {
        /// <summary>
        /// Gets or sets the <see cref="SynchronizationContext"/>. 
        /// If set to null, a default context without synchronization
        /// will be used.
        /// </summary>
        SynchronizationContext Context { get; set; }
    }
}