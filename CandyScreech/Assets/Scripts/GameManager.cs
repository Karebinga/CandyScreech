using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BreakInfinity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() => instance = this;

    public GameData data;

    [SerializeField] private TMP_Text candiesText;
    [SerializeField] private TMP_Text candiesPerSecondText;
    [SerializeField] private TMP_Text clickPowerText;


    public BigDouble ClickPower()
    {
        BigDouble total = 1;
        for (int i = 0; i < data.clickUpgradeLevel.Count; i++)
            total += UpgradesManager.instance.clickUpgradesBasePower[i] * data.clickUpgradeLevel[i];
        return total;
    }

    public BigDouble CandiesPerSecond()
    {
        BigDouble total = 0;
        for (int i = 0; i < data.productionUpgradeLevel.Count; i++)
            total += UpgradesManager.instance.productionUpgradesBasePower[i] * data.productionUpgradeLevel[i];
        return total;
    }

    private void Start()
    {
        data = new GameData();
        UpgradesManager.instance.StartUpgradeManager();
    }

    private void Update()
    {
        candiesText.text = data.candiesCount.ToString("F0");
        clickPowerText.text = "+" + ClickPower() + " candies";
        candiesPerSecondText.text = $"{CandiesPerSecond().ToString("F0")}/s";
        data.candiesCount += CandiesPerSecond()*Time.deltaTime;
    }


    public void GenerateCandies()
    {
        data.candiesCount += ClickPower();
    }
    
}
