using Frank;
using UnityEngine;

public class CableManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform powerPointTransformRef;
    public Transform playerHandsTransformRef;
    public GameObject CableEndARef;
    public GameObject CableEndBRef;
    public Vector3 customOffset = new Vector3(0, 0.5f, 0.5f);
    
    public LineRenderer connectedlineRenderer;
    public Material lineMaterial;
    public GameObject heldCableEnd;

    public void Start()
    {
	    WireMaker();
    }

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

    /// <summary>
    /// This function accepts two transforms.
    /// powerPointTransformRef is used to set EndA to the PowerPoints location.
    /// playerHandsTransformRef is used to set EndB to the player's hands location. 
    /// </summary>
    public void SetReferences(Transform powerPointTransform, Transform playerHandsTransform)
    {
        powerPointTransformRef = powerPointTransform; // variable is assigned the transform of a PowerPoint passed in by the player
        playerHandsTransformRef = playerHandsTransform; // variable is assigned a transform for the player's hands also passed in. 
        
        // WireMaker();
        CableSetup(); // uses the above stored references to set the positions of each cable end in space. 
    }

    public void CableSetup()
    {
        // CableEndARef.transform.position = (powerPointTransformRef.position + customOffset); // the position of endA is set to the power points position
        // CableEndBRef.transform.position = playerHandsTransformRef.position; // position of endB is set to the player's hand position
        //
        // heldCableEnd = CableEndBRef; // makes the held cable end equal to EndB
        // playerHandsTransformRef.GetComponent<Interact>().heldObject = heldCableEnd;
    }

    /// <summary>
    /// Updates endPosition based on the current value of the transform representing the player's hands.
    /// Necessary to update the direction of the LineRenderer.
    /// Relies upon cablesetup()
    /// </summary>
    public void TrackEndPosition()
    {
        CableEndBRef.transform.position = playerHandsTransformRef.position;
        
    }

    /// <summary>
    /// Sets the start and end positions of the linerenderer based on startPosition and endPosition.
    /// Called every frame. Uses the current value of StartPosition and endPosition to draw the line.
    /// Relies on TrackPosition, SetLinePoints & GetTransform functions
    /// </summary>
    public void SetWirePosition() 
    {
        
        connectedlineRenderer.SetPosition(0, CableEndARef.transform.position);
        connectedlineRenderer.SetPosition(1, CableEndBRef.transform.position);
        
    }


    // Update is called once per frame
    public void Update()
    {
        // TrackEndPosition();
        SetWirePosition();
    }

}
