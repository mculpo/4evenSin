using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform; 
    public float gravity = -9.81f; 
    public float groundCheckOffset = 0.1f; 

    private CharacterController characterController;
    private Vector3 velocity; 
    private bool isGrounded; 

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
        ApplyGravity();
    }

    private void MovePlayer()
    {
        
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        Vector3 moveDirection = cameraTransform.TransformDirection(input);
        moveDirection.y = 0f; 
        moveDirection.Normalize(); 

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    private void ApplyGravity()
    {
        
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}


