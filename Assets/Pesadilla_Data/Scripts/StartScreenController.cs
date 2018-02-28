using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour {

    public float transitionTime = 0.5f;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
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
