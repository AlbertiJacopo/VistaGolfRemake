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

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            GetStartTouch(touch);

            if (CheckInDeadzoneBall())
                GameManager.instance.EventManager.TriggerEvent(Constants.MOVEMENT_PLAYER, m_TouchStartPosition, touch);
            else
                GameManager.instance.EventManager.TriggerEvent(Constants.CAMERA_MOVEMENT, m_TouchStartPosition, touch);
        }
        else if(Input.touchCount == 2)
        {

        }

    }

    public void GetStartTouch(Touch touch)
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.y, m_Ball.transform.position.y, touch.position.x));

        if (touch.phase == TouchPhase.Began)
        {
            m_TouchStartPosition = touchPosition;
        }
    }

    public bool CheckInDeadzoneBall()
    {
        float distance = Vector3.Distance(m_TouchStartPosition, m_Ball.transform.position);

        if (distance < m_DeadZoneBallRadius)
            return true;
        else
            return false;
    }
}
