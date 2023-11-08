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
        if(ball.GetComponent<Rigidbody>().velocity.magnitude == 0f)
            ball.transform.position = m_BallPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.EventManager.TriggerEvent(Constants.PLAY_SOUND, Constants.SFX_BALLOUT);

        other.gameObject.GetComponent<Rigidbody>().Sleep();
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Undo(other.gameObject);
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
