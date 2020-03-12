using System;
using System.Collections.Generic;

namespace SoulsLike
{
    /// <summary>
    /// Save Globals (referenced by save file)
    /// </summary>
    [Serializable]
    public class SavedGlobals
    {
        /// <summary>
        /// Saved Globals Dictionary
        /// </summary>
        public Dictionary<string, int> globals = new Dictionary<string, int>();
    }
}
