using UnityEngine;
using System.Collections;

public class IsometricPlayer : MonoBehaviour {
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Parry Settings")]
    public float parryDuration = 0.2f;
    public float parryCooldown = 0.5f;
    public GameObject parryVisual;

    private Rigidbody2D rb;
    private Vector2 rawInput;

    [HideInInspector] public bool isParrying = false;
    private bool canParry = true;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        if (parryVisual != null) parryVisual.SetActive(false);
    }

    void Update() {
        // --- RAW INPUT (normalize YOK) ---
        rawInput.x = Input.GetAxisRaw("Horizontal");
        rawInput.y = Input.GetAxisRaw("Vertical");

        // --- Parry ---
        if (Input.GetKeyDown(KeyCode.Space) && canParry) {
            StartCoroutine(PerformParry());
        }
    }

    void FixedUpdate() {
        Vector2 move = rawInput;

        // 🔥 SADECE ÇAPRAZDA 2:1 ORANI
        if (move.x != 0 && move.y != 0) {
            move.y *= 0.5f; // 26.565°
        }

        move = move.normalized;

        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }

    IEnumerator PerformParry() {
        isParrying = true;
        canParry = false;

        yield return new WaitForSeconds(parryDuration);

        isParrying = false;

        yield return new WaitForSeconds(parryCooldown);
        canParry = true;
    }

    public void ShowSuccessfulParry() {
        Debug.Log("BAŞARILI PARRY! SARI KARE ÇIKIYOR!");
        StartCoroutine(PlayParryEffect());
    }

    IEnumerator PlayParryEffect() {
        if (parryVisual != null) {
            parryVisual.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            parryVisual.SetActive(false);
        }
    }
}
