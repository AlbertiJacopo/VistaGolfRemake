using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CameraStatesManager : StatesMachine<CameraState>
{
    public Vector2 TouchScreenStartPos;
    public Vector2 TouchScreenStartPos2;

    public CameraStatesManager(Vector2 touchScreenStartPos, Vector2 touchScreenStartPos2)
    {
        TouchScreenStartPos = touchScreenStartPos;
        TouchScreenStartPos2 = touchScreenStartPos2;
    }

    protected override void InitStates()
    {
        StatesList.Add(CameraState.Zoom, new CameraZoomState(CameraState.Zoom, this));
        StatesList.Add(CameraState.Rotation, new CameraRotationState(CameraState.Rotation, this));
    }
}



