using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poweruplvl2 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;//holds the global speed variable in m/s
    private Playerlvl2 _Player;//holds the player game object

    [SerializeField]//0 = Triple Shot, 1 = Speed, 2 = Shields. 3 life, 4 FMJ
    private int powerupId;
    [SerializeField]
    private AudioClip _clip;

    void Start(){
        _Player = GameObject.Find("Player").GetComponent<Playerlvl2>();
        if (_Player != null){
            Debug.LogError("Player Null");
        }
    }

    void Update(){
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

    private void OnTriggerEnter2D(Collider2D other){

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
                    case 3:
                        _Player.addlife();
                        break;
                    case 4:
                        _Player.onFMJCollection();
                        break;

                }//end switch
                
                
                
            }
            
            Destroy(this.gameObject);
        }
    }
    
}
