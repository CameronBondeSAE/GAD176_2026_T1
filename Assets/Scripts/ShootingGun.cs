using System;
using System.Collections;
using Anthill.AI;

using UnityEngine;

public class ShootingGun : MonoBehaviour
{
    public Collectstates States;
    public bool isEnemyDead = false;
    public Transform gunTransform;
    public GameObject bulletPrefab;
    public bool canShoot;
    public int bulletAmount = 1; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        if (canShoot == true)
        {
            for (int i = 0; i < bulletAmount; i++)
            {
                Instantiate(bulletPrefab,gunTransform.position,Quaternion.identity);
                canShoot = false;
                StartCoroutine(turnOn());
            }

            if (isEnemyDead == true)
            {
             GetComponentInParent<Collectstates>().isIsPlayerDead = true;
             GetComponentInParent<Collectstates>().isCanSeeplayer = false;
             GetComponentInParent<Collectstates>().isIsPlayerDead = false;
             GetComponentInParent<Collectstates>().isweapondDrawn = false;
             GetComponentInParent<Collectstates>().isLookingforplayer = true;
            }
        }
    }

    private IEnumerator turnOn()
    {
        yield return new WaitForSeconds(2);
        {
            canShoot = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
          Destroy(collision.gameObject);
          {
              isEnemyDead = true;
          }
        }
    }
}

