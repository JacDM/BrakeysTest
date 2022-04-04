using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    NewPlayerControls controls;
    Vector2 move;

    public CharacterController Controller;
    public float speed = 12f; 
    Vector3 velocity;
    public float gravity = -9.81f;
    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight=3f;
    public float CrouhHeight=2f; 
    public float SprintSpeed = 20f;
    public Animator animator;
    public float NewDefault=12f;
 
    

    //public TextMeshProUGUI textheight;
    //public TextMeshProUGUI textspeed;

    void Awake()
    {
        controls = new NewPlayerControls();
        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable(){ controls.Gameplay.Disable(); }
    // Update is called once per frame
    void Update()
    {

        var gamepad = Gamepad.current;
        /*textheight.text = "Jump Height: " + jumpHeight;
        textspeed.text = "Speed: "+speed;*/
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        


        float x = move.x;
        float z = move.y;
        Vector3 move3 = transform.right * x + transform.forward * z;

        Controller.Move(move3*speed*Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        Controller.Move(velocity * Time.deltaTime);
        
    }
    //Functions
    void Jump()
    {
        
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

}