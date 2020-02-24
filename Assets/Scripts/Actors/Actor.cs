using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulsLike
{
    public class Actor : Entity
    {
        public ActorStats actorStats;
        public Actor attackedBy;
        public Helper animationHelper;

        float fightWait;
        Animator animator;
        CapsuleCollider actorCollider;
        LayerMask actorLayerMasks;
        private GameObject weaponObject;
        public GameObject weaponPrefab;
        public IWeapon currentWeapon;
        public const float COMBAT_WAIT_TIME = 1.5f;

        protected override void Start()
        {
            base.Start();
            weaponPrefab = Resources.Load<GameObject>("Weapons/Sword");
            actorLayerMasks = LayerMask.GetMask("Actor");
            animator = GetComponentInChildren<Animator>();
            animationHelper = GetComponentInChildren<Helper>();
            if (!animationHelper)
            {
                animator.gameObject.AddComponent<Helper>();
            }
            animationHelper.weaponType = Helper.WeaponType.OneHanded;
            actorCollider = GetComponent<CapsuleCollider>();
            GiveWeapon();
        }

        private void GiveWeapon()
        {
            Transform weaponPlacement = gameObject.GetComponentInChildren<WeaponPlacementPoint>().transform;
            weaponObject = Instantiate(weaponPrefab);
            weaponObject.transform.parent = weaponPlacement;
            currentWeapon = weaponObject.GetComponent<IWeapon>();
            weaponObject.transform.localPosition = currentWeapon.weaponAttackPoint;
            weaponObject.transform.localRotation = Quaternion.Euler(currentWeapon.weaponAttackRotation);
            weaponObject.transform.localScale = currentWeapon.weaponAttackScale;
        }

        public bool CanDetect(Actor actor)
        {
            if (!Actors.instance.IsInProcessingRange(actor))
            {
                return false;
            }

            ActorStats actorStats = actor.actorStats;
            if (actorStats.isDead == true)
            {
                return false;
            }

            if (actorStats.isSneaking == true && attackedBy != actor)
            {
                // sneakChance: (((0.5 + Distance)/500) + (Fatigue/2) + (Luck/10)) * (Sneak/5)
                // spotChance: (((0.5 + Distance)/500) + (Fatigue/2) + (Luck*1.1))
                // detectionChance = sneakChance > spotChance
                float sneakChance = ((0.5f + (Vector3.Distance(transform.position, actor.transform.position) / 500) + (actorStats.fatigue / 2) + ((float)actorStats.luck / 10)) * ((float)actorStats.sneak / 5));
                float spotChance = (((0.5f + Vector3.Distance(transform.position, actor.transform.position)) / 500) + ((float)this.actorStats.fatigue / 2) + ((float)this.actorStats.luck * 1.1f));
                if (sneakChance > spotChance)
                {
                    return false;
                }
            }
            return true;
        }

        public virtual void Kill()
        {
            actorStats.currentHealth = 0f;
            actorStats.isDead = true;
            animator.enabled = false;
            actorCollider.enabled = false;
        }

        private void FightingAnimation()
        {
            animationHelper.PlayWeaponAnim(Helper.WeaponType.OneHanded);
        }

        public void Attack()
        {
            // Play attack animation
            FightingAnimation();

            Collider[] hitEnemies = Physics.OverlapSphere(currentWeapon.weaponAttackPoint, 1.5f);

            foreach (var enemy in hitEnemies)
            {
                bool canGet = enemy.transform.root.TryGetComponent(out Actor victim);
                if (canGet)
                {
                    victim.OnActorAttacked(this, 1.5f);
                }
            }
        }

        public void OnCombat(Actor actor)
        {
            // Start the attack animation, and also enable the weapon attack scripts.
            FightingAnimation();
            actor.OnActorAttacked(this, 1.5f);

            if (actor.actorStats.isDead == true)
            {
                actor = null;
            }
        }

        public void OnActorAttacked(Actor attacker, float weaponDamage)
        {
            // Actor health damage: WeaponDamage + Level/5 + (Luck/5) + (Endurance/4) - (Fatigue/100) - (defenderEndurance/4)
            float damage = (weaponDamage + attacker.actorStats.level) + ((attacker.actorStats.fatigue + 0.5f) / 4 - (actorStats.endurance + 0.5f) / 4)/5;
            if(damage < 1)
            {
                damage = 1;
            }
            attacker.actorStats.fatigue -= Random.Range(0.5f, 1.5f);
            actorStats.currentHealth -= damage;
            attackedBy = attacker;
            if(actorStats.currentHealth <= 0)
            {
                Kill();
            }
        }

        protected virtual void Update()
        {
            if(actorStats != null)
            {
                actorStats.actorPosition = new Vec3(transform.position);
                actorStats.actorRotation = new Quat(transform.rotation);
            }
        }

        protected virtual void OnRevive()
        {
            animator.enabled = true;
            actorStats.currentHealth = actorStats.maxHealth;
            actorStats.isDead = false;
        }

        public PlayerActor FindPlayerActorInArray(Actor[] actors)
        {
            if (actors.Length > 0)
            {
                foreach (Actor actor in actors)
                {
                    if(actor == PlayerActor.instance)
                    {
                        return PlayerActor.instance;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Find the nearest actor in a given list of actors.
        /// </summary>
        /// <param name="actors">List of actors</param>
        /// <returns>A single actor or null if actor array is empty.</returns>
        public Actor FindNearestActor(Actor[] actors, float range = Mathf.Infinity)
        {
            if (actors.Length > 0)
            {
                Actor nearestActor = null;
                float minDist = range;
                Vector3 currentPos = transform.position;
                foreach (Actor actor in actors)
                {
                    if (!Actors.instance.IsInProcessingRange(actor))
                    {
                        continue;
                    }
                    float distance = Vector3.Distance(actor.transform.position, currentPos);
                    if (distance < minDist && actor != this && actor.actorStats.isDead == false)
                    {
                        nearestActor = actor;
                        minDist = distance;
                    }
                }
                if (nearestActor != null)
                {
                    return nearestActor;
                }
            }
            Debug.LogWarning($"[FindNearestActor]: actors is empty");
            return null;
        }
    }
}