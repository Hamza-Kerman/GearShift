using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GearBarUI : MonoBehaviour {
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;

    private void Start() {
        if (GearSystem.Instance != null) {
            GearSystem.Instance.OnGearBarChanged += UpdateUI;
        }
    }

    private void OnDestroy() {
        if (GearSystem.Instance != null) {
            GearSystem.Instance.OnGearBarChanged -= UpdateUI;
        }
    }

    // Event ile çağrılıyor
    private void UpdateUI(float normalizedBar) {
        if (slider != null)
            slider.value = normalizedBar;

        if (text != null)
            text.text = GearSystem.Instance.gearLevel.ToString(); // ✅ burası düzeltildi
    }
}
