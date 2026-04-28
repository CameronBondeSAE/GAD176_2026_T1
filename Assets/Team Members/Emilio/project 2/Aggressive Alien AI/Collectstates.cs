using Anthill.AI;
using Anthill.AI;
using UnityEngine;

public enum AggresiveAlienAI
{
    Lookingforplayer,
    WeaponDrawn, 
    ShootingPlayer, 
    wandering, 
    isPlayerDead,
    CanSeeplayer,
}

public class Collectstates : MonoBehaviour , ISense
{
    public bool isLookingforplayer;
    public bool isweapondDrawn;
    public bool isShootingplayer;
    public bool isWanderingPlayer;
    public bool isIsPlayerDead;
    public bool isCanSeeplayer;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aworldState)
    {
        aworldState.Set(AggresiveAlienAI.Lookingforplayer, isLookingforplayer);
        aworldState.Set(AggresiveAlienAI.WeaponDrawn,isweapondDrawn);
        aworldState.Set(AggresiveAlienAI.ShootingPlayer, isShootingplayer);
        aworldState.Set(AggresiveAlienAI.wandering, isWanderingPlayer);
        aworldState.Set(AggresiveAlienAI.isPlayerDead, isIsPlayerDead);
        aworldState.Set(AggresiveAlienAI.CanSeeplayer, isCanSeeplayer);
        {
            // for (int i = 0; i < aAgent.currentPlan.Count; i++)
            // {
            //     Debug.Log(aAgent.currentPlan[i]);
            // }

        }
    }
}
