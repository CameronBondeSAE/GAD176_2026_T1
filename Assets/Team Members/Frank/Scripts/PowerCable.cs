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
        
        
        
    }
/// <summary>
/// This function gets
/// - a transform of a powerpoint passed by the player.
/// - a transform for the player's follow camera transform - An approximation of head
/// It then sets
/// - the starting positions of the wire to the PowerPoint's location
/// - the end position of the wire to the player's head location
/// </summary>
    public void SetStartingWirePosition(Transform powerPointTransform, Transform playerTransform) // this function accepts the position of an power point that the player has interacted with
    {
        connectedlineRenderer.SetPosition(0, powerPointTransform.position);
        connectedlineRenderer.SetPosition(1, playerTransform.position);
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
