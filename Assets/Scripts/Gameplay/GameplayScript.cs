using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SoulsLike
{
    public class GameplayScript : MonoBehaviour
    {
        public static GameObject scriptHolder;

        public static GameObject GetScriptHolder()
        {
            if (scriptHolder == null)
            {
                scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
                if (scriptHolder == null)
                {
                    Debug.LogError($"Cannot find ScriptHolder object.");
                    return null;
                }
            }
            return scriptHolder;
        }
    }
}
