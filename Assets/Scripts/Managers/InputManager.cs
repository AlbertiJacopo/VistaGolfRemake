using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float m_DeadZoneSwingRadius;
    [SerializeField] private float m_InputZoneBallRadius;
    [SerializeField] private Transform m_Ball;
    [SerializeField] private Transform m_Camera;
    private Vector3 m_TouchStartPosition;
    private Vector3 touchEndPosition = Vector3.zero;
    private Vector2 m_TouchScreenStartPos;
    private Vector2 m_TouchScreenStartPos2;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        //Gets the start position and triggers player or camera movement
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPosition = GetTouchWorldSpace(touch);
            
            if(touch.phase == TouchPhase.Began)
            {
                m_TouchStartPosition = touchPosition;
                m_TouchScreenStartPos = touch.position;
            }

            bool isInInputZone = CheckInInputZoneBall();

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 t1 = GetTouchWorldSpace(touch);
                float tmp = Vector3.Distance(m_TouchStartPosition, t1);
                if (tmp > m_DeadZoneSwingRadius)
                {
                    touchEndPosition = GetTouchWorldSpace(touch);
                }
                if (!isInInputZone)
                {
                    GameManager.instance.EventManager.TriggerEvent(Constants.UPDATE_CAMERA_ROTATION, m_TouchScreenStartPos, touch);
                }
            }
            else if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (isInInputZone)
                {
                    GameManager.instance.EventManager.TriggerEvent(Constants.MOVEMENT_PLAYER, m_TouchStartPosition, touchEndPosition);
                }
                else
                {
                    GameManager.instance.EventManager.TriggerEvent(Constants.STOP_CAMERA_ROTATION);
                }
            }
        }
        //Gets the 2 start positions and triggers camera zooming
        else if(!CheckInInputZoneBall() && Input.touchCount == 2)
        {
            Touch touch = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            if (touch.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                m_TouchScreenStartPos = touch.position;
                m_TouchScreenStartPos2 = touch.position;
                GameManager.instance.EventManager.TriggerEvent(Constants.START_CAMERA_ZOOMING, m_TouchScreenStartPos, m_TouchScreenStartPos2, touch, touch2);
            }
            if (touch.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
            {
                GameManager.instance.EventManager.TriggerEvent(Constants.STOP_CAMERA_ZOOMING);
            }
        }
    }

    /// <summary>
    /// Returns the worldspace position of the touch (returns a Vector3)
    /// </summary>
    /// <param name="touch"></param>
    /// <returns></returns>
    public Vector3 GetTouchWorldSpace(Touch touch)
    {
        float distanceFromCamera = Vector3.Distance(m_Camera.position, m_Ball.position);
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, distanceFromCamera));
        touchPosition.y = m_Ball.position.y;

        return touchPosition;
    }

    /// <summary>
    /// Checks if the touch is in the Ball Deadzone (returns a bool)
    /// </summary>
    /// <returns></returns>
    public bool CheckInInputZoneBall()
    {
        float distance = Vector3.Distance(m_TouchStartPosition, m_Ball.position);

        if (distance <= m_InputZoneBallRadius)
            return true;
        else
            return false;
    }
}
