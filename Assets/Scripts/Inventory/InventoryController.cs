using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulsLike
{
    public class InventoryControllerData
    {
        public InventoryUI inventoryUI;
        public PlayerInventoryUI playerInventoryUI;
        public float pressed = 1f;
    }

    public class InventoryController : MonoBehaviour
    {
        public static InventoryController instance;
        public InventoryControllerData inventoryControllerData;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if(inventoryControllerData == null)
            {
                inventoryControllerData = new InventoryControllerData();
            }

            // Inventory UI is attached to the same GameObject as this class.
            inventoryControllerData.inventoryUI = GetComponent<InventoryUI>();
            inventoryControllerData.playerInventoryUI = GetComponent<PlayerInventoryUI>();
        }

        private void Update()
        {
            if (SceneManager.GetSceneByName("MainMenu").isLoaded)
            {
                return;
            }

            if (inventoryControllerData.pressed < 1f)
            {
                inventoryControllerData.pressed += Time.deltaTime;
            }
            // If the player has recently pressed the 'I' key.
            if (InputUtility.instance.Inventory == true && inventoryControllerData.pressed >= 1f)
            {
                inventoryControllerData.pressed = 0f;
                // TODO: Onky show the player inventory interface (i.e. not the transfer screen)
                // FIXME: Inventory UI flashes when keyboard button is pressed.
                // Toggle the interface.
                if (inventoryControllerData.playerInventoryUI.showInterface == false)
                {
                    ShowInventory();
                }
                else
                {
                    MouseCursor.Disable();
                    HideInventory();
                }
            }
        }

        private void EnableCursor()
        {
            MouseCursor.Enable();
        }

        private void ShowInventory()
        {
            EnableCursor();
            inventoryControllerData.playerInventoryUI.showInterface = true;
            inventoryControllerData.playerInventoryUI.ClearInterface();

            // Add player items to UI
            foreach (var item in PlayerActor.instance.entityData.entityInventory.InventoryItems)
            {
                inventoryControllerData.playerInventoryUI.AddItemToUI(item);
            }
        }

        private void HideInventory()
        {
            inventoryControllerData.playerInventoryUI.ClearInterface();
            inventoryControllerData.playerInventoryUI.showInterface = false;
        }

        /// <summary>
        /// Show Inventory Transfer Menu
        /// </summary>
        /// <param name="entity"></param>
        public void ShowInventoryTransfer(Entity entity)
        {
            EnableCursor();
            inventoryControllerData.inventoryUI.showInterface = true;
            inventoryControllerData.inventoryUI.entity = entity;
            inventoryControllerData.inventoryUI.ClearInterface();

            // Add entity items to UI
            foreach(var item in entity.entityData.entityInventory.InventoryItems)
            {
                inventoryControllerData.inventoryUI.AddItemToUI(item, false);
            }

            // Add player items to UI
            foreach (var item in PlayerActor.instance.entityData.entityInventory.InventoryItems)
            {
                inventoryControllerData.inventoryUI.AddItemToUI(item, true);
            }
        }

        /// <summary>
        /// Hide Inventory Transfer
        /// </summary>
        public void HideInventoryTransfer()
        {
            inventoryControllerData.inventoryUI.ClearInterface();
            inventoryControllerData.inventoryUI.showInterface = false;
            inventoryControllerData.inventoryUI.entity = null;
        }
    }
}
