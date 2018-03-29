using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float HorizontalSeed = 0f;
    public float VerticalSeed = 5f;
    public float RotationalSeed = 10f;
    public float MovingFactorIncreaseStep = 0.3f;
    public float DecaySpeed = 0.2f;
    public float MaximumRotationAngle = 4.0f;
    public float MovementSpeed = 8f;

    public Transform CameraTransform;
    public Transform PlayerTransform;

    private float movingFactor = 0f;

    // Update is called once per frame
    void Update()
    {
        Quaternion totalRotation = PlayerTransform.rotation;
        float xPerlinNoise = Mathf.PerlinNoise(HorizontalSeed, Time.time * MovementSpeed) * 2f - 1f;
        float yPerlinNoise = Mathf.PerlinNoise(VerticalSeed, Time.time * MovementSpeed) * 2f - 1f;
        float zPerlinNoise = Mathf.PerlinNoise(VerticalSeed, Time.time * MovementSpeed) * 2f - 1f;
        float movementFactor = Mathf.Pow(movingFactor, 2f);
        float xAddedAngle = xPerlinNoise * MaximumRotationAngle * movementFactor;
        float yAddedAngle = yPerlinNoise * MaximumRotationAngle * movementFactor;
        float zAddedAngle = zPerlinNoise * MaximumRotationAngle * movementFactor;
        totalRotation.eulerAngles = totalRotation.eulerAngles + new Vector3(xAddedAngle, yAddedAngle, zAddedAngle);
        CameraTransform.rotation = totalRotation;

        movingFactor = Mathf.Max(movingFactor - (DecaySpeed * Time.deltaTime), 0.0f);
    }

    public void IncreaseMovingFactor()
    {
        movingFactor = Mathf.Min(movingFactor + MovingFactorIncreaseStep, 1f);
    }
}
