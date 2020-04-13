using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager Is NULL");

            return _instance;
        }
    }

    private UIManager _uiManager;

    [SerializeField]
    private int _enemyScore = 10;

    public int enemyScore 
    {
        get
        {
            return _enemyScore;
        }
    }

    [SerializeField]
    private bool _gameOver;
    public bool Gameover 
    {
        get 
        {
            return _gameOver;
        }
    }

    [SerializeField]
    private bool _isCoOp;
    public bool IsCoOp 
    {
        get 
        {
            return _isCoOp;
        }
    }

    [SerializeField]
    private bool _isPlayerDied;
    public bool IsPlayerDied 
    {
        get 
        {
            return _isPlayerDied;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = UIManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameOver && Input.GetKeyDown(KeyCode.R))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uiManager.PauseMenu();
        }
    }

    public void GameOver()
    {
        _gameOver = true;
        _uiManager.GameOver();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Single Player");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void PlayerDied()
    {
        _isPlayerDied = true;
    }
}
