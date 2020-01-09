using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public class InputManager : MonoBehaviour
    {
        public float vertical;
        public float horizontal;
        public bool running;

        public bool cameraDisabled;

        StateManager state;
        CameraManager cameraManager;
        float delta;

        // Start is called before the first frame update
        void Start()
        {
            state = GetComponent<StateManager>();
            state.Init();

            cameraManager = CameraManager.instance;
            cameraManager.Init(transform);

        }

        private void GetUpdate()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            running = Input.GetKey(KeyCode.LeftShift);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
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
            delta = Time.deltaTime;
            state.Tick(delta);
        }

        void UpdateState()
        {
            state.horizontal = horizontal;
            state.vertical = vertical;

            Vector3 v = vertical * cameraManager.transform.forward;
            Vector3 h = horizontal * cameraManager.transform.right;
            state.moveDirection = (v + h).normalized;
            float m = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            state.moveAmount = Mathf.Clamp01(m);
            state.isRunning = running;
        }
    }
}