using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject winPanel;

    // دالة إظهار شاشة الفوز
    public void ShowWinUI()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f; // إيقاف اللعبة عند الفوز
        }
    }

    // دالة إعادة اللعبة والرجوع للمرحلة الأولى
    public void RestartGame()
    {
        Time.timeScale = 1f; // إرجاع سرعة الوقت للصح
        
        // تحميل المرحلة الأولى برقمها (رقم 0 في Build Settings)
        SceneManager.LoadScene(0);
        
        // أو يمكنك كتابة اسم المرحلة الأولى بين التنصيص إذا أردتِ:
        // SceneManager.LoadScene("Level1");
    }
}