using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{

    public string audioName;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        AudioManager.instance.Play(audioName);
    }
}
