using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public int amount = 50;

    private void Start()
    {
        SpawnAI();
    }
    
    
    public void SpawnAI()
    {
        for (int i = 0; i < amount; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            Instantiate(prefab, transform.position, rotation);
        }
    }
}
