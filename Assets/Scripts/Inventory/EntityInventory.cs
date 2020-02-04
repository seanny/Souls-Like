using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SoulsLike
{
    public class EntityInventory : MonoBehaviour
    {
        /// <summary>
        /// Inventory Items
        /// </summary>
        [SerializeField] private List<Item> inventoryItems;
        public List<Item> InventoryItems => inventoryItems;

        private void Start()
        {
            if(inventoryItems == null)
            {
                inventoryItems = new List<Item>();
            }
        }

        /// <summary>
        /// Add item to inventory.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            inventoryItems.Add(item);
        }

        /// <summary>
        /// Add object to inventory.
        /// </summary>
        /// <param name="interactableObject"></param>
        public void AddItem(InteractableObject interactableObject)
        {
            Item item = Resources.Load<Item>($"Items/{interactableObject.interactableData.inventoryItem}");
            inventoryItems.Add(item);
        }

        /// <summary>
        /// Remove item from inventory.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(Item item)
        {
            inventoryItems.Remove(item);
        }

        /// <summary>
        /// Remove item from inventory.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveItem(int index)
        {
            inventoryItems.RemoveAt(index);
        }

        /// <summary>
        /// Remove all instances of item
        /// </summary>
        /// <param name="item"></param>
        public void RemoveAllItems(Item item)
        {
            for(int i = 0; i < inventoryItems.Count; i++)
            {
                if(inventoryItems[i] == item)
                {
                    inventoryItems.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Empty the inventory.
        /// </summary>
        public void RemoveAllItems()
        {
            inventoryItems.Clear();
        }
    }
}
