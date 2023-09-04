using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerMoneyText;
    [SerializeField] private Button[] _playerSpeedUpgradeButtons;
    [SerializeField] private Button[] _playerLightUpgradeButtons;
    [SerializeField] private Button[] _coinLughtUpgradeButtons;

    private const string KEY_PLAYER_LIGHT_UPGRADE = PlayerPrefsKeys.KEY_PLAYER_LIGHT_UPGRADE;
    private const string KEY_COIN_LIGHT_UPGRADE = PlayerPrefsKeys.KEY_COIN_LIGHT_UPGRADE;
    private const string KEY_PLAYER_SPEED_UPGRADE = PlayerPrefsKeys.KEY_PLAYER_SPEED_UPGRADE;
    private const string KEY_MONEY = PlayerPrefsKeys.KEY_MONEY;

    private void Awake()
    {
        PlayerPrefs.SetInt(KEY_MONEY, 2000);
        _playerMoneyText.text = $"{PlayerPrefs.GetInt(KEY_MONEY, 0)}$";
    }

    private void Start()
    {
        InitializeButtons(_playerSpeedUpgradeButtons, UpgradeTypeEnum.PlayerSpeed);
        InitializeButtons(_playerLightUpgradeButtons, UpgradeTypeEnum.PlayerLight);
        InitializeButtons(_coinLughtUpgradeButtons, UpgradeTypeEnum.CoinLight);
    }

    private void InitializeButtons(Button[] upgradeButtons, UpgradeTypeEnum upgradeType)
    {
        foreach (Button upgradeButton in upgradeButtons)
        {
            upgradeButton.interactable = false;
            TakeButtonSprite(upgradeButton, 1);
        }

        string type = upgradeType switch
        {
            UpgradeTypeEnum.PlayerSpeed => KEY_PLAYER_SPEED_UPGRADE,
            UpgradeTypeEnum.PlayerLight => KEY_PLAYER_LIGHT_UPGRADE,
            UpgradeTypeEnum.CoinLight => KEY_COIN_LIGHT_UPGRADE,
            _ => KEY_PLAYER_SPEED_UPGRADE,
        };

        UpgradeDegreeEnum upgradeDegree = (UpgradeDegreeEnum)PlayerPrefs.GetInt(type, 0);
        int upgradeDegreeInt = (int)upgradeDegree;

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if (i == upgradeDegreeInt + 1) break;
            upgradeButtons[i].interactable = true;
            TakeButtonSprite(upgradeButtons[i], 0);
        }

        if (!upgradeDegree.Equals(UpgradeDegreeEnum.Legendary))
        {
            upgradeButtons[upgradeDegreeInt].onClick.RemoveAllListeners();
            TakeButtonSprite(upgradeButtons[upgradeDegreeInt], 2);
            upgradeButtons[upgradeDegreeInt].onClick.AddListener(
                () => OnUpgradeButtonClick(
                    (upgradeDegreeInt + 1) * 200,
                    type,
                    upgradeDegree,
                    upgradeButtons)
            );
        }  
    }

    private void OnUpgradeButtonClick(int upgradeValue, string upgradeType, UpgradeDegreeEnum upgradeDegree, Button[] upgradeButtons)
    {
        int playerMoney = PlayerPrefs.GetInt(KEY_MONEY, 0);

        if (playerMoney < upgradeValue)
        {
            // Exception if not enough money
            return;
        }

        PlayerPrefs.SetInt(KEY_MONEY, playerMoney - upgradeValue);
        _playerMoneyText.text = $"{PlayerPrefs.GetInt(KEY_MONEY)}$";

        TakeButtonSprite(upgradeButtons[(int)upgradeDegree], 0);
        upgradeButtons[(int)upgradeDegree].onClick.RemoveAllListeners();

        PlayerPrefs.SetInt(upgradeType, (int)++upgradeDegree);

        if (upgradeDegree.Equals(UpgradeDegreeEnum.Legendary)) return;

        TakeButtonSprite(upgradeButtons[(int)upgradeDegree], 2);
        upgradeButtons[(int)upgradeDegree].interactable = true;
        upgradeButtons[(int) upgradeDegree].onClick.AddListener(
                () => OnUpgradeButtonClick(
                    ((int)upgradeDegree + 1) * 200,
                    upgradeType,
                    upgradeDegree,
                    upgradeButtons)
            );
    }

    private void TakeButtonSprite(Button button, int childIndex)
    {
        /* childIndex:
         * 0 - purchased
         * 1 - locked
         * 2 - open
         */
        button.image.sprite = button.transform.GetChild(childIndex).GetComponent<Image>().sprite;
    } 
}
