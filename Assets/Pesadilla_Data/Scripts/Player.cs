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
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			animator.SetTrigger ("playerLeft");

		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			animator.SetTrigger ("playerRight");
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			animator.SetTrigger ("playerBack");
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			animator.SetTrigger ("playerFoward");
		}

		if (Input.GetKeyUp(KeyCode.LeftArrow) && !isMoving)
		{
			animator.SetTrigger ("playerLIdle");
		}
		else if (Input.GetKeyUp(KeyCode.RightArrow) && !isMoving)
		{
			animator.SetTrigger ("playerRIdle");
		}
		else if (Input.GetKeyUp(KeyCode.UpArrow) && !isMoving)
		{
			animator.SetTrigger ("playerBIdle");
		}
		else if (Input.GetKeyUp(KeyCode.DownArrow) && !isMoving)
		{
			animator.SetTrigger ("playerFIdle");
		}


	}






}
