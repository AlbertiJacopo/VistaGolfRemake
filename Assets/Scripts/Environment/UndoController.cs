using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class UndoController : MonoBehaviour
{
    private Vector3 m_BallPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.EventManager.Register(Constants.SAVE_BALL_POSITION, SavingPosition);
    }

    public void SavingPosition(object[] param)
    {
        m_BallPosition = (Vector3)param[0];
    }

    public void Undo(GameObject ball)
    {
        if(ball.GetComponent<Rigidbody>().velocity.magnitude == 0)
            ball.transform.position = m_BallPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Debug.Log("velocity pre undo: " + other.gameObject.GetComponent<Rigidbody>().velocity);
        Undo(other.gameObject);
        Debug.Log("velocity post undo: " + other.gameObject.GetComponent<Rigidbody>().velocity);
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Debug.Log("velocity post reset: " + other.gameObject.GetComponent<Rigidbody>().velocity);
    }
}
