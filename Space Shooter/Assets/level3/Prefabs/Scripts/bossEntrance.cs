using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossEntrance : MonoBehaviour
{

    [SerializeField]
    private float _speed = 1.0f;//boss enterance speed

    private bool hasEntered = false;

    // Start is called before the first frame update
    public void Start()
    {
        transform.position = new Vector3(0, 10, 0);
    }

    // Update is called once per frame
    public void Update()
    {
         if(transform.position.y <= 3.0f)
         {
            hasEntered = true;
         }
        if (!hasEntered)
        {
            enterBoss();
        }
        if (hasEntered)
        {
            this.enabled = false;
        }
    }
    public void enterBoss()
    {
        Vector3 velocity = new Vector3(0, _speed * Time.deltaTime, 0);
        transform.position -= velocity;
    }
}
