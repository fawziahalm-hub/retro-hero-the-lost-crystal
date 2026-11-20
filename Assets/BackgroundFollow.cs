using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform mainCamera;
    [Range(0f, 1f)]
    public float parallaxSpeed = 0.5f; // سرعة حركة الخلفية (0.5 تعني نصف سرعة الكاميرا)

    private Vector3 lastCameraPosition;

    void Start()
    {
        if (mainCamera != null)
        {
            lastCameraPosition = mainCamera.position;
        }
    }

    void LateUpdate()
    {
        if (mainCamera != null)
        {
            // حساب مقدار حركة الكاميرا في هذه اللحظة
            Vector3 deltaMovement = mainCamera.position - lastCameraPosition;

            // تحريك الخلفية بنسبة من حركة الكاميرا
            transform.position += new Vector3(deltaMovement.x * parallaxSpeed, deltaMovement.y * parallaxSpeed, 0);

            lastCameraPosition = mainCamera.position;
        }
    }
}