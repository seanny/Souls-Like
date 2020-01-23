using System.Collections;
using UnityEngine;

namespace SoulsLike
{
    public class PlayerActor : Actor
    {
        public static PlayerActor instance { get; private set; }

        public Sword sword { get; private set; }

        protected override void Start()
        {
            base.Start();
            sword = GetComponentInChildren<Sword>();
            instance = this;
            StartCoroutine(EnemyCheck());
        }

        IEnumerator EnemyCheck()
        {
            bool enemy;
            while(true)
            {
                yield return new WaitForSeconds(1f);
                NonPlayerActor[] actors = FindObjectsOfType<NonPlayerActor>();

                enemy = false;
                foreach (NonPlayerActor actor in actors)
                {
                    if(actor.enemyActor == this && !actor.actorStats.isDead)
                    {
                        enemy = true;
                        break;
                    }
                }
                if(enemy == true)
                {
                    MusicManager.instance.PlayCombatMusic();
                }
                else
                {
                    MusicManager.instance.PlayRelaxMusic();
                }
            }
        }
    }
}
