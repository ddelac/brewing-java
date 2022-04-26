using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laserdown : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    // Update is called once per frame
    void Update()
    {
        //translate laser up
        //give it a speed (around 8)
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -6)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }

    }
}
