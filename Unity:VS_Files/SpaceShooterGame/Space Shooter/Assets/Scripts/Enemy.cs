using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScrubEnemyMovement();
    }

    void ScrubEnemyMovement()
    {
        //move enemy down 4 m/s
        //when goes off screen
        //respawn at top w/ new random x position
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -4f)
        {
            float xPos = Random.Range(-10, 10);
            transform.position = new Vector3(xPos, 7, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if other is player
        //damage player
        //destroy US
        if(other.tag == "Player")
        {
            //damage player
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            
            Destroy(this.gameObject);
        }

        //if other is laser
        //destroy laser
        //destroy us
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }
}
