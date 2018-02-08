using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWallController : MonoBehaviour {

    // Data about an obstacle
    public int StructurePoints = 100;

    // Collider of the obstacle
    public BoxCollider BoxCollider;

    // Feedback sounds
    public AudioClip ObstacleHitSound;
    public AudioClip ObstacleDestroyedSound;

    // Instance of the created broken obstacle and particle system
    public GameObject BrokenObstacle;
    public GameObject ParticleSystemPrefab;

    // Data about the blinking routine
    public float BlinkingDuration = 1.0f;
    public float BlinkingFrequency = 10.0f;
    public float BlinkingTransparency = 0.0f;

    // Detects collisions between the obstacle and things that can hurt it
    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case Tags.MechaBodyTag:
                // remove all structure points and destroy the obstacle
                TakeDamage(StructurePoints, other.gameObject);
                break;
            case Tags.MechaLaserTag:
                // TODO uncomment when laser mechanic is implemented
                // int damage = other.GetComponent<LaserProjectile>().Damage;
                // TakeDamage(damage, other.gameObject);
                TakeDamage(1, other.gameObject);
                break;
            case Tags.MechaMissileTag:
                // TODO uncomment when missile launcher mechanic is implemented
                // int damage = other.GetComponent<MissileProjectile>().Damage;
                // TakeDamage(damage, other.gameObject);
                TakeDamage(50, other.gameObject);
                break;
        }
    }

    private void TakeDamage(int damage, GameObject otherObject) {
        StructurePoints -= damage;

        if (StructurePoints <= 0) {
            AudioSource.PlayClipAtPoint(ObstacleDestroyedSound, transform.position);
            DestroyObstacle();
        } else {
            AudioSource.PlayClipAtPoint(ObstacleHitSound, transform.position);
            SpawnParticles(otherObject);
            StartCoroutine(BlinkColor());
        }
    }

    // Destroys the obstacles
    private void DestroyObstacle() {
        Destroy(gameObject);
        Instantiate(BrokenObstacle, transform.position, transform.rotation);
    }

    private void SpawnParticles(GameObject otherObject) {
        Rigidbody rb = otherObject.GetComponent<Rigidbody>();
        Quaternion particleSystemRotation = Quaternion.FromToRotation(Vector3.forward, -rb.velocity);
        GameObject particleSystemInstance = Instantiate(ParticleSystemPrefab, otherObject.transform.position, particleSystemRotation);
        Destroy(particleSystemInstance, 1.0f);
    }

    // Routine to blink the obstacle
    private IEnumerator BlinkColor() {
        float endTime = Time.time + BlinkingDuration;

        Color opaqueColor = GetComponent<Renderer>().material.color;

        Color transparentColor = GetComponent<Renderer>().material.color;
        transparentColor.a = BlinkingTransparency;
        
        while (Time.time < endTime) {
            yield return new WaitForSeconds(1 / (2 * BlinkingFrequency));
            GetComponent<Renderer>().material.color = transparentColor;
            yield return new WaitForSeconds(1 / (2 * BlinkingFrequency));
            GetComponent<Renderer>().material.color = opaqueColor;
        }
    }
}
