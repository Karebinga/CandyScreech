using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Navigation : MonoBehaviour
{
    public GameObject ClickUpgradesSelected;
    public GameObject ProductionUpgradesSelected;

    public TMP_Text ClickUpgradeTitleText;
    public TMP_Text ProductionUpgradeTitleText;

    public void SwitchUpgrades(string location)
    {
        UpgradesManager.instance.clickUpgradesScroll.gameObject.SetActive(false);
        UpgradesManager.instance.productionUpgradesScroll.gameObject.SetActive(false);

        ClickUpgradesSelected.SetActive(false);
        ProductionUpgradesSelected.SetActive(false);
        ClickUpgradeTitleText.color = Color.grey;
        ProductionUpgradeTitleText.color = Color.grey;


        switch (location)
        {
            case "click":
                ClickUpgradesSelected.SetActive(true);
                ClickUpgradeTitleText.color = Color.white;
                UpgradesManager.instance.clickUpgradesScroll.gameObject.SetActive(true);
                break;

            case "production":
                ProductionUpgradesSelected.SetActive(true);
                ProductionUpgradeTitleText.color = Color.white;
                UpgradesManager.instance.productionUpgradesScroll.gameObject.SetActive(true);
                break;
        }
    }
}
