using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public Dictionary<int, GameObject> enemyDict;
    // Start is called before the first frame update
    void Awake()
    {
        enemyDict = new Dictionary<int, GameObject>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            try {
                enemyDict.Add(other.gameObject.GetInstanceID(), other.gameObject);
            }
            catch (ArgumentException) {
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if ( other.gameObject.tag == "Enemy")
        {
           enemyDict.Remove(other.gameObject.GetInstanceID());
        }
    }

}
