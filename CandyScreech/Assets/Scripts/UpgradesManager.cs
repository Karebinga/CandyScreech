using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    private void Awake() => instance = this;

    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradesPrefab;

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;

    public Image Child;
    public GameObject[] childCostumes;
    public Image head;
    public Canvas gameCanvas;

    public string[] clickUpgradeNames;

    public BigDouble[] clickUpgradeBaseCost; 
    public BigDouble[] clickUpgradeCostMult;
    public BigDouble[] clickUpgradesBasePower;

    public List<Upgrades> productionUpgrades;
    public Upgrades productionUpgradesPrefab;

    public ScrollRect productionUpgradesScroll;
    public Transform productionUpgradesPanel;

    public string[] productionUpgradeNames;

    public BigDouble[] productionUpgradeBaseCost;
    public BigDouble[] productionUpgradeCostMult;
    public BigDouble[] productionUpgradesBasePower;

    public GameObject[] Children;

    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(GameManager.instance.data.clickUpgradeLevel, 1);
        Methods.UpgradeCheck(GameManager.instance.data.productionUpgradeLevel, 5);

        clickUpgradeNames = new[] { "Outfit Upgrade" };
        productionUpgradeNames = new[] { "John", "John2", "John3", "John4", "John5" };

        clickUpgradeBaseCost = new BigDouble[] { 10 };
        clickUpgradeCostMult = new BigDouble[] { 1.25 };
        clickUpgradesBasePower = new BigDouble[] { 1 };

        productionUpgradeBaseCost = new BigDouble[] { 25, 50, 75, 100, 125 };
        productionUpgradeCostMult = new BigDouble[] { 1.5, 1.75, 2, 2.5, 3 };
        productionUpgradesBasePower = new BigDouble[] { 1, 2, 5, 10, 15 };

        for (int i = 0; i < GameManager.instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradesPrefab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            clickUpgrades.Add(upgrade);
        }

        for (int i = 0; i < GameManager.instance.data.productionUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(productionUpgradesPrefab, productionUpgradesPanel);
            upgrade.UpgradeID = i;
            productionUpgrades.Add(upgrade);
        }

        //clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        //productionUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");

    }

    private void UpdateUpgradeUI(string type, int UpgradeID = -1)
    {
        var data = GameManager.instance.data;
        switch(type)
        {
            case "click":
                if (UpgradeID == -1)
                    for (int i = 0; i < clickUpgrades.Count; i++) UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, i);
                else UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, UpgradeID);
                break;
            case "production":
                if (UpgradeID == -1)
                    for (int i = 0; i < productionUpgrades.Count; i++) UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, i);
                else UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, UpgradeID);
                break;
        }

        void UpdateUI(List<Upgrades> upgrades, List<int> upgradeLevels, string[] upgradeNames, int ID)
        {
            var clickRate = upgradeLevels[ID] + 1;
            upgrades[ID].LevelText.text = upgradeNames[ID] + " Level " + upgradeLevels[ID].ToString();
            upgrades[ID].CostText.text = $"{UpgradeCost(type, ID).ToString("F0")} \n candies";
            upgrades[ID].NameText.text = "+" + clickRate + " candies per click";
        }
    }

    public BigDouble UpgradeCost(string type, int UpgradeID)
    {
        switch (type)
        {
            case "click": return clickUpgradeBaseCost[UpgradeID] * BigDouble.Pow(clickUpgradeCostMult[UpgradeID], 
                (BigDouble)GameManager.instance.data.clickUpgradeLevel[UpgradeID]);
            case "production":
                return productionUpgradeBaseCost[UpgradeID] * BigDouble.Pow(productionUpgradeCostMult[UpgradeID],
                (BigDouble)GameManager.instance.data.productionUpgradeLevel[UpgradeID]);
        }
        return 0;
    }

    public void BuyUpgrade(string type, int UpgradeID)
    {
        var data = GameManager.instance.data;
        
        switch(type)
        {
            case "click": Buy(data.clickUpgradeLevel);
                break;
            case "production": Buy(data.productionUpgradeLevel);
                break;
        }

        void Buy(List<int> upgradeLevels)
        {
            if (data.candiesCount >= UpgradeCost(type, UpgradeID) & upgradeLevels[UpgradeID] < 5)
            {
                data.candiesCount -= UpgradeCost(type, UpgradeID);
                upgradeLevels[UpgradeID] += 1;


                switch (type)
                {
                    case "click":
                        childCostumes[upgradeLevels[UpgradeID] - 1].SetActive(false);
                        if (upgradeLevels[UpgradeID] == 2)
                            head.color = Color.yellow;
                        else
                            head.color = Color.white;

                        childCostumes[upgradeLevels[UpgradeID]].SetActive(true);
                        break;
                    case "production":
                        Children[UpgradeID].SetActive(true);
                        break;
                }
            }
            UpdateUpgradeUI(type, UpgradeID);
        }
    }
}
