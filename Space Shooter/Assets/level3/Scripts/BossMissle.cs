using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossMissle : MonoBehaviour
{

    
    private float _speed = 2.5f;

    
    private GameObject target;

    [SerializeField]
    private GameObject _explosionEffect;

    private float _rotateSpeed = 200f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Level3_Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = rb.position - (Vector2)target.transform.position;

        direction.Normalize();//normalize to 1 without changing the direction of the vector 2

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        if(rotateAmount > 0)
        {
            rb.angularVelocity =  _rotateSpeed;
        }
        else if(rotateAmount < 0)
        {
            rb.angularVelocity = -_rotateSpeed;
        }
        else
        {
            _rotateSpeed = 0;
        }

        

        rb.velocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet")
        {
            Instantiate(_explosionEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }else if(other.tag == "Missle")
        {
            Instantiate(_explosionEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        else if(other.tag == "Bomb")
        {
            Instantiate(_explosionEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
