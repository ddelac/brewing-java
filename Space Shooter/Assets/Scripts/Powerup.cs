using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;//holds the global speed variable in m/s
    private Player _Player;//holds the player game object

    //IDs for Powerups
    //0 = Triple Shot
    //1 = Speed
    //2 = Shields
    [SerializeField]//0 = Triple Shot, 1 = Speed, 2 = Shields
    private int powerupId;

    //audio clip
    [SerializeField]
    private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3
        //destroy when leave screen
        //check for collision
        powerupMovement();
    }

    void powerupMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }
    //ontriggercollision
    //only collectable by the player(USE TAGS)
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player
        //set istripleshotactive to true
        //destroy US
        if (other.tag == "Player")
        {
       
            //activate powerups depending on the pwoerupId 
            if(_Player != null)
            {
                AudioSource.PlayClipAtPoint(_clip,transform.position);
                switch (powerupId)
                {
                    case 0:
                        _Player.onTripleShotCollection();
                        break;
                    case 1:
                        _Player.onSpeedBoostCollection();
                        break;
                    case 2:
                        _Player.onShieldCollection();
                        break;

                }//end switch
                
                
                
            }
            
            Destroy(this.gameObject);
        }
    }
    
}
