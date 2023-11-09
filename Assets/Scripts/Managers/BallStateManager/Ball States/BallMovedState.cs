using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovedState : State<BallStates>
{
    private BallStatesManager m_BallStatesManager;
    public BallMovedState(BallStates stateID, StatesMachine<BallStates> stateMachine = null) : base(stateID, stateMachine)
    {
        m_BallStatesManager = (BallStatesManager)stateMachine;
    }

    public void OnUpdate(Touch touch)
    {
        base.OnUpdate();
        if (Vector3.Distance(m_BallStatesManager.m_TouchStartPosition, m_BallStatesManager.GetTouchWorldSpace(touch)) > m_BallStatesManager.m_DeadZoneSwingRadius)
        {
            Vector3 direction = m_BallStatesManager.GetTouchWorldSpace(touch) - m_BallStatesManager.m_TouchStartPosition;
            m_BallStatesManager.m_TouchTempPosition = m_BallStatesManager.m_TouchStartPosition + (m_BallStatesManager.m_DeadZoneSwingRadius * direction.normalized);
            m_BallStatesManager.m_InputDirectionRenderer.SetPosition(0, m_BallStatesManager.m_Ball.position);

            if (Vector3.Distance(m_BallStatesManager.m_TouchStartPosition, m_BallStatesManager.GetTouchWorldSpace(touch)) <= m_BallStatesManager.m_MaxDistanceSwing)
                m_BallStatesManager.m_TouchEndPosition = m_BallStatesManager.GetTouchWorldSpace(touch);
            else
            {
                Vector3 dir = m_BallStatesManager.GetTouchWorldSpace(touch) - m_BallStatesManager.m_TouchStartPosition;
                m_BallStatesManager.m_TouchEndPosition = m_BallStatesManager.m_TouchStartPosition + (m_BallStatesManager.m_MaxDistanceSwing * dir.normalized);
            }

            m_BallStatesManager.m_InputDirectionRenderer.SetPosition(1, m_BallStatesManager.m_TouchEndPosition - (m_BallStatesManager.m_TouchTempPosition - m_BallStatesManager.m_Ball.position));

            m_BallStatesManager.m_MovePassed = true;

            CalculateWay();

            if (m_BallStatesManager.m_Ball.GetComponent<Rigidbody>().velocity.magnitude == 0f)
                m_BallStatesManager.EnableDisableRenderers(true);
        }
        else m_BallStatesManager.m_MovePassed = false;
    }

    private void CalculateWay()
    {
        Vector3 finalPoint = Vector3.zero;
        float actualLenght = 0;
        Vector3 directionBall = -(m_BallStatesManager.m_TouchEndPosition - m_BallStatesManager.m_TouchTempPosition);
        RaycastHit hit;

        m_BallStatesManager.m_RenderDistanceLenght = directionBall.magnitude * m_BallStatesManager.m_LineMultiplier;

        List<Vector3> wallHitPosition = new List<Vector3>();
        Vector3 normal;

        Physics.Raycast(m_BallStatesManager.m_Ball.position, directionBall.normalized, out hit);

        wallHitPosition.Add(hit.point);

        //actualLenght += Vector3.Distance(m_Ball.position, wallHitPosition[0]);
        if (actualLenght + Vector3.Distance(m_BallStatesManager.m_Ball.position, wallHitPosition[0]) <= m_BallStatesManager.m_RenderDistanceLenght)
        {
            actualLenght += Vector3.Distance(m_BallStatesManager.m_Ball.position, wallHitPosition[0]);

            //Debug.Log("actualLenght: " + actualLenght);

            normal = new Vector3(hit.normal.x, 0f, hit.normal.z);
            //wallHitPosition.Add(hit.point);

            Vector3 directionWall = Vector3.Reflect(directionBall.normalized, normal);

            int i = 0;
            while (actualLenght <= m_BallStatesManager.m_RenderDistanceLenght && finalPoint == Vector3.zero)
            {
                Physics.Raycast(wallHitPosition[i], directionWall.normalized, out hit);

                wallHitPosition.Add(hit.point);
                Vector3 pastDirectionWall = directionWall;

                directionWall = Vector3.Reflect(directionWall.normalized, normal);
                normal = new Vector3(hit.normal.x, 0f, hit.normal.z);

                //Debug.Log("totale: " + (actualLenght + Vector3.Distance(wallHitPosition[i], wallHitPosition[i + 1])));

                if (actualLenght + Vector3.Distance(wallHitPosition[i], wallHitPosition[i + 1]) >= m_BallStatesManager.m_RenderDistanceLenght)
                {
                    wallHitPosition.RemoveAt(i + 1);
                    finalPoint = CalcFInalPoint(actualLenght, pastDirectionWall, wallHitPosition[i]);
                    //Debug.Log("totale if: " + actualLenght);
                }
                else
                {
                    actualLenght += Vector3.Distance(wallHitPosition[i], wallHitPosition[i + 1]);
                }


                i++;
            }
        }
        else
        {
            wallHitPosition.RemoveAt(0);
            finalPoint = CalcFInalPoint(actualLenght, directionBall, m_BallStatesManager.m_Ball.position);
        }

        m_BallStatesManager.m_CalculatedDirection.positionCount = wallHitPosition.Count + 2;
        for (int j = 0; j < m_BallStatesManager.m_CalculatedDirection.positionCount; j++)
        {
            if (j == 0)
                m_BallStatesManager.m_CalculatedDirection.SetPosition(j, m_BallStatesManager.m_Ball.position);
            else if (j < m_BallStatesManager.m_CalculatedDirection.positionCount - 1)
                m_BallStatesManager.m_CalculatedDirection.SetPosition(j, wallHitPosition[j - 1]);
            else
                m_BallStatesManager.m_CalculatedDirection.SetPosition(j, finalPoint);
        }
    }

    private Vector3 CalcFInalPoint(float actualLenght, Vector3 direction, Vector3 wallHitPosition)
    {
        float distanceFromOrigin = m_BallStatesManager.m_RenderDistanceLenght - actualLenght;
        float factor = distanceFromOrigin / direction.magnitude;
        return wallHitPosition + direction * factor;
    }
}