using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    
    public bool isPushedBack = false;
    public bool isDying = false;
    public bool attacking = false;
    public int health=1;

    public Vector3 pushVelocity = new Vector3(0f,0f,0f);
    public NavMeshAgent agent;
    
    public float checkDistance = 10f;
    public GameObject player;
    public float distanceToPlayer;

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

        distanceToPlayer= Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (distanceToPlayer <= checkDistance)
        {
            agent.destination = player.transform.position;
        }

    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag== "PlayerAttack")
        {
            health--;
            if (health <= 0)
            {
                DestroyEnemy();
            }
        }
    }*/

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerAttack")
        {
            health--;
            if (health <= 0)
            {
                DestroyEnemy();
            }
        }
    }

    public void DestroyEnemy()
    {
        //have whatever you want for enemy dying stuff
        Destroy(this.gameObject);
    }

}
