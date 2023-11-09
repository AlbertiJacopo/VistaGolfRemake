using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTouchMoved : State<ZoomState>
{
    private RotationStatesManager m_RotationStatesManager;

    public ZoomTouchMoved(ZoomState stateID, StatesMachine<ZoomState> stateManager = null) : base(stateID, stateManager)
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
        //GameManager.instance.EventManager.TriggerEvent(Constants.UPDATE_CAMERA_ZOOMING, m_TouchScreenStartPos, m_TouchScreenStartPos2, touch.position, touch2.position);
    }
    public override void OnExit()
    {
        base.OnExit();

    }
}
