using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    private Rigidbody m_RigidBody;
    private Vector3 m_LastVelocity;

    [Header("Modificators")]
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private float m_ReducePercentage;
    [SerializeField] private float m_TimeMaxSpeed;

    private float m_currentDrag;
    private float m_currentAngularDrag;

    private void Start()
    {
        GameManager.instance.EventManager.Register(Constants.MOVEMENT_PLAYER, Movement);
        m_RigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        m_LastVelocity = m_RigidBody.velocity;
    }


    public void Movement(object[] param)
    {
        GameManager.instance.EventManager.TriggerEvent(Constants.SAVE_BALL_POSITION, gameObject.transform.position);

        Vector3 startPos = (Vector3)param[0];
        Vector3 endPos = (Vector3)param[1];

        Vector3 direction = (endPos - startPos);

        m_currentDrag = m_RigidBody.drag;
        m_currentAngularDrag = m_RigidBody.angularDrag;

        SettingUpDrags(0f, 0f);

        m_RigidBody.AddForce(-direction * m_MaxSpeed, ForceMode.Impulse);

        StartCoroutine(TimerMaxSpeed(m_TimeMaxSpeed));
    }

    private void SettingUpDrags(float drag, float angularDrag)
    {
        m_RigidBody.drag = drag;
        m_RigidBody.angularDrag = angularDrag;
    }

    private IEnumerator TimerMaxSpeed(float time)
    {
        yield return new WaitForSeconds(time);

        SettingUpDrags(m_currentDrag, m_currentAngularDrag);
    }

    public void Bounce(Vector3 normal)
    {
        float speed = m_LastVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(m_LastVelocity.normalized, normal);

        //speed = speed - ((m_ReducePercentage * speed) / 100);
        speed = speed * m_ReducePercentage;
        m_RigidBody.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (m_LastVelocity.y <= 0.1 && m_LastVelocity.y >= -0.1)
        {
            
            Vector3 normal = new Vector3(other.GetContact(0).normal.x, 0f, other.GetContact(0).normal.z);
            Bounce(normal);
        }
        else
        {
            //Bounce(other.contacts[0].normal);
            Bounce(other.GetContact(0).normal);
        }
    }
}
