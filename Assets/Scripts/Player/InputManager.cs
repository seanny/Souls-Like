using UnityEngine;

namespace SoulsLike
{
    public class InputManager : MonoBehaviour
    {
        [Header("Axis")]
        public float vertical;
        public float horizontal;
        public float controllerVertical;
        public float controllerHorizontal;
        public bool running;
        public Vector3 v, h;

        public bool cameraDisabled;

        StateManager state;
        CameraManager cameraManager;
        Actor attachedActor;
        float delta;

        // Start is called before the first frame update
        void Start()
        {
            state = GetComponent<StateManager>();
            state.Init();

            cameraManager = CameraManager.instance;
            cameraManager.Init(transform);

            attachedActor = GetComponent<Actor>();

        }

        private void GetUpdate()
        {
            vertical = InputUtility.instance.movementInput.x;
            horizontal = InputUtility.instance.movementInput.y;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(attachedActor.actorStats.isDead == true)
            {
                return;
            }

            delta = Time.fixedDeltaTime;
            GetUpdate();
            UpdateState();
            state.FixedTick(delta);
            if (!cameraDisabled)
            {
                cameraManager.Tick(delta);
            }
        }

        private void Update()
        {
            if (attachedActor.actorStats.isDead == true)
            {
                return;
            }
            delta = Time.deltaTime;
            state.Tick(delta);
        }

        void UpdateState()
        {
            state.horizontal = horizontal;
            state.vertical = vertical;

            v = vertical * cameraManager.transform.right;
            h = horizontal * cameraManager.transform.forward;
            state.moveDirection = (v + h).normalized;
            float m = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            state.moveAmount = Mathf.Clamp01(m);
            state.isRunning = running;
        }
    }
}