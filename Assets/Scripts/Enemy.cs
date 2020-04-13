using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager _gameManager;
    private PoolManager _poolManager;
    private UIManager _uiManager;

    [SerializeField]
    private float _speed = 3f;
    private float _inspectorSpeed;

    private bool _dead;
    private Animator _anim;
    private AudioSource _audioSource;
    private BoxCollider2D _boxCollider;

    private int _score;

    void Start()
    {
        _gameManager = GameManager.Instance;
        _poolManager = PoolManager.Instance;
        _uiManager = UIManager.Instance;
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.size = new Vector2(2.239966f, 2.849242f);

        _score = _gameManager.enemyScore;
    }

    void OnEnable()
    {
        StartCoroutine("ShootRoutine");
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5.73f)
        {
            transform.position = new Vector3(Random.Range(-8.47f ,8.49f), 5.62f, 0);
        }
    }

    void Shoot()
    {
        GameObject bullet = _poolManager.Request("enemy bullet");
        bullet.transform.position = transform.position + new Vector3(0, -1f, 0);
        bullet.transform.rotation = Quaternion.identity;
        bullet.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser" || other.tag == "Player")
        {
            _audioSource.Play();
            _anim.SetBool("Death", true);
            Destroy(GetComponent<BoxCollider2D>());
            _inspectorSpeed = _speed;
            _speed = 0;

            if (other.transform.tag == "Laser")
            {
                other.gameObject.SetActive(false);
                _uiManager.UpdateScore(_score);
            }

            StopCoroutine("ShootRoutine");
            StartCoroutine("InactiveRoutine");
        }
    }

    IEnumerator InactiveRoutine()
    {
        while(!_dead)
        {
            yield return new WaitForSeconds(0.89f);
            _dead = true;
            gameObject.SetActive(false);
            StopCoroutine(ShootRoutine());
            gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            _anim.SetBool("Death", false);
            _speed = _inspectorSpeed;
        }
        _dead = false;
    }

    IEnumerator ShootRoutine()
    {
        while(!_dead)
        {
            yield return new WaitForSeconds(Random.Range(2f, 3f));
            if (transform.position.y > -1.5f)
            {
                Shoot();
            }
            else
            {
                yield return new WaitForSeconds(1.2f);
                Shoot();
            }
            
        }
    }
}
