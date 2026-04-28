using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Sabre.AI
{
public enum AIType {Guard, Thief}

public class Health : MonoBehaviour
{
    public AIType type;
    private int health;
    private int baseHealth = 50;
    private Vector3 spawnVector;

    [SerializeField] private MeshRenderer head;
    [SerializeField] private MeshRenderer body;
    public bool DeadBool;

    void Awake()
    {
        spawnVector = transform.position;
        Spawn();
    }

    public int HealthGetSet
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if(health <= 0)
            {
                health = 0;
                Death();
            }
        }
    }

    private IEnumerator Death()
    {
        head.enabled = false;
        body.enabled = false;
        DeadBool = true;

        yield return new WaitForSeconds(10f);
        Spawn();
        
    }

    private void Spawn()
    {
        DeadBool = false;
        head.enabled = true;
        body.enabled = true;
        transform.position = spawnVector;
        health = baseHealth;

    }
}
}