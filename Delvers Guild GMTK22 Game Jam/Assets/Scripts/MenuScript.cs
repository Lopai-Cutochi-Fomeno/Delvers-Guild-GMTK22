using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;
    private bool gamepaused = false;
    public bool gameover = false;

    private void Awake()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //open menu
        if (Input.GetKeyDown(KeyCode.Escape)&&!gameover )
        {
            if (!gamepaused)
            {
                menu.SetActive(true);
                gamepaused = true;
                Time.timeScale = 0;
            }
            else 
            { 
                menu.SetActive(false);
                gamepaused = false;
                Time.timeScale = 1;
            }
        }
    }
}
