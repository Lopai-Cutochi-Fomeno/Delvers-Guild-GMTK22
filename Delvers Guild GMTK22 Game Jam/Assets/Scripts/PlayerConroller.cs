using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public float jumpstrength;
    public float coyoteTime;
    public bool touchingGround;
    public bool canJump=false;

    public float distanceToGround;

    [SerializeField]private LayerMask groundLayer;
    private BoxCollider boxcollider;
    private Rigidbody rb;
    public float coyotetimer = 0;

    void Start()
    {
        boxcollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, rb.velocity.y,0);
        
        //Jumping
        if (Input.GetAxisRaw("Vertical") == 1 && canJump)
        {
            canJump = false;
            rb.velocity = new Vector3(rb.velocity.x,jumpstrength,0);
        }
        
        //check if grounded
        if (isGrounded())
        {
            coyotetimer = 0;
            canJump = true;
        }
        else{
            coyotetimer += Time.deltaTime;
            if (coyotetimer > coyoteTime)
            {
                canJump=false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
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

        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.05f);
    }
}
