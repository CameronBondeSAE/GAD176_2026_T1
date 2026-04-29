using Anthill.AI;
using UnityEngine;

public class AlienAIViewer : AntAIState
{
    public float distance = 0.9f;
    [SerializeField] private GameObject alien;
     public RaycastHit player;
     [SerializeField]  private LayerMask players;
    public bool foundPlayer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        alien.GetComponent<AlienAIViewer>();
        {
    
        }
    }
}
