using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform m_SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.EventManager.Register(Constants.TOGGLE_BALL, BallToggle);
        GameManager.instance.EventManager.Register(Constants.SPAWN_BALL, TeleportBall);
    }

    /// <summary>
    /// Set ball gameobject active or not depending on which state the gameobject is
    /// <summary>
    public void BallToggle(object[] param)
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        else if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }

    public void TeleportBall(object[] param)
    {
        gameObject.transform.position = m_SpawnPoint.position;
    }
}
