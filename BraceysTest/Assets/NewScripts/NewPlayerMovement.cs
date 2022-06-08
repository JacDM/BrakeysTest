using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    public static Vector2 inputMovement;
    public static Vector3 movement;
	private Vector3 velocity;

	public Rigidbody rb;
    public LayerMask groundMask;
	public Transform GroundCheck;
	public Animator flipAnimation;
	private NewPlayerControls controls;

	public float speed = 10.0f;
	public float gravity = -9.81f;
	public float maxVelocityChange = 10.0f;
	public float jumpHeight = 2.0f;
	//private float duration = 90f;


	public  bool flipped = false;
	public bool flippingEnabled = true;
	private bool grounded = false;

	void Awake() { controls = new NewPlayerControls(); }
    
    void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Movement.performed += ctx => inputMovement = ctx.ReadValue<Vector2>();
		controls.Gameplay.Jump.performed += ctx => jump();
		if (flippingEnabled == false) { return; }
		controls.Gameplay.FlipGravity.performed += ctx => flip();
		rb.useGravity = false;
	}
    private void Update() 
	{ 
		if (Keyboard.current.xKey.wasPressedThisFrame) { flip(); }
	}

    void FixedUpdate()
	{
		if (grounded)
		{
			// Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(inputMovement.x, 0, inputMovement.y);
            if (flipped) { targetVelocity = new Vector3(-inputMovement.x, 0, inputMovement.y); }
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;

			// Apply a force that attempts to reach our target velocity
			velocity = rb.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rb.AddForce(velocityChange, ForceMode.VelocityChange);
		}
		rb.AddForce(new Vector3(0, gravity * rb.mass, 0));
	}
	void OnCollisionStay() { grounded = true; }
    void OnCollisionExit(){ grounded = false; }

    float CalculateJumpVerticalSpeed()
	{
		// From the jump height and gravity we deduce the upwards speed for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(gravity));
	}
	
	void flip()
    {
		//if (grounded != true) { return; } //Reenable when map is done
		if (!flipped) {
			//Physics.gravity = new Vector3(0f, 19.62f, 0f);
			flipAnimation.SetBool("Flipped", true);
		}
        else
		{
			//Physics.gravity = new Vector3(0f, -19.62f, 0f);
			flipAnimation.SetBool("Flipped", false);
		}
		flipped = !flipped;
		gravity = -gravity;
	}
	void jump()
    {
		if (grounded)
		{
			float verticalSpeed = CalculateJumpVerticalSpeed();
			if(flipped) { verticalSpeed = -verticalSpeed; }
			rb.velocity = new Vector3(velocity.x, verticalSpeed, velocity.z);
		}
	}
	
	private void OnDisable() { controls.Gameplay.Disable(); }
}
