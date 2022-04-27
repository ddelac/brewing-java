using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl2bossMovement : MonoBehaviour{

    [SerializeField]
    private float _speed = 1.0f;
    private lvl2bossEntrance _entrance;
    private float widthOrtho;
    private int state = 0;

    void Start(){
        _entrance = GameObject.Find("BOSS").GetComponent<lvl2bossEntrance>();
        if(_entrance == null){
            Debug.LogError("Entrance is null");
        }

        float screenRatio = (float)Screen.width / (float)Screen.height;
        widthOrtho = Camera.main.orthographicSize * screenRatio;
        
    }

    void Update(){
        if(_entrance.enter == true){
            movement(widthOrtho);
        }
        
    }

void movement(float w)
    {
        if (state == 0)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }

        if (state == 1)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        if (transform.position.x > w)
        {
            state = 1;
        }

        if (transform.position.x < -w)
        {
            state = 0;
        }
    }
}
