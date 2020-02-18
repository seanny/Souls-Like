using UnityEngine;

namespace SoulsLike
{
    [RequireComponent(typeof(EntityInventory))]
    public class Entity : MonoBehaviour
    {
        public EntityData entityData;

        protected virtual void Start()
        {
            if(entityData == null)
            {
                entityData = new EntityData();
            }
            bool canGetEntityInventory = TryGetComponent(out EntityInventory inventory);
            if(canGetEntityInventory == true)
            {
                entityData.entityInventory = GetComponent<EntityInventory>();
            }
            else
            {
                entityData.entityInventory = gameObject.AddComponent(typeof(EntityInventory)) as EntityInventory;
            }
        }

        /// <summary>
        /// Finds and returns the nearest entity (Actor, Chest, etc)
        /// </summary>
        /// <param name="entity">Entity to find from.</param>
        /// <param name="range">Range (Default to Infinity)</param>
        /// <returns>Entity</returns>
        public Entity FindNearestEntity(Entity[] entities, float range = Mathf.Infinity)
        {
            if(entities.Length < 1)
            {
                Debug.LogWarning($"[{name}.GetNearestEntity] Entities is empty.");
                return null;
            }

            Entity nearestEntity = null;
            float minDist = range;
            Vector3 currentPos = transform.position;
            foreach (Entity entity in entities)
            {
                float distance = Vector3.Distance(entity.transform.position, currentPos);
                if (distance < minDist && entity != this)
                {
                    nearestEntity = entity;
                    minDist = distance;
                }
            }
            if (nearestEntity != null)
            {
                return nearestEntity;
            }
            return null;
        }
    }
}
