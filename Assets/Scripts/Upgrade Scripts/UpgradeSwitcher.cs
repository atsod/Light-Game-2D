using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSwitcher : MonoBehaviour
{
    private const string KEY_PLAYER_LIGHT_UPGRADE = PlayerPrefsKeys.KEY_PLAYER_LIGHT_UPGRADE;
    private const string KEY_COIN_LIGHT_UPGRADE = PlayerPrefsKeys.KEY_COIN_LIGHT_UPGRADE;
    private const string KEY_PLAYER_SPEED_UPGRADE = PlayerPrefsKeys.KEY_PLAYER_SPEED_UPGRADE;

    private UpgradeDegreeEnum _playerLightUpgradeEnum;
    private UpgradeDegreeEnum _coinLightUpgradeEnum;
    private UpgradeDegreeEnum _playerSpeedUpgradeEnum;

    public static float playerLightOuterRadius;
    public static float coinLightOuterRadius;
    public static float usualPlayerSpeed;

    private void Awake()
    {
        _playerLightUpgradeEnum = (UpgradeDegreeEnum) PlayerPrefs.GetInt(KEY_PLAYER_LIGHT_UPGRADE, (int) UpgradeDegreeEnum.Default);
        _coinLightUpgradeEnum = (UpgradeDegreeEnum)PlayerPrefs.GetInt(KEY_COIN_LIGHT_UPGRADE, (int)UpgradeDegreeEnum.Default);
        _playerSpeedUpgradeEnum = (UpgradeDegreeEnum)PlayerPrefs.GetInt(KEY_PLAYER_SPEED_UPGRADE, (int)UpgradeDegreeEnum.Default);

        ChoosePlayerLightUpgrade();
        ChooseCoinLightUpgrade();
        ChooseSpeedUpgrade();
    }

    private void ChoosePlayerLightUpgrade()
    {
        playerLightOuterRadius = _playerLightUpgradeEnum switch
        {
            UpgradeDegreeEnum.Default => 1.7f,
            UpgradeDegreeEnum.Low => 2f,
            UpgradeDegreeEnum.Medium => 2.3f,
            UpgradeDegreeEnum.High => 2.6f,
            UpgradeDegreeEnum.Legendary => 3f,
            _ => 1.7f,
        };
    }

    private void ChooseCoinLightUpgrade()
    {
        coinLightOuterRadius = _coinLightUpgradeEnum switch
        {
            UpgradeDegreeEnum.Default => 1.4f,
            UpgradeDegreeEnum.Low => 1.8f,
            UpgradeDegreeEnum.Medium => 2.2f,
            UpgradeDegreeEnum.High => 2.6f,
            UpgradeDegreeEnum.Legendary => 7f,
            _ => 1.4f,
        };
    }

    private void ChooseSpeedUpgrade()
    {
        usualPlayerSpeed = _playerSpeedUpgradeEnum switch
        {
            UpgradeDegreeEnum.Default => 7f,
            UpgradeDegreeEnum.Low => 10f,
            UpgradeDegreeEnum.Medium => 13f,
            UpgradeDegreeEnum.High => 16f,
            UpgradeDegreeEnum.Legendary => 19f,
            _ => 7f,
        };
    }
}
