using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTouchEnded : State<ZoomState>
{
    private RotationStatesManager m_RotationStatesManager;

    public ZoomTouchEnded(ZoomState stateID, StatesMachine<ZoomState> stateManager = null) : base(stateID, stateManager)
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
        //GameManager.instance.EventManager.TriggerEvent(Constants.STOP_CAMERA_ZOOMING);
    }
    public override void OnExit()
    {
        base.OnExit();

    }
}
