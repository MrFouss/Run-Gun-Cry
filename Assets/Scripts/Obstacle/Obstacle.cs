using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    // Data about an obstacle
    public int structurePoints = 100;

    // Collider of the obstacle
    public BoxCollider boxCollider;

    // Feedback sounds
    public AudioClip obstacleHitSound;
    public AudioClip obstacleDestroyedSound;

    // Instance of the created broken obstacle and particle system
    public GameObject brokenObstacle;
    public GameObject particleSystem;

    // Data about the blinking routine
    public float blinkingDuration = 1.0f;
    public float blinkingFrequency = 10.0f;
    public float blinkingTransparency = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Detects collisions between the obstacle and:
    //  - the body of the mecha ("MechaBody")
    //  - a missile launched by the mecha ("MechaMissile")
    //  - a bullet fired by the mecha ("MechaBullet")
    private void OnTriggerEnter(Collider other) {
        switch (other.gameObject.tag) {
            case Tags.MechaBodyTag:
                // remove all structure points and destroy the obstacle
                TakeDamage(structurePoints, other.gameObject);
                break;
            case Tags.MechaBulletTag:
                // TODO uncomment when machine gun mechanic is implemented
                // int damage = other.GetComponent<Bullet>().Damage;
                // TakeDamage(damage, other.gameObject);
                TakeDamage(1, other.gameObject);
                break;
            case Tags.MechaMissileTag:
                // TODO uncomment when missile launcher mechanic is implemented
                // int damage = other.GetComponent<Missile>().Damage;
                // TakeDamage(damage, other.gameObject);
                TakeDamage(50, other.gameObject);
                break;
        }
    }

    private void TakeDamage(int damage, GameObject otherObject) {
        structurePoints -= damage;

        if (structurePoints <= 0) {
            AudioSource.PlayClipAtPoint(obstacleDestroyedSound, transform.position);
            DestroyObstacle();
        } else {
            AudioSource.PlayClipAtPoint(obstacleHitSound, transform.position);
            SpawnParticles(otherObject);
            StartCoroutine(BlinkColor());
        }
    }

    // Destroys the obstacles
    private void DestroyObstacle() {
        Destroy(gameObject);
        Instantiate(brokenObstacle, transform.position, transform.rotation);
    }

    private void SpawnParticles(GameObject otherObject) {
        Rigidbody rb = otherObject.GetComponent<Rigidbody>();
        Quaternion particleSystemRotation = Quaternion.FromToRotation(Vector3.forward, -rb.velocity);
        Instantiate(particleSystem, otherObject.transform.position, particleSystemRotation);
    }

    // Routine to blink the obstacle
    private IEnumerator BlinkColor() {
        float endTime = Time.time + blinkingDuration;

        Color opaqueColor = GetComponent<Renderer>().material.color;

        Color transparentColor = GetComponent<Renderer>().material.color;
        transparentColor.a = blinkingTransparency;
        
        while (Time.time < endTime) {
            yield return new WaitForSeconds(1 / (2 * blinkingFrequency));
            GetComponent<Renderer>().material.color = transparentColor;
            yield return new WaitForSeconds(1 / (2 * blinkingFrequency));
            GetComponent<Renderer>().material.color = opaqueColor;
        }
    }
}
