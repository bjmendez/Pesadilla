using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour {
    private GameObject Player;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player.SendMessage("AddHealth", 10);
            Destroy(gameObject);
        }
    }
}
