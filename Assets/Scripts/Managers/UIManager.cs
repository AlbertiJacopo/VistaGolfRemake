using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Score;
    [SerializeField] private TextMeshProUGUI m_UISwings;

    private float m_TotalSwingsCount;
    private float m_LevelSwingsCount;

    [SerializeField] private GameObject m_MainMenuScreen;
    [SerializeField] private GameObject m_OptionsScreen;
    [SerializeField] private Canvas m_PauseScreen;
    [SerializeField] private GameObject m_LevelSelectScreen;

    private void Start()
    {
        GameManager.instance.EventManager.Register(Constants.UPDATE_LEVEL_SWINGS, UpdateLevelSwingsCount);
        //GameManager.instance.EventManager.Register(Constants.UPDATE_TOTAL_SWINGS, UpdateTotalSwings);
    }

    /// <summary>
    /// update the total count of swings in the entire game
    /// </summary>
    /// <param name="param"></param>
    public void UpdateTotalSwings()
    {
        m_TotalSwingsCount += m_LevelSwingsCount;
    }

    /// <summary>
    /// restarts the current level
    /// </summary>
    public void RestartLevel()
    {
        m_TotalSwingsCount -= m_LevelSwingsCount;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        
    }

    /// <summary>
    /// update the count of swings for the level
    /// </summary>
    /// <param name="param"></param>
    public void UpdateLevelSwingsCount(object[] param)
    {
        m_LevelSwingsCount++;
        UpdateTotalSwings();
        m_UISwings.text = m_LevelSwingsCount.ToString();
    }

    /// <summary>
    /// pause screen
    /// </summary>
    public void Pause()
    {
        m_PauseScreen.gameObject.SetActive(true);
    }

    /// <summary>
    /// return to the main menu if in the option menu
    /// </summary>
    public void GoBackFromOption()
    {
        ToggleUIScreen(m_MainMenuScreen, m_OptionsScreen);
    }
    
    public void GoBackFromLevelSelect()
    {
        ToggleUIScreen(m_MainMenuScreen, m_LevelSelectScreen);
    }

    public void GoToLevelSelect()
    {
        ToggleUIScreen(m_LevelSelectScreen, m_MainMenuScreen);
    }

    /// <summary>
    /// start game
    /// </summary>
    /// <param name="sceneName"></param>
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// resume the ame if in pause menu
    /// </summary>
    public void Resume()
    {
        m_PauseScreen.gameObject.SetActive(false);
    }

    /// <summary>
    /// return to main menu
    /// </summary>
    /// <param name="sceneName"></param>
    public void GoToMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Used to ative or disactive a screen
    /// </summary>
    /// <param name="selectedScreen">the screen you want to active or disactive</param>
    /// <param name="previousScreen">the previous screen</param>
    public void ToggleUIScreen(GameObject selectedScreen, GameObject previousScreen)
    {
        if (!selectedScreen.gameObject.activeInHierarchy)
        {
            selectedScreen.SetActive(true);
            previousScreen.SetActive(false);
            //m_menuSound.PlayMenuClick();
            return;
        }
        else
        {
            previousScreen.SetActive(true);
            selectedScreen.SetActive(false);
            //m_menuSound.PlayBackButton();
            return;
        }
    }

    public void LoadSceneNumber(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
