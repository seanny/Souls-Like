using UnityEngine;

namespace SoulsLike
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager instance { get; private set; }

        public bool lockOn;
        public float followSpeed = 9;
        public float mouseSpeed = 2;
        public Transform target;
        public float minAngle = -35f;
        public float maxAngle = 35f;
        public float lookAngle;
        public float tiltAngle;

        public float controllerVertical;
        public float controllerHorizontal;

        [HideInInspector] public Transform pivot;
        [HideInInspector] public Transform cameraTransform;
        float targetSpeed;
        float turnSmoothing = .1f;
        float smoothX;
        float smoothY;
        float smoothXVelocity;
        float smoothYVelocity;

        public void Init(Transform t)
        {
            target = t;
            cameraTransform = Camera.main.transform;
            pivot = cameraTransform.parent;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Tick(float d)
        {
            float h = InputUtility.instance.cameraInput.x;
            float v = InputUtility.instance.cameraInput.y;

            targetSpeed = (mouseSpeed * 4) * Time.deltaTime;
            FollowTarget(d);
            HandleRotation(d, v, h, targetSpeed);
        }

        private void FollowTarget(float d)
        {
            transform.position = target.position;
        }

        void HandleRotation(float d, float v, float h, float targetSpeed)
        {
            if (turnSmoothing > 0)
            {
                smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXVelocity, turnSmoothing);
                smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYVelocity, turnSmoothing);
            }
            else
            {
                smoothX = h;
                smoothY = v;
            }

            if (lockOn)
            {

            }

            lookAngle += smoothX * targetSpeed;
            transform.rotation = Quaternion.Euler(0, lookAngle, 0);

            tiltAngle -= smoothY * targetSpeed;
            tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
            pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }
}