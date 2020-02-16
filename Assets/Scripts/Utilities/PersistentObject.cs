using UnityEngine;

namespace SoulsLike
{
    public class PersistentObject : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
