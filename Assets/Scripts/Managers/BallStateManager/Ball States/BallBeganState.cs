using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBeganState : State<BallStates>
{
    private BallStatesManager m_BallStatesManager;
    public BallBeganState(BallStates stateID, StatesMachine<BallStates> stateMachine = null) : base(stateID, stateMachine)
    {
        m_BallStatesManager = (BallStatesManager)stateMachine;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Vector3 touchPosition = m_BallStatesManager.GetTouchWorldSpace();
        m_BallStatesManager.m_TouchStartPosition = touchPosition;
        m_BallStatesManager.m_InputDirectionRenderer.SetPosition(0, m_BallStatesManager.m_Ball.position);
    }
}
