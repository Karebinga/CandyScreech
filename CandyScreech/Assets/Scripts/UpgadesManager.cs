using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;

public class UpgadesManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameData data;

    public BigDouble clickUpgradeBaseCost; 
    public BigDouble clickUpgradeCostMult;

    private void Start()
    {
        data = gameManager.data;
        clickUpgradeBaseCost = 10;
        clickUpgradeCostMult = 1.5;
    }

    public BigDouble Cost() => clickUpgradeBaseCost * BigDouble.Pow(clickUpgradeCostMult, data.clickUpgradeLevel);
}
