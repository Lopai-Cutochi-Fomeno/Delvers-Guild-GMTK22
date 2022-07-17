using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public float jumpstrength;
    public float coyoteTime;
    public bool canJump = true;
    public float baseGravity = 10;
    public float maxFallSpeed = 50;

    public int health = 6;
    public float invulnerabilityTime=1;

    public GameObject gameoverMenu;

    //public for the purpose of debugging and whatnot
    public float currentGravity;
    public bool canSmallJump = true;
    public bool canWallJump = true;
    public float distanceToGround;
    public float characterWidth;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody rb;
    public float coyotetimer = 0;
    public float wallcoyotetimer = 0;
    public float jumpStrengthMultiplierBaseValue = 1;
    public float jumpStrengthMultiplier ;
    private float invulnerabilityTimer = 0;
    

    Vector3 rotationAngle = new Vector3(0f, 180f, 0f);
    public float walkingDirection = -1;
    
    private bool isKnockedBack = false;
    private Vector3 knockbackVelocity = new Vector3(0f,0f,0f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //setting the gravity to base gravity
        currentGravity = baseGravity;
        jumpStrengthMultiplier = jumpStrengthMultiplierBaseValue;
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
        float lastWalkingRight = walkingDirection;
        if (Input.GetAxis("Horizontal") < 0) walkingDirection = 1.0f;
        if (Input.GetAxis("Horizontal") > 0) walkingDirection = -1.0f;
        if(lastWalkingRight != walkingDirection)
        {
            transform.Rotate(rotationAngle);
        }

        //check if grounded for jump stuff
        if (isGrounded() && rb.velocity.y <= 0.0f)
        {
            coyotetimer = 0;
            canJump = true;
            canSmallJump = true;
            canWallJump = true;
}
        else
        {
            coyotetimer += Time.deltaTime;
            if (coyotetimer > coyoteTime)
            {
                canJump = false;
            }
        }

        //check for walljumps
        if (wallTouch())
        {
            wallcoyotetimer =0;
        }
        else
        {
            wallcoyotetimer += Time.deltaTime;
        }

        //Jumping
        if (Input.GetKey(KeyCode.Space))
        {
            if (canJump)
            {
                canJump = false;
                rb.velocity = new Vector3(rb.velocity.x, jumpstrength * jumpStrengthMultiplier, 0);
                //reset multiplier
                // Yes, you can jump very high if you are very lucky
                jumpStrengthMultiplier = jumpStrengthMultiplierBaseValue;
            }
            else if (canWallJump && (wallcoyotetimer <= coyoteTime))
            {
                canWallJump = false;
                rb.velocity = new Vector3(rb.velocity.x, jumpstrength * jumpStrengthMultiplier, 0);
            }                      
        }
        
        if(isKnockedBack) 
        {
            rb.velocity += knockbackVelocity;
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
                AudioManager.instance.Play("GameOver");
                //Time.timeScale = 0;
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
        int groundMask = Physics.DefaultRaycastLayers ^ (1 << 4); // Only Ground ;
        if(Physics.Raycast(transform.position+ (Vector3.left* characterWidth), Vector3.down, distanceToGround + 0.05f, groundMask))
        {
            return true;
        }
        if (Physics.Raycast(transform.position + (Vector3.right * characterWidth), Vector3.down, distanceToGround + 0.05f, groundMask))
        {
            return true;
        }
        return false;
    }

    private bool wallTouch()
    {
        int groundMask = Physics.DefaultRaycastLayers ^ (1 << 4); // Only Ground ;
        if(Physics.Raycast(transform.position, Vector3.left, characterWidth + 0.2f, groundMask))
        {
            return true;
        }
        if (Physics.Raycast(transform.position, Vector3.right, characterWidth + 0.2f, groundMask))
        {
            return true;
        }
        return false;
    }

    public IEnumerator knockBack(Vector3 velocity, float seconds) 
    {
        isKnockedBack = true;
        knockbackVelocity = velocity * walkingDirection;
        yield return new WaitForSeconds(seconds);
        knockbackVelocity = new Vector3(0f,0f,0f);
        isKnockedBack = false;
    }
}
