using Anthill.AI;
using UnityEngine;

namespace Nicholas.AI
{
    public class AlienHunterAISenseBridge : MonoBehaviour, ISense
    {
        [Header("Alien Sense Reference")] [SerializeField]
        private Controller_AlienHunterSense alienSense;

        private void Start()
        {
            FindAlienSense();
        }

        private void FindAlienSense()
        {
            if (alienSense != null)
            {
                return;
            }

            alienSense = GetComponentInChildren<Controller_AlienHunterSense>(true);

            if (alienSense == null)
            {
                Debug.LogWarning($"{name}: Controller_AlienSense was not found. " +
                                 "Assign it to Controller_AlienAISenseBridge in the Inspector.");
            }
        }

        public void CollectConditions(AntAIAgent agent, AntAICondition worldState)
        {
            if (alienSense == null)
            {
                FindAlienSense();
            }

            if (alienSense == null)
            {
                return;
            }

            alienSense.CollectConditions(agent, worldState);
        }
    }
}