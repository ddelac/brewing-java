using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMissleAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject _MissleAttack;
    [SerializeField]
    private GameObject _missleLeft;
    [SerializeField]
    private GameObject _missleRight;

    // Start is called before the first frame update
    void Start()
    { 
        Instantiate(_MissleAttack, _missleLeft.transform.position, Quaternion.identity);//instantiate the missle at the left postion
        Instantiate(_MissleAttack, _missleRight.transform.position, Quaternion.identity);//instantiate the missle at the right position
        this.enabled = false;
    }
}
