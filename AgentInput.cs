using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AgentInput : MonoBehaviour, IAgentInput
{
    private Camera mainCamera;
    private bool fireButtonDown = false;

    [field: SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPressed { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> OnPointerPositionChange { get; set; }

    [field: SerializeField]
    public UnityEvent OnFireButtonPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnFireButtonReleased { get; set; }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Only move the player if no UI element is being hovered or clicked
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        GetMovmentInput();
        GetPointerInput();
        GetFireInput();
    }

    private void GetFireInput()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if (fireButtonDown == false)
            {
                fireButtonDown = true;
                OnFireButtonPressed?.Invoke();
            }
        }
        else
        {
            if (fireButtonDown)
            {
                fireButtonDown = false;
                OnFireButtonReleased?.Invoke();
            }
        }
    }

    private void GetPointerInput()
    {
        Vector3 mouseInWorldSpace;

        // Check for mouse input
        if (Input.mousePresent)
        {
            // Get mouse position in world space
            mouseInWorldSpace = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            // If controller is present, map right joystick to move the pointer
            float rightStickHorizontal = Input.GetAxis("Horizontal"); // Customize axis names based on your input settings
            float rightStickVertical = Input.GetAxis("Vertical");     // Customize axis names based on your input settings

            // Adjust the joystick input sensitivity and scale it to your needs
            float joystickSensitivity = 0.1f; // Adjust this to control the speed of the pointer

            // Get the current pointer position and apply joystick movement
            Vector3 currentPointerPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Fallback if the mouse starts at a certain position
            currentPointerPosition += new Vector3(rightStickHorizontal * joystickSensitivity, rightStickVertical * joystickSensitivity, 0);

            // Update mouseInWorldSpace with the adjusted joystick input
            mouseInWorldSpace = currentPointerPosition;
        }

        // Ensure the pointer position is updated for both mouse and controller inputs
        OnPointerPositionChange?.Invoke(mouseInWorldSpace);
    }

    private void GetMovmentInput()
    {
        OnMovementKeyPressed?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}
