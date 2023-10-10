using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float m_DeadZoneSwingRadius;
    [SerializeField] private float m_DeadZoneBallRadius;
    private GameObject m_Ball;
    private Vector3 m_TouchStartPosition;
    private Vector3 m_TouchStartPosition2;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        //Gets the start position and triggers player or camera movement
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            m_TouchStartPosition = GetStartTouch(touch);

            if (CheckInDeadzoneBall() && m_TouchStartPosition != Vector3.zero)
                GameManager.instance.EventManager.TriggerEvent(Constants.MOVEMENT_PLAYER, m_TouchStartPosition, touch);
            else
                GameManager.instance.EventManager.TriggerEvent(Constants.CAMERA_MOVEMENT, m_TouchStartPosition, touch);
        }
        //Gets the 2 start positions and triggers camera zooming
        else if(Input.touchCount == 2)
        {
            Touch touch = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            m_TouchStartPosition = GetStartTouch(touch);
            m_TouchStartPosition2 = GetStartTouch(touch2);
            if (!CheckInDeadzoneBall())
            {
                GameManager.instance.EventManager.TriggerEvent(Constants.CAMERA_ZOOMING, m_TouchStartPosition, m_TouchStartPosition2, touch, touch2);
            }
        }
    }

    /// <summary>
    /// Returns the worldspace position of the touch (returns a Vector3)
    /// </summary>
    /// <param name="touch"></param>
    /// <returns></returns>
    public Vector3 GetStartTouch(Touch touch)
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.y, m_Ball.transform.position.y, touch.position.x));

        if (touch.phase == TouchPhase.Began)
            return touchPosition;
        else
            return Vector3.zero;
    }

    /// <summary>
    /// Checks if the touch is in the Ball Deadzone (returns a bool)
    /// </summary>
    /// <returns></returns>
    public bool CheckInDeadzoneBall()
    {
        float distance = Vector3.Distance(m_TouchStartPosition, m_Ball.transform.position);

        if (distance < m_DeadZoneBallRadius)
            return true;
        else
            return false;
    }
}
