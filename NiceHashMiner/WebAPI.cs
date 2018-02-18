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
        public class AccountAnswer
        {
            public bool success { get; set; }
            public string uid { get; set; }
            public string username { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
        }

        public static AccountAnswer Account(string username, string password)
        {
            var values = new Dictionary<string, string>
            {
               { "username", username },
               { "password", password },
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

        private static string CustomRequest(string method, Dictionary<string, string> data)
        {
            var url = ConfigManager.GeneralConfig.ServerAddress + "/ajax/" + method;
            var postDataAsStr = String.Format("machine={0}", System.Environment.MachineName);
            foreach (KeyValuePair<string, string> entry in data)
            {
                var partition = String.Format("{0}={1}", entry.Key, entry.Value);
                postDataAsStr += ("&" + partition);
            }
            var content = Encoding.ASCII.GetBytes(postDataAsStr);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = content.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(content, 0, content.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
        
    }
}
