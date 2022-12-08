using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignInButtonHandler : MonoBehaviour
{
    public async void RequestSignIn()
    {
        await NearPersistentManager.Instance.WalletAccount.RequestSignIn(
            "dev-1666687644904-45000132622537",
            "Near Unity Client",
            new Uri("nearclientunity://wallet.testnet.near.org/success"),
            new Uri("nearclientunity://wallet.testnet.near.org/fail"),
            new Uri("nearclientios://wallet.testnet.near.org")
            );
    }
}
