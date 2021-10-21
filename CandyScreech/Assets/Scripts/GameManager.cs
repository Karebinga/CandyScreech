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
    [SerializeField] private TMP_Text clickPowerText;


    public BigDouble ClickPower()
    {
        BigDouble total = 1;
        for (int i = 0; i < data.clickUpgradeLevel.Count; i++)
            total += UpgradesManager.instance.clickUpgradesBasePower[i] * data.clickUpgradeLevel[i];
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
    }


    public void GenerateCandies()
    {
        data.candiesCount += ClickPower();
    }
    
}
