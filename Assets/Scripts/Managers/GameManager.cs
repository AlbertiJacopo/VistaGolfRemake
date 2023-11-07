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

    [SerializeField] private Animator m_Level;

    private bool m_AnimPlayed;

    private void Start()
    {
        m_UIManager = GetComponentInChildren<UIManager>();
        m_AudioManager= GetComponentInChildren<AudioManager>();
        m_SaveManager = GetComponentInChildren<SaveManager>();

        instance.EventManager.Register(Constants.WIN_GAME, WinGame);
        instance.EventManager.Register(Constants.ANIM_START_SINK, PlaySinkAnimaton);
    }

    public void PlaySinkAnimaton(object[] param)
    {
        m_Level.SetBool("EndLevel", true);

        while (!m_AnimPlayed)
        {
            if (AnimatorIsPlaying())
            { 
                m_AnimPlayed = true;
                EventManager.TriggerEvent(Constants.LOAD_NEXT_LEVEL);
            }
            else m_AnimPlayed = false;
        }
    }

    bool AnimatorIsPlaying()
    {
        if (!m_Level.GetBool("EndLevel"))
            return m_Level.GetCurrentAnimatorStateInfo(0).length >
                   m_Level.GetCurrentAnimatorStateInfo(0).normalizedTime;
        else
            return false;
    }

    public void WinGame(object[] param)
    {
        SceneManager.LoadScene((int)param[0]);
    }
}
