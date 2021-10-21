using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;

public class UpgradesManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameData data;

    public Upgrades clickUpgrades;

    public BigDouble clickUpgradeBaseCost; 
    public BigDouble clickUpgradeCostMult;

    public string clickUpgradeName;

    public void StartUpgradeManager()
    {
        clickUpgradeName = "Candies per click";
        data = gameManager.data;
        clickUpgradeBaseCost = 10;
        clickUpgradeCostMult = 1.5;
        UpdateClickUpgradeUI();
    }

    private void UpdateClickUpgradeUI()
    {
        clickUpgrades.CostText.text = "Cost: " + Cost().ToString("F0") + " candies";
        clickUpgrades.LevelText.text = data.clickUpgradeLevel.ToString();
        clickUpgrades.NameText.text = "+1 " + clickUpgradeName;
    }

    public BigDouble Cost() => clickUpgradeBaseCost * BigDouble.Pow(clickUpgradeCostMult, data.clickUpgradeLevel);

    public void BuyUpgrade()
    {
        if (data.candiesCount >= Cost())
        {
            data.candiesCount -= Cost();
            data.clickUpgradeLevel += 1;
        }
        UpdateClickUpgradeUI();
    }
}
