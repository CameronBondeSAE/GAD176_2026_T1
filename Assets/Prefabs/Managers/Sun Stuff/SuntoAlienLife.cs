using System.Collections.Generic;
using UnityEngine;

public class SuntoAlienLife : MonoBehaviour
{

    public SunController Sc;
    
    public bool AliensRun = false;
    public GameObject AlienPrefab;
    
    // 1. Expose the GameObject list to the Unity Inspector
    public List<GameObject> targetObjects = new List<GameObject>();
    
    // 2. Make the positions list
    public List<Vector3> positions = new List<Vector3>();

    // 3. A public method to retrieve all Vector3 positions at any given moment
    public void GetObjectPositions()
    {
        foreach (GameObject obj in targetObjects)
        {
            if (obj != null) // Avoid null reference errors if a slot is left empty
            {
                // Extract the Vector3 world position
                positions.Add(obj.transform.position); 
            }
        }
    }

    private void AlienKiller()
    {
        foreach (GameObject Prefab in GameObject.FindGameObjectsWithTag("Ai"))
        {
            GameObject Alien = GameObject.FindGameObjectWithTag("Ai");
            Destroy(Alien);   
        }
        
    }
    
    private void AlienMom()
    {
        if (positions.Count > 0)
        {
            for (int i = 0; i < AlienMomMap[Sc.DayNumber]; i++)
            {
                Instantiate(AlienPrefab, positions[Random.Range(0, positions.Count)], Quaternion.identity);
            }   
        }
    }

    public Dictionary<int, int> AlienMomMap = new Dictionary<int, int>()
    {
        { 1, 3 },
        { 2, 3 },
        { 3, 4 },
        { 4, 4 },
        { 5, 5 },
    };

    void Start()
    {
        GetObjectPositions();
    }

    void Update()
    {
        if (Sc.IsDay == true)
        {
            AlienKiller();
            AliensRun = false;
        }
        else if (Sc.IsDay == false && AliensRun == false)
        {
            AlienMom();
            AliensRun = true;
        }
    }
}
