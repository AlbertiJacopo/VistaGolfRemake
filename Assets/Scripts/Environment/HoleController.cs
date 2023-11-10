using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(CircleCollider2D))]
public class HoleController : MonoBehaviour
{
    [SerializeField] private int m_NextLevel;
    private Scene m_ActualScene;

    private void Start()
    {
        GameManager.instance.EventManager.Register(Constants.LOAD_NEXT_LEVEL, LoadNextLevel);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_ActualScene = SceneManager.GetActiveScene();

            GameManager.instance.EventManager.TriggerEvent(Constants.TOGGLE_BALL);
            GameManager.instance.EventManager.TriggerEvent(Constants.PLAY_SOUND, Constants.SFX_WIN);
            GameManager.instance.EventManager.TriggerEvent(Constants.ANIM_START_SINK);
        }
    }
    
    public void LoadNextLevel(object[] param)
    {
        GameManager.instance.EventManager.TriggerEvent(Constants.WIN_GAME, m_ActualScene.buildIndex + 1);
    }
}
