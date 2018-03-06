using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour {

	//How fast the enemy moves
	public float enemySpeed;
	//How close the enemy gets to the player
	public float stoppingDistance;
	//Distance from player where enemy will retreat
	public float retreatDistance;
	//Distance from player enemy will aggro
	public float aggroDistance;

	private float timeBetweenShots;
	public float startTimeBetweenShots;

	public GameObject projectile;
	private Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		timeBetweenShots = startTimeBetweenShots;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance (transform.position, player.position) < aggroDistance) {
			//Moves enemy towards player
			if (Vector2.Distance (transform.position, player.position) > stoppingDistance) {
				transform.position = Vector2.MoveTowards (transform.position, player.position, enemySpeed * Time.deltaTime);

				//Checks if enemy is near enough to stop moving towards player
			} else if (Vector2.Distance (transform.position, player.position) < stoppingDistance && Vector2.Distance (transform.position, player.position) > retreatDistance) {
				transform.position = this.transform.position;

				//Moves enemy away from player
			} else if (Vector2.Distance (transform.position, player.position) < retreatDistance) {
				transform.position = Vector2.MoveTowards (transform.position, player.position, -enemySpeed * Time.deltaTime);
			}

			if (timeBetweenShots <= 0) {

				Instantiate (projectile, transform.position, Quaternion.identity);
				timeBetweenShots = startTimeBetweenShots;

			} else {
				timeBetweenShots -= Time.deltaTime;
			}
		}
	}
}
