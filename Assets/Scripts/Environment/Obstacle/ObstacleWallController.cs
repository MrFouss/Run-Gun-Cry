using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWallController : MonoBehaviour {

    // Data about an obstacle
    public int StructurePoints = 100;

    private int health;

    // Damage dealt on collision
    public int Damage = 50;

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

    private Color startColor;

    private void Start() {
        health = StructurePoints;
        startColor = GetComponentInChildren<Renderer>().material.color;
    }

    // Detects collisions between the obstacle and things that can hurt it
    private void OnCollisionEnter(Collision other) {
		int damage;

        switch (other.gameObject.tag) {
            case Tags.MechaBodyTag:
                // remove all structure points and destroy the obstacle
                TakeDamage(health, other.gameObject);
                // invoke an event to let the scoring script know
                EventManager.onEnemyDestruction.Invoke(EnemyType.Obstacle, DestructionType.Collided);
                break;

			case Tags.MechaLaserTag:
            case Tags.MechaMissileTag:
                damage = other.gameObject.GetComponentInParent<ProjectileBehavior>().Damage;
                TakeDamage(damage, other.gameObject);
                break;

            default:
                break;
        }
    }

    private void TakeDamage(int damage, GameObject otherObject) {
        health -= damage;

        if (health <= 0) {
            AudioSource.PlayClipAtPoint(ObstacleDestroyedSound, transform.position, 1.0f);
            // if the obstacle is destroyed by the gunner, call an event to update the score
            if ((otherObject.tag == Tags.MechaLaserTag) || (otherObject.tag == Tags.MechaMissileTag))
            {
                EventManager.onEnemyDestruction.Invoke(EnemyType.Obstacle, DestructionType.Shot);
            }
            DestroyObstacle();
        } else {
            AudioSource.PlayClipAtPoint(ObstacleHitSound, transform.position, 1.0f);
            SpawnParticles(otherObject);
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, new Color(0.0f, 0.0f, 0.0f), 1 - ((float) health / StructurePoints));
        }
    }

    // Destroys the obstacles
    private void DestroyObstacle() {
        Instantiate(BrokenObstacle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void SpawnParticles(GameObject otherObject) {
        Rigidbody rb = otherObject.GetComponentInParent<Rigidbody>();
        Quaternion particleSystemRotation = Quaternion.FromToRotation(Vector3.forward, -rb.velocity);
        GameObject particleSystemInstance = Instantiate(ParticleSystemPrefab, otherObject.transform.position, particleSystemRotation);
        Destroy(particleSystemInstance, 1.0f);
    }
}
