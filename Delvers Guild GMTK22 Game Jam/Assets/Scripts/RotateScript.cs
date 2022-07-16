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
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space) && !running ) {
            running = true;
            StartCoroutine(Shaking(Random.rotation));
        }
    }
    IEnumerator Shaking(Quaternion angle)
    {
        Quaternion startRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float value = intensityRotation.Evaluate(elapsedTime/duration);
            transform.rotation = Quaternion.Lerp(startRotation, angle, value);
            yield return null;
        }
        transform.rotation = startRotation;
        running = false;
    }
}
