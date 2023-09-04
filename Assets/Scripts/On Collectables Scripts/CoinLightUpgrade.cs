using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CoinLightUpgrade : MonoBehaviour
{
    private Light2D _coinLight;
    
    private void Awake()
    {
        _coinLight = GetComponentInChildren<Light2D>();
        _coinLight.pointLightOuterRadius = UpgradeSwitcher.coinLightOuterRadius;
    }
}
