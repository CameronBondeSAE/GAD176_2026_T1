using UnityEngine;

public class PowerCable : MonoBehaviour
{
    public LineRenderer connectedlineRenderer;
    public Material lineMaterial;
    public Vector3 startPosition;
    public Vector3 endPosition;

    public void WireMaker()
    {
        connectedlineRenderer = gameObject.AddComponent<LineRenderer>();
        
        connectedlineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        connectedlineRenderer.startColor = Color.green;
        connectedlineRenderer.endColor = Color.green;
        connectedlineRenderer.positionCount = 2;
        connectedlineRenderer.startWidth = 0.05f;
        connectedlineRenderer.endWidth = 0.05f;
        SetWirePosition();
        
    }

    public void TrackPosition()
    {
        startPosition = transform.position;
        endPosition = transform.position + (Vector3.forward * 5);
        Debug.Log(startPosition);
        Debug.Log(endPosition);

    }

    public void SetWirePosition()
    {
        connectedlineRenderer.SetPosition(0, startPosition);
        connectedlineRenderer.SetPosition(1, endPosition);
        
    }
    
    void Start()
    {
        WireMaker();
    }

    // Update is called once per frame
    void Update()
    {
        TrackPosition();
        SetWirePosition();
    
    }
    
    
}
