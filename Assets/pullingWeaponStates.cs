using Anthill.AI;
using UnityEngine;

public class pullingWeaponStates : AntAIState
{
    public Collectstates States;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void Create(GameObject aGameObject)
    {
        States.isShootingplayer = true;
    }
}
