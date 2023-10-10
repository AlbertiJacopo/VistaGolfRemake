using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    private Rigidbody m_RigidBody;

    // Start is called before the first frame update

    private void Start()
    {
        GameManager.instance.EventManager.Register(Constants.MOVEMENT_PLAYER, Movement);
    }

    public void Movement(object[] param)
    {

    }

    public void Bounce()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
