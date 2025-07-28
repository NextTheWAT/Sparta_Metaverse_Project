using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Home,
    Game,
    HardGameOver,
    GameOver,
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] public HomeUI homeUI;
    [SerializeField] public GameUI gameUI;
    [SerializeField] public GameOverUI gameOverUI;
    [SerializeField] public HardGameOverUI hardgameOverUI;

    private UIState currentState = UIState.Home;

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        RefreshUIReferences();
        ChangeState(UIState.Home);
    }

    /// <summary>
    /// 각 씬마다 새로 생성된 UI들을 다시 참조하고 초기화
    /// </summary>
    public void RefreshUIReferences()
    {
        homeUI = FindObjectOfType<HomeUI>(true);
        if (homeUI != null) homeUI.Init(this);

        gameUI = FindObjectOfType<GameUI>(true);
        if (gameUI != null) gameUI.Init(this);

        gameOverUI = FindObjectOfType<GameOverUI>(true);
        if (gameOverUI != null) gameOverUI.Init(this);

        hardgameOverUI = FindObjectOfType<HardGameOverUI>(true);
        if (hardgameOverUI != null) hardgameOverUI.Init(this);
    }

    public void SetPlayGame()
    {
        Debug.Log("UIManager: SetPlayGame() 게임 시작");
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        Debug.Log($"UIManager: 현재 UI State: {currentState}");

        // UI가 null일 수 있으니 안전하게 처리
        if (homeUI != null) homeUI.SetActive(currentState);
        if (gameUI != null) gameUI.SetActive(currentState);
        if (gameOverUI != null) gameOverUI.SetActive(currentState);
        if (hardgameOverUI != null) hardgameOverUI.SetActive(currentState);
    }
}
