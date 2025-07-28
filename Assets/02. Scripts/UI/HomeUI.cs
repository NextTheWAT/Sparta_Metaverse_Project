using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static NPCState;

public class HomeUI : BaseUI
{
    //[SerializeField] private Button startButton;
    //[SerializeField] private Button exitButton;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        //startButton.onClick.AddListener(OnClickStartButton);
        //exitButton.onClick.AddListener(OnClickExitButton);
    }


    protected override UIState GetUIState()
    {
        return UIState.Home;
    }
}