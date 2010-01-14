using System;

namespace Geeky.VanityRemover.Core
{
    /// <summary>
    /// Options for the application.
    /// </summary>
    [Serializable]
    public struct Options
    {
        /// <summary>
        /// Run and quit without user intervention.
        /// </summary>
        public bool Quiet { get; set; }


        /// <summary>
        /// No output or logging.
        /// </summary>
        public bool Silent { get; set; }


        /// <summary>
        /// What to log.
        /// </summary>
        public bool Verbose { get; set; }


        /// <summary>
        /// Don't ignore special directories (System, Hidden, ... )
        /// </summary>
        public bool CleanAll { get; set; }


        /// <summary>
        /// Default options for the application.
        /// </summary>
        public static Options Default
        {
            get
            {
                return new Options
                {
                    Quiet = false,
                    Silent = false,
                    Verbose = false,
                    CleanAll = false,
                };
            }
        }
    }
}