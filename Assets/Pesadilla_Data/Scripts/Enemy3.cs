using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour {

	// How much damage the player takes when attacked
	public int attackDamage;
	public float enemySpeed;
	public float enemyDistance;

	private Animator animator;
	private Rigidbody2D rb2d;
	private Transform target;
	private bool isMoving;
	private Vector3 runPast = new Vector3(5,0,0);

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		if (Vector2.Distance (transform.position, target.position) > enemyDistance - 1) {
			//Enemy runs towards player position while enemy is far enough distance away
			transform.position = Vector2.MoveTowards (transform.position, target.position, enemySpeed * Time.deltaTime);
			}
		else{
			transform.position = Vector2.MoveTowards (transform.position, transform.position + runPast, enemySpeed * Time.deltaTime);
		}
	}
}

//Player position is at (x,y)
//I want enemy to run through (x,y) and end up at (x+z, y+z)
//