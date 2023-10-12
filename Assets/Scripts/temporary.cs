using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class temporary : MonoBehaviour
{
    public void GoToLevel(int iLevel)
    {
        SceneManager.LoadScene(iLevel);
    }
}
