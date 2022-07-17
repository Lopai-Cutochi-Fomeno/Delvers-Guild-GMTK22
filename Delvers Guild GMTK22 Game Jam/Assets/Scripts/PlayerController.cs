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

    //public for the purpose of debugging and whatnot
    public float currentGravity;
    public bool canSmallJump = true;
    public float distanceToGround;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody rb;
    public float coyotetimer = 0;
    public float jumpStrengthMultiplierBaseValue = 1;
    public float jumpStrengthMultiplier ;
    

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
        //Movement
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, rb.velocity.y, 0);

        //small jumps
        if (Input.GetKeyUp(KeyCode.Space) && !isGrounded() && rb.velocity.y > 0 && canSmallJump)
        {
            Debug.Log("small");
            canSmallJump = false;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 2, 0);
        }

        //check if grounded for jump stuff
        if (isGrounded() && rb.velocity.y <= 0.0f)
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
            rb.velocity = new Vector3(rb.velocity.x, jumpstrength * jumpStrengthMultiplier, 0);
            //reset multiplier
            // Yes, you can jump very high if you are very lucky
            jumpStrengthMultiplier = jumpStrengthMultiplierBaseValue;
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

    private void OnCollisionEnter(Collision collision)
    {
    }
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
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.05f, groundMask);
    }
}