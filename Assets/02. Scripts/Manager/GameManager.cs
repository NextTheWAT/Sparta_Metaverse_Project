using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum Difficulty { Easy, Hard }
    public Difficulty CurrentDifficulty { get; private set; }

    public PlayerController player { get; private set; }
    private ResourceController _playerResourceController;

    [SerializeField] private int currentWaveIndex = 0;

    private EnemyManager enemyManager;
    //private UIManager uiManager;

    private static bool shouldStartGame = false;
    private static int startWaveIndex = 0;

    private void Awake()
    {
        Debug.Log("GameManager Awake");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        player = FindObjectOfType<PlayerController>();
        player.Init(this);

        //uiManager = FindObjectOfType<UIManager>();

        _playerResourceController = player.GetComponent<ResourceController>();
        _playerResourceController.RemoveHealthChangeEvent(UIManager.Instance.gameUI.ChangePlayerHP);
        _playerResourceController.AddHealthChangeEvent(UIManager.Instance.gameUI.ChangePlayerHP);

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);
    }
    public void EasyStartGame()
    {
        CurrentDifficulty = Difficulty.Easy;
        shouldStartGame = true;
        startWaveIndex = 0;
        SceneManager.LoadScene("EasyTopDownScene");
    }

    public void HardStartGame()
    {
        CurrentDifficulty = Difficulty.Hard;
        shouldStartGame = true;
        startWaveIndex = 50;
        SceneManager.LoadScene("HardTopDownScene");
    }

    void StartNextWave()
    {
        Debug.Log("Start Wave");
        currentWaveIndex += 1;
        UIManager.Instance.gameUI.ChangeWave(currentWaveIndex);
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    public void EndOfWave()
    {
        Debug.Log("End of Wave");
        StartNextWave();
    }

    public void GameOver()
    {
        Debug.Log("Game Over함수 실행");
        enemyManager.StopWave();

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "EasyTopDownScene")
            UIManager.Instance.ChangeState(UIState.GameOver);
        else if (currentScene == "HardTopDownScene")
            UIManager.Instance.ChangeState(UIState.HardGameOver);
        else
            Debug.LogWarning("Unknown scene for GameOver: " + currentScene);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[GameManager] 씬 로드됨: {scene.name}");

        if (UIManager.Instance == null)
        {
            Debug.LogWarning("새 씬에서 UIManager를 찾을 수 없음");
            return;
        }

        _playerResourceController = FindObjectOfType<PlayerController>()?.GetComponent<ResourceController>();
        if (_playerResourceController != null)
        {
            _playerResourceController.RemoveHealthChangeEvent(UIManager.Instance.gameUI.ChangePlayerHP);
            _playerResourceController.AddHealthChangeEvent(UIManager.Instance.gameUI.ChangePlayerHP);
        }

        if (shouldStartGame)
        {
            shouldStartGame = false;
            StartCoroutine(DelayedStartGame(startWaveIndex));
        }
    }


    private IEnumerator DelayedStartGame(int startWave)
    {
        yield return new WaitForSeconds(0.1f); // 씬 완전히 초기화될 시간 확보

        player = FindObjectOfType<PlayerController>();
        player?.Init(this);

        enemyManager = FindObjectOfType<EnemyManager>();
        enemyManager?.Init(this);

        Debug.Log($"[GameManager] 게임 시작 - 시작 웨이브: {startWave}");
        currentWaveIndex = startWave;
        UIManager.Instance.SetPlayGame();
        StartNextWave();
    }


}
