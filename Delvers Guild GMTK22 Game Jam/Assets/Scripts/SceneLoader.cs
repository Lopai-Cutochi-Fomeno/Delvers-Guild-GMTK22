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
        LoadScene("Level");
        Time.timeScale = 1;
        AudioManager.instance.PlayStealDice();
    }
    
    public void ReloadScene()
    {
        LoadScene("Level");
        Time.timeScale = 1;
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
