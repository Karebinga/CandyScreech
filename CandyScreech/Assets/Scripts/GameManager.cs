using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameData data;

    [SerializeField] private TMP_Text candiesText;

    private void Start()
    {
        data = new GameData();
    }

    private void Update()
    {
        candiesText.text = data.candiesCount.ToString();
    }

    public void GenerateCandies()
    {
        data.candiesCount++;
    }
    
}
