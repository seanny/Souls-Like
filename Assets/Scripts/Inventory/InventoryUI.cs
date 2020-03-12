using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SoulsLike
{
    public class InventoryUI : MonoBehaviour
    {
        public bool showInterface;

        public Entity entity;

        [SerializeField] private List<UnityAction> buttonEvents;

        [Header("Prefabs")]
        public Button buttonPrefab;

        [Header("Inventory View")]
        public GameObject inventoryTransferUserInterface;

        [Header("Scroll Views")]
        [SerializeField] private GameObject playerScrollViewContent;
        [SerializeField] private GameObject entityScrollViewContent;

        [SerializeField] private List<InventoryButton> inventoryButtons;
        [SerializeField] private List<InventoryButton> entityInventoryButtons;

        private void Start()
        {
            inventoryButtons = new List<InventoryButton>();
            entityInventoryButtons = new List<InventoryButton>();
        }

        /// <summary>
        /// Add Item to Inventory UI
        /// </summary>
        /// <param name="item"></param>
        /// <param name="player"></param>
        public void AddItemToUI(Item item, bool player)
        {
            Button button = Instantiate(buttonPrefab);
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.Name;
            button.transform.parent = (player == true ? playerScrollViewContent.transform : entityScrollViewContent.transform);

            button.onClick.AddListener(delegate
            {
                Debug.Log($"{button.name} clicked (player = {player})"); 
                if (player)
                {
                    PlayerActor.instance.entityData.entityInventory.RemoveItem(item);
                    entity.entityData.entityInventory.AddItem(item);
                    RemoveItemFromPlayerUI(item);
                }
                else
                {
                    entity.entityData.entityInventory.RemoveItem(item);
                    PlayerActor.instance.entityData.entityInventory.AddItem(item);
                    RemoveItemFromEntityUI(item);
                }
                AddItemToUI(item, !player);
            });
            if(player)
            {
                inventoryButtons.Add(button.GetComponent<InventoryButton>());
            }
            else
            {
                entityInventoryButtons.Add(button.GetComponent<InventoryButton>());
            }
        }

        /// <summary>
        /// Remove Item From Player Inventory UI
        /// </summary>
        /// <param name="Item"></param>
        public void RemoveItemFromPlayerUI(Item Item)
        {
            for(int i = 0; i < inventoryButtons.Count; i++)
            {
                if(inventoryButtons[i].ItemName.text == Item.Name)
                {
                    Destroy(inventoryButtons[i].gameObject);
                    inventoryButtons.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Remove Item from Entity UI
        /// </summary>
        /// <param name="Item"></param>
        public void RemoveItemFromEntityUI(Item Item)
        {
            for (int i = 0; i < entityInventoryButtons.Count; i++)
            {
                if (entityInventoryButtons[i].ItemName.text == Item.Name)
                {
                    Destroy(entityInventoryButtons[i].gameObject);
                    entityInventoryButtons.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Clear Inventory UI
        /// </summary>
        public void ClearInterface()
        {
            for (int i = 0; i < inventoryButtons.Count; i++)
            {
                Destroy(inventoryButtons[i].gameObject);
            }
            for (int i = 0; i < entityInventoryButtons.Count; i++)
            {
                Destroy(entityInventoryButtons[i].gameObject);
            }
            inventoryButtons.Clear();
            entityInventoryButtons.Clear();
        }

        private void Update()
        {
            inventoryTransferUserInterface.SetActive(showInterface);
        }
    }
}
