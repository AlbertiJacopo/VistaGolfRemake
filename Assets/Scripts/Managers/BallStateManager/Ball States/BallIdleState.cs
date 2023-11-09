using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallIdleState : State<BallStates>
{
    private BallStatesManager m_BallStatesManager;
    public BallIdleState(BallStates stateID, StatesMachine<BallStates> stateMachine = null) : base(stateID, stateMachine)
    {
        m_BallStatesManager = (BallStatesManager)stateMachine;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        m_BallStatesManager.m_DeadZoneSwingSprite.SetActive(false);
        m_BallStatesManager.EnableDisableRenderers(false);
        return;
    }
}