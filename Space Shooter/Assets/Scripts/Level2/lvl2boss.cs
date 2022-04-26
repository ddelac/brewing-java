using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class lvl2boss : MonoBehaviour{
    [SerializeField]
    public int lives;
    [SerializeField]
    private GameObject _child;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private float _speed = 1.0f;
    private Playerlvl2 _player;

    private lvl2bossEntrance _entrance;
    public bool invisible = false;
    public bool spawn = false;
    float timer;
    public GameObject Cube;
    

    void Start(){
        _player = GameObject.Find("Player").GetComponent<Playerlvl2>();
        if(_player == null){
            Debug.LogError("Player is null");
        }
        _entrance = GameObject.Find("BOSS").GetComponent<lvl2bossEntrance>();
        if(_entrance == null){
            Debug.LogError("Entrance is null");
        }
    }

    // Update is called once per frame
    void Update(){
        if(_entrance.enter == true){
            timer += Time.deltaTime;
            if(timer > Random.Range(2f, 4f) ){
                childs();
                //spawn = true;
            }
            //if(timer > 7f){
               // inv();
               // timer = 0;
            //}
            invisible = false;
            //spawn = false;
        }
      
    }

    void childs(){
        Instantiate(_child, transform.position + new Vector3(0, -1.25f, 0), Quaternion.identity);
        Instantiate(_child, transform.position + new Vector3(1, -1.25f, 0), Quaternion.identity);
        Instantiate(_child, transform.position + new Vector3(-1, -1.25f, 0), Quaternion.identity);
    }

    void inv(){
        invisible = true;
        _shieldVisualizer.SetActive(true);
        Debug.LogError(GameObject.Find("BOSS").transform.position);
        if( GameObject.Find("BOSS").transform.position.x == 5){
            int pos = Random.Range(0, 2);
            if(pos == 0){
               // Cube=gameObject.Find("BOSS");
               // Cube.transform.Translate(Vector3(0,10,0));
                
            }
            if(pos == 1){
                //Cube=gameObject.Find("BOSS");
               // Cube.transform.Translate(Vector3(0,10,0));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "FMJ" && invisible == false){
            lives--;

        }
        if(other.tag == "TripleShot" && invisible == false){
            lives--;
        }



    }





}
