using UnityEngine;
using UnityEngine.SceneManagement; // ضروري لاستدعاء التنقل بين المشاهد

public class LevelMove : MonoBehaviour
{
    [SerializeField] private string nextLevelName; // اسم المستوى التالي

    private void OnTriggerEnter2D(Collider2D other)
    {
        // التأكد أن الذي لمس البوابة هو اللاعب
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}