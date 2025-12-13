using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Ayarlar")]
    public GameObject ucgenMermiPrefab; // ��gen prefab�n� buraya s�r�kle
    public float atisHizi = 1f; // Saniyede ka� mermi?
    public float mermiHizi = 10f; // Merminin u�u� h�z�

    private Transform player; // Hedef (Karakter)
    private float atisZamanlayicisi;

    void Start()
    {
        // Sahnedeki "Player" etiketli objeyi otomatik bulur
        // Not: Karakterine "Player" tag'i verdi�inden emin ol!
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return; // Player yoksa ate� etme

        atisZamanlayicisi -= Time.deltaTime;

        if (atisZamanlayicisi <= 0)
        {
            AtesEt();
            atisZamanlayicisi = 1f / atisHizi;
        }
    }

    void AtesEt()
    {
        // 1. Y�n� hesapla (Karakterin oldu�u yer - Benim oldu�um yer)
        Vector2 yon = (player.position - transform.position).normalized;

        // 2. Mermiyi olu�tur
        GameObject mermi = Instantiate(ucgenMermiPrefab, transform.position, Quaternion.identity);

        // 3. Mermiyi karaktere do�ru d�nd�r (��genin ucu baks�n diye)
        float angle = Mathf.Atan2(yon.y, yon.x) * Mathf.Rad2Deg;
        mermi.transform.rotation = Quaternion.Euler(0, 0, angle - 90); // ��genin ucu yukar� bak�yorsa -90 gerekir, sa�a bak�yorsa gerekmez.

        // 4. Mermiye h�z ver (Rigidbody2D kullanarak)
        Rigidbody2D rb = mermi.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = yon * mermiHizi;
        }
    }
}