using UnityEngine;

namespace SoulsLike
{
    public class SceneWeapon : InteractableObject
    {
        public ItemWeapon weaponItem;

        protected override void Start()
        {
            base.Start();
            interactableData.inventoryItem = weaponItem.Name;
            interactableData.position = new Vec3(transform.position);
            interactableData.rotation = new Quat(transform.rotation);
        }

        public override void OnInteract()
        {
            // TODO: Add item to inventory.
        }
    }
}
