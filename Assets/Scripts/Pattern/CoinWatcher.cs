using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoinWatcher
{
    public delegate void WalletCustomEvent(int money);

    private static WalletCustomEvent updateWalletData;
    public static event WalletCustomEvent WalletEvent
    {
        add
        {
            updateWalletData += value;
            value?.Invoke(money);
        }
        remove { updateWalletData -= value; }
    }
    
    private static int money = 0;
    
    public static void UpdateWallet(int newMoney)
    {
        money += newMoney;
        
        updateWalletData?.Invoke(money);
    }
}
