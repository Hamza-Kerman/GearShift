using UnityEngine;
using System.Collections;

public class IsometricPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Parry Settings")]
    public float parryDuration = 0.2f;      // Savuşturma penceresi süresi
    public float parryCooldown = 0.5f;      // Bekleme süresi
    public GameObject parryVisual;          // Sarı Kare

    private Rigidbody2D rb;
    private Vector2 movementInput;

    [HideInInspector] public bool isParrying = false;
    private bool canParry = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Başlangıçta kareyi gizle
        if (parryVisual != null) parryVisual.SetActive(false);
    }

    void Update()
    {
        // --- Hareket ---
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movementInput = new Vector2(moveX, moveY).normalized;

       

        // --- Parry Girdisi ---
        if (Input.GetKeyDown(KeyCode.Space) && canParry)
        {
            StartCoroutine(PerformParry());
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementInput * moveSpeed;
    }

    // Bu fonksiyon sadece "Ben şu an savunmadayım" modunu açar.
    // GÖRSEL AÇMAZ.
    IEnumerator PerformParry()
    {
        isParrying = true;
        canParry = false;

        // Burada görseli AÇMIYORUZ. Sadece mantık işliyor.
        // İstersen "Swish" diye boşa sallama sesi buraya eklenebilir.

        yield return new WaitForSeconds(parryDuration);

        isParrying = false;

        yield return new WaitForSeconds(parryCooldown);
        canParry = true;
    }

    // Bu fonksiyonu MERMİ (Projectile.cs) çağıracak.
    // Eğer zamanlama doğruysa burası çalışacak ve SARI KARE çıkacak.
    public void ShowSuccessfulParry()
    {
        Debug.Log("BAŞARILI PARRY! SARI KARE ÇIKIYOR!");
        StartCoroutine(PlayParryEffect());
    }

    IEnumerator PlayParryEffect()
    {
        if (parryVisual != null)
        {
            parryVisual.SetActive(true); // GÖRSEL BURADA AÇILIYOR
            yield return new WaitForSeconds(0.2f); // 0.2 saniye görünür kal
            parryVisual.SetActive(false); // Kapan
        }
    }
}