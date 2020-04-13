using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    public static SpawnManager Instance 
    {
        get 
        {
            if (_instance == null)
                Debug.LogError("Spawn Manager is NULL");

            return _instance;
        }
    }

    private GameManager _gameManager;
    private PoolManager _poolManager;

    private bool _stopSpawning = false;

    void Awake()
    {
        _instance = this;
    }
    
    void Start()
    {
        _gameManager = GameManager.Instance;
        _poolManager = PoolManager.Instance;

        StartCoroutine("EnemySpawningRoutine");
        StartCoroutine("PowerupSpawningRoutine");
    }

    void Update()
    {
        _stopSpawning = _gameManager.Gameover;
    }

    public void SwitchSpawning(bool value)
    {
        _stopSpawning = value;
    }

    IEnumerator EnemySpawningRoutine()
    {
        yield return new WaitForSeconds(2f);
        while (!_stopSpawning)
        {
            GameObject enemy = _poolManager.Request("enemy");
            Vector3 randomPos = new Vector3(Random.Range(-8.31f, 8.35f), 5.75f, 0);
            enemy.transform.position = randomPos;
            enemy.SetActive(true);
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }

    IEnumerator PowerupSpawningRoutine()
    {
        yield return new WaitForSeconds(2f);
        while(!_stopSpawning)
        {
            GameObject powerup = _poolManager.Request("powerup");
            Vector3 randomPos = new Vector3(Random.Range(-8.31f, 8.35f), 5.75f, 0);
            powerup.transform.position = randomPos;
            powerup.SetActive(true);
            yield return new WaitForSeconds(Random.Range(4f, 10f));
        }
    }
}
