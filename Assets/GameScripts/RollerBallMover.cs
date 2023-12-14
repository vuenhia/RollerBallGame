using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InventoryCollector))]

public class RollerBallMover : MonoBehaviour {

	private Camera playerCamera;

	[SerializeField]
	private int maxMoveForce = 0;

    [SerializeField]
    private int maxRotationalSpeed = 0;
    private float minMoveForce;
	private float moveForce;

	[SerializeField]
	private float jumpForce = 0;
	private bool grounded;

	[SerializeField]
	private float boostMultiplier;
	[SerializeField]
	private float boostDuration;
	private float boostTimer;

	private Rigidbody rb;

	private Vector3 forceVec;
	private float sideways;
	private float forwardback;

    private RollerBallMover passTo;
	private RollerBallMover passFrom;
	private float passBackDelay;

	// To help students during development and testing their game or recording their videos floatMode can be
	// used to adjust the roller ball position mid game.
	private bool floatMode;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		grounded = true;
		minMoveForce = 0.1f;
		moveForce = minMoveForce;
		rb.maxAngularVelocity = maxRotationalSpeed;
		floatMode = false;
	}
	
	// Update is called once per frame
	// When reading player/movement inputs make sure to use Update() and not FixedUpdate() as FixUpdate may not always catch
	// the key events given its slower run speed. It often misses the GetButton___() events
	void Update () {
		if (Input.GetButtonUp("FloatMode"))
		{
			// To help students during development and testing their game or recording their videos floatMode can be
			// used to adjust the roller ball position mid game.
			floatMode = !floatMode;
			rb.useGravity = !floatMode;
			rb.isKinematic = floatMode;
			Debug.Log("FloatMode: " + floatMode);
		}
		jumpRoller();
		addRollerForce();
	}

	private void addRollerForce()
	{
		// Read the inputs to get new direction
		sideways = -Input.GetAxis("MoveSideways");
		forwardback = Input.GetAxis("MoveForward");
		 
		if (Mathf.Abs(sideways) > 0.01f || Mathf.Abs(forwardback) > 0.01f)
		{
			// Get the angle of the player camera so we can adjust the forward and sideway directions of the intended movements
			forceVec = new Vector3(playerCamera.transform.forward.x, 0.0f, playerCamera.transform.forward.z).normalized;

			// Create a vector to hold the direction and magnitude of the desired new direction
			// By cross multiplying we can flip x and z gradually as the player rotates the view
			forceVec = new Vector3(
				forwardback * forceVec.z + (sideways * forceVec.x),
				0.0f,
				sideways * forceVec.z + (-forwardback * forceVec.x));

			// Add acceleration
			if (moveForce < maxMoveForce)
			{
				moveForce = (moveForce * 2f);
			}

			moveForce = Mathf.Clamp(moveForce, minMoveForce, maxMoveForce);

			if (Input.GetButton("RollerBoost"))
			{
				if (boostTimer > 0.0f)
				{
					moveForce = moveForce * boostMultiplier;
					boostTimer -= Time.deltaTime;
				}
			} else
			{
				if (boostTimer < boostDuration)
				{
					boostTimer += Time.deltaTime;
				}
			}

			if (floatMode)
			{
				// To help students during development and testing their game or recording their videos floatMode can be
				// used to adjust the roller ball position mid game.
				moveForce = (Input.GetButton("RollerBoost")?9:3);
				rb.gameObject.transform.Translate(new Vector3(-moveForce * sideways * Time.deltaTime, 0.0f, moveForce * forwardback * Time.deltaTime), playerCamera.transform);
				return;
			}

			// The movement vector is normalized, meaning values from -1 to +1. Multiply by our desired speed
			// Apply a force to the roller ball and let the physics system handle the movement
			rb.AddTorque(forceVec * moveForce * Time.deltaTime, ForceMode.Force);
		}
		else
		{
			if (moveForce > minMoveForce)
			{
				moveForce = (moveForce / 4f);
				moveForce = Mathf.Clamp(moveForce, minMoveForce, maxMoveForce);
			}
		}
	}

	private void jumpRoller()
	{
		if (floatMode)
		{
			// To help students during development and testing their game or recording their videos floatMode can be
			// used to adjust the roller ball position mid game.
			rb.gameObject.transform.Translate(new Vector3(0.0f, jumpForce/6.0f * Input.GetAxis("FloatMove") * Time.deltaTime, 0.0f), playerCamera.transform);
			return;
		}

		// Check if the roller is on the ground, we don't want to allow double jumping
		// Then check if the player wants to jump
		if (grounded && Input.GetAxis("Jump") > 0.1f)
		{
			// Add a Jump force in the Y axis (vertical) to the roller ball
			rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f));
			// Mark as not grounded, which will be reset once we hit the ground again
			grounded = false;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		// Checking the normalized vector should ensure the collision
		// is near the bottom of the roller and not the side or top
		if (collision.impulse.normalized.y > 0.5)
		{
			grounded = true;
		}
		//Debug.Log(collision.impulse.normalized.y);
        if (this.enabled && collision.gameObject.GetComponent<RollerBallMover>())
        {
            // Hit another roller ball, pass control to swap rollers
            // Debug.Log("HitRoller: " + this.enabled + collision.gameObject.name);
            passTo = collision.gameObject.GetComponent<RollerBallMover>();
			// To avoid instant pass back due to bouncing or speed add delay for 
			// returning to the roller we just left
			if (passTo == passFrom && Time.time < passBackDelay)
			{
				passTo = null;
				return;
			}
            StartCoroutine("passControls");
        }
	}

	public void setMaxMoveForce(float newForce)
	{
		moveForce = newForce;
	}

    public void setCamera(Camera newCam)
    {
        playerCamera = newCam;
    }

    public void setVelocity(Vector3 transVel, Vector3 angVel, RollerBallMover fromRoller)
    {
        if (rb == null) {
            Start();
        }
        rb.velocity = transVel;
        rb.angularVelocity = angVel;

		passFrom = fromRoller;
		// Set time in near future of when we can return to the previous roller
		passBackDelay = Time.time + 0.25f;
    }

    private IEnumerator passControls()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        this.enabled = false;
        passTo.setVelocity(rb.velocity, rb.angularVelocity, this);
		rb.velocity = new Vector3();
		rb.angularVelocity = new Vector3();
		passTo.enabled = true;

		if (passTo.GetComponent<InventoryCollector>().getPlayerData() == null)
		{
			passTo.GetComponent<InventoryCollector>().setPlayerData(gameObject.GetComponent<InventoryCollector>().getPlayerData());
		}
		passTo = null;
	}

	public float getBoostPercent()
	{
		return Mathf.Clamp(boostTimer / boostDuration, 0f, 1f);
	}
}
