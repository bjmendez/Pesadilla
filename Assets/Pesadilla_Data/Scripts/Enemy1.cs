using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour {

	// How much damage the player takes when attacked
	public int attackDamage;
	public float enemySpeed;
	public float enemyDistance;
	public float aggroDistance;
	public float attackDistance;

	private Animator animator;
	private Rigidbody2D rb2d;
	private Transform target;
	private bool isMoving;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		animator.SetTrigger ("Enemy1FrontIdle");
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		int enemyAnimator = enemyDirection ();
		if (Vector2.Distance (transform.position, target.position) < aggroDistance) {		//If Player is within aggroDistance of enemy
			if (Vector2.Distance (transform.position, target.position) > enemyDistance) {	//If Player is further than the distance the enemy stops moving
				transform.position = Vector2.MoveTowards (transform.position, target.position, enemySpeed * Time.deltaTime);	//Move enemy towards player
				if (enemyAnimator == 1) {
					animator.SetTrigger ("Enemy1WalkLeft");
				} else if (enemyAnimator == 2) {
					animator.SetTrigger ("Enemy1WalkDown");
				} else if (enemyAnimator == 3) {
					animator.SetTrigger ("Enemy1WalkRight");
				} else if (enemyAnimator == 4) {
					animator.SetTrigger ("Enemy1WalkUp");
				}
			}
		}
	}

	//Current direction enemy is moving in so we can set an animator trigger
	int enemyDirection(){
		Vector3 direction = (target.transform.position - transform.position).normalized;
		if (direction.x < 0) {	//If enemy is moving left
			return 1;
		} else if (direction.x > 0) {	//If enemy is moving right
			return 3;
		} else if (direction.y < 0) {	//If enemy is moving down (I think down is negative)
			return 2;
		} else if (direction.y > 0) {	//If enemy is moving up (I think up is positive)
			return 4;
		}
		return 0;
	}
}
