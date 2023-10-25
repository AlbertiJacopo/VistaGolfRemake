using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.EventManager.Register(Constants.SAVE_FLOAT, SaveFloat);
        GameManager.instance.EventManager.Register(Constants.LOAD_FLOAT, LoadFloat);
    }

    /// <summary>
    /// Saves a float in the PlayerPrefs using a key string
    /// </summary>
    /// <param name="param"></param>
    public void SaveFloat(object[] param)
    {
        string key = (string)param[0];
        float value = (float)param[1];
        PlayerPrefs.SetFloat(key, value);
    }

    /// <summary>
    /// Loads a float from the PlayerPrefs using a key string
    /// </summary>
    /// <param name="param">key (string)</param>
    public void LoadFloat(object[] param)
    {
        string key = (string)param[0];
        PlayerPrefs.GetFloat(key);
    }
}
