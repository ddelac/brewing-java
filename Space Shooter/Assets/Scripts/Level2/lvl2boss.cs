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
    public GameObject explosionEffect;

    private Playerlvl2 _player;
    private lvl2bossEntrance _entrance;
    public bool invisible = false;
    public bool spawn = false;
    public bool win = false;
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
        _shieldVisualizer.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        if(_entrance.enter == true){
            timer += Time.deltaTime;
            
           if(timer > Random.Range(2f, 4f) ){
               childs();
               new WaitForSeconds(Random.Range(2, 7));
                shield();
                timer = 0;
            }

        }
      
    }

    public void shield(){
        invisible = true;
        _shieldVisualizer.SetActive(true);
        StartCoroutine(shieldPowerDown());
    }

    IEnumerator shieldPowerDown(){
        yield return new WaitForSeconds(5.0f);
        invisible = false;
        _shieldVisualizer.SetActive(false);
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
            if(_entrance.enter == true){
                lives--;
                _player.addScore(100);
                if(lives == 0){
                    Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                    win = true;
                }
            }

        }
        if(other.tag == "TripleShot" && invisible == false){
            if(_entrance.enter == true){
                lives--;
                _player.addScore(100);
                if(lives == 0){
                    Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                    win = true;
                }
            }
        }



    }





}
