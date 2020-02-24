using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SoulsLike
{
    [Serializable]
    public class SavedGlobals
    {
        public Dictionary<string, int> globals = new Dictionary<string, int>();
    }
}
