using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Generics.Pattern.SingletonPattern;

public class GameManager : Singleton<GameManager>
{
    private UIManager m_UIManager;
    private AudioManager m_AudioManager;
    private EventManager m_EventManager = Factory.CreateEventManager();

    public UIManager UIManager { get => m_UIManager; }
    public AudioManager AudioManager { get => m_AudioManager; }
    public EventManager EventManager { get => m_EventManager; }

    private void Start()
    {
        m_UIManager = GetComponentInChildren<UIManager>();
        m_AudioManager= GetComponentInChildren<AudioManager>();
    }
}
