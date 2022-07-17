using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public AnimationCurve intensityRotation;
    public float duration = 1f;

    // Start is called before the first frame update
    private bool running = false;
    private Vector3 rotationStandard = new Vector3(90f, -90f, 45f);
    

    void Start()
    {
        
    }
    // Update is called once per frame
     void Update()
     {
         if(!running) {
            transform.Rotate(rotationStandard * Time.deltaTime);
         }
     }

    public void startRotation()
    {
        if (!running)
        {
            running = true;
            StartCoroutine(Rotate(Random.rotation));
        }

    }
    IEnumerator Rotate(Quaternion rotationTarget)
    {
        Quaternion startOrientation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float value = intensityRotation.Evaluate(elapsedTime/duration);
            transform.rotation = Quaternion.Lerp(startOrientation, rotationTarget, value);
            yield return null;
        }
        transform.rotation = startOrientation;
        running = false;
    }
}
