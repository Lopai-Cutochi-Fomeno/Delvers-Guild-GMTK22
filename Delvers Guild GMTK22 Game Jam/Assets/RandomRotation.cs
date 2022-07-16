using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{

    public Rigidbody rb;
    Vector2 lastMousePos = new Vector2(0,0);

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
            rb.AddTorque(new Vector3(Random.Range(0,100), Random.Range(0, 100), Random.Range(0, 100)), ForceMode.Acceleration);
            lastMousePos = Input.mousePosition;
        }
    }
}
