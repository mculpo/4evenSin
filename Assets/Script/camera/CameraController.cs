using System;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform target; 
    [SerializeField] private float distance = 5.0f; 
    [SerializeField] private float height = 2.0f; 

    [Header("Position Offsets")]
    [SerializeField] private float offsetX = 0.0f; 
    [SerializeField] private float offsetY = 0.0f; 

    [Header("Rotation Settings")]
    [SerializeField] private float sensitivity = 2.0f; 
    [SerializeField] private float rotationSpeed = 5.0f; 

    [Header("Camera Zoom Settings")]
    [SerializeField] private float minDistance = 2.0f; 
    [SerializeField] private float maxDistance = 10.0f; 

    [Header("Obstacle Detection")]
    [SerializeField] private LayerMask obstructionLayer; 
    [SerializeField] private float raycastDistance = 10.0f; 

    [Header("Pitch Distance Settings")]
    [SerializeField] private Vector2 pitchDistance = new Vector2(-10.0f, 60.0f);

    private float pitch = 0f; 
    private float yaw = 0f; 
    private Quaternion currentRotation;
    private Vector3 currentPosition;
    private Vector3 offset;

    [Header("Position Speed Settings")]
    [SerializeField] private float positionSpeed = 5.0f; 

    private void OnValidate()
    {
        if (target == null) return;
        UpdateCameraPositionAndRotation();
    }

    void Update()
    {
        if (Application.isPlaying && target != null && 
            GameManager.instance.CurrentGameState == GameState.Playing &&
            GameManager.instance.HasPlayerState(PlayerState.Rotation))
        {
            HandleCameraInput();
            UpdateCameraPositionAndRotation();
        }
    }

    private void Start()
    {
        if (!Application.isPlaying)
        {
            currentRotation = transform.rotation;
            currentPosition = transform.position;
        }
    }

    private void HandleCameraInput()
    {
        yaw += InputManager.instance.directionalRightInput.x * sensitivity;  
        pitch -= InputManager.instance.directionalRightInput.y * sensitivity; 

        pitch = Mathf.Clamp(pitch, pitchDistance.x, pitchDistance.y);
    }

    private void UpdateCameraPositionAndRotation()
    {
        {
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            offset = new Vector3(offsetX, height + offsetY, -distance);

            Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0);

            Vector3 targetOffset = targetRotation * offset;

            RaycastHit hit;
            if (Physics.Raycast(target.position, targetOffset.normalized, out hit, raycastDistance, obstructionLayer))
            {

                targetOffset = targetOffset.normalized * Mathf.Clamp(hit.distance, minDistance, maxDistance);
            }

            Vector3 targetPosition = target.position + targetOffset;

            currentRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
            currentPosition = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * positionSpeed);

            transform.position = currentPosition;
            transform.rotation = currentRotation;
        }
    }
}
