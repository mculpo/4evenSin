using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIconDisplay : MonoBehaviour
{
    public Transform worldObject; 
    public Camera mainCamera;

    [Header("Keyboard/Controler variables")]
    [SerializeField] private GameObject keyboard;
    [SerializeField] private GameObject gamepad;

    private RectTransform uiElement; 
    private Canvas canvas;

    private void OnEnable()
    {
        InputManager.instance.OnChanceInputDevice += OnChanceInputDevice;
    }
    private void OnDisable()
    {
        InputManager.instance.OnChanceInputDevice -= OnChanceInputDevice;
    }

    private void OnChanceInputDevice(InputManager.InputType type)
    {
        if(type.Equals(InputManager.InputType.KeyboardMouse))
        {
            keyboard.SetActive(true);
            gamepad.SetActive(false);
        } else
        {
            keyboard.SetActive(false);
            gamepad.SetActive(true);
        }
    }

    void Start()
    {
        uiElement = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (worldObject == null || mainCamera == null) return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldObject.position);

        uiElement.position = screenPos;

        //Debug.Log($"World Pos: {worldObject.position}, Screen Pos: {screenPos}");
    }
}
