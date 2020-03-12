using UnityEngine;

namespace SoulsLike
{
    public class Chest : InteractableObject
    {
        /// <summary>
        /// On Interact
        /// </summary>
        public override void OnInteract()
        {
            Debug.Log($"OnInteract {gameObject.name}");
            if(InventoryController.instance.inventoryControllerData.inventoryUI.entity == this)
            {
                InventoryController.instance.HideInventoryTransfer();
            }
            else
            {
                InventoryController.instance.ShowInventoryTransfer(this);
            }
        }
    }
}