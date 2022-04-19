using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Boss : MonoBehaviour
{


    [SerializeField]
    private GameObject _4laserAttack;

    [SerializeField]
    private GameObject _MissleAttack;
    [SerializeField]
    private GameObject _missleLeft;
    [SerializeField]
    private GameObject _missleRight;

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enterBoss());
        StartCoroutine(fourLaserAttack());
        //StartCoroutine(pauseAfterEntrance());
        //bossMissleAttack();
    }

    

    IEnumerator fourLaserAttack()
    {
        yield return new WaitForSeconds(10.0f);
        boss4LaserAttack b4la = GetComponent<boss4LaserAttack>();
        b4la.enabled = true;
        yield return new WaitForSeconds(20.0f);
        b4la.enabled = false;
        _4laserAttack.SetActive(false);
    }

    IEnumerator enterBoss()
    {
        bossEntrance be = GetComponent<bossEntrance>();
        be.enabled = true;
        yield return new WaitForSeconds(3.0f);
    }

   /* void bossEntrance()
    {
        Vector3 velocity = new Vector3(0, _speed * Time.deltaTime, 0);
        transform.position -= velocity;
    }

    void boss4LaserAttack()
    {
        float z = transform.rotation.eulerAngles.z;
        z += _rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }

    void bossMissleAttack()
    {
        Instantiate(_MissleAttack, _missleLeft.transform.position, Quaternion.identity);//instantiate the missle at the left postion
        Instantiate(_MissleAttack, _missleRight.transform.position, Quaternion.identity);//instantiate the missle at the right position
    }

    IEnumerator pauseAfterEntrance()
    {
        yield return new WaitForSeconds(3.0f);
        _4laserAttack.SetActive(true);
        boss4LaserAttack();
    }*/

    
    
}
