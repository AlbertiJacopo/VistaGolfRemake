using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float m_MaxDistance;
    [SerializeField] private float m_MinDistance;

    [SerializeField] private float m_RotationSpeed;

    private Vector2 m_StartPos;
    private Vector2 m_StartPos2;
    private Vector2 m_CurrentPos;
    private Vector2 m_CurrentPos2;

    private bool m_CanMove = false;
    private bool m_CanZoom = false;

    private void Start()
    {
        GameManager.instance.EventManager.Register(Constants.UPDATE_CAMERA_ROTATION, UpdateRotation);    
        GameManager.instance.EventManager.Register(Constants.STOP_CAMERA_ROTATION, StopRotation);    
    }

    private void Update()
    {
        if (m_CanMove)
        {
            CameraRotation();
        }

        if (m_CanZoom)
        {
            Zooming();
        }
    }

    #region Rotation

    public void UpdateRotation(object[] param)
    {
        if (!m_CanMove)
            m_CanMove = true;

        m_StartPos = (Vector2)param[0];
        m_CurrentPos = (Vector2)param[1];
    }

    private void CameraRotation()
    {
        Quaternion startingRotation = transform.rotation;
        Quaternion wantedRotation = Quaternion.Euler(0f, m_StartPos.x - m_CurrentPos.x, 0f);

        transform.rotation = Quaternion.RotateTowards(startingRotation, wantedRotation, Time.deltaTime * m_RotationSpeed);
    }

    public void StopRotation(object[] param)
    {
        m_CanMove = false;
    }

    #endregion

    #region Tracking

    public void StopTracking(object[] param)
    {

    }

    public void StartTracking(object[] param)
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
        Vector2 touch1Distance = new Vector2(m_CurrentPos.x - m_StartPos.x, m_CurrentPos.y - m_StartPos.y);
        Vector2 touch2Distance = new Vector2(m_CurrentPos2.x - m_StartPos2.x, m_CurrentPos2.y - m_StartPos2.y);

        Camera.main.orthographicSize = Vector2.Distance(touch1Distance, touch2Distance);
    }

    #endregion
}
