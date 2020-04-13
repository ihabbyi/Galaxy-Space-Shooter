using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    private GameManager _gameManager;
    private UIManager _uiManager;
    private PoolManager _poolManager;
    private SpawnManager _spawnManager;

    [SerializeField]
    private bool Player1, Player2;

    [SerializeField]
    private float _speed, _fireRate, _speedMultiplier;

    private float _nextTime;
    
    private int _livesP1 = 3, _livesP2 = 3;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private GameObject _thruster;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private bool _isTripleShotActive,
                 _isShieldActive,
                 _isSpeedActive;

    void Start()
    {
        if (Player1)
        {
            transform.position = new Vector3(-3.29f, -3.5f, 0);
        }
        else
        {
            transform.position = new Vector3(3.29f, -3.5f, 0);
        }
        _gameManager = GameManager.Instance;
        _uiManager = UIManager.Instance;
        _poolManager = PoolManager.Instance;
        _spawnManager = SpawnManager.Instance;
    }

    void Update()
    {
        if (!_gameManager.Gameover)
        {
            
            Inputs();
            Animations();

            if (Player1 && Input.GetKeyDown(KeyCode.Space) && Time.time > _nextTime)
            {
                _nextTime = Time.time + _fireRate;
                
                Shoot();
            }

            if (Player2 && Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _nextTime)
            {
                _nextTime = Time.time + _fireRate;
                
                Shoot();
            }
        }
        
    }

    void Inputs()
    {
        if (Player1 && _livesP1 > 0)
        {
            int vertical = 0, horizontal = 0;

            if (Input.GetKey(KeyCode.W))
            {
                vertical = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                vertical = -1;
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                vertical = 0;
            }

            if (Input.GetKey(KeyCode.D))
            {
                horizontal = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                horizontal = -1;
            }
            else if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                horizontal = 0;
            }

            Movement(vertical, horizontal);
        }

        if (Player2 && _livesP2 > 0)
        {
            int vertical = 0, horizontal = 0;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                vertical = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                vertical = -1;
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                vertical = 0;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                horizontal = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                horizontal = -1;
            }
            else if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                horizontal = 0;
            }

            Movement(vertical, horizontal);
        }
    }

    void Movement(int vertical, int horizontal)
    {
        Vector3 Movement = new Vector3(horizontal, vertical, 0);

        transform.Translate(Movement * _speed * Time.deltaTime);

        if (transform.position.y >= -1)
        {
            transform.position = new Vector3(transform.position.x, -1, 0);
        }
        else if (transform.position.y <= -4.22f)
        {
            transform.position = new Vector3(transform.position.x, -4.25f, 0);
        }

        if (transform.position.x >= 8.8f)
        {
            transform.position = new Vector3(-8.681849f, transform.position.y, 0);
        }
        else if (transform.position.x <= -8.681849f)
        {
            transform.position = new Vector3(8.8f, transform.position.y, 0);
        }
    }
    
    void Animations()
    {
        Animator anim = GetComponent<Animator>();
        
        if (Player1)
        {
            if (Input.GetKey(KeyCode.D))
            {
                anim.SetBool("Left", false);
                anim.SetBool("Right", true);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                anim.SetBool("Right", false);
                anim.SetBool("Left", true);
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                anim.SetBool("Right", false);
                anim.SetBool("Left", false);
            }
        }

        if (Player2)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetBool("Left", false);
                anim.SetBool("Right", true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("Right", false);
                anim.SetBool("Left", true);
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                anim.SetBool("Right", false);
                anim.SetBool("Left", false);
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = (_isTripleShotActive ?  _poolManager.Request("triple bullet") :  _poolManager.Request("bullet"));
        bullet.SetActive(true);
        bullet.transform.position = transform.position + new Vector3(0, 0.79f, 0);
        bullet.transform.rotation = Quaternion.identity;
    }

    private void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        
        if (Player1)
        {
            _livesP1 -= 1;
            _uiManager.UpdateLives(_livesP1, true);
            DisplayLives(_livesP1);
        }
        else
        {
            _livesP2 -= 1;
            _uiManager.UpdateLives(_livesP2, false);
            DisplayLives(_livesP2);
        }
    }

    void DisplayLives(int lives)
    {
        switch(lives)
        {
            case 2:
                _engines[Random.Range(0, 2)].SetActive(true);
                break;
            case 1:
                _engines.First(engine => !engine.activeSelf).SetActive(true);
                break;
            case 0:
                _explosion.SetActive(true);
                Destroy(GetComponent<BoxCollider2D>());
                Destroy(GetComponent<SpriteRenderer>());
                Destroy(_engines[0]);
                Destroy(_engines[1]);
                Destroy(_thruster);
                StartCoroutine("PlayerDisactivateRoutine");
                
                if(_gameManager.IsCoOp)
                {
                    if (_gameManager.IsPlayerDied)
                    {
                        _gameManager.GameOver();
                    }
                    else
                    {
                        _gameManager.PlayerDied();
                    }
                }
                else
                {
                    _gameManager.GameOver();
                }

                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyLaser" || other.tag == "Enemy")
        {
            Damage();
            if (other.tag == "EnemyLaser")
                other.gameObject.SetActive(false);
        }
        else if (other.tag == "Powerup")
        {
            string Powerup = other.gameObject.GetComponent<Powerup>().powerupSelected;
            other.gameObject.GetComponent<AudioSource>().Play();

            if (Powerup == "Triple_Shot")
            {
                _isTripleShotActive = true;
                StartCoroutine("TripleshotPowerupRoutine");
                
            }
            else if (Powerup == "Shield")
            {
                _isShieldActive = true;
                _shieldVisualizer.SetActive(true);
            }
            else if (Powerup == "Speed")
            {
                _isSpeedActive = true;
                StartCoroutine("SpeedPowerupRoutine");
            }

            other.gameObject.SetActive(false);
        }
    }

    IEnumerator SpeedPowerupRoutine()
    {
            _speed *= _speedMultiplier;
            yield return new WaitForSeconds(5);
            _speed /= _speedMultiplier;
            _isSpeedActive = false;
    }

    IEnumerator TripleshotPowerupRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    IEnumerator PlayerDisactivateRoutine()
    {
        yield return new WaitForSeconds(1.8f);
        gameObject.SetActive(false);
    }
}
