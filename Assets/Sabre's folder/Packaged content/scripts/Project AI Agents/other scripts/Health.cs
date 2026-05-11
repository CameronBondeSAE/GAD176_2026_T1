using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SabreHealth : CharacterBase
{
    private Vector3 spawnVector;

    [SerializeField] private MeshRenderer head;
    [SerializeField] private MeshRenderer body;

    void Awake()
    {
        spawnVector = transform.position;
        Spawn();
    }

    public override void Die()
    {
        base.Die();
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        head.enabled = false;
        body.enabled = false;
        isAlive = true;

        yield return new WaitForSeconds(10f);
        Spawn();
        
    }

    private void Spawn()
    {
        isAlive = false;
        head.enabled = true;
        body.enabled = true;
        transform.position = spawnVector;
        currentHealth = startingHealth;

    }
}
