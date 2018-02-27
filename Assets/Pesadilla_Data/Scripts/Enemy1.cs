﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour {

	// How much damage the player takes when attacked
	public int attackDamage;
	public float enemySpeed;

	private Animator animator;
	private Rigidbody2D rb2d;
	private Transform target;
	private bool isMoving;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards (transform.position, target.position, enemySpeed * Time.deltaTime);
	}
		
}