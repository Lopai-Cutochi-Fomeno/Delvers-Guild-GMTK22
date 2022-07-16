using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastLogic : MonoBehaviour
{
    public GameObject die;
    public GameObject dieCam;
    
    private float cooldown = -0.0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && cooldown <= 0){
            die.GetComponent<RotateScript>().startRotation();
            dieCam.GetComponent<ShakeScript>().startShake();
            

            cooldown = 3.0f;
        }
    }
}
