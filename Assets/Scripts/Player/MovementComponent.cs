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

    [SerializeField] private float m_BallAppearTimer;

    private void Start()
    {
        GameManager.instance.EventManager.Register(Constants.MOVEMENT_PLAYER, Movement);
        GameManager.instance.EventManager.Register(Constants.TOGGLE_BALL, BallToggle);
        m_RigidBody = GetComponent<Rigidbody>();
        StartCoroutine(WaitTimeBallAppear());
    }
    private void Update()
    {
        m_LastVelocity = m_RigidBody.velocity;
    }


    public void Movement(object[] param)
    {
        GameManager.instance.EventManager.TriggerEvent(Constants.SAVE_BALL_POSITION, gameObject.transform.position);
        GameManager.instance.EventManager.TriggerEvent(Constants.PLAY_SOUND, Constants.SFX_HITBALL);

        Vector3 startPos = (Vector3)param[0];
        Vector3 endPos = (Vector3)param[1];

        Vector3 direction = (endPos - startPos);
        m_RigidBody.AddForce(-direction * m_MaxSpeed, ForceMode.Impulse);
    }

    public void Bounce(Vector3 normal)
    {
        GameManager.instance.EventManager.TriggerEvent(Constants.PLAY_SOUND, Constants.SFX_HITWALL);
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

    private IEnumerator WaitTimeBallAppear()
    {
        yield return new WaitForSeconds(m_BallAppearTimer);
        BallToggle(null);
    }

    public void BallToggle(object[] param)
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        else if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }

}
