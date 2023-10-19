using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Score;
    [SerializeField] private TextMeshProUGUI m_UISwings;

    private float m_SwingsCount;

    [SerializeField] private Canvas m_MainMenuScreen;
    [SerializeField] private Canvas m_OptionsScreen;
    [SerializeField] private Canvas m_PauseScreen;

    private void Start()
    {
        GameManager.instance.EventManager.Register(Constants.UPDATE_SWINGS, UpdateSwingsCount);
    }

    public void UpdateSwingsCount(object[] param)
    {
        m_SwingsCount++;
        m_UISwings.text = "Swings: " + m_SwingsCount.ToString();
    }

    public void Pause()
    {
        m_PauseScreen.gameObject.SetActive(true);
    }

    public void GoBack()
    {
        ToggleUIScreen(m_MainMenuScreen, m_OptionsScreen);
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Resume()
    {
        m_PauseScreen.gameObject.SetActive(false);
    }

    public void GoToMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Used to ative or disactive a screen
    /// </summary>
    /// <param name="selectedScreen">the screen you want to active or disactive</param>
    /// <param name="previousScreen">the previous screen</param>
    public void ToggleUIScreen(Canvas selectedScreen, Canvas previousScreen)
    {
        if (!selectedScreen.gameObject.activeInHierarchy)
        {
            selectedScreen.gameObject.SetActive(true);
            previousScreen.gameObject.SetActive(false);
            //m_menuSound.PlayMenuClick();
            return;
        }
        else
        {
            previousScreen.gameObject.SetActive(true);
            selectedScreen.gameObject.SetActive(false);
            //m_menuSound.PlayBackButton();
            return;
        }
    }
}
