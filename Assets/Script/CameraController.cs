using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; 
    public float distance = 5.0f; 
    public float height = 2.0f; 
    public float rotationSpeed = 5.0f; 
    public float sensitivity = 2.0f; 
    public Vector2 pitchLimits = new Vector2(-40f, 60f); 

    private float pitch = 0f; 
    private float yaw = 0f; 

    void Update()
    {
        HandleInput();
        UpdateCameraPosition();
    }

    private void HandleInput()
    {
        
        float horizontalInput = Input.GetAxis("Mouse X") + Input.GetAxis("RightStickHorizontal");
        float verticalInput = Input.GetAxis("Mouse Y") + Input.GetAxis("RightStickVertical");

        yaw += horizontalInput * sensitivity;
        pitch -= verticalInput * sensitivity;

        pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);
    }

    private void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 positionOffset = rotation * new Vector3(0, height, -distance);

        transform.position = target.position + positionOffset;
        transform.LookAt(target);
    }
}
