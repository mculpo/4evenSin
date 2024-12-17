using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera Transform")]
    [SerializeField] private Transform cameraTransform;

    [Header("Move Config")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Gravity Config")]
    [SerializeField] private float gravity = -9.81f;

    [Header("Jump Config")]
    [SerializeField] private float jumpHeight = 2f;

    [Header("Dash Config")]
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashTime = 1f;

    [Header("Stats")]
    public AtributeData baseAtributes;
    public int baseHealth;
    public float baseDefense;
    public float baseStrength;
    public int health;
    public float defense;
    public float strength;
    public float regenTime = 3f;
    public int regeneratedHealthPoints;
    private float regenerationTime;

    private float dashTimeLeft;
    private Vector3 dashDirection;

    private CharacterController characterController;
    private AnimationController animationController;
    private ComboController comboController;
    private Vector3 velocity;
    private Vector3 lastMoveDirection;

    void Awake()
    {
        animationController = GetComponent<AnimationController>();
        characterController = GetComponent<CharacterController>();
        comboController = GetComponent<ComboController>();

        //Apply player stats
        baseHealth = baseAtributes.health;
        baseDefense = baseAtributes.defense;
        baseStrength = baseAtributes.strength;
        health = baseHealth;
        strength = baseStrength;
        defense = baseDefense;
        regenerationTime = regenTime;
    }

    private void OnEnable()
    {
        comboController.OnComboStepExecuted += OnComboStepExecuted;
        InputManager.instance.OnActionTriggeredDown += HandleActionTriggeredDown; 
    }

    private void OnDisable()
    {
        comboController.OnComboStepExecuted -= OnComboStepExecuted;
        InputManager.instance.OnActionTriggeredDown -= HandleActionTriggeredDown;
    }


    private void OnComboStepExecuted(string obj)
    {
        Debug.Log($"Animation name execute: {obj}");
        animationController.PlayAnimation(obj);
    }

    private void HandleActionTriggeredDown(InputManager.InputAction action)
    {
        switch (action)
        {
            case InputManager.InputAction.ButtonA:
                Jump();
                break;

            case InputManager.InputAction.ButtonB:
                Dash();
                break;

            case InputManager.InputAction.ButtonX:
                comboController.TryExecuteLightCombo();
                break;

            default:
                break;
        }
    }


    void Update()
    {
        // Future change because the player will have de Hit too
        // let comboController independent
        if (comboController != null && comboController.IsComboExecuting()) return;

        Vector3 totalMovement = Vector3.zero;

        if (dashTimeLeft > 0)
        {
            ApplyDash(ref totalMovement);
        }
        else
        {
            ApplyMove(ref totalMovement);
        }

        ApplyGravity(ref totalMovement);

        characterController.Move(totalMovement * Time.deltaTime);

        CheckAnimation();
        RegenerateHeath();
    }

    private void CheckAnimation()
    {
        if (!characterController.isGrounded || dashTimeLeft > 0)
            return;

        if (comboController.IsComboExecuting()) return;

        if (InputManager.instance.directionalLeftInput != Vector2.zero)
        {
            //Debug.Log($"Animation run: '{InputManager.instance.directionalLeftInput}'");
            animationController.PlayAnimation("run", 0f);
        }
        else
        {
           // Debug.Log($"Animation idle");
            animationController.PlayAnimation("idle", 0f);
        }
    }

    private void ApplyMove(ref Vector3 totalMovement)
    {
        var inputAxis = InputManager.instance.directionalLeftInput;
        Vector3 moveDirection = cameraTransform.TransformDirection(new Vector3(inputAxis.x, 0, inputAxis.y));
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (inputAxis != Vector2.zero)
        {
            lastMoveDirection = moveDirection;
            totalMovement += moveDirection * moveSpeed; 
        }

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    private void ApplyDash(ref Vector3 totalMovement)
    {
        if (dashTimeLeft > 0)
        {
            totalMovement += dashDirection * (dashDistance / dashTime);

            dashTimeLeft -= Time.deltaTime;

            if (dashTimeLeft <= 0)
            {
                dashTimeLeft = 0;
            }
        }
    }

    private void ApplyGravity(ref Vector3 totalMovement)
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        
        totalMovement.y += velocity.y;
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            animationController.PlayAnimation("jump", 0.05f);
        } 
    }
    private void Dash()
    {
        if (dashTimeLeft == 0)
        {
            animationController.PlayAnimation("dash", 0.05f);

            var inputAxis = InputManager.instance.directionalLeftInput;
            
            dashTimeLeft = dashTime;

            dashDirection = new Vector3(inputAxis.x, 0, inputAxis.y).normalized;

            dashDirection = inputAxis != Vector2.zero
            ? lastMoveDirection 
            : transform.forward.normalized;

            Debug.Log($"Dash Direction: {dashDirection}");
        }
    }

    /// <summary>
    /// Updates the base health and player current health
    /// </summary>
    /// <param name="stat"></param>
    public void UpdateBaseHealth(int stat)
    {
        baseHealth = Mathf.Clamp(stat, baseAtributes.minHealth, baseAtributes.maxHealth);
        health = baseHealth;
    }

    /// <summary>
    /// Updates the base defense and player current defense
    /// </summary>
    /// <param name="stat"></param>
    public void UpdateBaseDefense(float stat)
    {
        baseDefense = Mathf.Clamp(stat, baseAtributes.minDefense, baseAtributes.maxDefense);
        defense = baseDefense;
    }

    /// <summary>
    /// Updates the base strength and current strength
    /// </summary>
    /// <param name="stat"></param>
    public void UpdateBaseStrength(float stat)
    {
        baseStrength = Mathf.Clamp(stat, baseAtributes.minDefense, baseAtributes.maxDefense);
        strength = baseDefense;
    }


    /// <summary>
    /// Updates the health
    /// </summary>
    /// <param name="stat"></param>
    public void UpdateHealth(int stat)
    {
        health = Mathf.Clamp(stat, baseAtributes.minHealth, baseAtributes.maxHealth);
    }

    /// <summary>
    /// Updates the defense
    /// </summary>
    /// <param name="stat"></param>
    public void UpdateDefense(float stat)
    {
        defense = Mathf.Clamp(stat, baseAtributes.minDefense, baseAtributes.maxDefense);
    }

    /// <summary>
    /// Updates the strength
    /// </summary>
    /// <param name="stat"></param>
    public void UpdateStrength(float stat)
    {
        strength = Mathf.Clamp(stat, baseAtributes.minStrength, baseAtributes.maxStrength);
    }

    /// <summary>
    /// Regenerate health
    /// </summary>
    private void RegenerateHeath()
    {
        //Only regenerate if health is low
        if (health < baseHealth)
        {
            if (regenerationTime <= 0)
            {
                regenerationTime = regenTime;
                //Clamp it incase regenerated health points change
                health += Mathf.Clamp(regeneratedHealthPoints, 0, baseHealth);
            }
            else
            {
                regenerationTime -= Time.deltaTime;
            }
        }
    }
}


