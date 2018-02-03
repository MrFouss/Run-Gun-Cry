using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour {

    public BoxCollider boxCollider;
    public ObstacleData obstacleData;

    // Feedback sounds
    public AudioClip obstacleHitSound;
    public AudioClip obstacleDestroyedSound;

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
            case Tags.MechaMissileTag:
                obstacleData.DestroyObstacle();
                AudioSource.PlayClipAtPoint(obstacleDestroyedSound, transform.position);
                break;
            case Tags.MechaBulletTag:
                AudioSource.PlayClipAtPoint(obstacleHitSound, transform.position);
                break;
        }
    }
}
