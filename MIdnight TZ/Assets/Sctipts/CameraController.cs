using UnityEngine;

public class CameraController : MonoBehaviour
{
    public string carTag = "Car"; 
    public Vector3 offset;
    public float speed;

    private GameObject targetObject;
    private Rigidbody targetRB;

    void LateUpdate()
    {
        if (targetObject == null)
        {
            targetObject = GameObject.FindWithTag(carTag);

            if (targetObject != null)
                targetRB = targetObject.GetComponent<Rigidbody>();
        }

        if (targetObject != null)
        {
            Vector3 targetPosition = targetObject.transform.position + targetObject.transform.TransformVector(offset);

            Vector3 targetForward = targetRB ? (targetRB.velocity - targetObject.transform.forward).normalized : targetObject.transform.forward;

            Vector3 desiredPosition = targetPosition + targetForward * -5f;

            transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
            transform.LookAt(targetObject.transform);
        }
    }
}
