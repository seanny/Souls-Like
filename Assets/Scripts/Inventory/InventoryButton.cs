using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SoulsLike
{
    public class InventoryButton : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        public Image ItemImage => itemImage;

        [SerializeField] private TextMeshProUGUI itemName;
        public TextMeshProUGUI ItemName => itemName;

        [SerializeField] private TextMeshProUGUI itemWeight;
        public TextMeshProUGUI ItemWeight => itemWeight;

        [SerializeField] private TextMeshProUGUI itemValue;
        public TextMeshProUGUI ItemValue => itemValue;
    }
}
