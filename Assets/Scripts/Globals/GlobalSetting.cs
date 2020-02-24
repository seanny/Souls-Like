using UnityEngine;

namespace SoulsLike
{
    [CreateAssetMenu(fileName = "GlobalSetting", menuName = "Global Setting")]
    public class GlobalSetting : ScriptableObject
    {
        public string globalName;
        public int globalValue;
    }
}
