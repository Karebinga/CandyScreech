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
    public Sprite[] childSprite;
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

    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(GameManager.instance.data.clickUpgradeLevel, 3);

        clickUpgradeNames = new[] { "Click Power +1", "Click Power +5", "Click Power +10" };
        productionUpgradeNames = new[] { "+1 Candy", "+2 Candies", "+10 Candies" };

        clickUpgradeBaseCost = new BigDouble[] { 10, 50, 100 };
        clickUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55 };
        clickUpgradesBasePower = new BigDouble[] { 1, 5, 10 };

        productionUpgradeBaseCost = new BigDouble[] { 25, 100, 1000 };
        productionUpgradeCostMult = new BigDouble[] { 1.5, 1.75, 2 };
        productionUpgradesBasePower = new BigDouble[] { 1, 2, 10 };

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

        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        productionUpgradesScroll.normalizedPosition = new Vector2(0, 0);
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
            upgrades[ID].LevelText.text = upgradeLevels[ID].ToString();
            upgrades[ID].CostText.text = $"Cost: {UpgradeCost(type, ID).ToString("F0")} candies";
            upgrades[ID].NameText.text = upgradeNames[ID];
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
            if (data.candiesCount >= UpgradeCost(type, UpgradeID))
            {
                data.candiesCount -= UpgradeCost(type, UpgradeID);
                upgradeLevels[UpgradeID] += 1;


                switch (type)
                {
                    case "click":
                        Child.sprite = childSprite[upgradeLevels[UpgradeID]]; // сделать что-то с ошибкой
                        break;
                    case "production":
                        if (upgradeLevels[UpgradeID] > 0)
                        {
                            Image children = Instantiate(Child, gameCanvas.transform);
                            children.transform.position = Child.transform.position + new Vector3(Random.Range(-300, -100), 0);
                        }
                        break;
                }
            }
            UpdateUpgradeUI(type, UpgradeID);
        }
    }
}
