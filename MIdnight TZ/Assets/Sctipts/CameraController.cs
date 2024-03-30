using UnityEngine;

public class CameraController : MonoBehaviour
{
    public string carTag = "Car"; // Тег машины, которая будет загружаться из следующей сцены
    public Vector3 offset;
    public float speed;

    private GameObject targetObject;
    private Rigidbody targetRB;

    void LateUpdate()
    {
        if (targetObject == null)
        {
            // Находим машину по тегу при первом кадре после загрузки сцены
            targetObject = GameObject.FindWithTag(carTag);

            // Если машина найдена, получаем её Rigidbody
            if (targetObject != null)
                targetRB = targetObject.GetComponent<Rigidbody>();
        }

        // Проверяем, найдена ли машина
        if (targetObject != null)
        {
            Vector3 targetPosition = targetObject.transform.position + targetObject.transform.TransformVector(offset);

            // Получаем направление движения машины
            Vector3 targetForward = targetRB ? (targetRB.velocity - targetObject.transform.forward).normalized : targetObject.transform.forward;

            // Используем направление движения машины для определения позиции камеры
            Vector3 desiredPosition = targetPosition + targetForward * -5f;

            // Плавное перемещение камеры к целевой позиции
            transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);

            // Направляем камеру на машину
            transform.LookAt(targetObject.transform);
        }
    }
}
