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

        public Transform pivot;
        public Transform cameraTransform;
        float targetSpeed;
        float turnSmoothing = .1f;
        float smoothX;
        float smoothY;
        float smoothXVelocity;
        float smoothYVelocity;

        public void Init(Transform t)
        {
            target = t;
            AssignCameraTransform();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void AssignCameraTransform()
        {
            cameraTransform = Camera.main.transform;
            pivot = cameraTransform.parent;
        }

        public void Tick(float d)
        {
            if(!IsInLevel())
            {
                return;
            }
            float h = InputUtility.instance.cameraInput.x;
            float v = InputUtility.instance.cameraInput.y;

            targetSpeed = (mouseSpeed * 4) * Time.deltaTime;
            FollowTarget(d);
            HandleRotation(d, v, h, targetSpeed);
        }

        private void FollowTarget(float d)
        {
            if (!IsInLevel())
            {
                return;
            }
            transform.position = target.position;
        }

        void HandleRotation(float d, float v, float h, float targetSpeed)
        {
            if (!IsInLevel())
            {
                return;
            }
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
            if(pivot != null)
            {
                pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(this);
        }

        private bool IsInLevel()
        {
            bool inLevel = !UnityEngine.SceneManagement.SceneManager.GetSceneByName("MainMenu").isLoaded;
#if UNITY_EDITOR
            Debug.Log($"inLevel = {inLevel}");
#endif
            return inLevel;
        }

        private void Update()
        {
            if(IsInLevel())
            {
                if(cameraTransform == null || pivot == null)
                {
                    AssignCameraTransform();
                }
            }
        }
    }
}