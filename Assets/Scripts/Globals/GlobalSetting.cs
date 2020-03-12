using UnityEngine;

namespace SoulsLike
{
    [CreateAssetMenu(fileName = "GlobalSetting", menuName = "Global Setting")]
    public class GlobalSetting : ScriptableObject
    {
        /// <summary>
        /// Global Name
        /// </summary>
        public string globalName;

        // Global Value
        public int globalValue;
    }
}
