using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public PlayerInputManager playerInputManager;
    public CinemachineTargetGroup cinemachineTargetGroup;
    
    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += Joined;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= Left;
    }

    private void Joined(PlayerInput obj)
    {
        CinemachineTargetGroup.Target target = new CinemachineTargetGroup.Target();
        target.Object = obj.transform;

        cinemachineTargetGroup.Targets.Add(target);
    }

    private void Left(PlayerInput obj)
    {
        CinemachineTargetGroup.Target target = new CinemachineTargetGroup.Target();
        target.Object = obj.transform;

        cinemachineTargetGroup.Targets.Remove(target);
    }
}
