using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Playables;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Transform m_Ball;
    [SerializeField] private Transform m_Camera;
    [SerializeField] private float m_LineMultiplier;

    [Header("Ranges")]
    [SerializeField] private float m_DeadZoneSwingRadius;
    [SerializeField] private GameObject m_DeadZoneSwingSprite;
    [SerializeField] private float m_InputZoneBallRadius;
    [SerializeField] private GameObject m_InputZoneBallSprite;
    [SerializeField] private float m_MaxDistanceSwing;

    private bool m_MovePassed = false;

    private LineRenderer m_InputDirectionRenderer;
    private LineRenderer m_CalculatedDirection;

    private BallStatesManager m_BallStateManager;

    private void Start()
    {
        m_DeadZoneSwingSprite.transform.localScale = new Vector3(m_DeadZoneSwingRadius * 2, m_DeadZoneSwingRadius * 2, 0);
        m_InputZoneBallSprite.transform.localScale = new Vector3(m_InputZoneBallRadius * 2, m_InputZoneBallRadius * 2, 0);

        m_InputDirectionRenderer = m_Ball.transform.GetChild(0).GetComponent<LineRenderer>();
        m_CalculatedDirection = m_Ball.transform.GetChild(1).GetComponent<LineRenderer>();

        m_BallStateManager = new BallStatesManager(m_Ball, m_DeadZoneSwingRadius, m_DeadZoneSwingSprite, m_InputZoneBallRadius, m_InputZoneBallSprite, m_MaxDistanceSwing,
                                                   m_MovePassed, m_LineMultiplier, m_InputDirectionRenderer, m_CalculatedDirection);

        m_BallStateManager.CurrentState = m_BallStateManager.StatesList[BallStates.Idle];
    }


    // Update is called once per frame
    void Update()
    {
        //tracking player by camera continously
        GameManager.instance.EventManager.TriggerEvent(Constants.START_CAMERA_TRACKING, m_Ball.position);

        //showing graphic part of inputzone if the player is stopeed
        if (m_Ball.GetComponent<Rigidbody>().velocity.magnitude == 0f)
            ShowHideSprite(m_InputZoneBallSprite, m_Ball.position, true);
        else
            m_InputZoneBallSprite.SetActive(false);

        bool isTouching = SetState();

        m_BallStateManager.CurrentState.OnUpdate();

        if (isTouching && CheckInInputZoneBall() && 
           (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved) && 
           m_Ball.GetComponent<Rigidbody>().velocity.magnitude == 0f)
        {
            ShowHideSprite(m_DeadZoneSwingSprite, m_BallStateManager.m_TouchStartPosition, true);
        }
    }

    private bool SetState()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                m_BallStateManager.ChangeState(BallStates.Began);
                return true;
            }
            else if (Input.touchCount == 1 && CheckInInputZoneBall() && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                m_BallStateManager.ChangeState(BallStates.Moved);
                return true;
            }
            else if (Input.touchCount == 1 && CheckInInputZoneBall() && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled))
            {
                m_BallStateManager.ChangeState(BallStates.Ended);
                return true;
            }
            else
            {
                return true;
            }
        }
        else
        {
            m_BallStateManager.ChangeState(BallStates.Idle);
            return false;
        }
    }


    /// <summary>
    /// Show or hide the sprite and set its position
    /// </summary>
    /// <param name="Sprite"></param>
    /// <param name="position"></param>
    /// <param name="showing"></param>
    private void ShowHideSprite(GameObject Sprite, Vector3 position, bool showing)
    {
        Sprite.transform.position = new Vector3(position.x, m_Ball.transform.position.y, position.z);
        Sprite.SetActive(showing);
    }

    /// <summary>
    /// Checks if the touch is in the Ball Deadzone (returns a bool)
    /// </summary>
    /// <returns></returns>
    public bool CheckInInputZoneBall()
    {
        float distance = Vector3.Distance(m_BallStateManager.m_TouchStartPosition, m_Ball.position);

        if (distance <= m_InputZoneBallRadius)
            return true;
        else
            return false;
    }
}