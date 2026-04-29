using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class ThomasSoundEmitterEvent : UnityEvent<ThomasSoundEmitter> { }


public class AlienSpeak : MonoBehaviour
{
    //event that fires when an NPC is detected other scripts can subscribe to this
    public ThomasSoundEmitterEvent onNPCDetected;
    
    //optimize performance by limiting search
    private int npcLayer;

    void Awake()
    {
        //timing fix to avoid null reference error
        if (onNPCDetected == null)
        {
            onNPCDetected = new ThomasSoundEmitterEvent();
        }
    }

    void Start()
    {
        npcLayer = LayerMask.NameToLayer("NPC");
    }


    //checks if it's an NPC and fires the detection event if so.

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == npcLayer)
        {
            ThomasSoundEmitter soundEmitter = other.GetComponent<ThomasSoundEmitter>();
            
            if (soundEmitter != null)
            {
                //broadcast the event to all listeners, passing the SoundEmitter
                onNPCDetected?.Invoke(soundEmitter);
                //Debug.Log($"AlienSpeak detected NPC: {other.gameObject.name}");
            }
        }
    }
}