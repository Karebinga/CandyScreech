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

    public GameObject[] Costume0;
    public GameObject[] Costume1;
    public GameObject[] Costume2;
    public GameObject[] Costume3;
    public GameObject[] Costume4;
    public Image head0;
    public Image head1;
    public Image head2;
    public Image head3;
    public Image head4;

    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(GameManager.instance.data.clickUpgradeLevel, 1);
        Methods.UpgradeCheck(GameManager.instance.data.productionUpgradeLevel, 5);

        clickUpgradeNames = new[] { "Outfit Upgrade" };
        productionUpgradeNames = new[] { "Harry", "Vlad", "Space Guy", "Cartman", "Shrek" };

        clickUpgradeBaseCost = new BigDouble[] { 10 };
        clickUpgradeCostMult = new BigDouble[] { 3 };
        clickUpgradesBasePower = new BigDouble[] { 1 };

        productionUpgradeBaseCost = new BigDouble[] { 5, 25, 100, 400, 2000 };
        productionUpgradeCostMult = new BigDouble[] { 3, 3, 3, 3, 3 };
        productionUpgradesBasePower = new BigDouble[] { 1, 5, 15, 50, 200 };

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
        Debug.Log(UpgradeID);
        switch(type)
        {
            case "click": Buy(data.clickUpgradeLevel);
                break;
            case "production": Buy(data.productionUpgradeLevel);
                break;
        }

        void Buy(List<int> upgradeLevels)
        {
            if (data.candiesCount >= UpgradeCost(type, UpgradeID) & upgradeLevels[UpgradeID] < 6)
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
                        switch(UpgradeID)
                        {
                            case 0:
                                Costume0[upgradeLevels[0]-1].SetActive(false);
                                if (upgradeLevels[0] == 3)
                                    head0.color = Color.yellow;
                                else
                                    head0.color = Color.white;
                                Costume0[upgradeLevels[UpgradeID]].SetActive(true);
                                break;

                            case 1:
                                Costume1[upgradeLevels[1] - 1].SetActive(false);
                                if (upgradeLevels[1] == 3)
                                    head1.color = Color.yellow;
                                else
                                    head1.color = Color.white;
                                Costume1[upgradeLevels[UpgradeID]].SetActive(true);
                                break;

                            case 2:
                                Costume2[upgradeLevels[2] - 1].SetActive(false);
                                if (upgradeLevels[2] == 3)
                                    head2.color = Color.yellow;
                                else
                                    head2.color = Color.white;
                                Costume2[upgradeLevels[UpgradeID]].SetActive(true);
                                break;

                            case 3:
                                Costume3[upgradeLevels[3] - 1].SetActive(false);
                                if (upgradeLevels[3] == 3)
                                    head3.color = Color.yellow;
                                else
                                    head3.color = Color.white;
                                Costume3[upgradeLevels[UpgradeID]].SetActive(true);
                                break;

                            case 4:
                                Costume4[upgradeLevels[4] - 1].SetActive(false);
                                if (upgradeLevels[4] == 3)
                                    head4.color = Color.yellow;
                                else
                                    head4.color = Color.green;
                                Costume4[upgradeLevels[UpgradeID]].SetActive(true);
                                break;
                               

                        }



                        break;
                }
            }
            UpdateUpgradeUI(type, UpgradeID);
        }
    }
}
