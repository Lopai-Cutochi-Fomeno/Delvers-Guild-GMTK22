using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public AnimationCurve intensityRotation;
    public float duration = 1f;

    // Start is called before the first frame update
    private bool running = false;
    void Start()
    {
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space)) {
    //         startRotation();
    //     }
    // }

    public void startRotation()
    {
        if (!running)
        {
            running = true;
            StartCoroutine(Shaking(Random.rotation));
        }

    }
    IEnumerator Shaking(Quaternion rotationTarget)
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
