using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private Slider hpSlider;

    private void Start()
    {
        Debug.Log("GameUI: Start");
        UpdateHPSlider(1);
    }
    public void UpdateHPSlider(float percentage)
    {
        Debug.Log("UpdateHPSlider 함수 실행");
        hpSlider.value = percentage;
    }

    public void UpdateWaveText(int wave)
    {
        Debug.Log($"UpdateWaveText 함수 실행: {wave}번째 웨이브");
        waveText.text = wave.ToString();
    }
    public void ChangeWave(int waveIndex)
    {
        UpdateWaveText(waveIndex);
    }

    public void ChangePlayerHP(float currentHP, float maxHP)
    {
        UpdateHPSlider(currentHP / maxHP);
    }
    protected override UIState GetUIState()
    {
        Debug.Log("GetUIState 함수 실행 Game 스테이트");
        return UIState.Game;
    }
}