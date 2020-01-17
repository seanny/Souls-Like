using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulsLike
{
    [Serializable]
    public class AiSequence
    {
        public NonPlayerActor actor;
        public LinkedList<AiState> aiStates = new LinkedList<AiState>();
        public StateTypeID lastAiState;
        public StateTypeID currentAiState;
        public bool StatesEmpty
        {
            get
            {
                if (aiStates.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public AiSequence(NonPlayerActor actor)
        {
            this.actor = actor;
        }

        public void AddState(AiState state)
        {
            aiStates.AddLast(state);
        }

        public void Execute(float delta)
        {
            if (aiStates.Count < 1)
            {
                Debug.LogWarning("[AiSequence.Execute]: aiStates is empty.");
                lastAiState = StateTypeID.None;
                return;
            }

            AiState aiState = aiStates.First();

            StateTypeID stateTypeID = aiState.stateType;
            if (stateTypeID == StateTypeID.Combat)
            {
                AiState actualCombat = null;
                float nearestDistance = Mathf.Infinity;
                Vector3 actorPos = actor.transform.position;

                foreach(AiState state in aiStates)
                {
                    if(state.stateType != StateTypeID.Combat)
                    {
                        continue;
                    }

                    Actor target = Actors.instance.FindTarget(actor);
                    float distance = Vector3.Distance(target.transform.position, actorPos);
                    if(distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        actualCombat = state;
                    }
                }

                if(nearestDistance < Mathf.Infinity && aiStates.First() != actualCombat)
                {
                    // Move actualCombat to the front of the queue
                    aiStates.Remove(actualCombat);
                    aiStates.AddFirst(actualCombat);
                }
                aiState = aiStates.First();
            }

            Debug.Log($"Executing {aiState.ToString()}");
            currentAiState = aiState.stateType;
            if (aiState.Execute(delta))
            {
                lastAiState = aiState.stateType;
                if(aiState.canRepeat)
                {
                    aiStates.AddLast(aiState);
                }
                aiStates.RemoveFirst();
            }
        }
    }
}
