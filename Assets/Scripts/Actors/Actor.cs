using UnityEngine;

namespace SoulsLike
{
    public class Actor : MonoBehaviour
    {
        public ActorStats actorStats;
        public Actor attackedBy;

        public Helper animationHelper;

        protected virtual void Start()
        {
            animationHelper = GetComponentInChildren<Helper>();
            animationHelper.weaponType = Helper.WeaponType.OneHanded;
        }

        public void OnActorAttacked(Actor attacker, float weaponDamage)
        {
            // Actor health damage: WeaponDamage + Level/5 + (Luck/5) + (Endurance/4) - (Fatigue/100) - (defenderEndurance/4)
            float damage = weaponDamage + attacker.actorStats.level + attacker.actorStats.endurance - attacker.actorStats.fatigue - actorStats.endurance / 4;
            if(damage < 1)
            {
                damage = 1;
            }
            Debug.Log($"Damage = {damage}");
            actorStats.currentHealth -= damage;
            attackedBy = attacker;
            if(actorStats.currentHealth <= 0)
            {
                actorStats.currentHealth = 0;
                actorStats.isDead = true;
            }
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