using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolManager : MonoBehaviour
{
    private static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Pool Manager Is NULL");

            return _instance;
        }
    }
    
    [SerializeField]
    private GameObject _bulletsContainer,
                       _bulletPrefab,
                       _enemiesContainer,
                       _enemyPrefab,
                       _enemyBulletsContainer,
                       _enemyBulletPrefab,
                       _powerupsContainer,
                       _powerupPrefab,
                       _tripelBulletsContainer,
                       _tripleBulletsPrefab;

    [SerializeField]
    private int _numberOfBullets,
                _numberOfEnemyBullets,
                _numberOfEnemies,
                _numberOfPowerups,
                _numberOfTripleBullets;

    private List<GameObject> _bulletPool = new List<GameObject>();
    private List<GameObject> _enemyBulletPool = new List<GameObject>();
    private List<GameObject> _enemyPool = new List<GameObject>();
    private List<GameObject> _powerupPool = new List<GameObject>();
    private List<GameObject> _tripleBulletsPool = new List<GameObject>();

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        GenerateBullets();
        GenerateEnemyBullets();
        GenerateEnemies();
        GeneratePowerups();
        GenerateTripleBullets();
    }


    List<GameObject> GenerateBullets()
    {
        for (int i = 1; i <= _numberOfBullets; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.transform.parent = _bulletsContainer.transform;
            bullet.name = "Laser " + (_bulletPool.Count + 1);
            _bulletPool.Add(bullet);
        }

        return _bulletPool;   
    }

    List<GameObject> GenerateEnemyBullets()
    {
        for (int i = 1; i <= _numberOfEnemyBullets; i++)
        {
            GameObject bullet = Instantiate(_enemyBulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.transform.parent = _enemyBulletsContainer.transform;
            bullet.name = "Enemy_Laser " + (_enemyBulletPool.Count + 1);
            _enemyBulletPool.Add(bullet);
        }

        return _enemyBulletPool;   
    }

    List<GameObject> GenerateEnemies()
    {
        for (int i = 1; i <= _numberOfEnemies; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab, Vector3.zero, Quaternion.identity);
            enemy.transform.parent = _enemiesContainer.transform;
            enemy.name = "Enemy " + (_enemyPool.Count + 1);
            _enemyPool.Add(enemy);
        }

        return _enemyPool;   
    }

    List<GameObject> GeneratePowerups()
    {
        for (int i = 1; i <= _numberOfPowerups; i++)
        {
            GameObject powerup = Instantiate(_powerupPrefab, Vector3.zero, Quaternion.identity);
            powerup.transform.parent = _powerupsContainer.transform;
            powerup.name = "Powerup " + (_powerupPool.Count + 1);
            _powerupPool.Add(powerup);
        }

        return _powerupPool;   
    }

    List<GameObject> GenerateTripleBullets()
    {
        for (int i = 1; i <= _numberOfTripleBullets; i++)
        {
            GameObject tripleBullet = Instantiate(_tripleBulletsPrefab, Vector3.zero, Quaternion.identity);
            tripleBullet.transform.parent = _tripelBulletsContainer.transform;
            tripleBullet.name = "Triple Bullet " + (_tripleBulletsPool.Count + 1);
            _tripleBulletsPool.Add(tripleBullet);
        }

        return _tripleBulletsPool;   
    }

    GameObject New(string type)
    {
        if (type == "bullet")
        {
            GameObject bullet = Instantiate(_bulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.transform.parent = _bulletsContainer.transform;
            bullet.name = "Laser " + (_bulletPool.Count + 1);
            _bulletPool.Add(bullet);

            return bullet;
        }
        else if (type == "enemy bullet")
        {
            GameObject bullet = Instantiate(_enemyBulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.transform.parent = _enemyBulletsContainer.transform;
            bullet.name = "Laser " + (_enemyBulletPool.Count + 1);
            _enemyBulletPool.Add(bullet);

            return bullet;
        }
        else if (type == "enemy")
        {
            GameObject enemy = Instantiate(_enemyPrefab, Vector3.zero, Quaternion.identity);
            enemy.transform.parent = _enemiesContainer.transform;
            enemy.name = "Enemy " + (_enemyPool.Count + 1);
            _enemyPool.Add(enemy);

            return enemy;
        }
        else if (type == "powerup")
        {
            GameObject powerup = Instantiate(_powerupPrefab, Vector3.zero, Quaternion.identity);
            powerup.transform.parent = _powerupsContainer.transform;
            powerup.name = "Powerup " + (_powerupPool.Count + 1);
            _powerupPool.Add(powerup);

            return powerup;
        }
        else if (type == "triple bullet")
        {
            GameObject tripleBullet = Instantiate(_tripleBulletsPrefab, Vector3.zero, Quaternion.identity);
            tripleBullet.transform.parent = _tripelBulletsContainer.transform;
            tripleBullet.name = "Triple Bullet " + (_tripleBulletsPool.Count + 1);
            _tripleBulletsPool.Add(tripleBullet);

            return tripleBullet;
        }
        else
        {
            Debug.LogError("New() Type Undefined!");
        }

        return null;
    }

    public GameObject Request(string type)
    {
        if (type == "bullet")
        {
            GameObject availableBullet = _bulletPool.FirstOrDefault(bullet => bullet.activeSelf == false);

            if (availableBullet == null)
                availableBullet = New("bullet");
                
            return availableBullet;
        }
        else if (type == "enemy bullet")
        {
            GameObject availableBullet = _enemyBulletPool.FirstOrDefault(bullet => bullet.activeSelf == false);

            if (availableBullet == null)
                availableBullet = New("enemy bullet");
                
            return availableBullet;
        }
        else if (type == "enemy")
        {
            GameObject availableEnemy = _enemyPool.FirstOrDefault(enemy => enemy.activeSelf == false);

            if (availableEnemy == null)
                availableEnemy = New("powerup");
                
            return availableEnemy;
        }
        else if (type == "powerup")
        {
            GameObject availablePowerup = _powerupPool.FirstOrDefault(powerup => powerup.activeSelf == false);

            if (availablePowerup == null)
                availablePowerup = New("powerup");
                
            return availablePowerup;
        }  
        else if (type == "triple bullet")
        {
            GameObject availableTripleBullet = _tripleBulletsPool.FirstOrDefault(tripleBullet => tripleBullet.activeSelf == false);

            if (availableTripleBullet == null)
                availableTripleBullet = New("triple bullet");
                
            return availableTripleBullet;
        }
        else
        {
            Debug.LogError("Request() Type Undefined!");
        }

        return null;
    }
}
