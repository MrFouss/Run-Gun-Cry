using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObstacle : MonoBehaviour {

    public float fadingStartDelay = 5.0f;
    public float fadingDuration = 1.0f;

    private float startTime;
    private Color startColor;
    private Color endColor;

    // Use this for initialization
    void Start () {
        startTime = Time.time + fadingStartDelay;
        startColor = GetComponent<Renderer>().material.color;
        endColor = GetComponent<Renderer>().material.color;
        endColor.a = 0.0f;

        // for each cube, set the starting material (for fading)
        foreach (Renderer child in GetComponentsInChildren<Renderer>()) {
            child.material = GetComponent<Renderer>().material;
        }

        Destroy(gameObject, fadingStartDelay + fadingDuration);
    }
	
	// Update is called once per frame
	void Update () {
        // for each cube, set the right color
        foreach (Renderer child in GetComponentsInChildren<Renderer>()) {
            if (Time.time > startTime && Time.time < startTime + fadingDuration) {
                child.material.color = Color.Lerp(startColor, endColor, (Time.time - startTime) / fadingDuration);
            }
        }
    }
}
