using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTouchMoved : State<RotationState>
{
    private RotationStatesManager m_RotationStatesManager;

    public RotationTouchMoved(RotationState stateID, StatesMachine<RotationState> stateManager = null) : base(stateID, stateManager)
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
        ////checking if inside or outside the deadzone
        //if (!isInInputZone)
        //{
        //    //rotate camera if outside the inputzone
        //    GameManager.instance.EventManager.TriggerEvent(Constants.UPDATE_CAMERA_ROTATION, touch.deltaPosition);
        //}
        //else
        //{
        //    if (Vector3.Distance(m_TouchStartPosition, GetTouchWorldSpace(touch)) > m_DeadZoneSwingRadius)
        //    {
        //        Vector3 direction = GetTouchWorldSpace(touch) - m_TouchStartPosition;
        //        m_TouchTempPosition = m_TouchStartPosition + (m_DeadZoneSwingRadius * direction.normalized);
        //        m_InputDirectionRenderer.SetPosition(0, m_Ball.position);

        //        if (Vector3.Distance(m_TouchStartPosition, GetTouchWorldSpace(touch)) <= m_MaxDistanceSwing)
        //            m_TouchEndPosition = GetTouchWorldSpace(touch);
        //        else
        //        {
        //            Vector3 dir = GetTouchWorldSpace(touch) - m_TouchStartPosition;
        //            m_TouchEndPosition = m_TouchStartPosition + (m_MaxDistanceSwing * dir.normalized);
        //        }



        //        m_InputDirectionRenderer.SetPosition(1, m_TouchEndPosition - (m_TouchTempPosition - m_Ball.position));

        //        m_MovePassed = true;

        //        CalculateWay();

        //        if (m_Ball.GetComponent<Rigidbody>().velocity.magnitude == 0f)
        //            EnableDisableRenderers(true);
        //    }
        //    else m_MovePassed = false;
        //}
    }
    public override void OnExit()
    {
        base.OnExit();

    }
}
