using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CameraStatesManager : StatesMachine<CameraState>
{
    public Vector2 TouchScreenStartPos;
    public Vector2 TouchScreenStartPos2;

    private InputManager m_InputManager;

    public CameraStatesManager(InputManager inputManager)
    {
        m_InputManager = inputManager;
    }

    protected override void InitStates()
    {
        StatesList.Add(CameraState.Zoom, new CameraZoomState(m_InputManager, CameraState.Zoom, this));
        StatesList.Add(CameraState.Rotation, new CameraRotationState(m_InputManager, CameraState.Rotation, this));
    }
}



