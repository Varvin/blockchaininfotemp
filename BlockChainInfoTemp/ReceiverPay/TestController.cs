using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Info.Blockchain.API.Client;
using Info.Blockchain.API.Models;
using Info.Blockchain.API.Receive;
using Microsoft.AspNetCore.Mvc;

namespace ReceiverPay
{
    [Route("api/v1/[controller]")]
    public class TestController : Controller
    {
        private BlockchainHttpClient _client = new BlockchainHttpClient(null, "https://api.blockchain.info/v2/");

        private List<string> results = new List<string>();

        [HttpGet("create")]
        public async Task<ReceivePaymentResponse> CreateTemperalWallet()
        {
            try
            {
                var r = new Receive(_client);
                var v = await r.GenerateAddressAsync(
                    "tpubDDg5TTo8n2E3s738RTfzWRQn4uDaKBpNfHf95XPn9e7FaxKW1wibd11Mz4Z9QPH7rLpPsaakjzZabW5c7kj1vgh49SuKjqbpJ8WA61Fn5AU",
                    HttpUtility.HtmlEncode("http://109.86.200.177:12123/api/v1/test/receive-temp?code=123123"), "my_api_code", null);

                return v;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
        
        [HttpGet("receive-temp")]
        public void Responce(string code, string transaction_hash, string address,int confirmations, decimal value)
        {
            Console.WriteLine($"code {code}, transaction_hash {transaction_hash}, address {address}, confirmations {confirmations}, value {value}");
        }

        [HttpGet("results")]
        public List<string> Results()
        {
            return results;
        }
    }
}