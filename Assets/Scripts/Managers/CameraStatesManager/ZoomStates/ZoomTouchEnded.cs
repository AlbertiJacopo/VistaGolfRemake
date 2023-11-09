using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTouchEnded : State<ZoomState>
{
    private ZoomStatesManager m_ZoomStatesManager;

    public ZoomTouchEnded(ZoomState stateID, StatesMachine<ZoomState> stateManager = null) : base(stateID, stateManager)
    {
        m_ZoomStatesManager = (ZoomStatesManager)stateManager;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        GameManager.instance.EventManager.TriggerEvent(Constants.STOP_CAMERA_ZOOMING);
    }
    public override void OnExit()
    {
        base.OnExit();

    }
}
