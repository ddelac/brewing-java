using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _PlayerDead;

    [SerializeField]
    private GameObject _BossDead;

    private Level3Manager gm;
    private bool bossDead = false;
    private bool playerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        _PlayerDead.gameObject.SetActive(false);
        _BossDead.gameObject.SetActive(false);
        gm = GameObject.Find("Level3Manager").GetComponent<Level3Manager>();
        if(gm == null)
        {
            Debug.Log("Level3Manager is null");
        }
    }

    public void bossDied()
    {
        bossDead = true;
    }

    public void playerDied()
    {
        playerDead = true;
    }

    public void GameOverSequence()
    {
        if (bossDead)
        {
            gm.GameOver();
            _PlayerDead.SetActive(true);
        }
        if (playerDead)
        {
            gm.GameOver();
            _BossDead.SetActive(true);
        }
    }
}
