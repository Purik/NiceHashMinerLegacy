using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;


namespace NiceHashMiner.Configs.Data
{
    public class SecretConfig
    {
        private readonly string SecretsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), typeof(SecretConfig).FullName);

        public bool Exists(Guid key)
        {
            return File.Exists(GetPath(key));
        }

        public string Get(Guid key)
        {
            try
            {
                var bytes = File.ReadAllBytes(GetPath(key));
                bytes = ProtectedData.Unprotect(bytes, null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (DirectoryNotFoundException)
            {
                return null;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public void Set(Guid key, string secret)
        {
            var bytes = Encoding.UTF8.GetBytes(secret);
            bytes = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
            Directory.CreateDirectory(SecretsPath);
            File.WriteAllBytes(GetPath(key), bytes);
        }

        private string GetPath(Guid key)
        {
            return Path.Combine(SecretsPath, key.ToString());
        }
    }
}
