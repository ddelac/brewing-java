using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerlvl2 : MonoBehaviour
{
    [SerializeField]
    public int kills;
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

    private bool _stopSpawning = false;

    public bool boss = false;

    public int _count = 0;


    public void StartSpawning(){

        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnEnemyRoutine2());
        StartCoroutine(SpawnEnemyRoutine3());
        StartCoroutine(SpawnPowerUpRoutine());
            
    }

    IEnumerator SpawnEnemyRoutine(){

        if(_count+1 >= kills){
            leveldone();
            Debug.Log("Boss fight");
        }
       
        while (_count < kills){
            Debug.Log(_count);
            Vector3 PosToSpawn = new Vector3(Random.Range(-9f, 9f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, PosToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(2, 8));
            if(_count+1 >= kills){
                leveldone();
                Debug.Log("Boss fight");
            }
        }
        yield return new WaitForSeconds(3f);
    }

    IEnumerator SpawnEnemyRoutine2(){

        if(_count+1 >= kills){
            leveldone();
            Debug.Log("Boss fight");
        }
        yield return new WaitForSeconds(5f);
        while (_count < kills){
            Debug.Log(_count);
            Vector3 PosToSpawn2 = new Vector3(Random.Range(-6f, 6f), Random.Range(1f, 5f), 0);
            GameObject newEnemy2 = Instantiate(_enemyPrefab2,PosToSpawn2,Quaternion.identity);
            newEnemy2.transform.parent = _enemyContainer2.transform;
            yield return new WaitForSeconds(5f);
            if(_count+1 >= kills){
                leveldone();
                Debug.Log("Boss fight");
            }
        }
    }

        IEnumerator SpawnEnemyRoutine3(){
        if(_count+2 >= kills){
            leveldone();
            Debug.Log("Boss fight");
        }
        yield return new WaitForSeconds(3f);
        while (_count < kills){
            Debug.Log(_count);
            Vector3 PosToSpawn3 = new Vector3(Random.Range(-12f, -1f), 10, 0);
            GameObject newEnemy3 = Instantiate(_enemyPrefab3, PosToSpawn3, Quaternion.identity);
            newEnemy3.transform.parent = _enemyContainer3.transform;
            yield return new WaitForSeconds(Random.Range(3, 8));
            if(_count+2 >= kills){
                leveldone();
                Debug.Log("Boss fight");
            }
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            Vector3 PosToSpawn = new Vector3(Random.Range(-9f, 9f), 7, 0);
            int randomPowerup = Random.Range(0, 5);
            Instantiate(powerups[randomPowerup], PosToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));


        }
    }

    //stops enemy spawns on player death
    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }

    public void counter(int x){
        _count += x;
    }

    public void leveldone(){
        boss = true;
    }
}
