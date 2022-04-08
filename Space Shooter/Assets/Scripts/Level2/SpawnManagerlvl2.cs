using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerlvl2 : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]                
    private GameObject _enemyPrefab2;
    [SerializeField]
    private GameObject _enemyPrefab3;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyContainer2;
    [SerializeField]
    private GameObject _enemyContainer3;
    [SerializeField]
    private GameObject[] powerups;
    private Playerlvl2 _Player;
    private bool _stopSpawning = false;

    public void StartSpawning(){
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnEnemyRoutine2());
        StartCoroutine(SpawnEnemyRoutine3());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    //spawn game object every 5 seconds
    //create a coroutine of type IEnumerator -- Yield events
    //while loop -- infinate game loop
    IEnumerator SpawnEnemyRoutine(){
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false){
            Vector3 PosToSpawn = new Vector3(Random.Range(-9f, 9f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, PosToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(2, 8));
        }
    }

    IEnumerator SpawnEnemyRoutine2(){
        yield return new WaitForSeconds(5f);
        while (_stopSpawning == false){
            Vector3 PosToSpawn2 = new Vector3(-20, 10, 0);
            GameObject newEnemy2 = Instantiate(_enemyPrefab2,PosToSpawn2,Quaternion.identity);
            newEnemy2.transform.parent = _enemyContainer2.transform;
            yield return new WaitForSeconds(5f);
        }
    }

        IEnumerator SpawnEnemyRoutine3(){
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false){
            Vector3 PosToSpawn3 = new Vector3(Random.Range(-9f, 9f), 7, 0);
            GameObject newEnemy3 = Instantiate(_enemyPrefab3, PosToSpawn3, Quaternion.identity);
            newEnemy3.transform.parent = _enemyContainer3.transform;
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            Vector3 PosToSpawn = new Vector3(Random.Range(-9f, 9f), 7, 0);
            int randomPowerup = Random.Range(0, 4);
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
