using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.5f;

    // Update is called once per frame
    void Update()
    {
        //translate laser up
        //give it a speed (around 8)
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if(transform.position.y > 8f)
        {
            //destorys the game object connected to this script
            //check if this object has a parent
                //if so destroy parent
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
