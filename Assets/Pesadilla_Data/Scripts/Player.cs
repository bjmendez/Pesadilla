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
	

	private bool isMoving = false;
	private Rigidbody2D rb2d;
	private Animator animator;					//Used to store a reference to the Player's animator component.
	private BoardCreator boardScript;

	
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




		rb2d.MovePosition (rb2d.position + movement * speed * Time.deltaTime);


	

	}

	private void Update ()
	{
		if (Input.GetAxisRaw ("Horizontal") == 0 && Input.GetAxisRaw ("Vertical") == 0) {
			isMoving = false;
		} else {
			isMoving = true;
		}
		if (Input.GetKeyDown(KeyCode.A) )
		{
			animator.SetTrigger ("playerLeft");

		}
		else if (Input.GetKeyDown(KeyCode.D))
		{
			animator.SetTrigger ("playerRight");
		}
		else if (Input.GetKeyDown(KeyCode.W))
		{
			animator.SetTrigger ("playerBack");
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			animator.SetTrigger ("playerFoward");
		}

		if (Input.GetKeyUp(KeyCode.A) && !isMoving)
		{
			animator.SetTrigger ("playerLIdle");
		}
		else if (Input.GetKeyUp(KeyCode.D) && !isMoving)
		{
			animator.SetTrigger ("playerRIdle");
		}
		else if (Input.GetKeyUp(KeyCode.W) && !isMoving)
		{
			animator.SetTrigger ("playerBIdle");
		}
		else if (Input.GetKeyUp(KeyCode.S) && !isMoving)
		{
			animator.SetTrigger ("playerFIdle");
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


