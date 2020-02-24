using System;
using System.Collections.Generic;

namespace SoulsLike
{
    [Serializable]
    public class SavedGlobals
    {
        public Dictionary<string, int> globals = new Dictionary<string, int>();
    }
}
