using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    private float _timeSinceFire = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        _timeSinceFire += Time.deltaTime;
        if (_timeSinceFire > 3f)
        {
            //destorys the game object connected to this script
            //check if this object has a parent
            //if so destroy parent
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
