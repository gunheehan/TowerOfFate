using UnityEngine;
using UnityEngine.UI;

public class UIPlayState : MonoBehaviour, IUIInterface
{
    [SerializeField] private Text Text_Money;

    private void OnEnable()
    {
        CoinWatcher.WalletEvent += UpdateWalletData;
    }

    private void OnDisable()
    {
        CoinWatcher.WalletEvent -= UpdateWalletData;
    }

    private void UpdateWalletData(int money)
    {
        Text_Money.text = money.ToString();
    }
}
