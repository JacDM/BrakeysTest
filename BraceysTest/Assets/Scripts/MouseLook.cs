using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 10f;
    public Transform playerBody;
    float xRotation = 0f;
    public TextMeshProUGUI Sensitivity;
    NewPlayerControls controls;
    Vector2 lookPosition;
    public NewPlayerMovement movementScript;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Awake()
    {
        controls = new NewPlayerControls();
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }   

    void Update()
    {
        controls.Gameplay.Rotation.performed += ctx => lookPosition = ctx.ReadValue<Vector2>();
        float mouseX = lookPosition.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookPosition.y * mouseSensitivity * Time.deltaTime;
        if(movementScript.flipped == true) 
        { 
            mouseX = -mouseX; 
        }
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        
        Sensitivity.text = "Mouse Sensitivity: " + mouseSensitivity;
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }   
}