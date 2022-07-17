using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    public void StartGame()
    {
        Time.timeScale = 1;
        LoadScene("Level");
    }
    
    public void ReloadScene()
    {
        Time.timeScale = 1;
        LoadScene("Level");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
