using UnityEngine;
using UnityEngine.UI;

namespace SoulsLike
{
    public abstract class BaseHUD : MonoBehaviour
    {
        protected Scrollbar m_Scrollbar;

        // Start is called before the first frame update
        void Start()
        {
            m_Scrollbar = GetComponent<Scrollbar>();
        }
    }
}
