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



	//Start overrides the Start function of MovingObject
	protected void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();

	}


	//This function is called when the behaviour becomes disabled or inactive.


	private void FixedUpdate(){

		float moveHorizontal = Input.GetAxisRaw ("Horizontal");

		float moveVertical = Input.GetAxisRaw ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);




		rb2d.MovePosition (rb2d.position + movement * speed * Time.deltaTime);




	}

	







}
