using UnityEngine;
using UnityEngine.UI;

public class Stack_HomeUI : Stack_BaseUI
{
    Button startButton;
    Button exitButton;

    protected override Stack_UIState GetUIState()
    {
        return Stack_UIState.Home;
    }

    public override void Init(Stack_UIManager uiManager)
    {
        base.Init(uiManager);

        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
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