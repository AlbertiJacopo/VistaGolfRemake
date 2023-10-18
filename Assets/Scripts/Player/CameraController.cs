using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    [Tooltip("Max zooming distance")]
    [SerializeField] private float m_MaxDistance;
    [Tooltip("Min zooming distance")]
    [SerializeField] private float m_MinDistance;

    [SerializeField] private float m_RotationSpeed;
    [Tooltip("High value = less sensibility")]
    [SerializeField] private float m_ZoomingSensibility;
    [SerializeField] private float m_MouseZoomingSensibility;

    private Camera m_Camera;

    private Vector2 m_StartPos;
    private Vector2 m_StartPos2;
    private Vector2 m_LastPos = Vector2.zero;
    private Vector2 m_CurrentPos;
    private Vector2 m_CurrentPos2;

    private Vector3 m_BallPosition;

    private float m_CurrentScrollDelta = 0f;

    private bool m_CanZoom = false;
    private bool m_CanTrack = false;

    private float m_RotationY;
    private Vector3 m_OriginalRotation;
    private float m_Direction = -1;


    private void Start()
    {
        GameManager.instance.EventManager.Register(Constants.UPDATE_CAMERA_ROTATION, UpdateRotation);
        GameManager.instance.EventManager.Register(Constants.STOP_CAMERA_ROTATION, StopRotation);
        GameManager.instance.EventManager.Register(Constants.UPDATE_CAMERA_ZOOMING, UpdateZooming);
        GameManager.instance.EventManager.Register(Constants.STOP_CAMERA_ZOOMING, StopZooming);
        GameManager.instance.EventManager.Register(Constants.START_CAMERA_TRACKING, StartTracking);
        GameManager.instance.EventManager.Register(Constants.STOP_CAMERA_TRACKING, StopTracking);
        m_Camera = GetComponentInChildren<Camera>();
        CheckZoomingDistance();

        m_CurrentScrollDelta = m_Camera.orthographicSize;

        m_OriginalRotation = transform.eulerAngles;
        m_RotationY = m_OriginalRotation.y;
    }

    private void Update()
    {

        if (m_CanZoom)
        {
            Zooming();
        }
        else
        {
            m_CurrentScrollDelta += Input.mouseScrollDelta.y;
            m_Camera.orthographicSize = m_CurrentScrollDelta * -1 / m_MouseZoomingSensibility;

            if (m_Camera.orthographicSize < m_MinDistance)
            {
                m_Camera.orthographicSize = m_MinDistance;
            }

            else if (m_Camera.orthographicSize > m_MaxDistance)
            {
                m_Camera.orthographicSize = m_MaxDistance;
            }
        }


    }

    #region Rotation

    public void UpdateRotation(object[] param)
    {
        //m_StartPos = (Vector2)param[0];
        //m_CurrentPos = (Vector2)param[1];

        Vector2 delta = (Vector2)param[0];

        //Quaternion startingRotation = transform.rotation;
        //Quaternion wantedRotation = Quaternion.Euler(0f, m_StartPos.x - m_CurrentPos.x, 0f);

        //float deltaY = m_StartPos.y - m_CurrentPos.y;
        m_RotationY += delta.x * Time.deltaTime * m_RotationSpeed * m_Direction;

        transform.eulerAngles = new Vector3(0f, m_RotationY, 0f);

        ////float angle = Quaternion.Angle(startingRotation, wantedRotation);

        ////transform.rotation = Quaternion.Euler(0f, angle * m_RotationSpeed, 0f);

        //transform.rotation = Quaternion.RotateTowards(startingRotation, wantedRotation, Time.deltaTime * m_RotationSpeed);

    }

    public void StopRotation(object[] param)
    {

    }

    #endregion

    #region Tracking

    public void StopTracking(object[] param)
    {
        if (!m_CanTrack)
            m_CanTrack = true;
    }

    public void StartTracking(object[] param)
    {
        if (m_CanTrack)
            m_CanTrack = false;

        m_BallPosition = (Vector3)param[0];
        transform.position = m_BallPosition;
    }

    private void CameraTrack()
    {

    }

    #endregion

    #region Zooming
    public void UpdateZooming(object[] param)
    {
        if (!m_CanZoom)
            m_CanZoom = true;

        m_StartPos = (Vector2)param[0];
        m_StartPos2 = (Vector2)param[1];
        m_CurrentPos = (Vector2)param[2];
        m_CurrentPos2 = (Vector2)param[3];
    }

    public void StopZooming(object[] param)
    {
        m_CanZoom = false;
    }

    private void Zooming()
    {
        float startDistance = Vector2.Distance(m_StartPos, m_StartPos2);
        float currentDistance = Vector2.Distance(m_CurrentPos, m_CurrentPos2);

        m_Camera.orthographicSize -= ((currentDistance - startDistance) / m_ZoomingSensibility);

        CheckZoomingDistance();
    }

    private void CheckZoomingDistance()
    {
        if (m_Camera.orthographicSize < m_MinDistance)
        {
            m_Camera.orthographicSize = m_MinDistance;
        }

        else if (m_Camera.orthographicSize > m_MaxDistance)
        {
            m_Camera.orthographicSize = m_MaxDistance;
        }
    }

    #endregion
}
