using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SoulsLike
{
    public class PlayerInventoryUI : MonoBehaviour
    {
        public bool showInterface;

        public Entity entity;

        [Header("Prefabs")]
        public Button buttonPrefab;

        [Header("Inventory View")]
        public GameObject inventoryTransferUserInterface;

        [Header("Scroll Views")]
#pragma warning disable 0649
        [SerializeField] private GameObject playerScrollViewContent;
#pragma warning restore 0649

        [SerializeField] private List<InventoryButton> inventoryButtons;

        private void Start()
        {
            inventoryButtons = new List<InventoryButton>();
        }

        public void AddItemToUI(Item item)
        {
            Button button = Instantiate(buttonPrefab);
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.Name;
            button.transform.SetParent(playerScrollViewContent.transform, false);
            inventoryButtons.Add(button.GetComponent<InventoryButton>());
        }

        public void RemoveItemFromUI(Item Item)
        {
            for (int i = 0; i < inventoryButtons.Count; i++)
            {
                if (inventoryButtons[i].ItemName.text == Item.Name)
                {
                    InventoryButton invButton = inventoryButtons[i];
                    inventoryButtons.Remove(invButton);
                    Destroy(invButton);
                    break;
                }
            }
        }

        public void ClearInterface()
        {
            for (int i = 0; i < inventoryButtons.Count; i++)
            {
                Destroy(inventoryButtons[i].gameObject);
            }
            inventoryButtons.Clear();
        }

        private void Update()
        {
            inventoryTransferUserInterface.SetActive(showInterface);
        }
    }
}