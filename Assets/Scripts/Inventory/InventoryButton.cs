using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SoulsLike
{
    public class InventoryButton : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private Image itemImage;
        public Image ItemImage => itemImage;

        [SerializeField] private TextMeshProUGUI itemName;
        public TextMeshProUGUI ItemName => itemName;

        [SerializeField] private TextMeshProUGUI itemWeight;
        public TextMeshProUGUI ItemWeight => itemWeight;

        [SerializeField] private TextMeshProUGUI itemValue;
        public TextMeshProUGUI ItemValue => itemValue;
#pragma warning restore 0649
    }
}
