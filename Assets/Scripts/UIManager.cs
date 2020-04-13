using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UI Manager Is NULL");

            return _instance;
        }
    }

    private GameManager _gameManager;

    private int _score, _bestScore;

    [SerializeField]
    private Text _scoreText, _bestScoreText, _gameOverText, _restartText;

    [SerializeField]
    private GameObject _livesDisplayP1, _livesDisplayP2, _pauseMenu;

    [SerializeField]
    private Sprite[] _lives;

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _bestScore = PlayerPrefs.GetInt("best", 0);
        _bestScoreText.text = "best score: " + _bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (_score > _bestScore)
        {
            UpdateBestScore(_score);
        }
    }

    public void UpdateScore(int newScore)
    {
        _score += newScore;
        _scoreText.text = "score: " + _score;
    }

    public void UpdateBestScore(int newBestScore)
    {
        PlayerPrefs.SetInt("best", newBestScore);
        _bestScore = newBestScore;
        _bestScoreText.text = "best score: " + newBestScore;
    }

    public void UpdateLives(int currentLives, bool Player1)
    {
        if(Player1)
        {
            _livesDisplayP1.GetComponent<Image>().sprite = _lives[currentLives];
        }
        else
        {
            _livesDisplayP2.GetComponent<Image>().sprite = _lives[currentLives];
        }
        

    }

    public void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine("RestartTextRoutine");
    }

    public void PauseMenu()
    {
        if (!_pauseMenu.activeSelf)
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void RestartBTN()
    {
        Time.timeScale = 1;
        _gameManager.RestartGame();
    }

    public void QuitBTN()
    {
        Time.timeScale = 1;
        _gameManager.Quit();
    }

    IEnumerator RestartTextRoutine()
    {
        while(_gameManager.Gameover)
        {
            yield return new WaitForSeconds(.75f);
            _restartText.gameObject.SetActive(false);
            yield return new WaitForSeconds(.75f);
            _restartText.gameObject.SetActive(true);
        }
    }
}


