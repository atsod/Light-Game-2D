using TMPro;
using UnityEngine;

public class CoinsCollecting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerMoneyText;

    private const string KEY_MONEY = PlayerPrefsKeys.KEY_MONEY;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(KEY_MONEY))
            PlayerPrefs.SetInt(KEY_MONEY, 0);

        _playerMoneyText.text = $"{PlayerPrefs.GetInt(KEY_MONEY)}$";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<CoinLightUpgrade>() != null)
        {
            int money = PlayerPrefs.GetInt(KEY_MONEY);
            PlayerPrefs.SetInt(KEY_MONEY, ++money);

            _playerMoneyText.text = $"{money}$";

            Destroy(other.gameObject);
        }
    }
}
