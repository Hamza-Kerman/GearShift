using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    [Header("Saldýrý Ayarlarý")]
    public GameObject yumrukHitbox;
    public float saldiriSuresi = 0.2f;
    public float saldiriBeklemeSuresi = 0.5f;
    public float yumrukMesafesi = 1.0f; // Yumruðun karakterden ne kadar uzakta çýkacaðý

    private bool saldiriYapabilir = true;
    private Vector3 sonHareketYonu = Vector3.right; // Varsayýlan olarak saða baksýn

    void Update()
    {
        // Yön takibi (Karakterin nereye gittiðini anla)
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Eðer karakter hareket ediyorsa son yönü kaydet
        if (x != 0 || y != 0)
        {
            sonHareketYonu = new Vector3(x, y, 0).normalized;
        }

        // Saldýrý Tetikleme (Sol Týk veya F)
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F)) && saldiriYapabilir)
        {
            StartCoroutine(Saldir());
        }
    }

    IEnumerator Saldir()
    {
        saldiriYapabilir = false;

        if (yumrukHitbox != null)
        {
            // 1. Yumruðun yerini güncelle (En son gidilen yöne göre)
            yumrukHitbox.transform.localPosition = sonHareketYonu * yumrukMesafesi;

            // 2. Yumruðu aç
            yumrukHitbox.SetActive(true);
        }

        yield return new WaitForSeconds(saldiriSuresi);

        if (yumrukHitbox != null)
        {
            yumrukHitbox.SetActive(false);
        }

        yield return new WaitForSeconds(saldiriBeklemeSuresi);
        saldiriYapabilir = true;
    }
}