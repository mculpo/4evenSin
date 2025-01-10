using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public enum InputType
    {
        KeyboardMouse,
        Gamepad
    }
    public enum InputAction
    {
        Move,               // Movement (using keyboard or joystick)
        ButtonA,            // A Button (Xbox)
        ButtonB,            // B Button (Xbox)
        ButtonX,            // X Button (Xbox)
        ButtonY,            // Y Button (Xbox)
        LeftShoulder,       // LB Button (Xbox)
        RightShoulder,      // RB Button (Xbox)
        LeftTrigger,        // LT Button (Xbox)
        RightTrigger,       // RT Button (Xbox)
        Back,               // Select Button (Xbox)
        Start,              // Start Button (Xbox)
        DPadUp,             // D-Pad Up (Xbox)
        DPadDown,           // D-Pad Down (Xbox)
        DPadLeft,           // D-Pad Left (Xbox)
        DPadRight,          // D-Pad Right (Xbox)
        LeftStickUp,        // Left Stick Y-axis movement (Xbox)
        LeftStickDown,      // Left Stick Y-axis movement (Xbox)
        LeftStickLeft,      // Left Stick X-axis movement (Xbox)
        LeftStickRight,     // Left Stick X-axis movement (Xbox)
        RightStickUp,       // Right Stick Y-axis movement (Xbox)
        RightStickDown,     // Right Stick Y-axis movement (Xbox)
        RightStickLeft,     // Right Stick X-axis movement (Xbox)
        RightStickRight     // Right Stick X-axis movement (Xbox)
    }

    private Dictionary<KeyCode, InputAction> inputMap;

    private List<InputAction> actionQueueDown = new List<InputAction>();
    private List<InputAction> actionQueueUp = new List<InputAction>();
    public Vector2 directionalLeftInput { get; private set; }
    public Vector2 directionalRightInput { get; private set; }

    public event Action<InputAction> OnActionTriggeredDown;
    public event Action<InputAction> OnActionTriggeredUp;
    public event Action<InputType> OnChanceInputDevice;

    public InputType CurrentInputType { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            Initialize(this);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        InitializeInputMaps();
        CurrentInputType = InputType.KeyboardMouse;
    }

    void Update()
    {
        DetectInputDevice();
        actionQueueDown.Clear();
        actionQueueUp.Clear();
        ProcessKeyInput();
        ProcessAxisInput();
        TriggerActions();
    }

    private void DetectInputDevice()
    {
        if (IsGamepadConnected())
        {
            SetInputType(InputType.Gamepad);
        }
        else
        {
            SetInputType(InputType.KeyboardMouse);
        }
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
            // Xbox Joystick Buttons
            { KeyCode.Joystick1Button0, InputAction.ButtonA }, // A on Xbox
            { KeyCode.Joystick1Button1, InputAction.ButtonB }, // B on Xbox
            { KeyCode.Joystick1Button2, InputAction.ButtonX }, // X on Xbox
            { KeyCode.Joystick1Button3, InputAction.ButtonY }, // Y on Xbox

            // Xbox Shoulder Buttons
            { KeyCode.Joystick1Button4, InputAction.LeftShoulder }, // LB on Xbox
            { KeyCode.Joystick1Button5, InputAction.RightShoulder }, // RB on Xbox

            // Xbox Trigger Buttons
            { KeyCode.Joystick1Button6, InputAction.LeftTrigger }, // LT on Xbox
            { KeyCode.Joystick1Button7, InputAction.RightTrigger }, // RT on Xbox

            // Xbox Select and Start Buttons
            { KeyCode.Joystick1Button8, InputAction.Back }, // Select on Xbox
            { KeyCode.Joystick1Button9, InputAction.Start }, // Start on Xbox

            // Xbox D-Pad Buttons
            { KeyCode.Joystick1Button10, InputAction.DPadUp }, // D-Pad Up on Xbox
            { KeyCode.Joystick1Button11, InputAction.DPadDown }, // D-Pad Down on Xbox
            { KeyCode.Joystick1Button12, InputAction.DPadLeft }, // D-Pad Left on Xbox
            { KeyCode.Joystick1Button13, InputAction.DPadRight }, // D-Pad Right on Xbox

            // Keyboard Buttons for actions
            { KeyCode.Space,            InputAction.ButtonA },
            { KeyCode.LeftShift,        InputAction.ButtonB },
            { KeyCode.Mouse0,           InputAction.ButtonX },
            { KeyCode.F,                InputAction.ButtonY },
            { KeyCode.Escape,           InputAction.Start },

            // Movement with keyboard
            { KeyCode.W,                InputAction.Move },
            { KeyCode.A,                InputAction.Move },
            { KeyCode.S,                InputAction.Move },
            { KeyCode.D,                InputAction.Move },
            { KeyCode.UpArrow,          InputAction.Move },
            { KeyCode.LeftArrow,        InputAction.Move },
            { KeyCode.DownArrow,        InputAction.Move },
            { KeyCode.RightArrow,       InputAction.Move },
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
    private bool IsGamepadConnected()
    {
        var joystickNames = Input.GetJoystickNames();
        return joystickNames.Length > 0 && !string.IsNullOrEmpty(joystickNames[0]);
    }
    private void SetInputType(InputType newInputType)
    {
        if (CurrentInputType != newInputType)
        {
            CurrentInputType = newInputType;
            OnChanceInputDevice?.Invoke(CurrentInputType);
            Debug.Log($"Tipo de entrada alterado para: {CurrentInputType}");
        }
    }
}
