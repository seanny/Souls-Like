using UnityEngine;

namespace SoulsLike
{
    public class Chest : InteractableObject
    {
        public override void OnInteract()
        {
            Debug.Log($"OnInteract {gameObject.name}");
            if(InventoryController.instance.inventoryUI.entity == this)
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