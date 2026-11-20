using UnityEngine;
using UnityEngine.SceneManagement; // مهم جداً لإعادة المشهد

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // يتأكد سواء كان الـ Tag هو Player أو اسم المجسم Hero
        if (other.CompareTag("Player") || other.gameObject.name == "Hero")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}