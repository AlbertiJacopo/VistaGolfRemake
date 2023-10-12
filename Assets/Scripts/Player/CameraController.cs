using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    [Tooltip ("Max zooming distance")]
    [SerializeField] private float m_MaxDistance;
    [Tooltip ("Min zooming distance")]
    [SerializeField] private float m_MinDistance;

    [SerializeField] private float m_RotationSpeed;
    [Tooltip ("High value = less sensibility")]
    [SerializeField] private float m_ZoomingSensibility;

    private Camera m_Camera;

    private Vector2 m_StartPos;
    private Vector2 m_StartPos2;
    private Vector2 m_LastPos;
    private Vector2 m_CurrentPos;
    private Vector2 m_CurrentPos2;

    private Vector3 m_BallPosition;

    private bool m_CanZoom = false;
    private bool m_CanTrack = false;

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

    }

    private void Update()
    {

        if (m_CanZoom)
        {
            Zooming();
        }

    }

    #region Rotation

    public void UpdateRotation(object[] param)
    {
        m_StartPos = (Vector2)param[0];
        m_CurrentPos = (Vector2)param[1];

        if ((m_StartPos.x < m_CurrentPos.x && m_CurrentPos.x < m_LastPos.x) ||
            (m_StartPos.x > m_CurrentPos.x && m_CurrentPos.x > m_LastPos.x))
        {
            m_StartPos = m_LastPos;
        }

        Quaternion startingRotation = transform.rotation;
        Quaternion wantedRotation = Quaternion.Euler(0f, m_StartPos.x - m_CurrentPos.x, 0f);

        transform.rotation = Quaternion.RotateTowards(startingRotation, wantedRotation, Time.deltaTime * m_RotationSpeed);
        
        m_LastPos = m_CurrentPos;
    }

    public void StopRotation(object[] param)
    {
        m_CanMove = false;
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

        m_Camera.orthographicSize += (currentDistance - startDistance) / m_ZoomingSensibility;

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
