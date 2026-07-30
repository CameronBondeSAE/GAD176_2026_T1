using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sabre.AI
{
    public class BehaviourToggles : MonoBehaviour
    {
        [SerializeField] private Spawner spawnerRef;

        public void TogglePathfind()
        {
            foreach(GameObject NPC in spawnerRef.playerList)
            {
                NPC.GetComponent<BehaviourManager>().pathFindRef.enabled = !NPC.GetComponent<BehaviourManager>().pathFindRef.enabled;
            }
        }
        public void ToggleTurnTowards()
        {
            foreach(GameObject NPC in spawnerRef.playerList)
            {
                NPC.GetComponent<BehaviourManager>().turnTowardsRef.enabled = !NPC.GetComponent<BehaviourManager>().turnTowardsRef.enabled;
            }
        }
        public void ToggleMoveForward()
        {
            foreach(GameObject NPC in spawnerRef.playerList)
            {
                NPC.GetComponent<BehaviourManager>().moveForwardRef.enabled = !NPC.GetComponent<BehaviourManager>().moveForwardRef.enabled;
            }
        }
        public void ToggleAvoid()
        {
            foreach(GameObject NPC in spawnerRef.playerList)
            {
                NPC.GetComponent<BehaviourManager>().frontAvoidRef.enabled = !NPC.GetComponent<BehaviourManager>().frontAvoidRef.enabled;
                NPC.GetComponent<BehaviourManager>().leftAvoidRef.enabled = !NPC.GetComponent<BehaviourManager>().leftAvoidRef.enabled;
                NPC.GetComponent<BehaviourManager>().rightAvoidRef.enabled = !NPC.GetComponent<BehaviourManager>().rightAvoidRef.enabled;
            }
        }
        public void ToggleAlign()
        {
            foreach(GameObject NPC in spawnerRef.playerList)
            {
                NPC.GetComponent<BehaviourManager>().allignRef.enabled = !NPC.GetComponent<BehaviourManager>().allignRef.enabled;
            }
        }
        public void ToggleSeperation()
        {
            foreach(GameObject NPC in spawnerRef.playerList)
            {
                NPC.GetComponent<BehaviourManager>().seperationRef.enabled = !NPC.GetComponent<BehaviourManager>().seperationRef.enabled;
            }
        }


    }
}