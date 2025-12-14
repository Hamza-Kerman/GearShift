using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed = 3f;
    public float detectionRadius = 5f;
    public float attackRadius = 1f;
    public float attackCooldown = 1.5f; // saniye
    public int damage = 10;

    public Transform player;

    private float lastAttackTime;

    void Update() {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRadius) {
            if (distance > attackRadius) {
                // Oyuncuya doðru hareket et
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else {
                // Saldýrý zamaný
                if (Time.time > lastAttackTime + attackCooldown) {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    void Attack() {
        // Basitçe konsola yazdýrýyoruz, buraya animasyon ve hasar eklenebilir
        Debug.Log("Enemy attacks! Damage: " + damage);
        // Örneðin: player.GetComponent<PlayerHealth>().TakeDamage(damage);
    }

    void OnDrawGizmosSelected() {
        // Algýlama ve saldýrý alanýný görebilmek için
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
