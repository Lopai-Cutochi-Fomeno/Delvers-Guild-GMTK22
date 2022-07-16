using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndShake : MonoBehaviour
{
    public AnimationCurve intensityRotation;
    public AnimationCurve intensityShake;
    public float duration = 1f;
    public float rotationSpeed = 0f;

    // Start is called before the first frame update
    private Vector3 degreePerSecond;
    private bool running = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space) && !running ) {
            running = true;
            degreePerSecond = new Vector3(Random.Range(0.0f, 359f),
                                          Random.Range(0.0f, 359f),
                                          Random.Range(0.0f, 359f)) * rotationSpeed;


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
            transform.Rotate(degreePerSecond * Time.deltaTime * intensityRotation.Evaluate(elapsedTime/duration));
            yield return null;
        }
        transform.position = startPosition;
        running = false;
    }
}
