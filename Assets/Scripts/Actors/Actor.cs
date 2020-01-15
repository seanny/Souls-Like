using UnityEngine;

namespace SoulsLike
{
    public class Actor : MonoBehaviour
    {
        public ActorStats actorStats;
        public Actor attackedBy;
        public Helper animationHelper;

        float fightWait;
        Animator animator;

        public const float COMBAT_WAIT_TIME = 1.5f;

        protected virtual void Start()
        {
            animator = GetComponentInChildren<Animator>();
            animationHelper = GetComponentInChildren<Helper>();
            animationHelper.weaponType = Helper.WeaponType.OneHanded;
        }

        public bool CanDetect(Actor actor)
        {
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

        public void FightingAnimation()
        {
            animationHelper.PlayWeaponAnim(Helper.WeaponType.OneHanded);
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
            Debug.Log($"[{name}] Damage by {attacker.name} = {(weaponDamage + attacker.actorStats.level)} + {((attacker.actorStats.fatigue + 0.5f) / 4 - (actorStats.endurance + 0.5f) / 4) / 5}");
            actorStats.currentHealth -= damage;
            attackedBy = attacker;
            if(actorStats.currentHealth <= 0)
            {
                OnDeath();
            }
        }

        protected virtual void OnDeath()
        {
            animator.enabled = false;
            actorStats.currentHealth = 0;
            actorStats.isDead = true;
        }

        protected virtual void OnRevive()
        {
            animator.enabled = true;
            actorStats.currentHealth = actorStats.maxHealth;
            actorStats.isDead = false;
        }

        /// <summary>
        /// Find the nearest actor in a given list of actors.
        /// </summary>
        /// <param name="actors">List of actors</param>
        /// <returns>A single actor or null if actor array is empty.</returns>
        public Actor FindNearestActor(Actor[] actors, float range)
        {
            if (actors.Length > 0)
            {
                Actor nearestActor = null;
                float minDist = Mathf.Infinity;
                Vector3 currentPos = transform.position;
                foreach (Actor actor in actors)
                {
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