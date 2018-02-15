using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	

	private BoardCreator boardScript;

	// Use this for initialization
	void Awake () {


		boardScript = GetComponent<BoardCreator> ();

		InitGame ();
	}


	void InitGame(){
		boardScript.BoardSceneSetUp ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
