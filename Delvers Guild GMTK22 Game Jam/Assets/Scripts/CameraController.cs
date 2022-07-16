using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Quaternion startRotation;
    public float maxXRot = 25f;
    public float moveSpeed = 100f;
    public bool isLookingDown = false;

    public AnimationCurve xRotCurve;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            RotateCamera();
        }
    }

    public void RotateCamera()
    {
        if (!isLookingDown)
        {
            RotateForSeconds(moveSpeed);
        }
    }

    public async void RotateForSeconds(float duration)
    {
        Debug.Log("RotateForSeconds was called.");
        var end = Time.time + duration;
        isLookingDown = true;
        float currentTime = 0f;
        Quaternion currentRotation = transform.rotation;
        while (Time.time < end)
        {
            //float newXRot = Mathf.LerpAngle(xRot, maxXRot, Time.deltaTime / duration);
            //transform.rotation = Quaternion.Euler(new Vector3(newXRot, transform.rotation.y, transform.rotation.z));
            currentTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(currentRotation, Quaternion.Euler(new Vector3(maxXRot, transform.rotation.y, transform.rotation.z)), xRotCurve.Evaluate(currentTime / duration));
            await Task.Yield();
        }
        isLookingDown = false;
    }
}

