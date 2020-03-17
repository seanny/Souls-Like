using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SoulsLike
{
    public class AiStateManager : MonoBehaviour
    {
        public List<AiState> aiStatePrefabs;
        private List<AiState> m_AiStates = new List<AiState>();

        private void Start()
        {
            foreach(var item in aiStatePrefabs)
            {
                AiState aiState = Instantiate(item, transform).GetComponent<AiState>();
                m_AiStates.Add(aiState);
            }
        }

        /// <summary>
        /// Find the next AiState.
        /// </summary>
        /// <returns></returns>
        public AiState FindNextState()
        {
            Debug.Log($"[AiStateManager] Searching for next AI State.");
            foreach (AiState aiState in m_AiStates)
            {
                // Check if there is less than 1 AiCondition
                if(aiState.aiConditions.Count < 1)
                {
                    // If aiState has no conditions, use this state right away.
                    Debug.Log($"[AiStateManager] Using {aiState} right away as it has no conditions.");
                    return aiState;
                }
                // Check if all conditions are true
                foreach(AiCondition aiCondition in aiState.aiConditions)
                {
                    // Check if the AiCondition.IsConditionMet is true.
                    if(aiCondition.IsConditionMet() == true)
                    {
                        return aiState;
                    }
                }
            }
            return null;
        }

        private void Update()
        {
            Execute();
        }

        /// <summary>
        /// Execute all AiStates.
        /// </summary>
        private void Execute()
        {
            if(m_AiStates.Count < 1)
            {
                Debug.LogWarning("[AiStateManager.Execute]: aiStates is empty.");
                return;
            }
            AiState aiState = FindNextState();
            if(aiState == null)
            {
                Debug.LogWarning("[AiStateManager.Execute]: Cannot get next AiState");
                return;
            }
            aiState.Execute();
        }
    }
}
