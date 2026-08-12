using System.Collections;
using Unity.Netcode;
using UnityEngine;

public struct RotationalPhysicsPacket : INetworkSerializable
{
    public Quaternion rotation;
    
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref rotation);
    }
}

public class NetworkRotationalPhysics : NetworkBehaviour
{
    [SerializeField] private Transform myTransform;

    [SerializeField] private float updateDelay = 0.01f;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            StartCoroutine(LoopForUpdates());
        }
    }
    
    private IEnumerator LoopForUpdates()
    {
        while (true)
        {
            UpdateRotationPhysicsRpc(new  RotationalPhysicsPacket
            {
                rotation = myTransform.rotation
            });
            
            yield return new WaitForSeconds(updateDelay);
        }
    }
    
    [Rpc(SendTo.ClientsAndHost, Delivery = RpcDelivery.Unreliable)]
    private void UpdateRotationPhysicsRpc(RotationalPhysicsPacket packet)
    {
        transform.rotation = packet.rotation;
    }
}
