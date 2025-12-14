using System;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance;

    public static event Action<int> OnHealthChanged;

    int MaxHealth = 100;
    int CurrentHealth;

    void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start() {
        CurrentHealth = MaxHealth;

        // UI ilk deðeri alsýn
        OnHealthChanged?.Invoke(CurrentHealth);
    }

    public int GetHealth() {
        return CurrentHealth;
    }

    public void TakeDamage(int damage) {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        //  UI'ye haber ver
        OnHealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0) {
            Debug.Log("Player Dead");
        }
    }
}
