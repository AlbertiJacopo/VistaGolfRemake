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

    public void Movement(object[] param)
    {
        Vector3 startPos = (Vector3)param[0];
        Vector3 endPos = (Vector3)param[1];

        Vector3 direction = (endPos - startPos).normalized;
        m_RigidBody.AddForce(-direction * m_ForceMultiplier, ForceMode.Impulse);
    }

    private void Update()
    {
        m_LastVelocity = m_RigidBody.velocity;
    }

    public void Bounce(Collision collision)
    {
        float speed = m_LastVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(m_LastVelocity.normalized, collision.contacts[0].normal);

        m_RigidBody.velocity = direction * Mathf.Max(speed, 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bounce(collision);
    }
}
