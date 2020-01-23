using UnityEngine;

namespace SoulsLike
{
    public class StateManager : MonoBehaviour
    {
        [HideInInspector] public float vertical;
        [HideInInspector] public float horizontal;
        [HideInInspector] public Vector3 moveDirection;
        [HideInInspector] public float moveAmount;

        public float moveSpeed = 2f;
        public float runSpeed = 3.5f;
        public float rotateSpeed = 5f;
        public float groundSpeed = 0.5f;
        [HideInInspector] public bool isGrounded;
        [HideInInspector] public bool isRunning;
        [HideInInspector] public bool lockOn;
        [HideInInspector] public bool drinkingPotion;
        private bool drinkingRoutine;
        float waitTime = 0f;

        [HideInInspector] public GameObject activeObject;
        [HideInInspector] public Animator animator;
        [HideInInspector] public Rigidbody rigidBody;
        [HideInInspector] public float delta;
        public LayerMask ignoreLayers;

        public void Init(bool isPlayer = false)
        {
            SetupAnimator();
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.angularDrag = 999;
            rigidBody.drag = 4;
            rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            gameObject.layer = 9;
            ignoreLayers = ~(1 << 10);

            animator.SetBool("IsGrounded", true);
        }

        private void SetupAnimator()
        {
            if (activeObject == null)
            {
                animator = GetComponentInChildren<Animator>();
                if (animator != null)
                {
                    activeObject = animator.gameObject;
                }
            }

            if (animator == null)
            {
                animator = activeObject.GetComponent<Animator>();
            }
            animator.applyRootMotion = false;
        }

        public void FixedTick(float d)
        {
            delta = d;

            rigidBody.drag = (moveAmount > 0 || isGrounded == false) ? 0 : 4;

            float targetSpeed;
            if (isRunning)
            {
                targetSpeed = runSpeed;
            }
            else
            {
                targetSpeed = moveSpeed;
            }

            if(isGrounded)
            {
                rigidBody.velocity = moveDirection * (targetSpeed * moveSpeed);
            }

            if(isRunning)
            {
                lockOn = false;
            }

            if(!lockOn)
            {
                Vector3 targetDirection = moveDirection;
                targetDirection.y = 0;
                if (targetDirection == Vector3.zero)
                {
                    targetDirection = transform.forward;
                }
                Quaternion transformRotation = Quaternion.LookRotation(targetDirection);
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, transformRotation, delta * moveAmount * rotateSpeed);
                transform.rotation = targetRotation;
            }
            HandleMovementAnimations();
        }

        public void Tick(float d)
        {
            delta = d;
            isGrounded = IsGrounded();
            animator.SetBool("IsGrounded", isGrounded);
            if(!isRunning)
            {
                lockOn = InputUtility.instance.LockOn;
            }
            waitTime += d;
            if (InputUtility.instance.GeneralAttacking == true && waitTime > 1.5f)
            {
                waitTime = 0f;
                PlayerActor.instance.Attack();
            }
        }

        private void HandleMovementAnimations()
        {
            float animationSpeed = moveAmount;
            if(!isRunning)
            {
                animationSpeed = Mathf.Clamp(animationSpeed, 0, 0.5f);
            }
            animator.SetFloat("Vertical", animationSpeed, 0.2f, delta);
        }

        public bool IsGrounded()
        {
            bool r = false;
            Vector3 origin = transform.position + (Vector3.up * groundSpeed);
            Vector3 direction = new Vector3(0, -1);
            float distance = 1f;
            RaycastHit raycastHit;
            if (Physics.Raycast(origin, direction, out raycastHit, distance, ignoreLayers))
            {
                r = true;
                Vector3 targetPosition = raycastHit.point;
                transform.position = targetPosition;
            }
            return r;
        }
    }
}