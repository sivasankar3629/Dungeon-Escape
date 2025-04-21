using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is Null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion
    public bool HasKeyToCastle { get; set; }

    [Header("Timer")]
    [SerializeField] bool _haveTimer = false;
    [SerializeField] int _time = 1;
    bool _timerRunning = false;

    [Header("General")]
    [SerializeField] GameObject _gameOverUI;
    [SerializeField] TextMeshProUGUI _gameStatusText;
    [SerializeField] PlayerAnimation _playerAnimation;
    [HideInInspector] public bool _gameOver = false;

    private void Update()
    {
        if (_haveTimer && Input.anyKeyDown && !_timerRunning)
        {
            _timerRunning = true;
            UIManager.Instance.StartTimer(_time);
        }
        
        if (UIManager.Instance.gameOver)
        {
            GameLost();
        }
    }

    public void GameWon()
    {
        _gameOver = true;
        StartCoroutine(GameOver("You Won"));
    }

    public void GameLost()
    {
        _gameOver = true;
        _playerAnimation.DeathAnim();
        StartCoroutine(GameOver("You Lost"));
    }

    IEnumerator GameOver(string _gameStatus)
    {
        _gameStatusText.text = _gameStatus;
        yield return new WaitForSeconds(2f);
        _gameOverUI.SetActive(true);
    }
}