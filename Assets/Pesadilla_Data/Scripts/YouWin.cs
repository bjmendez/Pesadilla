using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour {

	public float transitionTime = 0.5f;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			StartCoroutine(ToStartMenu());
		}
	}

	IEnumerator ToStartMenu()
	{
		yield return new  WaitForSeconds(transitionTime);

		SceneManager.LoadScene("StartMenu");

	}
}
