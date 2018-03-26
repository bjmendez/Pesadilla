using UnityEngine;
using System.Collections;
using UnityEngine.UI;	//Allows us to use UI.
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;


	
public class Player : MonoBehaviour
{
	
	public float speed = 1.5f;
	public Transform transform;
	string m_ClipName;
	AnimatorClipInfo[] m_CurrentClipInfo;
	

	private bool isMoving = false;
	private Rigidbody2D rb2d;
	private Animator animator;					//Used to store a reference to the Player's animator component.
	private BoardCreator boardScript;
	private string Direction;
	private int health;
	private bool isAttacking = false;
	
	void Awake (){
		GameObject g = GameObject.Find ("GameManager");
		boardScript = g.GetComponent<BoardCreator> ();
	}
	
	//Start overrides the Start function of MovingObject
	protected void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();

		//Get a component reference to the Player's animator component
		animator = GetComponent<Animator>();

	
	}
	
	
	//This function is called when the behaviour becomes disabled or inactive.

		
	private void FixedUpdate(){

		float moveHorizontal = Input.GetAxisRaw ("Horizontal");

		float moveVertical = Input.GetAxisRaw ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);


		movement.Normalize ();
		m_CurrentClipInfo = animator.GetCurrentAnimatorClipInfo(0);

		m_ClipName = m_CurrentClipInfo[0].clip.name;
		if(m_ClipName == "rightAttack" || m_ClipName == "leftAttack" || m_ClipName == "backAttack" || m_ClipName == "frontAttack"){
			//rb2d.MovePosition (rb2d.position  + Vector2.zero * 0 * Time.deltaTime);
			//Input.ResetInputAxes ();
			return;
		}
		rb2d.MovePosition (rb2d.position + movement * speed * Time.deltaTime);


	

	}

	private void Update ()
	{
		
		if (Input.GetAxisRaw ("Horizontal") == 0 && Input.GetAxisRaw ("Vertical") == 0) {
			isMoving = false;
		} else {
			isMoving = true;
		}
	
	


		if (Input.GetKeyDown(KeyCode.A) && isMoving)
		{
			animator.SetTrigger ("playerLeft");
			Direction = "LEFT";


		}
		else if (Input.GetKeyDown(KeyCode.D) && isMoving)
		{
			animator.SetTrigger ("playerRight");
			Direction = "RIGHT";


		}
		else if (Input.GetKeyDown(KeyCode.W) && isMoving )
		{
			animator.SetTrigger ("playerBack");
			Direction = "BACK";

		}
		else if (Input.GetKeyDown(KeyCode.S) && isMoving)
		{
			animator.SetTrigger ("playerFoward");
			Direction = "FRONT";

		}

		if (Input.GetKeyUp(KeyCode.A) && !isMoving)
		{
			
			animator.SetTrigger ("playerLIdle");
			Direction = "LEFT";


		}
		else if (Input.GetKeyUp(KeyCode.D) && !isMoving)
		{
			animator.SetTrigger ("playerRIdle");
			Direction = "RIGHT";

		}
		else if (Input.GetKeyUp(KeyCode.W) && !isMoving)
		{
			animator.SetTrigger ("playerBIdle");
			Direction = "BACK";


		}
		else if (Input.GetKeyUp(KeyCode.S) && !isMoving)
		{
			animator.SetTrigger ("playerFIdle");
			Direction = "FRONT";
		

		}

		if (Input.GetMouseButtonDown (0)) {
			


			if(Direction == "RIGHT"){
				animator.SetTrigger ("rightAttack");
			}
			if (Direction == "LEFT") {
				animator.SetTrigger ("leftAttack");
			}
			if (Direction == "BACK") {
				animator.SetTrigger ("backAttack");
			}
			if (Direction == "FRONT") {
				animator.SetTrigger ("frontAttack");
			}
		
		}


	}


	
	private void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Exit") {
			//print (scenePaths);

			if (boardScript.GetRoomLength () == 1) {
				SceneManager.LoadSceneAsync ("Main");
				return;
			}
			SceneManager.LoadSceneAsync("Boss");


		}
	}
	



	


}


