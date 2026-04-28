using Team_Members.Jackson.AI_Senses.Vision;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Vision))]
public class VisionSceneEditor : Editor
{
    private void OnSceneGUI()
    {
        Vision vision = (Vision)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(vision.transform.position, vision.transform.up, vision.transform.forward, 360, vision.radius);

        Vector3 viewAngle01 = DirectionFromAngle(vision.transform.eulerAngles.y, -vision.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(vision.transform.eulerAngles.y, vision.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(vision.transform.position, vision.transform.position + viewAngle01 * vision.radius);
        Handles.DrawLine(vision.transform.position, vision.transform.position + viewAngle02 * vision.radius);

        if (vision.canSeePlayer)
        {
            if (vision.playerRef != null)
            {
                Handles.color = Color.green;
                Handles.DrawLine(vision.transform.position, vision.playerRef.transform.position);
            }
        }
        else
        {
            if (vision.playerRef != null)
            {
                Handles.color = Color.red;
                Handles.DrawLine(vision.transform.position, vision.playerRef.transform.position);
            }
        }
    }
    
    // Used a video for this as its very tricky math
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    
}
