using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroidlvl2 : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private SpawnManagerlvl2 _spawnManager;

    public bool startTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerlvl2>();
        transform.position = new Vector3(0, 4, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0f, 0f, .5f);
        transform.Rotate(direction);
    }

    //check for laser collision
    //instantiate explosion at the postion of the asteroid
    //destroy the explosion animation after 3 seconds
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.15f);
            startTimer = true;
            _spawnManager.StartSpawning();
        }
    }
}
