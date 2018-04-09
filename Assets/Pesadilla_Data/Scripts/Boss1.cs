using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss1 : MonoBehaviour {


	public int BossHealth;					// The Boss's Health can be set manually in the inspector
	public int BossAttack = 5;				// The attack this boss can infict on the player
	private bool isBossDead;

	private Transform playerToKillPos;		// The position of the player
	private Animator animator;				// An animator for changes in the bosse's walk 
	private float timeCountStart;
	private float timeCountNow;

	public float moveSpeed;					// Used for setting how fast the Boss walks
	private Vector3 difference;				// Will hold a vector containing the <x, y, z> coordinates
											// of the boss and the player.
	private Vector3 playerPosition;			// Will hold the player's current position in vector format.




	// This method will always run first.
	void Start () {
		
		// Returns the position of the Player
		playerToKillPos = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

		// Sets the animator to use this GameObject's Animator.
		animator = GetComponent<Animator> ();
		animator.SetTrigger ("BossBlendTree");

		isBossDead = false;


	}
		

	Vector3 normalizeVect (Vector3 inputVect){
		/// <summary>
		/// Calculates a unit vector from a given vector contiaining only <x, y, z> coordinates.
		/// </summary>
		/// <param name="inputVect">The vector to be transformed.</param>
		/// <returns>
		/// A unit Vector
		/// </returns>
		/// <remarks>
		/// Becuase the distance can be large from the player and Boss, we don't want the boss
		/// move any faster becuase its further away.
		/// </remarks>

		float sum = (inputVect.x * inputVect.x) + (inputVect.y * inputVect.y) + (inputVect.z * inputVect.z);

		return new Vector3 (inputVect.x/sum, inputVect.y/sum, inputVect.z/sum);
		 

	}


	// Update is called once per frame
	void Update () {
		
		// Will pudate the position of the player once per frame.
		playerToKillPos = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

		// Uses the axes from the playerToKillPos Transform variable.
		playerPosition = new Vector3 (playerToKillPos.position.x, playerToKillPos.position.y, 0.0f);

		// Caluculates and converts the difference to a unit vector. 
		difference = transform.position - playerPosition;

		Debug.Log (BossHealth);

		if (isBossDead) {

			animator.SetTrigger ("BossDead");

		} else {
			

			if (magnitude (difference) > .08) {
			
				difference = normalizeVect (difference);

				// Updates the Bosses potiotion.

				transform.position -= difference * moveSpeed * Time.deltaTime;

				// The SetFloat Method from the animator object dictates what movement the 
				// Boss demonstrates. 
				animator.SetFloat ("Horizontal", -difference.x);
				animator.SetFloat ("Vertical", -difference.y);

			}

		}



	}
	float magnitude (Vector3 inputVector){

		return (inputVector.x * inputVector.x) + (inputVector.y * inputVector.y) + (inputVector.z * inputVector.z);
	
	
	
	}
		
	void OnCollisionEnter2D(Collision2D other){
		/// <summary>
		/// Used when two "Collider" componenets from different GameObjects collide.
		/// </summary>
		/// <param name="other">The Objects that collided with "this" GameObject 
		/// <returns>
		/// void
		/// </returns>
		/// 


		timeCountStart = 0.0f;
		timeCountNow = 0.0f;


		// Debug.Log ("boi");
		if (other.gameObject.name ==  "Player_Explore(Clone)") {
			
			
			// Calls the TakeDamage() method from the other GameObject and 
			// passes 3 as am argument.
			other.gameObject.SendMessage ("TakeDamage", 3);

		}
	}

	void OnCollisionStay2D(Collision2D other){
		/// <summary>
		/// Used when two "Collider" componenets from different GameObjects collide.
		/// </summary>
		/// <param name="other">The Objects that collided with "this" GameObject 
		/// <returns>
		/// void
		/// </returns>
		/// 
		/// 

		timeCountNow += Time.deltaTime;
		// Debug.Log (counting);

		if (other.gameObject.name ==  "Player_Explore(Clone)" && (timeCountNow - timeCountStart) > .5) {
			timeCountStart = timeCountNow;

			// Calls the TakeDamage() method from the other GameObject and 
			// passes 3 as am argument.
			other.gameObject.SendMessage ("TakeDamage", 3);

		}
	}

	/*
	void OnCollisionExit2D(Collision2D other){
		/// <summary>
		/// Used when two "Collider" componenets from different GameObjects collide.
		/// </summary>
		/// <param name="other">The Objects that collided with "this" GameObject 
		/// <returns>
		/// void
		/// </returns>
		/// 
		/// 

		timeCountStart = 0.0f;
		timeCountNow = 0.0f;
	}
	*/
		

	// Used when this Boss is attacked
	void TakeDamage(int damage){
		BossHealth -= damage;
		CheckDead ();
		Debug.Log (BossHealth);
	}

	void CheckDead(){
		if (BossHealth <= 0) {
			isBossDead = true;
			animator.SetTrigger ("BossDied");
			Destroy (this.gameObject);
		}
	}


}
