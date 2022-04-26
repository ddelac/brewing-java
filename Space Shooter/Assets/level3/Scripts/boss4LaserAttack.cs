using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss4LaserAttack : MonoBehaviour
{

    [SerializeField]
    private float _rotationSpeed = 15f;//rotational speed of the boss

    [SerializeField]
    private GameObject _4laserAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LaserAttack();
    }

    void LaserAttack()
    {
        float z = transform.rotation.eulerAngles.z;
        z += _rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }
}
