using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTouchMoved : State<ZoomState>
{
    private ZoomStatesManager m_ZoomStatesManager;

    public ZoomTouchMoved(ZoomState stateID, StatesMachine<ZoomState> stateManager = null) : base(stateID, stateManager)
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
        //DA SETTARE IL TOUCH
        GameManager.instance.EventManager.TriggerEvent(Constants.UPDATE_CAMERA_ZOOMING, m_ZoomStatesManager.TouchScreenStartPos, m_ZoomStatesManager.TouchScreenStartPos2, 
                                                        m_ZoomStatesManager.InputManager.GetTouch(0).position,
                                                        m_ZoomStatesManager.InputManager.GetTouch(1).position);
    }
    public override void OnExit()
    {
        base.OnExit();

    }
}
