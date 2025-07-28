using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stack_ScoreUI : Stack_BaseUI
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI bestComboText;

    public Button startButton;
    public Button exitButton;

    public override void Init(Stack_UIManager uiManager)
    {
        base.Init(uiManager);




        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    protected override Stack_UIState GetUIState()
    {
        return Stack_UIState.Score;
    }

    public void SetUI(int score, int combo, int bestScore, int bestCombo)
    {
        scoreText.text = score.ToString();
        comboText.text = combo.ToString();
        bestScoreText.text = bestScore.ToString();
        bestComboText.text = bestCombo.ToString();
    }

    void OnClickStartButton()
    {
        uiManager.OnClickStart();
    }

    void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }

}