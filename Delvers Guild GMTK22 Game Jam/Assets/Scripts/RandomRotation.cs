using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{

    public Rigidbody rb;
    Vector3 lastMousePos = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(Input.mousePosition, lastMousePos)>0.01f)
        {
            Vector3 dir = Input.mousePosition - lastMousePos;

            rb.AddTorque(dir, ForceMode.Impulse);
            lastMousePos = Input.mousePosition;
        }
    }
}
