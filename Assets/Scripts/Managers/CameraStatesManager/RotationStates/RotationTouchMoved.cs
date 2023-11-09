using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTouchMoved : State<RotationState>
{
    private RotationStatesManager m_RotationStatesManager;

    public RotationTouchMoved(RotationState stateID, StatesMachine<RotationState> stateManager = null) : base(stateID, stateManager)
    {
        m_RotationStatesManager = (RotationStatesManager)stateManager;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        //DA SETTARE IL TOUCH
        GameManager.instance.EventManager.TriggerEvent(Constants.UPDATE_CAMERA_ROTATION, touch.deltaPosition);
        
    }
    public override void OnExit()
    {
        base.OnExit();

    }
}
