using UnityEngine;
using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
namespace Sabre.AI
{
public class AttackState : AntAIState
{
    protected Health TargetHealth;
    private GuardSense Senses;
    protected bool AttackCooldown;
    [SerializeField] private int baseAttack = 10;
    public override void Create(GameObject aGameObject)
    {
        Senses = aGameObject.GetComponent<GuardSense>();
    }

    public override void Enter()
    {
        TargetHealth = Senses.CurrentTarget.GetComponent<Health>();
        AttackCooldown = false;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        if(AttackCooldown == false)
        {
            AttackCoroutine();
        }
    }

    protected IEnumerator AttackCoroutine()
    {
        AttackCooldown = true;

        int attackVal = baseAttack;
        if(Senses.WeaponHeld == true)
        {
            attackVal += 7;
        }

        TargetHealth.HealthGetSet -= attackVal;

        float randVariable = Random.Range(0, 2);
        yield return new WaitForSeconds(3f + randVariable);
        AttackCooldown = false;
    }

    public override void Exit()
    {
        Senses.FightThief = false;
        Senses.ThiefDefeated = true;
        AttackCooldown = false;
        Senses.CurrentTarget = null;
    }

}
}