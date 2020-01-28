using System.Collections;
using UnityEngine;

namespace SoulsLike
{
    class InputUtility : MonoBehaviour
    {
        public static InputUtility instance;

        public PlayerInputActions playerInputActions;

        public Vector2 movementInput;
        public Vector2 cameraInput;

        public bool PowerAttacking { get; private set; }
        public bool GeneralAttacking { get; private set; }
        public bool LockOn { get; private set; }
        public bool Interaction { get; private set; }

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.PlayerControls.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            playerInputActions.PlayerControls.CameraMovement.performed += ctx => CameraMovement(ctx.ReadValue<Vector2>());

            playerInputActions.PlayerControls.LockOn.started += ctx =>
            {
                LockOn = true;
            };

            playerInputActions.PlayerControls.LockOn.canceled += ctx =>
            {
                LockOn = false;
            };

            playerInputActions.PlayerControls.Interaction.started += ctx =>
            {
                Interaction = true;
            };

            playerInputActions.PlayerControls.Interaction.canceled += ctx =>
            {
                Interaction = false;
            };

            playerInputActions.PlayerControls.GeneralAttack.performed += ctx =>
            {
                if (!PowerAttacking) 
                {
                    GeneralAttack();
                }
            };
            playerInputActions.PlayerControls.PowerAttack.performed += ctx =>
            {
                PowerAttacking = true;
                PowerAttack();
            };
            instance = this;
        }

        void PowerAttack()
        {
            StartCoroutine(EndPowerAttack());
        }

        IEnumerator EndPowerAttack()
        {
            yield return new WaitForSeconds(1);
            PowerAttacking = false;
        }

        void GeneralAttack()
        {
            GeneralAttacking = true;
            StartCoroutine(EndGeneralAttack());
        }

        IEnumerator EndGeneralAttack()
        {
            yield return new WaitForSeconds(1);
            GeneralAttacking = false;
        }

        void CameraMovement(Vector2 vector)
        {
            cameraInput = vector;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        private void OnEnable()
        {
            playerInputActions.Enable();
        }

        private void OnDisable()
        {
            playerInputActions.Disable();
        }
    }
}
