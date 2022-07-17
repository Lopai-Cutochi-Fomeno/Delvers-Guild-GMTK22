using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CastLogic : MonoBehaviour
{
    public GameObject die;
    public GameObject ice;
    public GameObject dieCam;
    // Time to fire
    public float castTime = 1.1f;
    public float animationDuration = 1.0f;

    public float cooldown = 3.0f;
    private bool canCast = true;
    private float timeSinceCast;
    private Dictionary<int, GameObject> inTriggerRange;


    void Start() {
        die.GetComponent<RotateScript>().duration = animationDuration;
        dieCam.GetComponent<ShakeScript>().duration = animationDuration;
        inTriggerRange = gameObject.GetComponent<TriggerManager>().enemyDict;
        timeSinceCast = cooldown; // set to match cooldown initially
    }
    // Update is called once per frame
    void Update()
    {
        if (timeSinceCast >= cooldown)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                die.GetComponent<RotateScript>().startRotation();
                dieCam.GetComponent<ShakeScript>().startShake();
                timeSinceCast = 0.0f;
                canCast = true; // shouldn't matter that this is here
            }
        }
        else
        {
            timeSinceCast += Time.deltaTime;
            if (canCast && timeSinceCast >= castTime)
            {
                canCast = false;
                int abilitySelector = Random.Range(0, 6);
                // StartCoroutine(Cast(abilitySelector));
                StartCoroutine(Cast(3));
            }

        }
    }
    private IEnumerator Cast(int abilitySelector)
    {
        switch (abilitySelector)
        {
            case 0:
                //Power up the Jump
                gameObject.GetComponent<PlayerController>().jumpStrengthMultiplier += 0.5f;
                Debug.Log(0 + " This is a High Jump " + abilitySelector);
                break;
            case 1:
                Debug.Log(1 + " Freeze Enemies " + abilitySelector);
                {
                var list = new List<GameObject>(inTriggerRange.Values);
                foreach( GameObject o in list)
                {
                    StartCoroutine(AgentCon(o, 2.0f));
                }
                }
                break;
            case 2:
                Debug.Log(2 + " Player Gets Knocked Back " + abilitySelector);
                break;
            case 3:
                Debug.Log(3 + " Knock Enemies Back" + abilitySelector);
                {
                var list = new List<GameObject>(inTriggerRange.Values);
                foreach( GameObject o in list)
                {
                    Vector3 direction = o.transform.position - gameObject.transform.position ;
                    Vector3 force = Vector3.Normalize(Vector3.Scale(direction, Vector3.right)) * 40;
                    Debug.Log(force);
                    StartCoroutine(pushBackEnemy(o, 0.5f, force));

                }
                }
                break;
            case 4:
                Debug.Log(4 + " Duplicate Enemies" + abilitySelector);
                {
                var list = new List<GameObject>(inTriggerRange.Values);
                int cloned = 0;
                foreach( GameObject o in list) 
                {
                    GameObject go = GameObject.Instantiate(o);
                    go.transform.position = o.transform.position;
                    cloned++;
                }
                Debug.Log(cloned);
                }
                break;
            case 5:
                Debug.Log(5 + " Surrounding Enemies Expload" + abilitySelector);
                {var list = new List<GameObject>(inTriggerRange.Values);
                foreach( GameObject o in list) 
                {
                    int id = o.GetInstanceID();
                    Vector3 pos = o.transform.position;
                    GameObject explodice = GameObject.Instantiate(die);
                    explodice.layer = 1;
                    explodice.transform.position = pos;
                    Debug.Log(gameObject.transform.position);
                    Debug.Log(explodice.transform.position);
                    StartCoroutine(DestroyInSeconds(explodice, 1.5f));
                    Destroy(o);
                    //idc if it works
                    inTriggerRange.Remove(id);
                }}
                break;
            default:
                Debug.Log("this should never happen");
                break;
        }
        yield return null;
    }

    private IEnumerator DestroyInSeconds(GameObject o, float seconds) {

        yield return new WaitForSeconds(seconds);
        Destroy(o);
        yield return null;
    }
    private IEnumerator AgentCon(GameObject o, float seconds) {
        Debug.Log(o.name + " frozen");
        var agent = o.GetComponent<NavMeshAgent>();
        agent.velocity = new Vector3(0f,0f,0f);
        agent.isStopped = true;
        // o.GetComponent<MeshRenderer>().enabled = false;
        var newice = GameObject.Instantiate(ice, o.transform.position, o.transform.rotation);
        yield return new WaitForSeconds(seconds);
        Destroy(newice);
        // o.GetComponent<MeshRenderer>().enabled = true;
        agent.isStopped = false;
        yield return null;
    }
    
    private IEnumerator pushBackEnemy(GameObject o, float seconds, Vector3 velocity) 
    {
        EnemyController controller = o.GetComponent<EnemyController>();
        controller.isPushedBack = true;
        controller.pushVelocity = velocity;
        controller.agent.velocity = velocity;
        yield return new WaitForSeconds(seconds);
        controller.isPushedBack = false;
        controller.agent.velocity = new Vector3(0,0,0);
        
    }
    

}
