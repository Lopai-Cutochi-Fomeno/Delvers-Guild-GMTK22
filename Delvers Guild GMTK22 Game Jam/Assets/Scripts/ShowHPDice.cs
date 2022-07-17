using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHPDice : MonoBehaviour
{
    public Vector3[] rotations;
    public int healthinput = 6;
    private int lastinput = 0;
    private bool rotationInProgress = false;
    private float rotduration = 0.5f;
    private Quaternion currentangle;
    private Quaternion nextangle;
    private float elapsedTime;
    // start is called before the first frame update
    void Start()
    {
        lastinput = healthinput;
        rotations = new Vector3[6];
        Debug.Log(rotations.Length);
        rotations[0] = new Vector3(180f, 0f, 0f);
        rotations[1] = new Vector3(0f, 270f, 0f);
        rotations[2] = new Vector3(90f, 0f, 0f);
        rotations[3] = new Vector3(270f, 0f, 0f);
        rotations[4] = new Vector3(0f, 90f, 0f);
        rotations[5] = new Vector3(0f, 0f, 0f);
        transform.rotation = Quaternion.Euler(rotations[healthinput -1]);
        currentangle = transform.rotation;
        nextangle = currentangle;

    }

    // Update is called once per frame
    void Update()
    {
        if(healthinput != lastinput) {
            Debug.Log(healthinput);
            lastinput = healthinput;
            elapsedTime = 0;
            currentangle = transform.rotation;
            if(healthinput > 0) {
                nextangle = Quaternion.Euler(rotations[healthinput -1]);
            }
            else {
                Destroy(gameObject);
            }
        }

        transform.rotation = Quaternion.Lerp(currentangle,nextangle,Mathf.Min(elapsedTime/rotduration, 1));
        elapsedTime += Time.deltaTime;

    }
}
