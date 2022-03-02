using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn game object every 5 seconds
    //create a coroutine of type IEnumerator -- Yield events
    //while loop -- infinate game loop
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Vector3 PosToSpawn = new Vector3(Random.Range(-9f, 9f), 7, 0);
            Instantiate(_enemyPrefab, PosToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }

        //yield return null; wait 1 frame

        //then call this line
       //yield return new WaitForSeconds(5.0f); // use this to determine the length of the yeild
        //while loop -- inifinate loop
            //Instantiate enemy prefab
            //yield wait for 5 seconds

    }
}
