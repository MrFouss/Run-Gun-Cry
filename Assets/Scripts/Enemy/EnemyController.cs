using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public enum DamageType{SHOTBYPLAYER,COLLISION};

    public int Health = 100;

    public int Damage = 10;

    private AudioSource audioSource;

    public AudioClip HitByLaserSound;
    public AudioClip DestructionSound;

    public Animation HitByLaserAnimation;
    public Animation DestructionAnimation;

    public GameObject SpawningEffectPrefab;
    public GameObject ExplosionPrefab;

	// Use this for initialization
	protected void Start () {
        Instantiate(SpawningEffectPrefab, gameObject.transform.position, gameObject.transform.rotation);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1.0f;
    }
	
    protected void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case Tags.ObstacleWallTag:
                TakeDamage(Health / 2, DamageType.COLLISION);
                break;
            case Tags.MechaBodyTag:
                TakeDamage(Health, DamageType.COLLISION);
                break;
            case Tags.MechaLaserTag:
				EventManager.onShotHitting.Invoke(ShotType.Laser);
				TakeDamage(collision.gameObject.GetComponentInParent<ProjectileBehavior>().Damage, DamageType.SHOTBYPLAYER);
				// TODO uncomment when these files are added
				//HitByLaserAnimation.Play();
				//audioSource.clip = HitByLaserSound;
				//audioSource.Play();
                break;
            case Tags.MechaMissileTag:
				EventManager.onShotHitting.Invoke(ShotType.Missile);
				TakeDamage(collision.gameObject.GetComponentInParent<ProjectileBehavior>().Damage, DamageType.SHOTBYPLAYER);
				// TODO uncomment when these files are added
                //HitByLaserAnimation.Play();
                //audioSource.clip = HitByLaserSound;
                //audioSource.Play();
                break;
            default:
                break;
        }
    }

    public void TakeDamage(int damage, DamageType dmgType)
    {
        StartCoroutine("DamageTakenFeedback");
        Health -= damage;
        if (Health <= 0)
        {
            // TODO uncomment when these files are added
            //audioSource.clip = DestructionSound;
            //audioSource.Play();
            if (dmgType == DamageType.SHOTBYPLAYER)
            {
                switch (gameObject.tag)
                {
                    case Tags.EnemyCowardTag:
                        EventManager.onEnemyDestruction.Invoke(EnemyType.Coward, DestructionType.Shot);
                        break;
                    case Tags.EnemyChargerTag:
                        EventManager.onEnemyDestruction.Invoke(EnemyType.Charger, DestructionType.Shot);
                        break;
                    case Tags.EnemyLaserTag:
                        EventManager.onEnemyDestruction.Invoke(EnemyType.Shooter, DestructionType.Shot);
                        break;
                    default:
                        break;
                }
            } else
            {
                switch (gameObject.tag)
                {
                    case Tags.EnemyCowardTag:
                        EventManager.onEnemyDestruction.Invoke(EnemyType.Coward, DestructionType.Collided);
                        break;
                    case Tags.EnemyChargerTag:
                        EventManager.onEnemyDestruction.Invoke(EnemyType.Charger, DestructionType.Collided);
                        break;
                    case Tags.EnemyLaserTag:
                        EventManager.onEnemyDestruction.Invoke(EnemyType.Shooter, DestructionType.Collided);
                        break;
                    default:
                        break;
                }
            }
            StartCoroutine("Death");
        }
    }

    IEnumerator Death()
    {
        // TODO uncomment when these files are added
        //DestructionAnimation.Play();
        //yield return DestructionAnimation.clip.length;
        Instantiate(ExplosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);

        yield return null;
    }

    IEnumerator DamageTakenFeedback()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;
        Color baseColor = mat.GetColor("_EmissionColor");

        for (int i = 1; i < 10; i++)
        {
            if (i < 5)
            {
                baseColor.r = Mathf.Min(baseColor.r + 0.2f, 1);
            }
            else
            {
                baseColor.r = Mathf.Max(baseColor.r - 0.2f, 0);
            }
            mat.SetColor("_EmissionColor", baseColor);
            yield return new WaitForSeconds(0.03f);
        }
        yield return null;
    }
}
