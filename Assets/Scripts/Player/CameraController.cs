using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float m_MaxDistance;
    [SerializeField] private float m_MinDistance;

    [SerializeField] private float m_RotationSpeed;

    private Vector2 m_StartPos;
    private Vector2 m_CurrentPos;

    private bool m_CanMove = false;

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
    }

    public void UpdateRotation(object[] param)
    {
        if (!m_CanMove)
        {
            m_CanMove = true;
        }

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

    public void StopTracking(object[] param)
    {

    }

    public void StartTracking(object[] param)
    {

    }

    public void Zooming(object[] param)
    {

    }
}
