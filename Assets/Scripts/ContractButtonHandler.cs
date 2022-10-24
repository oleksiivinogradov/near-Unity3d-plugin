using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using NearClientUnity;
using NearClientUnity.Utilities;
using NearClientUnityTests.Utils;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class ContractButtonHandler : MonoBehaviour
{
    [SerializeField] private Text debugText; 
        
    public async void RequestContract()
    {
        debugText.text = "pending";
        var walletAccountId = NearPersistentManager.Instance.WalletAccount.GetAccountId();
        Account account = await NearPersistentManager.Instance.Near.AccountAsync(walletAccountId);
        string contractId = "dev-1666289612816-19049345992155";
        ContractOptions contractOptions = new ContractOptions()
        {
            changeMethods = new [] {"purchase"},
            sender = walletAccountId,
            viewMethods = new [] {"value_in_main_coin", "get_minimal_fee", "token_ids_and_owners"}
        };
        ContractNear _contractNear = new ContractNear(account, contractId, contractOptions);
        debugText.text = "pendingMethods " + walletAccountId;
        var tokenIdsAndOwners = await _contractNear.View("token_ids_and_owners", null);
        //debugText.text = tokenIdsAndOwners["result"];
        dynamic args = new ExpandoObject();
        args.value = "0";
        var buyResponse = await _contractNear.Change("purchase", args);
        //Debug.Log(buyResponse["result"]);
        //debugText.text = buyResponse["result"];
        
    }
}
