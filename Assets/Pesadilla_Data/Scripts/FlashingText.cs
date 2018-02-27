using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour {

    private Text startText;

	void Start () {
        startText = GetComponent<Text>(); //Getting title text
        StartCoroutine(startFlashing());
	}
	
    IEnumerator startFlashing()
    {
        //Changing alpha of text to create blinking/flashing text animation
        for (; ; )
        {
            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, 0);
            yield return new WaitForSeconds(0.45f);

            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, 1);
            yield return new WaitForSeconds(0.45f);
        }

    }


}
