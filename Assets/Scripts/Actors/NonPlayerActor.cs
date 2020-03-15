using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace SoulsLike
{
    [RequireComponent(typeof(StateManager))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class NonPlayerActor : Actor
    {
        public const float MIN_FIGHT_DISTANCE = 1.5f;

        public NpcMovement NpcMovement => m_NpcMovement;
        public NpcCombat NpcCombat => m_NpcCombat;

        [SerializeField] private NpcMovement m_NpcMovement;
        [SerializeField] private NpcCombat m_NpcCombat;


        public StateManager stateManager;
        float delta;
        bool canRun;
        public AiState[] aiStates;
        public bool isEnemy;

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            stateManager = GetComponent<StateManager>();
            if (!stateManager)
            {
                stateManager = gameObject.AddComponent<StateManager>();
            }
            stateManager.Init();
            m_NpcMovement = gameObject.AddComponent<NpcMovement>();
            m_NpcCombat = gameObject.AddComponent<NpcCombat>();
            actorStats.maxHealth = 80f + (actorStats.level / 2) + Random.Range(0, 10);
            actorStats.currentHealth = actorStats.maxHealth;
            canRun = true;
        }

        /// <summary>
        /// Kill Actor
        /// </summary>
        public override void Kill()
        {
            base.Kill();
            m_NpcMovement.Disable();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if(actorStats.isDead == true || !canRun)
            {
                return;
            }

            float delta = Time.deltaTime;
            stateManager.Tick(delta);
        }

        private void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            stateManager.FixedTick(delta);
        }
    }
}