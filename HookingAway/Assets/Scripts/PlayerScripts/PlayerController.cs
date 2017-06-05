using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	//Modifiers
    public bool isMovementEnabled = true;
    public bool isJumpingEnabled = true;
	public bool isDoubleJumpEnabled = true;
	public bool isDownForceEnabled = false;

	//Movement speed
    public float horizontalSpeed = 8f;
	private float horizontalSpeedStore;
    public float speedMultiplier;

	//Speed increase modifiers
    public float speedIncreasePoint;
	private float speedIncreasePointStore;
    private float speedPointCount;
	private float speedPointCountStore;

	//Jumping
	private float jumpForce = 300f;
    private float downForce = 600f;
    private Vector2 movement;

	//Collision detection
    public Transform groundCheck;
    public bool grounded = false;
    float groundRadius = 0.1f;
    public LayerMask whatIsGround;

	//Sliding
    public Transform ceilCheck;
    public bool ceiled = false;
    public bool crouching;
	public float slidingTime = 0.8f;

	//Other objects
    private Rigidbody2D playerRigidbody2D;
    private Animator playerAnim;
    private CameraController cameraControl;
	private GameManager theGameManager;
	private ScoreManager theScoreManager;

    private float yVelocity;

	public AudioSource jumpSound;
	public AudioSource deathSound;
	public AudioSource landSound;
	public AudioSource collectibleSound;
	private bool playLandSoundOnce;

	public int scoreToGive;


    void Start()
    {
		//Get the necessary objects from game scene
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        cameraControl = GameObject.Find("MainCamera").GetComponent<CameraController>();
		theGameManager = GameObject.Find("_GM").GetComponent<GameManager>();
		theScoreManager = FindObjectOfType<ScoreManager> ();

		//Store default values to another variable, so when the game is restarted, it set values from them
		horizontalSpeedStore = horizontalSpeed;
		speedPointCountStore = speedPointCount;
		speedIncreasePointStore = speedIncreasePoint;
    }


    void Update()
    {

        if (isMovementEnabled)
        {
            
            if(transform.position.x > speedPointCount)
            {
				//Set the next point where the speed will increase
                speedPointCount += speedIncreasePoint; 

				//Multiply the speed when needed
                horizontalSpeed = horizontalSpeed * speedMultiplier;
                speedIncreasePoint = speedIncreasePoint * speedMultiplier;
            }

			//Set the player movement speed to a variable
			movement = new Vector2(horizontalSpeed, playerRigidbody2D.velocity.y);

            JumpingFunction();
            SlidingFunction();

        }

		//Check if player is grounded or not, then set the variables accordingly
        if(!grounded)
        {
            yVelocity = playerRigidbody2D.velocity.y;
            cameraControl.shouldShake = true;
			playLandSoundOnce = true;
        }

		if (grounded) 
		{
			isDoubleJumpEnabled = true;
			if (playLandSoundOnce) {
				landSound.Play();
				playLandSoundOnce = false;
			}
		}

		//Shake the camera if a certain Y velocity has been reached
        if(grounded && yVelocity <= -14f && cameraControl.shouldShake == true)
        {
            cameraControl.ShakeCamera(0.08f, 0.3f);
        }

    }

	//Set the player physics in the FixedUpdate() function
    void FixedUpdate()
    {

        if (isMovementEnabled)
        {

            //Apply user input to velocity
            playerRigidbody2D.velocity = movement;

            //Check if player is grounded or ceiled
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
            ceiled = Physics2D.OverlapCircle(ceilCheck.position, groundRadius, whatIsGround);

			//Set the crouching animation if the player is crouching
            playerAnim.SetBool("Crouching", crouching);

        }
    }


    float timeStamp;

    void SlidingFunction()
    {
		//Check if player should be or can slide/crouch
		if (grounded && (ceiled || (Input.GetKeyDown(KeyCode.DownArrow) || SwipeManager.Instance.isSwiping(SwipeDirection.Down))))
        {
            crouching = true;
			//Set for how long should the player slide
            timeStamp = Time.time + slidingTime;
        }
        else
        {
            if(timeStamp <= Time.time || !grounded)
            {
                crouching = false;
            }
        }
    }


    void JumpingFunction()
    {
        if (isJumpingEnabled)
        {
			//Check if player can jump or double jump and set the variables accordingly
			if (grounded && (Input.GetKeyDown(KeyCode.Space) || SwipeManager.Instance.isSwiping(SwipeDirection.Up)))
            {
                playerRigidbody2D.AddForce(Vector2.up * jumpForce);
				jumpSound.Play ();
            }

			if (!grounded && isDoubleJumpEnabled && (Input.GetKeyDown(KeyCode.Space) || SwipeManager.Instance.isSwiping(SwipeDirection.Up))) 
			{
				playerRigidbody2D.AddForce(Vector2.up * jumpForce);
				isDoubleJumpEnabled = false;
				jumpSound.Play ();
			}

			//Allow player to push himself downwards
            if (isDownForceEnabled)
            {
				if (!grounded && (Input.GetKeyDown(KeyCode.DownArrow) || SwipeManager.Instance.isSwiping(SwipeDirection.Down)))
                {
                    playerRigidbody2D.AddForce(Vector2.down * downForce);
                }
            }
        }
    }

	void OnTriggerEnter2D (Collider2D other) 
	{
		//Check if player has hit a killbox
		if (other.gameObject.tag == "killbox") {
			cameraControl.ShakeCamera(0.08f, 0.3f);
			deathSound.Play ();
			theGameManager.ToggleDeathMenu();
			//Reset variables to their starting position
			horizontalSpeed = horizontalSpeedStore;
			speedIncreasePoint = speedIncreasePointStore;
			speedPointCount = speedPointCountStore;

		}
		//Check if player has collected a coin
		if (other.gameObject.tag == "Collectible")
		{
			theScoreManager.AddScore(scoreToGive);
			other.gameObject.SetActive (false);
			collectibleSound.Play ();
		}
	}
}
