using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomState : State<CameraState>
{
    private CameraStatesManager m_CameraStatesManager;
    private ZoomStatesManager m_ZoomStatesManager;
    private InputManager m_InputManager;

    public CameraZoomState(InputManager inputManager, CameraState stateID, StatesMachine<CameraState> stateManager = null) : base(stateID, stateManager)
    {
        m_CameraStatesManager = (CameraStatesManager)stateManager;
        m_InputManager = inputManager;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (m_ZoomStatesManager == null)
            m_ZoomStatesManager = new ZoomStatesManager(m_CameraStatesManager.TouchScreenStartPos, m_CameraStatesManager.TouchScreenStartPos2, m_InputManager);
    }

    public override void OnUpdate()
    {
        m_ZoomStatesManager.CurrentState.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();

    }
}
