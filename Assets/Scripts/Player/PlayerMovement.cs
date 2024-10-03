using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	private Vector3 movement;
	private Vector2 movementInput;
	private Animator anim;
	private Rigidbody playerRigidbody;
	private int floorMask;
	private float camRayLength = 100f;
	private Transform myTransform;
	private Camera mainCam;
	private int id_walking = Animator.StringToHash("IsWalking");
	

    private void OnEnable()
    {
		StaticInput.controls.Enable();
		StaticInput.controls.Player.Movement.performed += Movement;
        StaticInput.controls.Player.Movement.canceled += Movement;
    }

    void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
		myTransform = transform;
		mainCam = Camera.main;
	}

	void FixedUpdate()
	{
		//float h = Input.GetAxisRaw("Horizontal");
		//float v = Input.GetAxisRaw("Vertical");

		Move(movementInput.x, movementInput.y);
		Turning();
		Animating(movementInput.x, movementInput.y);
	}

	void Movement(InputAction.CallbackContext dir)
	{
        float h = dir.ReadValue<Vector2>().x;
        float v = dir.ReadValue<Vector2>().y;

        movementInput.x = h;
		movementInput.y = v;

		if (dir.canceled)
		{
			movementInput = Vector2.zero;
		}

    }

	void Move(float h, float v)
	{
		movement.Set(h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition(myTransform.position + movement);
	}

	void Turning()
	{
		Ray camRay = mainCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	void Animating(float h, float v)
	{
		bool walking = h != 0f || v != 0f;

		anim.SetBool(id_walking, walking);
	}
}
