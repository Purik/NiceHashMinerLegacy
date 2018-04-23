using System;
using System.Collections.Generic;
using System.Text;
using NiceHashMiner.Configs;
using System.Net;
using Newtonsoft.Json;
using System.IO;    // for StreamReader
using System.Reflection;

namespace NiceHashMiner
{
    public static class WebAPI
    {
        private static string SOFTWARE_VERSION = "0.0.0";
        public class AccountAnswer
        {
            public bool success { get; set; }
            public string uid { get; set; }
            public string username { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string worker_name { get; set; }
        }

        public class BalancesAnswer
        {

            public class BalanceItem
            {
                public string btc { get; set; }
                public string usd { get; set; }
            }

            public class Prediction
            {
                public BalanceItem day { get; set; }
                public BalanceItem week { get; set; }
                public BalanceItem month { get; set; }
            }

            public class Value
            {
                public BalanceItem personal_volume { get; set; }
                public Prediction prediction { get; set; }
            }

            public bool success { get; set; }
            public Value value { get; set; }
        }

        public static AccountAnswer Account(string username, string password)
        {
            var values = new Dictionary<string, string>
            {
               { "username", username },
               { "password", password },
               { "version",  SOFTWARE_VERSION },
            };
            var json = CustomRequest("account", values);
            if (json != null)
            {
                AccountAnswer ret = JsonConvert.DeserializeObject<AccountAnswer>(json);
                return ret;
            }
            else {
                return null;
            }
        }

        public static BalancesAnswer Balances(string uid)
        {
            var values = new Dictionary<string, string>
            {
               { "uid", uid },
            };
            var json = CustomRequest("balances", values);
            if (json != null)
            {
                BalancesAnswer ret = JsonConvert.DeserializeObject<BalancesAnswer>(json);
                return ret;
            }
            else
            {
                return null;
            }
        }

        public static void UpdateMachineInfo(string worker_name)
        {
            var values = new Dictionary<string, string>
            {
               { "version",  SOFTWARE_VERSION },
               { "worker_name",  worker_name },
            };
            var json = CustomRequest("update-machine-info", values);
        }

        private static string CustomRequest(string method, Dictionary<string, string> data)
        {
            var url = ConfigManager.GeneralConfig.ServerAddress + "/ajax/" + method;
            var postDataAsStr = String.Format("machine={0}", System.Environment.MachineName);
            foreach (KeyValuePair<string, string> entry in data)
            {
                var partition = String.Format("{0}={1}", entry.Key, entry.Value);
                postDataAsStr += ("&" + partition);
            }
            var content = Encoding.UTF8.GetBytes(postDataAsStr);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = content.Length;
            request.Timeout = 1000;
            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(content, 0, content.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch
            {
                return null;
            }
        }
        
    }
}
