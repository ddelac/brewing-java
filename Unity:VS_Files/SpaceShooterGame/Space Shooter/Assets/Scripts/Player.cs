using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.00001f;
    private float _canFire = -.1f;
    [SerializeField]
    private int _lives = 4;
    // Start is called before the first frame update
    void Start(){
        //take the current position and give it a start position(0,0,0)
        transform.position = new Vector3(0, 0, 0);
    }//end start

    // Update is called once per frame
    void Update(){
        //call movement
        movement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            shooting();
        }
            

    }//end update

    void movement(){

        //movement 
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //                  new Vector3(1,0,0) * UsrInput * 3.5 * real time
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        //optimization of above code
        //Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        //transform.Translate(direction * speed * Time.deltaTime);

        //cieling and floor on Y axis 
        if (transform.position.y >= 2){
            transform.position = new Vector3(transform.position.x, 2, 0);
        }
        else if (transform.position.y <= -4){
            transform.position = new Vector3(transform.position.x, -4, 0);
        }//end if else
        //optimization of above code
        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.posotion.y,-4,0),0);
        //Math.Clamp(variable to clamp, value 1, value 2) keeps clamped variable between value 1 and value 2

        //wrapping the X axis' 
        if (transform.position.x > 11){
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11){
            transform.position = new Vector3(11, transform.position.y, 0);
        }//end if ekse

    }//end movement

    void shooting()
    {
        //if i hit the space key
        //spawn game object
        //Debug.Log("Space Key Pressed");
        //Shows message in the terminal if working properly
        _canFire = Time.time + _fireRate;
                                                       //offset for laser to instantiate above player and not in player
        Instantiate(_laserPrefab, transform.position + new Vector3(0,0.8f,0), Quaternion.identity);

       
    }

    public void Damage()
    {
        _lives--;

        //check if we are dead
        if(_lives < 1)
        {
            Destroy(this.gameObject);
        }
    }

}//end class
