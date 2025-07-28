using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public enum Stack_UIState
{
    Home,
    Game,
    Score,
    Exit,
}

public class Stack_UIManager : MonoBehaviour
{
    static Stack_UIManager instance;
    public static Stack_UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    Stack_UIState currentState = Stack_UIState.Home;

    Stack_HomeUI homeUI = null;

    Stack_GameUI gameUI = null;

    Stack_ScoreUI scoreUI = null;

    TheStack theStack = null;
    private void Awake()
    {
        instance = this;
        theStack = FindObjectOfType<TheStack>();

        homeUI = GetComponentInChildren<Stack_HomeUI>(true);
        homeUI?.Init(this);
        gameUI = GetComponentInChildren<Stack_GameUI>(true);
        gameUI?.Init(this);
        scoreUI = GetComponentInChildren<Stack_ScoreUI>(true);
        scoreUI?.Init(this);

        ChangeState(Stack_UIState.Home);
    }


    public void ChangeState(Stack_UIState state)
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        theStack.Restart();
        ChangeState(Stack_UIState.Game);
    }


    public void OnClickExit()
    {
        ChangeState(Stack_UIState.Home);
        SceneManager.LoadScene("MainScene");
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#else
//        Application.Quit(); // 어플리케이션 종료
//#endif
    }
    public void UpdateScore()
    {
        gameUI.SetUI(theStack.Score, theStack.Combo, theStack.MaxCombo);
    }
    public void SetScoreUI()
    {
        scoreUI.SetUI(theStack.Score, theStack.MaxCombo, theStack.BestScore, theStack.BestCombo);

        ChangeState(Stack_UIState.Score);
    }

}
