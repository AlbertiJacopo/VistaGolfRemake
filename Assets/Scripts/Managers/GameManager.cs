using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Generics.Pattern.SingletonPattern;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private UIManager m_UIManager;
    private AudioManager m_AudioManager;
    private EventManager m_EventManager = Factory.CreateEventManager();
    private SaveManager m_SaveManager;

    public EventManager EventManager { get => m_EventManager; }
    public SaveManager SaveManager { get => m_SaveManager; }

    private void Start()
    {
        m_UIManager = GetComponentInChildren<UIManager>();
        m_AudioManager= GetComponentInChildren<AudioManager>();
        m_SaveManager = GetComponentInChildren<SaveManager>();

        instance.EventManager.Register(Constants.WIN_GAME, WinGame);
    }

    public void WinGame(object[] param)
    {
        SceneManager.LoadScene((int)param[0]);
    }
}
