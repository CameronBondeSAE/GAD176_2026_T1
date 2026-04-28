using UnityEngine;
using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
namespace Sabre.AI
{
    public class DeadState : AntAIState
    {

        private GuardSense Senses;
        public override void Create(GameObject aGameObject)
        {
            Senses = aGameObject.GetComponent<GuardSense>();
        }
        public override void Enter()
        {
            Senses.inventory.DropObject();
        }
    }
}