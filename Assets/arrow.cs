using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour {
    public float speed;
    public int projectileDamage;

    private Transform player;
    private Vector3 target;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;      //Player's position
        target = new Vector3(player.position.x, player.position.y);     //Target position of our projectile
    }

    // Update is called once per frame
    void Update()
    {
        //Projectile will move towards player position when fired (won't track player movement)
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        
    }
}
