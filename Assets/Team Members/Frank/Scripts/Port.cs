using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Port : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public List<GameObject> PlugObjectList = new List<GameObject>();
    public List<Transform> PlugTransformList = new List<Transform>();
    public Transform portTransform;
    //public Transform plugBackTransform;
    public int portChildCount;
    public LineRenderer connectedlineRenderer;
    public Material lineMaterial;

    public void PlugListPopulate()
    {
        portChildCount = transform.childCount;
        for (int currentPlug = 0; currentPlug < portChildCount; currentPlug++)
        {
            PlugObjectList.Add(transform.GetChild(currentPlug).gameObject);
        }
        
    }

    public void PlugTransformListPopulate()
    {
        foreach (GameObject currentPlugObject in PlugObjectList)
        {
            PlugTransformList.Add(currentPlugObject.transform);
        }
    }

    public void WireMaker()
    {
        foreach (Transform currentPlugTransform in PlugTransformList)
        {
            if (currentPlugTransform.gameObject.GetComponent<CableEnd>().isConnected == true)
            {
                connectedlineRenderer = gameObject.AddComponent<LineRenderer>();

                //connectedlineRenderer.material = lineMaterial;
                connectedlineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                connectedlineRenderer.startColor = Color.green;
                connectedlineRenderer.endColor = Color.green;
                connectedlineRenderer.positionCount = 2;
                connectedlineRenderer.startWidth = 0.05f;
                connectedlineRenderer.endWidth = 0.05f;
                connectedlineRenderer.SetPosition(0, portTransform.position);
                connectedlineRenderer.SetPosition(1, currentPlugTransform.position);
                
            }
            
            else if (currentPlugTransform.gameObject.GetComponent<CableEnd>().isConnected == false)
            {
                Destroy(gameObject.GetComponent<LineRenderer>());
            }
        }
    }
    
    
    void Start()
    {
        PlugListPopulate();
        PlugTransformListPopulate();
    }

    // Update is called once per frame
    void Update()
    {
     
        WireMaker();
    }
}
