using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Transform m_Ball;
    [SerializeField] private Transform m_Camera;

    private Vector3 m_TouchStartPosition;
    private Vector3 m_TouchEndPosition = Vector3.zero;
    private Vector2 m_TouchScreenStartPos;
    private Vector2 m_TouchScreenStartPos2;

    [Header("Ranges")]
    [SerializeField] private float m_DeadZoneSwingRadius;
    [SerializeField] private GameObject m_DeadZoneSwingSprite;
    [SerializeField] private float m_InputZoneBallRadius;
    [SerializeField] private GameObject m_InputZoneBallSprite;

    private bool m_MovePassed = false;

    private LineRenderer m_InputDirectionRenderer;
    private LineRenderer m_CalculatedDirection;

    private void Start()
    {
        m_DeadZoneSwingSprite.transform.localScale = new Vector3(m_DeadZoneSwingRadius * 2, m_DeadZoneSwingRadius * 2, 0);
        m_InputZoneBallSprite.transform.localScale = new Vector3(m_InputZoneBallRadius * 2, m_InputZoneBallRadius * 2, 0);

        m_InputDirectionRenderer = m_Ball.transform.GetChild(0).GetComponent<LineRenderer>();
        m_CalculatedDirection = m_Ball.transform.GetChild(1).GetComponent<LineRenderer>();
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

        //hiding touch deadzone if there're no touchs
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

            Debug.Log("touch: " + touchPosition);

            //taking the first touch
            if(touch.phase == TouchPhase.Began)
            {
                m_TouchStartPosition = touchPosition;
                m_TouchScreenStartPos = touch.position;
                m_InputDirectionRenderer.SetPosition(0, m_TouchStartPosition);
            }

            bool isInInputZone = CheckInInputZoneBall();

            //showing deadzone when touch
            if (isInInputZone && touch.phase == TouchPhase.Began && m_Ball.GetComponent<Rigidbody>().velocity.magnitude == 0f)
                ShowHideSprite(m_DeadZoneSwingSprite, m_TouchStartPosition, true);

            if (touch.phase == TouchPhase.Moved)
            {
                //checking if inside or outside the deadzone
                if (!isInInputZone)
                {
                    //rotate camera if outside the inputzone
                    GameManager.instance.EventManager.TriggerEvent(Constants.UPDATE_CAMERA_ROTATION, m_TouchScreenStartPos, touch.position);
                }
                else
                {
                    if (Vector3.Distance(m_TouchStartPosition, GetTouchWorldSpace(touch)) > m_DeadZoneSwingRadius)
                    {
                        m_TouchEndPosition = GetTouchWorldSpace(touch);
                        m_MovePassed = true;
                        m_InputDirectionRenderer.SetPosition(1, m_TouchEndPosition);



                        EnableDisableRenderers(true);
                    }
                    else m_MovePassed = false;
                }
            }
            
            //move the player if the touch is ended or canceled and the player is stopped
            else if((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) 
                     && isInInputZone && m_MovePassed 
                     && m_Ball.GetComponent<Rigidbody>().velocity.magnitude == 0f)
            {
                GameManager.instance.EventManager.TriggerEvent(Constants.MOVEMENT_PLAYER, m_TouchStartPosition, m_TouchEndPosition);
                m_DeadZoneSwingSprite.SetActive(false);

                EnableDisableRenderers(false);
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
                m_TouchScreenStartPos2 = touch2.position;
            }
            if (touch.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                GameManager.instance.EventManager.TriggerEvent(Constants.UPDATE_CAMERA_ZOOMING, m_TouchScreenStartPos, m_TouchScreenStartPos2, touch.position, touch2.position);
            }

            if (touch.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
            {
                GameManager.instance.EventManager.TriggerEvent(Constants.STOP_CAMERA_ZOOMING);
            }
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
        Sprite.transform.position = new Vector3(position.x, position.y - (m_Ball.transform.localScale.y / 2 - 0.00001f), position.z);
        Sprite.SetActive(showing);
    }

    /// <summary>
    /// Returns the worldspace position of the touch (returns a Vector3)
    /// </summary>
    /// <param name="touch"></param>
    /// <returns></returns>
    public Vector3 GetTouchWorldSpace(Touch touch)
    {
        float distanceFromCamera = Vector3.Distance(m_Camera.position, m_Ball.position);
        // = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, distanceFromCamera));
        Vector3 touchPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        // create a logical plane at this object's position
        // and perpendicular to world Y:
        Plane plane = new Plane(Vector3.up, m_Ball.position);
        float distance = 0; // this will return the distance from the camera
        if (plane.Raycast(ray, out distance))
        { // if plane hit...
            touchPosition = ray.GetPoint(distance); // get the point
                                                  // pos has the position in the plane you've touched
        }
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

        Debug.Log("m_TouchStartPosition " + m_TouchStartPosition);
        Debug.Log("m_Ball.position " + m_Ball.position);

        if (distance <= m_InputZoneBallRadius)
            return true;
        else
            return false;
    }

    private void CalculateWay()
    {
        Vector3 directionBall = -(m_TouchEndPosition - m_TouchStartPosition);
        RaycastHit hit;

        Vector3 wallHitPosition;
        Vector3 normal;

        Physics.Raycast(m_Ball.position, directionBall, out hit);

        wallHitPosition = hit.point;
        normal = new Vector3(hit.normal.x, 0f, hit.normal.z);

        Vector3 directionWall = Vector3.Reflect(directionBall.normalized, normal);
    }

    private void EnableDisableRenderers(bool value)
    {
        m_InputDirectionRenderer.gameObject.SetActive(value);
        m_CalculatedDirection.gameObject.SetActive(value);
    }
}
