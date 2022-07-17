using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    
    public bool isPushedBack = false;
    public bool isDying = false;
    public bool attacking = false;

    public Vector3 pushVelocity = new Vector3(0f,0f,0f);
    public NavMeshAgent agent;
    public float checkDistance = 10f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(isPushedBack) {
            agent.velocity = pushVelocity;
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            agent.destination = player.transform.position;
    }

}
