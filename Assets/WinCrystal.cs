using UnityEngine;

public class WinCrystal : MonoBehaviour
{
    [Header("شاشة الفوز")]
    public GameObject winCanvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // يتجاهل أي شيء يلمسه (مثل Ground) إلا إذا كان الـ Tag حقه "Player"
        if (other.CompareTag("Player"))
        {
            if (winCanvas != null)
            {
                winCanvas.SetActive(true);
            }

            Time.timeScale = 0f; // إيقاف اللعبة
            gameObject.SetActive(false); // إخفاء الكريستالة
        }
    }
}