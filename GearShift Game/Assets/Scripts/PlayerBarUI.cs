using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    [SerializeField] Slider slider;

    void OnEnable() {
        Player.OnHealthChanged += UpdateUI;
    }

    void OnDisable() {
        Player.OnHealthChanged -= UpdateUI;
    }

    void UpdateUI(int health) {
        slider.value = health;
    }
}


