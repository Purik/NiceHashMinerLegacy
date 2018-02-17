using System;
using System.Collections.Generic;
using System.Text;
using NiceHashMiner.Configs;
using System.Net;
using Newtonsoft.Json;
using System.IO;    // for StreamReader


namespace NiceHashMiner
{
    public static class WebAPI
    {
        public static bool IsAuthorized()
        {
            var username = "minikspb@gmail.com";// ConfigManager.SecretConfig.Get(usernameKey);
            var password = "vbytyreif_"; // ConfigManager.SecretConfig.Get(passwordKey);
            if (username == null)
            {
                return false;
            }
            if (password == null)
            {
                return false;
            }

            var values = new Dictionary<string, string>
            {
               { "username", username },
               { "password", password },
               { "machine", System.Environment.MachineName }
            };
            CustomRequest("account", values);
            return false;
        }

        private static object CustomRequest(string method, Dictionary<string, string> data)
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
            var ret = JsonConvert.DeserializeObject(responseString);
            return ret;
        }

        private static Guid usernameKey = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709");
        private static Guid passwordKey = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857701");
    }
}
