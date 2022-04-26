using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class lvl2bossEntrance : MonoBehaviour{

    [SerializeField]
    private float _speed = 1.0f;
    private bool hasEntered = false;
    private SpawnManagerlvl2 _spawnManager;

    public bool enter = false;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerlvl2>();
        transform.position = new Vector3(0, 10, 0);
    }

    // Update is called once per frame
    void Update(){
        if(_spawnManager.boss == true){
            if(transform.position.y <= 5f){
              hasEntered = true;
            }
            if (!hasEntered){
               enterBoss();
            }
            if (hasEntered){
                enter = true;
                this.enabled = false;
            }
        }
    }
    public void enterBoss()
    {
        Vector3 velocity = new Vector3(0, _speed * Time.deltaTime, 0);
        transform.position -= velocity;
    }
}
