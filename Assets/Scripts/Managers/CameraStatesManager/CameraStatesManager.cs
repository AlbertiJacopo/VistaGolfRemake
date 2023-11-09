using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CameraStatesManager : StatesMachine<CameraState>
{
    public Transform PlayerTransform;

    public CameraStatesManager(Transform playerTransform, Dictionary<CameraState, State<CameraState>> stateList = null, State<CameraState> currentState = null, State<CameraState> previousState = null) : base()
    {
        PlayerTransform = playerTransform;
    }

    protected override void InitStates()
    {
        StatesList.Add(CameraState.Zoom, new ZoomState(CameraState.Zoom, this));
        StatesList.Add(CameraState.Rotation, new RotationState(CameraState.Rotation, this));
    }
}

public enum CameraState
{
    Zoom,
    Rotation
}

