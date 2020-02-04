using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SoulsLike
{
    [Serializable]
    public struct Quat
    {
        public Quat(Quaternion quaternion)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }

        public float x;
        public float y;
        public float z;
        public float w;
    }

    [Serializable]
    public struct Vec3
    {
        public Vec3(Vector3 vector3)
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
        }

        public float x;
        public float y;
        public float z;
    }

    [Serializable]
    public class InteractableData
    {
        public string inventoryItem;
        public Vec3 position;
        public Quat rotation;
    }

    public abstract class InteractableObject : Entity
    {
        public InteractableData interactableData;

        /// <summary>
        /// Called when the user interacts with an object.
        /// Do not call the base of this method as it throws a NonImplementedException.
        /// </summary>
        public virtual void OnInteract()
        {
            throw new NotImplementedException();
        }
    }
}
