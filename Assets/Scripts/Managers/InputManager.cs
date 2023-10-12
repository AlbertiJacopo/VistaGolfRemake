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
    private Vector3 m_TouchStartPosition2;
    [SerializeField] private GameObject m_DeadZoneSwingSprite;
    [SerializeField] private GameObject m_InputZoneBallSprite;
    private bool m_MovePassed = false;

    private void Start()
    {
        m_DeadZoneSwingSprite.transform.localScale = new Vector3(m_DeadZoneSwingRadius * 2, m_DeadZoneSwingRadius * 2, 0);
        m_InputZoneBallSprite.transform.localScale = new Vector3(m_InputZoneBallRadius * 2, m_InputZoneBallRadius * 2, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (m_Ball.GetComponent<Rigidbody>().velocity.magnitude == 0f)
        {
            m_InputZoneBallSprite.transform.position =new Vector3(m_Ball.position.x, m_Ball.position.y - (m_Ball.localScale.y / 2) + 0.001f, m_Ball.position.z);
            m_InputZoneBallSprite.SetActive(true);
        }
        else
            m_InputZoneBallSprite.SetActive(false);

        if (Input.touchCount == 0)
        {
            m_DeadZoneSwingSprite.SetActive(false);
            return;
        }

        //Gets the start position and triggers player or camera movement
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPosition = GetTouchWorldSpace(touch);
            
            if(touch.phase == TouchPhase.Began)
            {
                m_TouchStartPosition = touchPosition;
            }

            bool isInInputZone = CheckInInputZoneBall();

            if (isInInputZone && touch.phase == TouchPhase.Began)
            {
                m_DeadZoneSwingSprite.transform.position = m_TouchStartPosition;
                m_DeadZoneSwingSprite.SetActive(true);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 t1 = GetTouchWorldSpace(touch);
                float tmp = Vector3.Distance(m_TouchStartPosition, t1);
                if (tmp > m_DeadZoneSwingRadius)
                {
                    touchEndPosition = GetTouchWorldSpace(touch);
                    m_MovePassed = true;
                }
                else m_MovePassed = false;

                if (!isInInputZone)
                {
                    GameManager.instance.EventManager.TriggerEvent(Constants.CAMERA_MOVEMENT, m_TouchStartPosition, touchEndPosition);
                }
            }
            else if((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) 
                     && isInInputZone && m_MovePassed 
                     && m_Ball.GetComponent<Rigidbody>().velocity.magnitude == 0f)
            {
                GameManager.instance.EventManager.TriggerEvent(Constants.MOVEMENT_PLAYER, m_TouchStartPosition, touchEndPosition);
                m_DeadZoneSwingSprite.SetActive(false);
            }
        }
        //Gets the 2 start positions and triggers camera zooming
        else if(Input.touchCount == 2)
        {
            Touch touch = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            m_TouchStartPosition = GetTouchWorldSpace(touch);
            m_TouchStartPosition2 = GetTouchWorldSpace(touch2);
            if (!CheckInInputZoneBall())
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
