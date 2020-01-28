using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace SoulsLike
{
    class InteractableUI : MonoBehaviour
    {
        public static InteractableUI instance { get; private set; }
        public TextMeshProUGUI interactableText;
        Actor[] actors;
        public Actor nearestActor { get; private set; }
        private string interactionKey;
        public static bool IsInteracting { get; private set; }

        private void Awake()
        {
            instance = this;
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

        void FindNearestActorToPlayer()
        {
            actors = FindObjectsOfType<NonPlayerActor>();
            nearestActor = PlayerActor.instance.FindNearestActor(actors);
        }

        void ToggleLabelDependingOnDistanceFromPlayer()
        {
            var player = PlayerActor.instance;
            if (Vector3.Distance(nearestActor.transform.position, player.transform.position) < 1.5f
                && Actors.instance.IsInEnemyFaction(player, nearestActor) == false)
            {
                interactableText.text = $"{interactionKey}) {nearestActor.actorStats.name}";
                interactableText.gameObject.SetActive(true);
            }
            else interactableText.gameObject.SetActive(false);
        }

        void CheckIfUserIsTryingToInteract()
        {
            if (InputUtility.instance.Interaction == true && nearestActor != null)
            {
                IsInteracting = true;
            }
            else IsInteracting = false;
            Debug.Log($"IsInteracting = {IsInteracting}");
        }

        private void Update()
        {
            CheckControllerAndChangeUserInterfaceIfNeeded();
            FindNearestActorToPlayer();
            ToggleLabelDependingOnDistanceFromPlayer();
            CheckIfUserIsTryingToInteract();
        }
    }
}
