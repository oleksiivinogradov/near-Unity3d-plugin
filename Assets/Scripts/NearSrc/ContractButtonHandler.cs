using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using NearClientUnity;
using NearClientUnity.Utilities;
using NearClientUnityTests.Utils;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ContractButtonHandler : MonoBehaviour
{
    [SerializeField] private Text debugText; 
        
    public async void RequestContract()
    {
        debugText.text = "pending";
        var walletAccountId = NearPersistentManager.Instance.WalletAccount.GetAccountId();
        Account account = await NearPersistentManager.Instance.Near.AccountAsync(walletAccountId);
        string contractId = "dev-1666687644904-45000132622537";
        ContractOptions contractOptions = new ContractOptions()
        {
            changeMethods = new [] {"purchase", "add_moto"},
            sender = walletAccountId,
            viewMethods = new [] {"value_in_main_coin", "get_minimal_fee", "token_ids_and_owners"}
        };
        ContractNear _contractNear = new ContractNear(account, contractId, contractOptions);
        debugText.text = "pendingMethods " + walletAccountId;
        dynamic args = new ExpandoObject();
        args.type_nft = 3;
        // IDictionary<string, object> getMinimalFee = await _contractNear.View("get_minimal_fee", null);
        // UInt128 getMinimalFeeResult = UInt128.Parse(getMinimalFee["result"].ToString());
        // JObject addMotoResponse = await _contractNear.Change("add_moto", args, 50000000000000, getMinimalFeeResult);
        // var addMotoResponseString = addMotoResponse.ToString();
         IDictionary<string, object> valueInMainCoin = await _contractNear.View("value_in_main_coin", args);
         string valueInMainCoinString = (float.Parse(valueInMainCoin["result"].ToString()) * 1.01f).ToString("0");
         UInt128 valueInMainCoinResult = UInt128.Parse(valueInMainCoinString);
         debugText.text = "valueInMainCoin: " + valueInMainCoinResult;
         JObject buyResponse = await _contractNear.Change("purchase", args, 300000000000000, valueInMainCoinResult);
         string[] response = new string[2];
         var buyResponseString = buyResponse.ToString();
         if (buyResponse.ContainsKey("status") && buyResponse["status"].Contains("SuccessValue")) 
         {
             response[0] = "success";
             if (buyResponseString.Contains("status") && buyResponseString.Contains("EVENT_JSON") && buyResponseString.Contains("token_ids"))
             {
                 var eventJson = JObject.Parse("{" + buyResponse["receipts_outcome"][0]["outcome"]["logs"][0] + "}");
                 response[1] = eventJson["EVENT_JSON"]["data"][0]["token_ids"][0].ToString();
             }
                 
         }
         else
         {
             response[0] = "fail";
         }
        debugText.text = "buyResponse: " + string.Join(", ", response);
        
    }
}
