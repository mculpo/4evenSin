// Classe base abstrata para Controladores
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public StateMachine stateMachine;

    [Header("Configurações Gerais")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float gravity = -9.81f;
    [SerializeField] public float jumpHeight = 2f;
    [SerializeField] public float dashDistance = 5f;
    [SerializeField] public float dashTime = 1f;

    public CharacterController characterController;
    public AnimationController animationController;
    public ComboController comboController;
    public Transform cameraTranform;

    public Vector3 velocity;
    public Vector3 lastMoveDirection;

    protected virtual void Awake()
    {
        stateMachine = new StateMachine();
        characterController = GetComponent<CharacterController>();
        animationController = GetComponent<AnimationController>();
        comboController = GetComponent<ComboController>();
        cameraTranform = Camera.main.transform;
    }

    protected virtual void Update()
    {
        stateMachine.Update();
    }

    // Métodos para movimentação e gravidade
    public virtual void ApplyMove(Vector3 direction)
    {
        Vector3 moveDirection = direction.normalized;
        moveDirection.y = 0;

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public virtual void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(new Vector3(0, velocity.y, 0) * Time.deltaTime);
    }

}
