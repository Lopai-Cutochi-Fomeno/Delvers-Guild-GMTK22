using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject[] agents;

    private void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Update()
    {
        
    }
}
