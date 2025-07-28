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
        Debug.Log("UpdateHPSlider �Լ� ����");
        hpSlider.value = percentage;
    }

    public void UpdateWaveText(int wave)
    {
        Debug.Log($"UpdateWaveText �Լ� ����: {wave}��° ���̺�");
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
        Debug.Log("GetUIState �Լ� ���� Game ������Ʈ");
        return UIState.Game;
    }
}