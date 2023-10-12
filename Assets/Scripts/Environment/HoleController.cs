using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class HoleController : MonoBehaviour
{
    [SerializeField] private int m_NextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.EventManager.TriggerEvent(Constants.WIN_GAME, m_NextLevel);
        }
    }
}
