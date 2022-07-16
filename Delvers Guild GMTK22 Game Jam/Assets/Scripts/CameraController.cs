using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float startXRot;
    public float maxXRot = 25f;
    public float moveSpeed = 0.01f;
    public bool isLookingDown = false;
    float timeCount = 0.0f;
    float currentXRot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startXRot = transform.rotation.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            //rotate the camera down and keep it there until the user stops pressing
            currentXRot = maxXRot;
            RotateCamera();
        }
        else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            currentXRot = -maxXRot;
            RotateCamera();
        }
        else
        {
            currentXRot = startXRot;
            RotateCamera();

        }

    }

    public void RotateCamera()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(currentXRot, transform.rotation.y, transform.rotation.z)), timeCount * moveSpeed);
        timeCount += Time.deltaTime;
    }

    //public void RotateCamera()
    //{
    //    if (!isLookingDown)
    //    {
    //        RotateForSeconds(moveSpeed);
    //    }
    //}

    //public async void RotateForSeconds(float duration)
    //{
    //    var end = Time.time + duration;
    //    isLookingDown = true;
    //    float currentTime = 0f;
    //    Quaternion currentRotation = transform.rotation;
    //    while (Time.time < end)
    //    {
    //        //float newXRot = Mathf.LerpAngle(xRot, maxXRot, Time.deltaTime / duration);
    //        //transform.rotation = Quaternion.Euler(new Vector3(newXRot, transform.rotation.y, transform.rotation.z));
    //        currentTime += Time.deltaTime;
    //        transform.rotation = Quaternion.Lerp(currentRotation, Quaternion.Euler(new Vector3(maxXRot, transform.rotation.y, transform.rotation.z)), currentTime / duration);
    //        await Task.Yield();
    //    }
    //    isLookingDown = false;
    //}
}

