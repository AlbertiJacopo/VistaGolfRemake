using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    private Rigidbody m_RigidBody;
    [SerializeField] private float m_ForceMultiplier;
    private Vector3 m_LastVelocity;

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
        Vector3 startPos = (Vector3)param[0];
        Vector3 endPos = (Vector3)param[1];

        Vector3 direction = (endPos - startPos).normalized;
        m_RigidBody.AddForce(-direction * m_ForceMultiplier, ForceMode.Impulse);
    }

    public void Bounce(Vector3 normal)
    {
        float speed = m_LastVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(m_LastVelocity.normalized, normal);

        m_RigidBody.velocity = direction * Mathf.Max(speed, speed/2);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (m_RigidBody.velocity.y == 0)
        {
            Vector3 normal = new Vector3(other.contacts[0].normal.x, 0f, other.contacts[0].normal.z);
            Bounce(normal);
        }
        else
        {
            Bounce(other.contacts[0].normal);
        }
    }
}
