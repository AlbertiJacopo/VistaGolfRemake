using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    private Rigidbody m_RigidBody;
    [SerializeField] private float m_ForceMultiplier;

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

    public void Bounce()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
