
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json.Linq;
using System;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public interface IBlockchainWriter
{
    void Write(CapturedInquiry inquiry);
}

public class NethereumWriter : IBlockchainWriter
{
    private const string RPC_URL = "https://polygon-mainnet.g.alchemy.com/v2/CnmU2i3SqSZCMZ0ovJqG9";
    private const string PRIVATE_KEY = "bcd91e2f0b19b0608229034a80ed95c03ab3406e1ef00a24f96ef7441f55ff24";
    private const string FROM_ADDRESS = "0x879449E0B9584a520404fF94BD8d79bb042Bb050";

    public void Write(CapturedInquiry inquiry)
    {
        Task.Run(async () =>
        {
            try
            {
                var account = new Account(PRIVATE_KEY);
                var web3 = new Web3(account, RPC_URL);
                // UTF8 data ko hex string me convert karna
                var data = Encoding.UTF8.GetBytes($@"Type: TravelInquiry, EnquiryData: {inquiry}");
                var dataString = "0x" + BitConverter.ToString(data).Replace("-", "");
                var value = new HexBigInteger(UnitConversion.Convert.ToWei(0.0001m));
                var gasPrice = new HexBigInteger(UnitConversion.Convert.ToWei(20, UnitConversion.EthUnit.Gwei));
                var gasLimit = new HexBigInteger(100000);
                var txnInput = new TransactionInput
                {
                    From = account.Address,
                    To = "0x000000000000000000000000000000000000dead",
                    Value = value,
                    Gas = gasLimit,
                    GasPrice = gasPrice,
                    Data = dataString
                };

                var txnHash = await web3.Eth.Transactions.SendTransaction.SendRequestAsync(txnInput);
                Console.WriteLine("Txn Hash: " + txnHash);


            }
            catch (Exception ex)
            {
                Console.WriteLine("Blockchain write failed: " + ex.Message);
            }
        });
    }
}
