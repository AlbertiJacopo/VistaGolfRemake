using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationState : State<CameraState>
{
    private CameraStatesManager m_CameraStatesManager;
    private RotationStatesManager m_RotationStatesManager;

    private InputManager m_InputManager;

    public CameraRotationState(InputManager inputManager, CameraState stateID, StatesMachine<CameraState> stateManager = null) : base(stateID, stateManager)
    {
        m_CameraStatesManager = (CameraStatesManager)stateManager;
        m_InputManager = inputManager;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (m_RotationStatesManager == null)
            m_RotationStatesManager = new RotationStatesManager(m_InputManager);
    }

    public override void OnUpdate()
    {
        m_RotationStatesManager.CurrentState.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();

    }
}

