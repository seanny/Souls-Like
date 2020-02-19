using System.Collections;
using UnityEngine;

namespace SoulsLike
{
    class InputUtility : MonoBehaviour
    {
        public static InputUtility instance;

        public PlayerInputActions playerInputActions { get; private set; }

        public Vector2 movementInput;
        public Vector2 cameraInput;

        public bool PowerAttacking { get; private set; }
        public bool GeneralAttacking { get; private set; }
        public bool LockOn { get; private set; }
        public bool Interaction { get; private set; }
        public bool QuestJournal { get; private set; }
        public bool Inventory { get; private set; }
        public bool CommandConsole { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(this);
            CreatePlayerInputActions();
        }

        private void CreatePlayerInputActions()
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

            playerInputActions.PlayerControls.QuestJournal.performed += ctx =>
            {
                if (!QuestJournal)
                {
                    QuestJournal = true;
                    StartCoroutine(FreeQuestKey());
                }
            };

            playerInputActions.PlayerControls.Inventory.performed += ctx =>
            {
                Debug.Log($"Button {playerInputActions.PlayerControls.Inventory} performed");
                if (!Inventory)
                {
                    Inventory = true;
                    StartCoroutine(FreeInventoryKey());
                }
            };

            playerInputActions.PlayerControls.CommandConsole.performed += ctx =>
            {
                if (!Inventory)
                {
                    CommandConsole = true;
                    StartCoroutine(FreeCommandKey());
                }
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
        }

        IEnumerator FreeQuestKey()
        {
            yield return new WaitForSeconds(0.5f);
            QuestJournal = false;
        }

        IEnumerator FreeInventoryKey()
        {
            yield return new WaitForSeconds(0.5f);
            Inventory = false;
        }

        IEnumerator FreeCommandKey()
        {
            yield return new WaitForSeconds(0.5f);
            CommandConsole = false;
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

        private void OnEnable()
        {
            if (playerInputActions != null)
            {
                playerInputActions.Enable();
            }
        }

        private void OnDisable()
        {
            if(playerInputActions != null)
            {
                playerInputActions.Disable();
            }
        }
    }
}
