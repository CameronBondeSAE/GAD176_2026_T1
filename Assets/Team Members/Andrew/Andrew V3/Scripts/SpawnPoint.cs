using System;
using StarterAssets;
using UnityEngine;

namespace SpaceGame
{
    public class SpawnPoint : MonoBehaviour
    {
       //:todo PlayerClass Not FirstPersonController

       public float timer;

       public float spawnCooldown;
       private bool timerFinished = true;
       
       public enum SpawnType
       {
           Player,
           Enemy,
           Other
           
       }

       public SpawnType spawnType;
       
       public void Update()
       {
           if (timerFinished)return;

           timer -= Time.deltaTime;
           if (timer <= 0)
           {
               timerFinished = true;
           }
       }

       public Vector3 Spawn()
       {
           //todo Check (if) can spawn 

           
           timer = spawnCooldown;
           timerFinished = false;
           return transform.position;
       }

       public bool AreaClear()
       {
           if (!timerFinished) return false;
          
           Collider[] hits = Physics.OverlapSphere(transform.position, 2f);
           foreach (var obj in hits)
           {
               if (obj.GetComponent<FirstPersonController>())
               {
                   return false;
               }
               
           }

           return timerFinished;
       }
    }
}
