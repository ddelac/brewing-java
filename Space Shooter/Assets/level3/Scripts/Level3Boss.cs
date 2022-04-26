using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Boss : MonoBehaviour
{


    [SerializeField]
    private GameObject _4laserAttack;

    [SerializeField]
    private int health = 500;

    [SerializeField]
    private GameObject _explosionEffect;

    private bool hasEntered = false;
    private bool isFinished = true;

    int currentState = 0;

    public HealthBar healthbar;

    private Level3UIManager uim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enterBoss());
        healthbar.setMaxHealth(health);

    }

    private void Update()
    {
        if(hasEntered && isFinished && currentState == 0)
        {
            isFinished = false;
            StartCoroutine(fourLaserAttack());
            currentState = 1;
        }
        if(hasEntered && isFinished && currentState == 1)
        {
            isFinished = false;
            StartCoroutine(missleAttack());
            currentState = 2;
        }
        if(hasEntered && isFinished && currentState == 2)
        {
            isFinished = false;
            StartCoroutine(shooting());
            currentState = 0;
        }
    }


    public void entered()
    {
        hasEntered = true;
    }
    

    IEnumerator fourLaserAttack()
    {
        yield return new WaitForSeconds(3.0f);
        _4laserAttack.SetActive(true);
        boss4LaserAttack b4la = GetComponent<boss4LaserAttack>();
        b4la.enabled = true;
        yield return new WaitForSeconds(24.0f);
        b4la.enabled = false;
        _4laserAttack.SetActive(false);
        isFinished = true;
    }

    IEnumerator enterBoss()
    {
        bossEntrance be = GetComponent<bossEntrance>();
        be.enabled = true;
        yield return new WaitForSeconds(3.0f);
    }

    IEnumerator missleAttack()
    {
        yield return new WaitForSeconds(1.0f);
        bossMissleAttack bma = GetComponent<bossMissleAttack>();
        bma.enabled = true;
        isFinished = true;
    }

    IEnumerator shooting()
    {
        BossShoot bs = GetComponent<BossShoot>();
        bs.enabled = true;
        yield return new WaitForSeconds(30f);
        bs.enabled = false;
        isFinished = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Damage(5);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Missle")
        {
            Damage(15);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Bomb")
        {
            Damage(7);
            Destroy(other.gameObject);
        }
    }

    private void Damage(int d)
    {
        health -= d;
        healthbar.setHealth(health);
        if(health <= 0)
        {
            Instantiate(_explosionEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
