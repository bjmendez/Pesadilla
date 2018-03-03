using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectedText : MonoBehaviour, IPointerEnterHandler {

    public Text text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(selectedText());
    }


    IEnumerator selectedText()
    {

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        yield return new WaitForSeconds(0.1f);

        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        yield return new WaitForSeconds(0.4f);

    }

}
