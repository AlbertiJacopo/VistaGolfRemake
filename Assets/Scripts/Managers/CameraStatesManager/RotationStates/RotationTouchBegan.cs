using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RotationTouchBegan : State<RotationState>
{
    private RotationStatesManager m_RotationStatesManager;

    public RotationTouchBegan(RotationState stateID, StatesMachine<RotationState> stateManager = null) : base(stateID, stateManager)
    {
        //m_RotationStatesManager = (RotationStatesManager)m_RotationStateManager;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        //m_TouchStartPosition = touchPosition;
        //m_TouchScreenStartPos = touch.position;
        //m_InputDirectionRenderer.SetPosition(0, m_Ball.position);
    }
    public override void OnExit()
    {
        base.OnExit();
    
    }
}
