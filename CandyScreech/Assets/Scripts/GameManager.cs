using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BreakInfinity;

public class GameManager : MonoBehaviour
{
    public UpgradesManager upgradesManager;
    public GameData data;

    [SerializeField] private TMP_Text candiesText;
    [SerializeField] private TMP_Text clickPowerText;

    private void Start()
    {
        data = new GameData();
        data.candiesCount = 1;
        upgradesManager.StartUpgradeManager();
    }

    private void Update()
    {
        candiesText.text = data.candiesCount.ToString("F0");
        clickPowerText.text = "+" + ClickPower() + " candies";
    }

    public BigDouble ClickPower() => 1 + data.clickUpgradeLevel;

    public void GenerateCandies()
    {
        data.candiesCount += ClickPower();
    }
    
}
