using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
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

    [SerializeField] private Image roof;
    [SerializeField] private Image main;
    [SerializeField] private Image window;
    [SerializeField] private Image door;
    [SerializeField] private Transform house;


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
        ChangeColor();
        data = new GameData();
        UpgradesManager.instance.StartUpgradeManager();
    }

    private void Update()
    {
        candiesText.text = data.candiesCount.ToString("F0");
        //clickPowerText.text = "+" + ClickPower() + " candies";
        //candiesPerSecondText.text = $"{CandiesPerSecond().ToString("F0")}/s";
        data.candiesCount += CandiesPerSecond()*Time.deltaTime;
    }


    public void GenerateCandies()
    {
        data.candiesCount += ClickPower();
        if ((int)data.candiesCount % 10 == 0)
        {
            house.DORotate(new Vector3(0, 0, house.transform.rotation.z + 360f), 0.4f, RotateMode.FastBeyond360);
            ChangeColor();
        }
    }
    
    public void ChangeColor()
    {
        roof.color = UnityEngine.Random.ColorHSV();
        window.color = UnityEngine.Random.ColorHSV();
        main.color = UnityEngine.Random.ColorHSV();
        door.color = UnityEngine.Random.ColorHSV();
    }
}
