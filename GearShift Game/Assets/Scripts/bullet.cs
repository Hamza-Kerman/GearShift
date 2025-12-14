using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    public static event Action DamageBlocked;

    public float lifetime = 5f; // Merminin ömrü

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check collision with Player
        if (other.CompareTag("Player"))
        {
            IsometricPlayer playerScript = other.GetComponent<IsometricPlayer>();

            if (playerScript != null)
            {
                // Check if player is currently parrying
                if (playerScript.isParrying)
                {
                    // SUCCESSFUL PARRY
                    playerScript.ShowSuccessfulParry();
                    Destroy(gameObject); // Destroy projectile, no damage
                    DamageBlocked?.Invoke();
                }
                else
                {

                    // FAILED PARRY - TAKE DAMAGE
                    Debug.Log("Player Hit! Taking Damage...");
                    Player.Instance.TakeDamage(10);

                    Destroy(gameObject);
                }
            }
        }
        // Destroy if hits a wall (but not the Enemy itself)
        else if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}