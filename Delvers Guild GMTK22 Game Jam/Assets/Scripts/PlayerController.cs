using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public float jumpstrength;
    public float coyoteTime;
    public bool canJump = false;
    public float baseGravity = 10;
    public float maxFallSpeed = 50;

    public int health = 6;
    public float invulnerabilityTime=1;

    public GameObject gameoverMenu;

    //public for the purpose of debugging and whatnot
    public float currentGravity;
    public bool canSmallJump = true;
    public float distanceToGround;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody rb;
    public float coyotetimer = 0;
    private float invulnerabilityTimer = 0;

    Vector3 rotationAngle = new Vector3(0f, 180f, 0f);
    public bool walkingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //setting the gravity to base gravity
        currentGravity = baseGravity;
    }

    // Update is called once per frame
    void Update()
    {
        //degrease the invulnerabilityTimer
        if (invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
        }

        //Movement
            rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, rb.velocity.y, 0);

        //small jumps
        if (Input.GetKeyUp(KeyCode.Space) && !isGrounded() && rb.velocity.y > 0 && canSmallJump)
        {
            canSmallJump = false;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 2, 0);
        }

        //rotate the player into walking direction
        bool lastWalkingRight = walkingRight;
        if (Input.GetAxis("Horizontal") < 0) walkingRight = false;
        if (Input.GetAxis("Horizontal") > 0) walkingRight = true;
        if(lastWalkingRight != walkingRight)
        {
            transform.Rotate(rotationAngle);
        }

        //check if grounded for jump stuff
        if (isGrounded())
        {
            coyotetimer = 0;
            canJump = true;
            canSmallJump = true;
        }
        else
        {
            coyotetimer += Time.deltaTime;
            if (coyotetimer > coyoteTime)
            {
                canJump = false;
            }
        }

        //Jumping
        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            canJump = false;
            rb.velocity = new Vector3(rb.velocity.x, jumpstrength, 0);
        }
    }
    private void FixedUpdate()
    {
        //apply custom gravity
        if (rb.velocity.y > -1 * maxFallSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - currentGravity * Time.deltaTime, 0);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && invulnerabilityTimer <= 0)
        {
            //reset invulnerability time
            invulnerabilityTimer = invulnerabilityTime;

            //degrease health and either call gameover funktion or update UI
            health--;
            if (health == 0)
            {
                //Gameover
                Debug.Log("GameOver");
                Time.timeScale = 0;
                gameoverMenu.SetActive(true);
                this.GetComponent<MenuScript>().gameover = true;
            }
            else
            {
                //Update UI
                Debug.Log(health);
            }
        }
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")&& invulnerabilityTimer<=0)
        {
            //reset invulnerability time
            invulnerabilityTimer = invulnerabilityTime;
            
            //degrease health and either call gameover funktion or update UI
            health--;
            if (health == 0)
            {
                //Gameover
                Debug.Log("GameOver");
                Time.timeScale = 0;
                gameoverMenu.SetActive(true);
                this.GetComponent<MenuScript>().gameover = true;
            }
            else
            {
                //Update UI
                Debug.Log(health);
            }
        }
    }*/

    private void setGravity(float gravity)
    {

    }

    private bool isGrounded()
    {
        //not optimal but good enough, try to get the box working or cast 2 rays on the border of the character to fix

        /*if (Physics.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, Vector3.down, transform.rotation, 0.05f, groundLayer))
        {
            Debug.Log(transform.position);
        }
            
        return Physics.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, Vector3.down, transform.rotation, 0.1f, groundLayer);
        */
        int groundMask = 1; // Only Ground ;
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.05f, groundMask);
    }
}