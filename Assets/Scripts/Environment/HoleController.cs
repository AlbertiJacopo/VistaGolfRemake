using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(CircleCollider2D))]
public class HoleController : MonoBehaviour
{
    [SerializeField] private int m_NextLevel;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Scene actualScene = SceneManager.GetActiveScene();
            
            GameManager.instance.EventManager.TriggerEvent(Constants.WIN_GAME, actualScene.buildIndex + 1);
        }
    }
}
