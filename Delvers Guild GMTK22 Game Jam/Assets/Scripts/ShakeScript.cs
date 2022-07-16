using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
    public AnimationCurve intensityShake;
    public float duration = 1f;

    // Start is called before the first frame update
    private bool running = false;

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown (KeyCode.E) && !running ) {
    //         startShake();
    //     }
    // }
    public void startShake() {
        if (!running) {
            StartCoroutine(Shaking());
        }
    }
    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float strength = intensityShake.Evaluate(elapsedTime/ duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
        running = false;
    }
}
