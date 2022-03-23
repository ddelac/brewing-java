using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    //spawn game object every 5 seconds
    //create a coroutine of type IEnumerator -- Yield events
    //while loop -- infinate game loop
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            Vector3 PosToSpawn = new Vector3(Random.Range(-9f, 9f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, PosToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }

        
       //yield return new WaitForSeconds(5.0f); // use this to determine the length of the yeild
        //while loop -- inifinate loop
            //Instantiate enemy prefab
            //yield wait for 5 seconds

    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            Vector3 PosToSpawn = new Vector3(Random.Range(-9f, 9f), 7, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], PosToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));

        }
    }

    //stops enemy spawns on player death
    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
