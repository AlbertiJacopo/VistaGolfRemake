using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationState : State<CameraState>
{
    private CameraStatesManager m_CameraStatesManager;
    private RotationStatesManager m_RotationStatesManager;

    public CameraRotationState(CameraState stateID, StatesMachine<CameraState> stateManager = null) : base(stateID, stateManager)
    {
        m_CameraStatesManager = (CameraStatesManager)stateManager;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (m_RotationStatesManager == null)
        m_RotationStatesManager = new RotationStatesManager();
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        base.OnExit();

    }
}

