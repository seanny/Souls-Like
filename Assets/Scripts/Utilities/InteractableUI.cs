using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SoulsLike
{
    public class InteractableUI : MonoBehaviour
    {
        public static InteractableUI instance { get; private set; }
        public TextMeshProUGUI interactableText;
        Entity[] entities;
        public Entity nearestEntity { get; private set; }
        private string interactionKey;
        public static bool IsInteracting { get; private set; }
        public float recentInteract = 0.5f;
        PlayerActor player;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(this);
        }

        private void Start()
        {
            player = PlayerActor.instance;
        }

        void CheckControllerAndChangeUserInterfaceIfNeeded()
        {
            var gamepads = Gamepad.all;
            bool foundXbox = false;
            foreach(var gamepad in gamepads)
            {
                if(gamepad.displayName == "XInputControllerWindows")
                {
                    foundXbox = true;
                }
            }

            if(foundXbox)
            {
                interactionKey = "A";
            }
            else
            {
                interactionKey = "F";
            }
        }

        void FindNearestEntityToPlayer()
        {
            entities = FindObjectsOfType<Entity>();
            nearestEntity = PlayerActor.instance.FindNearestEntity(entities);
        }

        void ToggleLabelDependingOnDistanceFromPlayer()
        {
            if(nearestEntity != null && player != null)
            {
                if (Vector3.Distance(nearestEntity.transform.position, player.transform.position) < 1.5f)
                {
                    if (nearestEntity.TryGetComponent(out Actor actor))
                    {
                        if (Actors.instance.IsInEnemyFaction(player, actor) == false)
                        {
                            interactableText.text = $"{interactionKey}) {actor.actorStats.name}";
                        }
                    }
                    else
                    {
                        interactableText.text = $"{interactionKey}) Open";
                    }
                    interactableText.gameObject.SetActive(true);
                }
                else interactableText.gameObject.SetActive(false);
            }
        }

        void CheckIfUserIsTryingToInteract()
        {
            if (!SceneManager.GetSceneByName("MainMenu").isLoaded)
            {
                return;
            }

            if (InputUtility.instance.Interaction == true
                && nearestEntity != null
                && recentInteract >= 0.5f
                && Vector3.Distance(nearestEntity.transform.position, player.transform.position) < 1.5f)
            {
                IsInteracting = true;
                recentInteract = 0f;
                if (nearestEntity.TryGetComponent(out InteractableObject interactableObject) == true)
                {
                    interactableObject.OnInteract();
                }
            }
            else IsInteracting = false;
        }

        private void Update()
        {
            if(recentInteract < 0.5f)
            {
                recentInteract += Time.deltaTime;
            }
            CheckControllerAndChangeUserInterfaceIfNeeded();
            FindNearestEntityToPlayer();
            ToggleLabelDependingOnDistanceFromPlayer();
            CheckIfUserIsTryingToInteract();
        }
    }
}
