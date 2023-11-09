using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Generics.Pattern.StatePattern;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine.Playables;

public class BallStatesManager : StatesMachine<BallStates>
{
    public Vector3 m_TouchTempPosition;
    public Vector3 m_TouchStartPosition;
    public Vector3 m_TouchEndPosition = Vector3.zero;
    
    public Transform m_Ball;
    
    public float m_DeadZoneSwingRadius;
    public GameObject m_DeadZoneSwingSprite;
    public float m_InputZoneBallRadius;
    public GameObject m_InputZoneBallSprite;
    public float m_MaxDistanceSwing;
    
    public bool m_MovePassed = false;
    
    public float m_LineMultiplier;
    public LineRenderer m_InputDirectionRenderer;
    public LineRenderer m_CalculatedDirection;
    public float m_RenderDistanceLenght;

    public BallStatesManager(Transform ball, float deadZoneSwingRadius, GameObject deadZoneSwingSprite, 
                            float inputZoneBallRadius, GameObject inputZoneBallSprite, float maxDistanceSwing, 
                            bool movePassed, 
                            float lineMultiplier, LineRenderer inputDirectionRenderer, LineRenderer calculatedDirection) : base()
    {
        m_Ball = ball;

        m_DeadZoneSwingRadius = deadZoneSwingRadius;
        m_DeadZoneSwingSprite = deadZoneSwingSprite;
        m_InputZoneBallRadius = inputZoneBallRadius;
        m_InputZoneBallSprite = inputZoneBallSprite;
        m_MaxDistanceSwing = maxDistanceSwing;

        m_MovePassed = movePassed;

        m_LineMultiplier = lineMultiplier;
        m_InputDirectionRenderer = inputDirectionRenderer;
        m_CalculatedDirection = calculatedDirection;
    }

    protected override void InitStates()
    {
        StatesList.Add(BallStates.Idle, new BallIdleState(BallStates.Idle, this));
        StatesList.Add(BallStates.Began, new BallBeganState(BallStates.Began, this));
        StatesList.Add(BallStates.Moved, new BallMovedState(BallStates.Moved, this));
        StatesList.Add(BallStates.Ended, new BallEndedState(BallStates.Ended, this));
    }

    public Vector3 GetTouchWorldSpace()
    {
        Vector3 touchPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        Plane plane = new(Vector3.up, m_Ball.position);
        if (plane.Raycast(ray, out float distance))
        {
            touchPosition = ray.GetPoint(distance); 
        }
        touchPosition.y = m_Ball.position.y;

        return touchPosition;
    }

    public void EnableDisableRenderers(bool value)
    {
        m_InputDirectionRenderer.gameObject.SetActive(value);
        m_CalculatedDirection.gameObject.SetActive(value);
    }
}
