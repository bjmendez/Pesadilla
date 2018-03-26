using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour {

    public float transitionTime = 0.5f;
	
	void Update () {
		if (Input.GetKeyDown("space"))
        {
            StartCoroutine(ToLevel());
        }
	}

    IEnumerator ToLevel()
    {
        yield return new  WaitForSeconds(transitionTime);

        SceneManager.LoadScene("StartMenu");

    }

   
}
