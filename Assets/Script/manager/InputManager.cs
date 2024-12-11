using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public enum InputAction { Move, ButtonA, ButtonB, ButtonX, ButtonY }

    private Dictionary<KeyCode, InputAction> inputMap;

    private List<InputAction> actionQueueDown = new List<InputAction>();
    private List<InputAction> actionQueueUp = new List<InputAction>();
    public Vector2 directionalLeftInput { get; private set; }
    public Vector2 directionalRightInput { get; private set; }

    public event Action<InputAction> OnActionTriggeredDown;
    public event Action<InputAction> OnActionTriggeredUp;

    private void Awake()
    {
        Initialize(this);
        InitializeInputMaps();
    }

    void Update()
    {
        actionQueueDown.Clear();
        actionQueueUp.Clear();
        ProcessKeyInput();
        ProcessAxisInput();
        TriggerActions();
    }

    private void InitializeInputMaps()
    {
        /* Config standard inputs
         *
         * Xbox Controller Buttons (Player 1)
            Controller  Button	KeyCode (Windows)	        Description
            A	                KeyCode.Joystick1Button0	A Button (Confirm)
            B	                KeyCode.Joystick1Button1	B Button (Cancel)
            X	                KeyCode.Joystick1Button2	X Button
            Y	                KeyCode.Joystick1Button3	Y Button
            LB (Left Bumper)	KeyCode.Joystick1Button4	Left Bumper
            RB (Right Bumper)	KeyCode.Joystick1Button5	Right Bumper
            Back	            KeyCode.Joystick1Button6	Back Button
            Start	            KeyCode.Joystick1Button7	Start Button
            Left Stick	        KeyCode.Joystick1Button8	Left Stick Click
            Right Stick	        KeyCode.Joystick1Button9	Right Stick Click
         */
        inputMap = new Dictionary<KeyCode, InputAction>
        {
            { KeyCode.Space,            InputAction.ButtonA },
            { KeyCode.Joystick1Button0, InputAction.ButtonA },
            { KeyCode.LeftShift,        InputAction.ButtonB },
            { KeyCode.Joystick1Button1, InputAction.ButtonB },
            { KeyCode.Joystick1Button2, InputAction.ButtonX },
            { KeyCode.Mouse0,           InputAction.ButtonX },
        };
    }

    private void ProcessKeyInput()
    {
        foreach (var key in inputMap.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                actionQueueDown.Add(inputMap[key]);
            }

            if (Input.GetKeyUp(key))
            {
                actionQueueUp.Add(inputMap[key]);
            }
        }
    }

    private void ProcessAxisInput()
    {
        directionalLeftInput = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );

        directionalRightInput = new Vector2(
            Input.GetAxis("Mouse X") + Input.GetAxis("RightStickHorizontal"),
            Input.GetAxis("Mouse Y") + Input.GetAxis("RightStickVertical")
        );
    }

    private void TriggerActions()
    {
        foreach (var action in actionQueueDown)
        {
            OnActionTriggeredDown?.Invoke(action);
        }

        foreach (var action in actionQueueUp)
        {
            OnActionTriggeredUp?.Invoke(action);
        }
    }
}
