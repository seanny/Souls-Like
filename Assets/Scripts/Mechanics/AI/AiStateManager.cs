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
                if(aiState.aiConditions.Count < 1)
                {
                    Debug.Log($"[AiStateManager] Using {aiState} right away as it has no conditions.");
                    // If aiState has no conditions, use this state right away.
                    return aiState;
                }
                // Check if all conditions are true
                foreach(AiCondition aiCondition in aiState.aiConditions)
                {
                    if(!string.IsNullOrEmpty(aiCondition.scriptClass) && !string.IsNullOrEmpty(aiCondition.scriptMethod))
                    {
                        Type magicType = Type.GetType(aiCondition.scriptClass);
                        ConstructorInfo magicConstructor = magicType.GetConstructor(Type.EmptyTypes);
                        object magicClassObject = magicConstructor.Invoke(new object[] { });

                        MethodInfo magicMethod = magicType.GetMethod(aiCondition.scriptMethod);
                        if(magicMethod.ReturnType != typeof(float))
                        {
                            Debug.LogError($"[AiStateManager] {aiCondition.scriptClass}.{aiCondition.scriptMethod} return type must be a float.");
                            break;
                        }
                        object magicValue = magicMethod.Invoke(magicClassObject, aiCondition.scriptParamaters);

                        switch(aiCondition.comparisonOperator)
                        {
                            case ComparisonOperator.EqualTo:
                                if((float)magicValue == aiCondition.value)
                                {
                                    Debug.Log($"[AiStateManager] Using {aiState} as == {aiCondition.value}.");
                                    return aiState;
                                }
                                break;
                            case ComparisonOperator.LessOrEqualTo:
                                if ((float)magicValue <= aiCondition.value)
                                {
                                    Debug.Log($"[AiStateManager] Using {aiState} as <= {aiCondition.value}.");
                                    return aiState;
                                }
                                break;
                            case ComparisonOperator.LessThan:
                                if ((float)magicValue < aiCondition.value)
                                {
                                    Debug.Log($"[AiStateManager] Using {aiState} as < {aiCondition.value}.");
                                    return aiState;
                                }
                                break;
                            case ComparisonOperator.MoreOrEqualTo:
                                if ((float)magicValue >= aiCondition.value)
                                {
                                    Debug.Log($"[AiStateManager] Using {aiState} as >= {aiCondition.value}.");
                                    return aiState;
                                }
                                break;
                            case ComparisonOperator.MoreThan:
                                if ((float)magicValue > aiCondition.value)
                                {
                                    Debug.Log($"[AiStateManager] Using {aiState} as > {aiCondition.value}.");
                                    return aiState;
                                }
                                break;
                            case ComparisonOperator.NotEqualTo:
                                if ((float)magicValue != aiCondition.value)
                                {
                                    Debug.Log($"[AiStateManager] Using {aiState} as != {aiCondition.value}.");
                                    return aiState;
                                }
                                break;
                        }
                        
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
