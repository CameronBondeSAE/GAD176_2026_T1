
using Anthill.AI;
using UnityEngine;

public class AlienShottingState : AntAIState
{
    
    public ShootingGun ShootingGun;
    public Collectstates states;
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
        ShootingGun = aGameObject.GetComponent<ShootingGun>();
    }
    
}
