using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTouchBegan : State<ZoomState>
{
    private ZoomStatesManager m_ZoomStatesManager;

    public ZoomTouchBegan(ZoomState stateID, StatesMachine<ZoomState> stateManager = null) : base(stateID, stateManager)
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
        m_ZoomStatesManager.TouchScreenStartPos = m_ZoomStatesManager.InputManager.GetTouch(0).position;
        //DA SETTARE IL TOUCH
        m_ZoomStatesManager.TouchScreenStartPos2 = m_ZoomStatesManager.InputManager.GetTouch(0).position;
    }
    public override void OnExit()
    {
        base.OnExit();

    }
}