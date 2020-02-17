using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulsLike
{
    public class InventoryController : MonoBehaviour
    {
        public static InventoryController instance;
        public InventoryUI inventoryUI;
        public PlayerInventoryUI playerInventoryUI;
        public float pressed = 1f;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            // Inventory UI is attached to the same GameObject as this class.
            inventoryUI = GetComponent<InventoryUI>();
            playerInventoryUI = GetComponent<PlayerInventoryUI>();
        }

        private void Update()
        {
            if (SceneManager.GetSceneByName("MainMenu").isLoaded)
            {
                return;
            }

            if (pressed < 1f)
            {
                pressed += Time.deltaTime;
            }
            // If the player has recently pressed the 'I' key.
            if (InputUtility.instance.Inventory == true && pressed >= 1f)
            {
                pressed = 0f;
                // TODO: Onky show the player inventory interface (i.e. not the transfer screen)
                // FIXME: Inventory UI flashes when keyboard button is pressed.
                // Toggle the interface.
                if (playerInventoryUI.showInterface == false)
                {
                    ShowInventory();
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    HideInventory();
                }
            }
        }

        private void EnableCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void ShowInventory()
        {
            EnableCursor();
            playerInventoryUI.showInterface = true;
            playerInventoryUI.ClearInterface();

            // Add player items to UI
            foreach (var item in PlayerActor.instance.EntityInventory.InventoryItems)
            {
                playerInventoryUI.AddItemToUI(item);
            }
        }

        private void HideInventory()
        {
            playerInventoryUI.ClearInterface();
            playerInventoryUI.showInterface = false;
        }

        public void ShowInventoryTransfer(Entity entity)
        {
            EnableCursor();
            inventoryUI.showInterface = true;
            inventoryUI.entity = entity;
            inventoryUI.ClearInterface();

            // Add entity items to UI
            foreach(var item in entity.EntityInventory.InventoryItems)
            {
                inventoryUI.AddItemToUI(item, false);
            }

            // Add player items to UI
            foreach (var item in PlayerActor.instance.EntityInventory.InventoryItems)
            {
                inventoryUI.AddItemToUI(item, true);
            }
        }

        public void HideInventoryTransfer()
        {
            inventoryUI.ClearInterface();
            inventoryUI.showInterface = false;
            inventoryUI.entity = null;
        }
    }
}
