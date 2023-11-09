using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTouchBegan : State<ZoomState>
{
    private RotationStatesManager m_RotationStatesManager;

    public ZoomTouchBegan(ZoomState stateID, StatesMachine<ZoomState> stateManager = null) : base(stateID, stateManager)
    {
        //m_RotationStatesManager = (RotationStatesManager)m_ZoomStateManager;
    }

    public override void OnEnter()
    {
        base.OnEnter();

    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        //m_TouchScreenStartPos = touch.position;
        //m_TouchScreenStartPos2 = touch2.position;
    }
    public override void OnExit()
    {
        base.OnExit();

    }
}