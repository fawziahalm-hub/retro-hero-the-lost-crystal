using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // مجسم الشخصية
    public Vector3 offset = new Vector3(0, 0, -10); // المسافة بين الكاميرا والشخصية

    void LateUpdate()
    {
        if (target != null)
        {
            // تحريك الكاميرا لموقع الشخصية مع إبقاء المسافة z ثابتة
            transform.position = target.position + offset;
        }
    }
}
