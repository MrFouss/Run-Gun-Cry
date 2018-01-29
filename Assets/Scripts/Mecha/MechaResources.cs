using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaResources : MonoBehaviour {

    // resources
    public static int health;
    public static int shield;
    public static int energy;

    private static bool isDead;
    private static bool isShieldEmpty;
    private static bool isEnergyEmpty;

	// Use this for initialization
	void Start () {
        // initial resources
        health = 100;
        shield = 100;
        energy = 75;

        isDead = false;
        isShieldEmpty = false;
        isEnergyEmpty = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0)
        {
            isDead = true;
            Debug.Log("Mecha is Destoyed");
        }

        if (shield <= 0)
        {
            isShieldEmpty = true;
        }
        else
        {
            isShieldEmpty = false;
        }

        if (energy <= 0)
        {
            isEnergyEmpty = true;
        }
        else
        {
            isEnergyEmpty = false;
        }
    }

    public static void Mecha_DamageTaken(int damage)
    {
        if (isShieldEmpty)
        {
            health -= damage;
        }
        else
        {
            if (damage - shield > 0)
            {
                int remainingDamage = damage - shield;
                shield = 0;
                health -= remainingDamage;
            }
            else
            {
                shield -= damage;
            }

        }
        Debug.Log("Health : " + health + "\n" + "Shield : " + shield);
    }
}
