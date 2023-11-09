using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEndedState : State<BallStates>
{
    private BallStatesManager m_BallStatesManager;
    public BallEndedState(BallStates stateID, StatesMachine<BallStates> stateMachine = null) : base(stateID, stateMachine)
    {
        m_BallStatesManager = (BallStatesManager)stateMachine;
    }

    public void OnUpdate(Touch touch)
    {
        base.OnUpdate();

        if(m_BallStatesManager.m_MovePassed && m_BallStatesManager.m_Ball.GetComponent<Rigidbody>().velocity.magnitude == 0f)
        {
            GameManager.instance.EventManager.TriggerEvent(Constants.MOVEMENT_PLAYER, m_BallStatesManager.m_TouchTempPosition, m_BallStatesManager.m_TouchEndPosition);
            m_BallStatesManager.m_DeadZoneSwingSprite.SetActive(false);

            GameManager.instance.EventManager.TriggerEvent(Constants.UPDATE_LEVEL_SWINGS);

            m_BallStatesManager.EnableDisableRenderers(false);
        }
    }
}