using System;
using UnityEngine;

public class HasarVerici : MonoBehaviour
{
    public static event Action OnDamageDeal;
    private void OnTriggerEnter2D(Collider2D digerObje)
    {
        // Eðer çarptýðýmýz þeyin etiketi "Enemy" ise
        if (digerObje.CompareTag("Enemy"))
        {
            // Düþmaný yok et
            Destroy(digerObje.gameObject);
            OnDamageDeal?.Invoke();

            // Ýstersen buraya vuruþ efekti veya ses ekleyebilirsin
            Debug.Log("Düþman vuruldu!");
        }
    }
}