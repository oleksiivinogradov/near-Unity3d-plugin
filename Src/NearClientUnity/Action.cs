﻿using System.Dynamic;
using NearClientUnity.Utilities;

namespace NearClientUnity
{
    public class Action
    {
        private ActionType _type;
        private dynamic _args;

        public ActionType Type => _type;
        public dynamic Args => _args;

        public Action(ActionType type, dynamic args)
        {
            _type = type;
            _args = args;
        }

        public static Action CreateAccount()
        {
            return new Action(ActionType.CreateAccount, null);
        }

        public static Action DeployContract(byte[] code)
        {
            dynamic args = new ExpandoObject();
            args.Code = code;
            return new Action(ActionType.DeployContract, args);
        }

        public static Action FunctionCall(string methodName, byte[] methodArgs, int gas, UInt128 deposit)
        {
            dynamic args = new ExpandoObject();
            args.MethodName = methodName;
            args.MethodArgs = methodArgs;
            args.Gas = gas;
            args.Deposit = deposit;
            return new Action(ActionType.FunctionCall, args);
        }

        public static Action Transfer(UInt128 deposit)
        {
            dynamic args = new ExpandoObject();
            args.Deposit = deposit;
            return new Action(ActionType.Transfer, args);
        }

        public static Action Stake(UInt128 stake, PublicKey publicKey)
        {
            dynamic args = new ExpandoObject();
            args.Stake = stake;
            args.PublicKey = publicKey;
            return new Action(ActionType.Stake, args);
        }

        public static Action AddKey(PublicKey publicKey, AccessKey accessKey)
        {
            dynamic args = new ExpandoObject();
            args.PublicKey = publicKey;
            args.AccessKey = accessKey;
            return new Action(ActionType.AddKey, args);
        }

        public static Action DeleteKey(PublicKey publicKey)
        {
            dynamic args = new ExpandoObject();
            args.PublicKey = publicKey;
            return new Action(ActionType.DeleteKey, args);
        }

        public static Action DeleteAccount(string beneficiaryId)
        {
            dynamic args = new ExpandoObject();
            args.BeneficiaryId = beneficiaryId;
            return new Action(ActionType.DeleteAccount, args);
        }
    }
}