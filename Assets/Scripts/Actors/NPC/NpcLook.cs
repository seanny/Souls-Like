using UnityEngine;

namespace SoulsLike
{
    public class NpcLook : MonoBehaviour
    {
        /// <summary>
        /// Smooth Look towards direction
        /// </summary>
        /// <param name="targetDir"></param>
        /// <param name="lookSpeed"></param>
        public void SmoothLook(Vector3 targetDir, int lookSpeed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
        }
    }
}
