using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;

    private void Awake()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //open menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu.activeSelf)
            {
                menu.SetActive(true);
            }
            else { menu.SetActive(false); }
        }
    }
}
