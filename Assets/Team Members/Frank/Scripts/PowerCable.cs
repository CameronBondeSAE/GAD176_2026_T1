using UnityEngine;

namespace Frank
{
    public class PowerCable : MonoBehaviour
{
    public LineRenderer connectedlineRenderer;
    public Material lineMaterial;
    public Transform playerTransformRef;
    public Transform powerPointTransformRef;
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

    public void GetTransforms(Transform powerPointTransform, Transform playerTransform) // pass in a transform for the player's head or hands and a powerpoint transform
    {
        playerTransformRef = playerTransform; // stores a reference to the playerTransform, used for tracking player positions dynamically
        powerPointTransformRef = powerPointTransform; // stores a reference to the PowerPoint transform
        
        SetlinePoints(); // transforms have been captured. 
        
    }

    public void SetlinePoints() // Assign vectors to StartPosition and EndPosition Variables
    {
        startPosition = powerPointTransformRef.position; // assigns the transform.position of the transform held in PowerPointTransformRef 
        endPosition = playerTransformRef.position; //assigns the transform.position of the transform held in PlayerTransformRef 
        Debug.Log(startPosition); // Debug a line to the console to confirm the value of Start Position
        Debug.Log(endPosition); //Debug a line to the console to confirm the value of Start Position
    }
    /// <summary>
    /// Updates endPosition based on the current value of the transform representing the player's hands.
    /// Necessary to update the direction of the LineRenderer.
    /// Relies upon SetLinePoints() and GetTransforms()
    /// </summary>
    public void TrackPosition()
    {
        endPosition = playerTransformRef.position;
        
    }
    /// <summary>
    /// Sets the start and end positions of the linerenderer based on startPosition and endPosition.
    /// Called every frame. Uses the current value of StartPosition and endPosition to draw the line.
    /// Relies on TrackPosition, SetLinePoints & GetTransform functions
    /// </summary>
    public void SetWirePosition() 
    {
        
        connectedlineRenderer.SetPosition(0, startPosition);
        connectedlineRenderer.SetPosition(1, endPosition);
        
    }
/// <summary>
/// This function gets
/// - a transform of a PowerPoint passed by the player.
/// - a transform for the player's follow camera transform - An approximation of head
/// It then sets
/// - the starting positions of the wire to the PowerPoint's location
/// - the end position of the wire to the player's head location
/// </summary>

    
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

}
