using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    [Serializable]
    public class EntityInventoryData
    {
        public List<Item> inventoryItems = new List<Item>();
    }

    public class EntityInventory : MonoBehaviour
    {
        EntityInventoryData entityInventoryData;

        /// <summary>
        /// Inventory Items
        /// </summary>
#pragma warning disable 0649
        [SerializeField] private List<Item> inventoryItems;
#pragma warning restore 0649
        public List<Item> InventoryItems => inventoryItems;

        private void Start()
        {
            if(entityInventoryData == null)
            {
                entityInventoryData = new EntityInventoryData();
            }
        }

        /// <summary>
        /// Add item to inventory.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            entityInventoryData.inventoryItems.Add(item);
        }

        /// <summary>
        /// Add object to inventory.
        /// </summary>
        /// <param name="interactableObject"></param>
        public void AddItem(InteractableObject interactableObject)
        {
            Item item = Resources.Load<Item>($"Items/{interactableObject.interactableData.inventoryItem}");
            entityInventoryData.inventoryItems.Add(item);
        }

        /// <summary>
        /// Remove item from inventory.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(Item item)
        {
            entityInventoryData.inventoryItems.Remove(item);
        }

        /// <summary>
        /// Remove item from inventory.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveItem(int index)
        {
            entityInventoryData.inventoryItems.RemoveAt(index);
        }

        /// <summary>
        /// Remove all instances of item
        /// </summary>
        /// <param name="item"></param>
        public void RemoveAllItems(Item item)
        {
            for(int i = 0; i < entityInventoryData.inventoryItems.Count; i++)
            {
                if(entityInventoryData.inventoryItems[i] == item)
                {
                    entityInventoryData.inventoryItems.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Empty the inventory.
        /// </summary>
        public void RemoveAllItems()
        {
            entityInventoryData.inventoryItems.Clear();
        }
    }
}
