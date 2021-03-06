﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObstacleWallController : MonoBehaviour {

    public float FadingStartDelay = 5.0f;
    public float FadingDuration = 1.0f;

    private float startTime;
    private Color startColor;
    private Color endColor;

    // Use this for initialization
    void Start () {
        startTime = Time.time + FadingStartDelay;
        startColor = GetComponentInChildren<Renderer>().material.color;
        endColor = GetComponentInChildren<Renderer>().material.color;
        endColor.a = 0.0f;

        Destroy(gameObject, FadingStartDelay + FadingDuration);
    }
	
	// Update is called once per frame
	void Update () {
        // for each cube, set the right color
        foreach (Renderer child in GetComponentsInChildren<Renderer>()) {
            if (Time.time > startTime && Time.time < startTime + FadingDuration) {
                child.material.color = Color.Lerp(startColor, endColor, (Time.time - startTime) / FadingDuration);
            }
        }
    }
}
