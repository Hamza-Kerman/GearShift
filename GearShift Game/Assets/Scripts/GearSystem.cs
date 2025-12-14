using UnityEngine;
using System;

public class GearSystem : MonoBehaviour {
    public static GearSystem Instance;

    public int gearLevel = 1;
    public int maxGear = 6;

    public float gearBar = 50f;
    public float maxGearBar = 100f;

    public float decayRate = 10f;   // saniyede azalacak
    public float hitIncrease = 20f;  // düþmana vurunca artýþ
    public float blockIncrease = 10f; // bloklama artýþý

    // UI için event (0-1 arasý normalize)
    public event Action<float> OnGearBarChanged;

    void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void OnEnable() {
        HasarVerici.OnDamageDeal += OnHitEnemy;
        Projectile.DamageBlocked += OnProjectileBlocked;
    }

    void OnDisable() {
        HasarVerici.OnDamageDeal -= OnHitEnemy;
        Projectile.DamageBlocked -= OnProjectileBlocked;
    }

    void Update() {
        // Bar zamanla azalýr
        gearBar -= decayRate * Time.deltaTime;
        if (gearBar < 0) gearBar = 0;

        // Gear artýþý
        while (gearBar >= maxGearBar && gearLevel < maxGear) {
            gearLevel++;
            gearBar = maxGearBar / 2f; // artýþta yarýdan baþlat
        }

        // Gear düþüþü
        while (gearBar <= 0 && gearLevel > 1) {
            gearLevel--;
            gearBar = maxGearBar; // düþüþte bar dolu olsun
        }


        // UI event
        OnGearBarChanged?.Invoke(gearBar / maxGearBar);
    }

    public void OnHitEnemy() {
        gearBar += hitIncrease;

        // Gear artýþý kontrolü
        while (gearBar >= maxGearBar && gearLevel < maxGear) {
            gearLevel++;
            gearBar -= maxGearBar;
        }

        OnGearBarChanged?.Invoke(gearBar / maxGearBar);
        Debug.Log($"Hit Enemy: Gear {gearLevel}, Bar {gearBar}");
    }

    private void OnProjectileBlocked() {
        gearBar += blockIncrease;

        // Gear artýþý kontrolü
        while (gearBar >= maxGearBar && gearLevel < maxGear) {
            gearLevel++;
            gearBar -= maxGearBar;
        }

        OnGearBarChanged?.Invoke(gearBar / maxGearBar);
        Debug.Log($"Block: Gear {gearLevel}, Bar {gearBar}");
    }
}
