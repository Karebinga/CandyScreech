using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public int UpgradeID;
    public GameObject UpgradeButton;
    public TMP_Text LevelText;
    public TMP_Text NameText;
    public TMP_Text CostText;

    public void BuyClickUpgrade() => UpgradesManager.instance.BuyUpgrade("click", UpgradeID);
    public void BuyProductionUpgrade() => UpgradesManager.instance.BuyUpgrade("production", UpgradeID);
}
