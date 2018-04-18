using UnityEngine;
using System.Collections;
using UnityEngine.UI;	//Allows us to use UI.
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;



public class Player : MonoBehaviour
{


	public float speed = 1.5f;					// Speed of player
	public Transform transform;					// player object
	string m_ClipName;							// name of current animation
	AnimatorClipInfo[] m_CurrentClipInfo;  		// array of animation information
	public Text levelText;						// Used for denoting current level and boss room
	public static int levelCount = 1;			// counter to hold current level
	public static bool isBoss = false;			// are we currently in a boss room
	public bool isdead = false;


	public static int health = 50;
	public Text healthText;
	public Slider healthBar;
	public AudioClip gotHit;
	public AudioClip attacking;

	private bool isMoving = false;				// is player currently moving 
	private Rigidbody2D rb2d;					//rigidbody attached to player object
	private Animator animator;					//Used to store a reference to the Player's animator component.

	private BoardCreator boardScript;
	private string Direction;
	private bool isAttacking = false;
	private int level_count;
	private Text textforkill;
    private GameObject panel;
	public static int attackDmg = 2;
	public Text txt;

	void Awake (){

		GameObject g = GameObject.Find ("GameManager"); // find the game manager object
        panel = GameObject.Find("Panel");
        panel.SetActive(false);
		boardScript = g.GetComponent<BoardCreator> (); // use to it to get a reference to the current boardcreator

	}

	//Start overrides the Start function of MovingObject
	protected void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();    // get the rigidbody attatched to the player
			



		animator = GetComponent<Animator>(); //Get a component reference to the Player's animator component

		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
		txt = healthText = GameObject.Find("kill kill kill").GetComponent<Text>();
		healthBar.value = health;
		StartCoroutine (delayMessage());

		levelText = GameObject.Find("LevelText").GetComponent<Text>(); // Get the current text that is on the screen

