using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy3 : MonoBehaviour
{
    public enum FollowType{
        MoveTowards,
        Lerp
    }

    public FollowType Type = FollowType.MoveTowards;


    [SerializeField]
    private GameObject _laserPrefab;
    private Playerlvl2 _player;
    [SerializeField]
    public GameObject explosionEffect;
    private AudioSource _audioSource;

    public xmov Path;
    public float Speed = 1;
    public float MaxDistanceToGoal = .2f;
    [SerializeField]
    private float _fireRate = 0.5f;//holds the fire rate of the laser
    private float _canFire = -5f;


    private IEnumerator<Transform> _currentPoint;

    // Start is called before the first frame update
    void Start(){

        _player = GameObject.Find("Player").GetComponent<Playerlvl2>();//assign player component to access player methods
        if(_player == null){
            Debug.LogError("Player is null");
        }
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null){
            Debug.LogError("Audio Source on enemy is NULL");
        }
        if(Path == null){
            Debug.LogError("path null", gameObject);
        }

        _currentPoint = Path.GetPathEnumerator();
        _currentPoint.MoveNext();

        if(_currentPoint.Current == null){
            return;
        }

        transform.position = _currentPoint.Current.position;
    }

    // Update is called once per frame
    void Update(){

        if(_currentPoint == null || _currentPoint.Current == null){
            return;
        }
        if(Type == FollowType.MoveTowards){
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
        }else if(Type == FollowType.Lerp){
            transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
        }
        var disatnceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if(disatnceSquared < MaxDistanceToGoal * MaxDistanceToGoal){
            _currentPoint.MoveNext();
        }

        StartCoroutine(shooting());
    }


    IEnumerator shooting(){
        yield return new WaitForSeconds(1f);
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, -1.25f, 0), Quaternion.identity);
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        //if other is player
        //damage player
        //destroy US
        if(other.tag == "Player"){
            //damage player
            Playerlvl2 player = other.transform.GetComponent<Playerlvl2>();
            if(player != null)
            {
                player.Damage();
            }

            //destroy collider
            Destroy(GetComponent<Collider2D>());
            //trigger anim
            //_anim.SetTrigger("OnEnemyDeath");
            //_speed = 0;
            //_audioSource.Play();
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            //Destroy(this.gameObject,2.5f);
            
        }

        //if other is laser
        //destroy laser
        //destroy us
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);//destroy laser
            if(_player != null)
            {
                _player.addScore(20);
            }

            //destroy collider
            Destroy(GetComponent<Collider2D>());
            //trigger anim
            //_anim.SetTrigger("OnEnemyDeath");
            //_speed = 0;
            //_audioSource.Play();
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            //Destroy(this.gameObject,2.5f);//destroy enemy

        }

    }

}