		if(isBoss == false) // if its a boss room
		{
			levelText.text = "Level " + levelCount; // Set the level text
		}
		else
		{
			levelText.text = "BOSS"; // set the level text to BOSS
		}
	}

	void TakeDamage(int damage){ //Method to decrement health
		health -= damage;
		healthBar.value = health;
		if (!isdead) {
			SoundManager.instance.PlaySingle (gotHit);
		}

		//Debug.Log (health);

		CheckDead (); // check if the player is dead after taking damage
	}


	public int GetLevelCount(){
		return levelCount;
	}

	void CheckDead(){ //Check if player is dead
		if (health <= 0) { // if health is less than or equal to 0 than player is dead
			animator.SetTrigger("playerDeath"); // trigger player dying animation
			isdead = true;
			StartCoroutine(Example());

			isBoss = false;
			levelCount = 0;
			health = 50;


		}

	}
	IEnumerator Example() 
	{
		
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("GameOver");

	}
	IEnumerator delayMessage() 
	{

		yield return new WaitForSeconds(5);
		txt.enabled = false;

	}
    IEnumerator delayMessage(GameObject obj)
    {

        yield return new WaitForSeconds(2);
        obj.SetActive(false);

    }


    private void FixedUpdate(){

		float moveHorizontal = Input.GetAxisRaw ("Horizontal"); //Get horizontal input

		float moveVertical = Input.GetAxisRaw ("Vertical"); // get vertical input 

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical); // set a vector equal to horitonzal and vertical movement


		movement.Normalize (); // Normalize movement so its not choppy

		// If the player is in the middle of attacking then don't allow movement
		m_CurrentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
		m_ClipName = m_CurrentClipInfo[0].clip.name;
		if(m_ClipName == "rightAttack" || m_ClipName == "leftAttack" || m_ClipName == "backAttack" || m_ClipName == "frontAttack"){
			//rb2d.MovePosition (rb2d.position  + Vector2.zero * 0 * Time.deltaTime);
			//Input.ResetInputAxes ();
			return;
		}

		//move the player
		if(!isdead){
			rb2d.MovePosition (rb2d.position + movement * speed * Time.deltaTime);
		}





	}

	private void Update ()
	{



		//if game is not currently paused
		if (!PauseMenu.isPaused && !isdead) {


			//Detect if player is currently moving
			if (Input.GetAxisRaw ("Horizontal") == 0 && Input.GetAxisRaw ("Vertical") == 0) {
				isMoving = false;
			} else {
				isMoving = true;
			}



			//If player is moving and 'A' is pressed trigger left walking animation and set direction to left
			if (Input.GetKeyDown (KeyCode.A) && isMoving) {
				animator.SetTrigger ("playerLeft");
				Direction = "LEFT";


			} 
			//If player is moving and 'D' is pressed trigger right walking animation and set direction to right
			else if (Input.GetKeyDown (KeyCode.D) && isMoving) {
				animator.SetTrigger ("playerRight");
				Direction = "RIGHT";


			} 
			//If player is moving and 'W' is pressed trigger back walking animation and set direction to back
			else if (Input.GetKeyDown (KeyCode.W) && isMoving) {
				animator.SetTrigger ("playerBack");
				Direction = "BACK";

			} 
			//If player is movign and 'S' is pressed trigger front walking animation and set direction to front
			else if (Input.GetKeyDown (KeyCode.S) && isMoving) {
				animator.SetTrigger ("playerFoward");
				Direction = "FRONT";

			}

			//If player lets go of 'A' and isn't moving play left idle animation and set direction to left
			if (Input.GetKeyUp (KeyCode.A) && !isMoving) {

				animator.SetTrigger ("playerLIdle");
				Direction = "LEFT";


			} 
			//If player lets go of 'D' and isn't moving play right idle animation and set direction to right
			else if (Input.GetKeyUp (KeyCode.D) && !isMoving) {
				animator.SetTrigger ("playerRIdle");
				Direction = "RIGHT";

			} 
			//If player lets go of 'W' and isn't moving play back idle animation and set direction to back
			else if (Input.GetKeyUp (KeyCode.W) && !isMoving) {
				animator.SetTrigger ("playerBIdle");
				Direction = "BACK";


			} 
			//If player lets go of 'S' and isn't moving player front idle animation and set direction to front
			else if (Input.GetKeyUp (KeyCode.S) && !isMoving) {
				animator.SetTrigger ("playerFIdle");
				Direction = "FRONT";


			}


			//If player pressed left mouse button
			if (Input.GetMouseButtonDown (0)) {

				SoundManager.instance.PlaySingle (attacking);
				//If player is facing right attack in that direction
				if (Direction == "RIGHT") {

					//play right attackign animation
					animator.SetTrigger ("rightAttack");

					//Set that area that is considered to be "hit" which is roughly 2 blocks to right of player
					Vector2 A = new Vector2 (transform.position.x+1, transform.position.y+1);
					Vector2 B = new Vector2 (transform.position.x+1, transform.position.y-1);

					//all enemies in path of weapon take damage
					Collider2D[] hitObjects = Physics2D.OverlapAreaAll (A, B);
					for (int i = 0; i < hitObjects.Length; i++) {
						//tell enemy thats hit to take 2 damage
						hitObjects [i].SendMessage ("TakeDamage", attackDmg, SendMessageOptions.DontRequireReceiver);

					}


				}
				//If player is facing left attack in that direction
				if (Direction == "LEFT") {
					//play left attacking animation
					animator.SetTrigger ("leftAttack");

					//Set that area that is considered to be "hit" which is roughly 2 blocks to left of player
					Vector2 A = new Vector2 (transform.position.x-1, transform.position.y+1);
					Vector2 B = new Vector2 (transform.position.x-1, transform.position.y-1);

					//all enemies in path of weapon take damage
					Collider2D[] hitObjects = Physics2D.OverlapAreaAll (A, B);
					for (int i = 0; i < hitObjects.Length; i++) {
						//tell enemy thats hit to take 2 damage
						hitObjects [i].SendMessage ("TakeDamage", attackDmg, SendMessageOptions.DontRequireReceiver);
					}

				}
				// if player is facing back attack in that direction
				if (Direction == "BACK") {
					//play back attacking animation
					animator.SetTrigger ("backAttack");

					//Set that area that is considered to be "hit" which is roughly 2 blocks infront of player
					Vector2 A = new Vector2 (transform.position.x-1, transform.position.y+1);
					Vector2 B = new Vector2 (transform.position.x+1, transform.position.y+1);

					//all enemies in path of weapon take damage
					Collider2D[] hitObjects = Physics2D.OverlapAreaAll (A, B);
					for (int i = 0; i < hitObjects.Length; i++) {
						//tell enemy thats hit to take 2 damage
						hitObjects [i].SendMessage ("TakeDamage", attackDmg, SendMessageOptions.DontRequireReceiver);
					}

				}
				//if player is facing front attack in that direction
				if (Direction == "FRONT") {
					//play front attackign animation
					animator.SetTrigger ("frontAttack");

					//Set that area that is considered to be "hit" which is roughly 2 blocks behind of player
					Vector2 A = new Vector2 (transform.position.x-1, transform.position.y-1);
					Vector2 B = new Vector2 (transform.position.x+1, transform.position.y-1);


					//all enemies in path of weapon take damage
					Collider2D[] hitObjects = Physics2D.OverlapAreaAll (A, B);
					for (int i = 0; i < hitObjects.Length; i++) {
						//tell enemy thats hit to take 2 damage
						hitObjects [i].SendMessage ("TakeDamage", attackDmg, SendMessageOptions.DontRequireReceiver);
					}

				}



			}
			if(Input.GetKeyDown (KeyCode.G)){
				health = 999;
				attackDmg = 10;
			}


		}
	}


    //Loads levels of the dungeon
	private void OnTriggerEnter2D (Collider2D other){
		//if object collided with is the exit
		if (other.tag == "Exit") {


			//if its the boss room
            if (boardScript.GetRoomLength() == 1)
            {					

				//increment level counter			
				levelCount++;
				
				isBoss = false;

				//destroy pause GUI
                Destroy(GameObject.Find("PauseGUI"));


				//If its the top of the tower load the youwin scene
				if (levelCount == 6) {
					levelCount = 1;
					health =50;
					SceneManager.LoadScene("YouWin");
					return;
				}
				// you beat the boss go to next level
				if(boardScript.GetEnemyCount() == 0){
					SceneManager.LoadScene("Main");

                }
                else
                {
                    panel.SetActive(true);
                    StartCoroutine(delayMessage(panel));
                }

                return;
            }
            else
            {
				//currently boss room
                DontDestroyOnLoad(GameObject.Find("PauseGUI"));
								isBoss = true;
				//load boss room

				if(boardScript.GetEnemyCount() == 0){
					SceneManager.LoadScene("Boss");
				}
                else
                {
                    panel.SetActive(true);
                    StartCoroutine(delayMessage(panel));
                }


            }


        }
    }








}
